using Biind;
using LanguageLookup.Example;
using LanguageLookup.Example.Examples;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageLookup.Tests.Examples
{
	public class _01
	{
		[Fact]
		public void Works()
		{
			var printer =
				new TestPrinter
				(
					"Hello, World!"
				);

			new _01_SimpleExample().Run(printer);

			printer.NoneLeft();
		}
	}
}
