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
			[Value("_")]
			[Value("A", "a")]
			[Value("B", "b")]
			string Char { get; }
		}

		[Fact]
		public void IsntCompletelyBroken()
		{
			var loader = new DefaultLoader<ILang>();

			var _ = loader.Load("_");

			_.Char
				.Should()
				.Be("_");

			var a = loader.Load("a");

			a.Char
				.Should()
				.Be("A");

			var b = loader.Load("b");

			b.Char
				.Should()
				.Be("B");
		}
	}
}