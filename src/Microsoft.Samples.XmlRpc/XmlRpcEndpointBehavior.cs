
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace Microsoft.Samples.XmlRpc
{
    public class XmlRpcEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            CustomBinding binding = new CustomBinding(endpoint.Binding);
            TransportBindingElement tbe = binding.Elements.Find<TransportBindingElement>();
            tbe.ManualAddressing = false;
            endpoint.Binding = binding;

            foreach (OperationDescription opDesc in endpoint.Contract.Operations)
            {
                ReplaceFormatterBehavior(opDesc, endpoint);
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ContractFilter = new MatchAllMessageFilter();
            endpointDispatcher.DispatchRuntime.OperationSelector = new XmlRpcOperationSelector(endpoint.Contract);
            

            foreach (OperationDescription opDesc in endpoint.Contract.Operations)
            {
                ReplaceFormatterBehavior(opDesc, endpoint);
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        private void ReplaceFormatterBehavior(OperationDescription operationDescription, ServiceEndpoint endpoint)
        {
            if (operationDescription.Behaviors.Find<XmlRpcOperationFormatterBehavior>() == null)
            {
                XmlRpcOperationFormatterBehavior formatterBehavior = 
                    new XmlRpcOperationFormatterBehavior(
                      operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>(),
                      operationDescription.Behaviors.Find<XmlSerializerOperationBehavior>());
                operationDescription.Behaviors.Remove<DataContractSerializerOperationBehavior>();
                operationDescription.Behaviors.Remove<XmlSerializerOperationBehavior>();
                operationDescription.Behaviors.Add(formatterBehavior);
            }
        }
    }
}
