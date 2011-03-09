using System;
using System.ServiceModel;

namespace StaticWriter.Services
{
	[ServiceContract]
	public interface IHelloService
	{
		[OperationContract]
		string SayHello();
	}

	public class HelloService:IHelloService
	{
		public string SayHello()
		{
			return "Hello";
		}
	}
}