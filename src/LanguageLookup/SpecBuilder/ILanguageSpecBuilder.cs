namespace LanguageLookup
{
	public interface ILanguageSpecBuilder<TLanguageInterface>
	{
		ILanguageTypeBuilder<TLanguageInterface> CreateNewBuilder();
	}
}