
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.Xml;
using System.ServiceModel;

namespace Microsoft.Samples.XmlRpc
{
    class XmlRpcOperationSelector : IDispatchOperationSelector
    {
        ContractDescription _contract;
        public XmlRpcOperationSelector(ContractDescription contract)
        {
            _contract = contract;
        }

        public string SelectOperation(ref System.ServiceModel.Channels.Message message)
        {
            XmlRpcMessage xmlRpcMessage = CreateXmlRpcMessage(message);
            message = xmlRpcMessage;

            if (xmlRpcMessage.Properties.ContainsKey("XmlRpcMethodName"))
            {
                string methodName = (string)message.Properties["XmlRpcMethodName"];
                foreach (OperationDescription op in _contract.Operations)
                {
                    if ( op.Messages[0].Action.EndsWith(methodName) )
                    {
                        return op.Name;
                    }
                }
            }
            throw new EndpointNotFoundException();
        }

        private static XmlRpcMessage CreateXmlRpcMessage(System.ServiceModel.Channels.Message message)
        {
            XmlDictionaryReader messageReader = message.GetReaderAtBodyContents();
            string methodName;

            do
            {
                if (messageReader.IsStartElement(XmlRpcProtocol.MethodCall))
                {
                    messageReader.ReadStartElement();
                    messageReader.MoveToContent();
                    if (!messageReader.IsStartElement(XmlRpcProtocol.MethodName))
                    {
                        throw new XmlRpcFormatException(Properties.Resources.EXCEPTION_MISSING_METHODNAME);
                    }
                    else
                    {
                        messageReader.ReadStartElement();
                        messageReader.MoveToContent();
                        if (messageReader.NodeType == XmlNodeType.Text)
                        {
                            methodName = messageReader.ReadString();
                            messageReader.ReadEndElement();
                        }
                        else
                        {
                            throw new XmlRpcFormatException(Properties.Resources.EXCEPTION_MISSING_METHODNAME);
                        }
                        if (messageReader.IsStartElement(XmlRpcProtocol.Params))
                        {
                            return new XmlRpcMessage(methodName, messageReader, true);
                        }
                        else
                        {
                            messageReader.Close();
                            return new XmlRpcMessage(methodName);
                        }
                    }
                }
                else if (messageReader.IsStartElement(XmlRpcProtocol.MethodResponse))
                {
                    messageReader.ReadStartElement();
                    messageReader.MoveToContent();
                    if (messageReader.IsStartElement(XmlRpcProtocol.Params))
                    {
                        return new XmlRpcMessage(messageReader);
                    }
                    else
                    {
                        messageReader.Close();
                        return new XmlRpcMessage();
                    }
                }
            }
            while (messageReader.Read());
            throw new XmlRpcFormatException(Properties.Resources.EXCEPTION_INVALID_MESSAGE);
        }
    }
}
