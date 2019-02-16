using FluentAssertions;
using LanguageLookup;
using Xunit;

namespace LanguageLookupTests
{
	// i'm really lazy lol

	public class EhJustMakeSureItIsntCompletelyBroken
	{
		[Group("_")]
		public interface ILang
		{
			[Value("underscore")]
			[Value("A", "a")]
			[Value("B", "b")]
			string Char { get; }
		}

		[Fact]
		public void IsntCompletelyBroken()
		{
			var loader = new DefaultLoader<ILang>();

			var lang_ = loader.Load("_");

			lang_.Char
				.Should()
				.Be("underscore");

			var langA = loader.Load("a");

			langA.Char
				.Should()
				.Be("A");

			var langB = loader.Load("b");

			langB.Char
				.Should()
				.Be("B");
		}
	}
}