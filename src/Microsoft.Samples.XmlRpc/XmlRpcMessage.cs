
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Samples.XmlRpc
{
    public class XmlRpcMessage : Message
    {
        MessageHeaders headers;
        MessageProperties properties;
        bool isFault;
        bool isInboundRequest;
        XmlDictionaryReader bodyReader;
        
        public XmlRpcMessage()
        {
            bodyReader = null;
            headers = new MessageHeaders(MessageVersion.None);
            properties = new MessageProperties();
        }

        public XmlRpcMessage(string methodName, XmlDictionaryReader paramsSection, bool isInboundRequest)
            : this()
        {
            this.bodyReader = paramsSection;
            this.bodyReader.MoveToContent();
            this.isFault = false;
            this.isInboundRequest = isInboundRequest;
            this.properties.Add("XmlRpcMethodName", methodName);
        }

        public XmlRpcMessage(string methodName)
            : this()
        {
            bodyReader = null;
            isFault = false;
            properties.Add("XmlRpcMethodName", methodName);
        }

        public XmlRpcMessage(XmlDictionaryReader paramsSection)
            : this()
        {
            bodyReader = paramsSection;
            bodyReader.MoveToContent();
            isFault = false;
        }

        public XmlRpcMessage(MessageFault fault)
            : this()
        {
            isFault = true;
            bodyReader = XmlRpcDataContractSerializationHelper.CreateFaultReader(fault);
            bodyReader.MoveToContent();
        }

        public override MessageHeaders Headers
        {
            get { return headers; }
        }

        public override MessageProperties Properties
        {
            get { return properties; }
        }

        public bool IsXmlRpcMethodCall
        {
            get { return properties.ContainsKey("XmlRpcMethodName"); }
        }

        public override bool IsFault
        {
            get
            {
                return isFault;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return bodyReader == null;
            }
        }

        public override MessageVersion Version
        {
            get { return MessageVersion.None; }
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            if (!IsEmpty)
            {
                if (!isInboundRequest)
                {
                    writer.WriteStartElement(IsXmlRpcMethodCall ? XmlRpcProtocol.MethodCall : XmlRpcProtocol.MethodResponse);

                    if (IsXmlRpcMethodCall)
                    {
                        writer.WriteStartElement(XmlRpcProtocol.MethodName);
                        writer.WriteString((string)properties["XmlRpcMethodName"]);
                        writer.WriteEndElement();
                    }
                }
                writer.WriteNode(bodyReader, true);
                
                if (!isInboundRequest)
                {
                    writer.WriteEndElement();
                }
                
                writer.Flush();
            }
        }

        protected override void OnWriteMessage(XmlDictionaryWriter writer)
        {
            OnWriteBodyContents(writer);
        }

        protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
        {
        }
        
        protected override void OnWriteStartBody(XmlDictionaryWriter writer)
        {
        }

        protected override void OnWriteStartHeaders(XmlDictionaryWriter writer)
        {
        }

        protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
        {
            return base.OnCreateBufferedCopy(maxBufferSize);
        }


    }
}
