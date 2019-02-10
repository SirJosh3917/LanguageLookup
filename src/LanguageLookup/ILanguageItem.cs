using System;

namespace LanguageLookup
{
	public interface ILanguageItem
	{
		Type Type { get; }
		string Name { get; }
		IMetadata[] Values { get; }
	}
}