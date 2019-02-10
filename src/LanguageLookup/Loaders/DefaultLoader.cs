using System.Linq;

namespace LanguageLookup
{
	public class DefaultLoader<TLanguageInterface> : ILoader<TLanguageInterface>
	{
		private readonly LanguageSpecBuilder<TLanguageInterface> _specBuilder;

		public DefaultLoader() => _specBuilder = new LanguageSpecBuilder<TLanguageInterface>(new LanguageItemMetadataGenerator<TLanguageInterface>());

		public TLanguageInterface Load(string group)
		{
			var type = _specBuilder.CreateNewBuilder();

			type.Write((languageItem) =>
			{
				return languageItem.Values.First(x => x.Group == group).Value;
			});

			return type.Build();
		}
	}
}