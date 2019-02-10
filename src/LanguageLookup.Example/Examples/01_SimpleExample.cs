using System;

namespace LanguageLookup.Example.Examples
{
	[Example(1)]
	public class _01_SimpleExample : IExample
	{
		// use a class to hold our consts for each language,
		// prevents us from typos
		public class Groups
		{
			public const string English = "english";
		}

		// the language interface, where we specify the language
		public interface ILanguage
		{
			// metadata for the language specification generators
			// but if we load "english", we'll get "Hello, World!"
			// all we have to do to get that value is access this property
			[Value("Hello, World!", Groups.English)] string Greeting { get; }
		}

		public void Run()
		{
			// we assign a Loader to a variable because creating a new loader is an expensive operation.
			// loading a language is *also* an expensive operation, so you should seek to minimize these operations as much as possible.
			var loader =

				// use a DefaultLoader for our language
				new DefaultLoader<ILanguage>();

			var language =

				loader

				// load the English group
				.Load(Groups.English);

			// write the greeting to the screen
			Console.WriteLine(language.Greeting);
		}
	}
}