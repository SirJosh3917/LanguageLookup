using Biind;
using LanguageLookup.Example;
using LanguageLookup.Example.Examples;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageLookup.Tests.Examples
{
	public class _02
	{
		[Fact]
		public void Works()
		{
			var printer =
				new TestPrinter
				(
					"Thank you!",
					"¡Gracias!",
					"ありがと"
				);

			new _02_MultipleLanguages().Run(printer);

			printer.NoneLeft();
		}
	}
}
