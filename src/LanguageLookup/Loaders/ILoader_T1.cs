namespace LanguageLookup
{
	public interface ILoader<TLanguageInterface>
	{
		TLanguageInterface Load(string group);
	}
}