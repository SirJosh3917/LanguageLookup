using System;

namespace LanguageLookup.Example.Examples
{
	[Example(3)]
	public class _03_DefaultGroup : IExample
	{
		public class Groups
		{
			public const string English = "english";
			public const string Spanish = "spanish";
		}

		// we can specify a default group so we don't have
		// to repeat a group in the value for a default value
		[Group(Groups.English)]
		public interface ILanguage
		{
			[Value("Hello!")]
			[Value("¡Hola!", Groups.Spanish)]
			string Hello { get; }
		}

		public void Run()
		{
			var loader = new DefaultLoader<ILanguage>();

			var english = loader
				.Load(Groups.English);

			var spanish = loader
				.Load(Groups.Spanish);

			// writes "Hello!" even though we never put English on it
			Console.WriteLine(english.Hello);

			// properly writes ¡Hola!
			Console.WriteLine(spanish.Hello);
		}
	}
}