using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace StaticWriter.Tests {
	[TestFixture]
	public class Class1 {
		private TextWriter oldWriter;
		private StringWriter writer;

		[SetUp]
		public void Setup()
		{
			oldWriter = Console.Out;
			writer = new StringWriter();
			Console.SetOut(writer);
		}

		[Test]
		public void Runs_code_on_start()
		{
			
		}

		[TearDown]
		public void Teardown()
		{
			Console.SetOut(oldWriter);
		}

	}
}
