using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaticWriter.Services;

namespace StaticWriter.ConsoleHost {
	class Program {
		static void Main(string[] args) {
			using(var host = Bootstrapper.CreateServiceHost())
			{
				Console.WriteLine("Opening host...");
				host.Open();

				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
				host.Close();

			}


		}
	}
}
