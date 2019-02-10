using System;

namespace LanguageLookup.Example.Examples
{
	[Example(2)]
	public class _02_MultipleLanguages : IExample
	{
		public class Groups
		{
			// now we have multiple groups
			public const string English = "english";

			public const string Spanish = "spanish";
			public const string Japanese = "japanese";
		}

		public interface ILanguage
		{
			// we can use multiple value attributes
			// to specify multiple strings for different languages
			[Value("Thank you!", Groups.English)]
			[Value("¡Gracias!", Groups.Spanish)]
			[Value("ありがと", Groups.Japanese)]
			string ThankYou { get; }
		}

		public void Run()
		{
			var loader = new DefaultLoader<ILanguage>();

			// we load each group here
			var english = loader
				.Load(Groups.English);

			var spanish = loader
				.Load(Groups.Spanish);

			var japanese = loader
				.Load(Groups.Japanese);

			// each language is under the same interface
			// we can use the same properties to access each
			// making our code very clean

			// Thank you!
			PrintThankYou(english);

			// ¡Gracias!
			PrintThankYou(spanish);

			// ありがと
			PrintThankYou(japanese);
		}

		public void PrintThankYou(ILanguage language)

			// now i don't have to know what language i'm speaking
			// i can just trust the interface to have the language implemented correctly
			=> Console.WriteLine(language.ThankYou);
	}
}