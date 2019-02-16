using FluentAssertions;
using LanguageLookup.Example;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLookup.Tests.Examples
{
	public class TestPrinter : IPrinter
	{
		private readonly IEnumerable<string> _expect;
		private readonly IEnumerator<string> _enumerator;

		public TestPrinter(params string[] expect) : this((IEnumerable<string>)expect)
		{
		}

		public TestPrinter(IEnumerable<string> expect)
		{
			_expect = expect;
			_enumerator = _expect.GetEnumerator();
		}

		public void Print(string str)
		{
			if (!_enumerator.MoveNext()) throw new Exception("Ran out of items to check for");

			str.Should()
				.Be(_enumerator.Current);
		}

		public void NoneLeft()
			=> _enumerator.MoveNext().Should().BeFalse();
	}
}
