using System;
using System.Linq;
using System.Reflection;

namespace LanguageLookup
{
	public class LanguageItemMetadataGenerator<TLanguageInterface> : ILanguageItemMetadataGenerator<TLanguageInterface>
	{
		private readonly string _defaultGroup;

		internal class WorkingLanguageItem : ILanguageItem
		{
			public Type Type { get; set; }
			public string Name { get; set; }
			public IMetadata[] Values { get; set; }
		}

		internal class WorkingMetadata : IMetadata
		{
			public string Group { get; set; }
			public string Value { get; set; }
		}

		public LanguageItemMetadataGenerator()
		{
			_defaultGroup =
				// TODO: :thinking:
				(typeof(TLanguageInterface)
					.GetCustomAttribute<GroupAttribute>()
					?? new GroupAttribute(null))
				.Group;
		}

		public ILanguageItem GenerateItem(PropertyInfo propertyInfo)
		{
			var workingLanguageItem = new WorkingLanguageItem
			{
				Type = propertyInfo.PropertyType,
				Name = propertyInfo.Name
			};

			var values = propertyInfo
				.GetCustomAttributes<ValueAttribute>()
				?.ToArray();

			if (values == null) throw new ArgumentNullException(propertyInfo.Name, $"Property {propertyInfo.Name} on type {typeof(TLanguageInterface)} has no defualt value specified");

			var generatedMetadataValues = values
				.Select(x => new WorkingMetadata
				{
					Value = x.Value ?? throw new ArgumentNullException($"Value on {propertyInfo.Name} on type {typeof(TLanguageInterface)} can't be null."),
					Group = x.Group ?? _defaultGroup ?? throw new ArgumentNullException($"No default group specified for {propertyInfo.Name} on type {typeof(TLanguageInterface)} can't be null.")
				})
				.ToArray();

			// TODO: verify no duplicate metadata or else throw an exception

			workingLanguageItem.Values = generatedMetadataValues;

			return workingLanguageItem;
		}
	}
}