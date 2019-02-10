using System.Reflection;

namespace LanguageLookup
{
	public interface ILanguageItemMetadataGenerator<TLanguageInterface>
	{
		ILanguageItem GenerateItem(PropertyInfo propertyInfo);
	}
}