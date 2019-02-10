namespace LanguageLookup
{
	public interface ILoader<TLanguageInterface, TData>
	{
		TLanguageInterface Load(string group, TData data);
	}
}