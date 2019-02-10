using System.Collections.Generic;

namespace LanguageLookup
{
	public interface ILanguageTypeBuilder<TLanguageInterface>
	{
		IEnumerable<ILanguageItem> GetItems { get; }

		void Write(HandleLanguageItem handleItem);

		TLanguageInterface Build();
	}
}