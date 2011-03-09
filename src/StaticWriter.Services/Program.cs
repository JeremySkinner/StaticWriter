//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Samples.XmlRpc;
using StaticWriter.Services;

namespace TinyBlogEngine.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML-RPC Blog Demo");
            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, -1, "/blogdemo/").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(BloggerAPI));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(IBloggerAPI), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./blogger"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

            var webBinding = new WebHttpBinding(WebHttpSecurityMode.None);
            var epFeed = serviceHost.AddServiceEndpoint(typeof(IFeed), webBinding, new Uri(baseAddress, "./feed"));
            epFeed.Behaviors.Add(new WebHttpBehavior());
            serviceHost.Open();

            Console.WriteLine("Blogger API endpoint listening at {0}", epXmlRpc.ListenUri);
            Console.WriteLine("Perform manual test with your favorite metaWeblog blogging tool (e.g. Word 2007 or Windows Live Writer)");
            Console.Write("Press ENTER to quit");
            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
