using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Samples.XmlRpc;

namespace StaticWriter.Services
{
	public static class Bootstrapper
	{
		public static ServiceHost CreateServiceHost()
		{
			/*var uri = new Uri("http://localhost:8686/hello");
			var host = new ServiceHost(typeof(HelloService), uri);

			var binding = new BasicHttpBinding();
			host.AddServiceEndpoint(typeof(IHelloService), binding, uri);


			var metadata = new ServiceMetadataBehavior();
			metadata.HttpGetEnabled = true;
			metadata.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			host.Description.Behaviors.Add(metadata);
			
			return host;*/

			var baseAddress = new Uri("http://localhost:8686/blog");

//			var baseAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, -1, "/blogdemo/").Uri;
			var serviceHost = new ServiceHost(typeof(BloggerAPI), baseAddress);
			
			var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(IBloggerAPI), new WebHttpBinding(WebHttpSecurityMode.None), baseAddress);
			epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

			var webBinding = new WebHttpBinding(WebHttpSecurityMode.None);
			var epFeed = serviceHost.AddServiceEndpoint(typeof(IFeed), webBinding, new Uri(baseAddress, "./feed"));
			epFeed.Behaviors.Add(new WebHttpBehavior());

			return serviceHost;
		}
	}
}