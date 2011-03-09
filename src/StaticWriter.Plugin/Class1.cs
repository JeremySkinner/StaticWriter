using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WindowsLive.Writer.Api;

namespace StaticWriter.Plugin {


	[Initializer("E7BDC004-E97D-4686-ACD4-996358C45EDA", "Static Writer")]
	public class Bootstrapper : PublishNotificationHook {
		static ServiceHost service;

		public class Initializer : WriterPluginAttribute {
			public Initializer(string id, string name) : base(id, name) {

				new Thread(() => {

				var service = StaticWriter.Services.Bootstrapper.CreateServiceHost();
				service.Open();

				File.AppendAllText("C:\\log.txt", "Opened.\r\n");

				}) { IsBackground = true }.Start();

			}
		}
	}
	
}
