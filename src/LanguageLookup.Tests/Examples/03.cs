using Biind;
using LanguageLookup.Example;
using LanguageLookup.Example.Examples;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageLookup.Tests.Examples
{
	public class _03
	{
		[Fact]
		public void Works()
		{
			var printer =
				new TestPrinter
				(
					"Hello!",
					"¡Hola!"
				);

			new _03_DefaultGroup().Run(printer);

			printer.NoneLeft();
		}
	}
}
