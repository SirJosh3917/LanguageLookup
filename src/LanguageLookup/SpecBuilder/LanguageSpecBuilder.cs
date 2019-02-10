using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace LanguageLookup
{
	public class LanguageSpecBuilder<TLanguageInterface> : ILanguageSpecBuilder<TLanguageInterface>
	{
		internal class LanguageItem : ILanguageItem
		{
			public LanguageItem(PropertyInfo propertyInfo, ILanguageItem languageItem)
			{
				Property = propertyInfo;
				InnerLanguageItem = languageItem;
			}

			public PropertyInfo Property { get; }
			public ILanguageItem InnerLanguageItem { get; }

			public Type Type => InnerLanguageItem.Type;
			public string Name => InnerLanguageItem.Name;
			public IMetadata[] Values => InnerLanguageItem.Values;
		}

		internal class LanguageTypeBuilder : ILanguageTypeBuilder<TLanguageInterface>
		{
			internal LanguageTypeBuilder(TypeBuilder typeBuilder, LanguageItem[] properties)
			{
				_typeBuilder = typeBuilder;
				_properties = properties;
			}

			private readonly LanguageItem[] _properties;
			private readonly TypeBuilder _typeBuilder;

			public IEnumerable<ILanguageItem> GetItems => _properties;

			public void Write(HandleLanguageItem handleItem)
			{
				foreach (var item in _properties)
				{
					var result = handleItem(item);

					CreateProperty(item, result);
				}
			}

			private void CreateProperty(LanguageItem item, string value)
			{
				var property = item.Property;
				var type = _typeBuilder;

				var propertyBuilder = type.DefineProperty
				(
					name: property.Name,
					attributes: property.Attributes,
					returnType: property.PropertyType,
					parameterTypes: null
				);

				var getMethod = type.DefineMethod
				(
					name: $"get_{property.Name}",
					attributes: MethodAttributes.Public |
						MethodAttributes.Virtual |
						MethodAttributes.HideBySig |
						MethodAttributes.SpecialName,
					returnType: property.PropertyType,
					parameterTypes: new Type[] { }
				);

				var il = getMethod.GetILGenerator();
				il.Emit(OpCodes.Ldstr, value);
				il.Emit(OpCodes.Ret);

				propertyBuilder.SetGetMethod(getMethod);

				type.DefineMethodOverride
				(
					methodInfoBody: getMethod,
					methodInfoDeclaration: property.GetGetMethod()
				);
			}

			public TLanguageInterface Build()
				=> (TLanguageInterface)Activator.CreateInstance(_typeBuilder.CreateTypeInfo().AsType());
		}

		private readonly AssemblyName _assemblyName;
		private readonly AssemblyBuilder _assemblyBuilder;
		private readonly ModuleBuilder _moduleBuilder;

		private readonly PropertyInfo[] _properties;
		private readonly LanguageItem[] _languageItems;
		private readonly ILanguageItemMetadataGenerator<TLanguageInterface> _languageItemMetadataGenerator;

		public LanguageSpecBuilder(ILanguageItemMetadataGenerator<TLanguageInterface> languageItemMetadataGenerator)
		{
			_properties = typeof(TLanguageInterface).GetProperties();
			_languageItems =
				_properties
				.Select(property => (property: property, languageItem: languageItemMetadataGenerator.GenerateItem(property)))
				.Select(propertyLanguageItem => new LanguageItem(propertyLanguageItem.property, propertyLanguageItem.languageItem))
				.ToArray();

			_assemblyName = new AssemblyName(Guid.NewGuid().ToString());
			_assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(_assemblyName, AssemblyBuilderAccess.RunAndCollect);
			_moduleBuilder = _assemblyBuilder.DefineDynamicModule(_assemblyName.Name);
		}

		public ILanguageTypeBuilder<TLanguageInterface> CreateNewBuilder()
			=> new LanguageTypeBuilder
			(
				typeBuilder: CreateTypeBuilder(),
				properties: _languageItems
			);

		private TypeBuilder CreateTypeBuilder()
			=> _moduleBuilder.DefineType
			(
				name: Guid.NewGuid().ToString(),
				attr: TypeAttributes.Class | TypeAttributes.Public,
				interfaces: new[] { typeof(TLanguageInterface) },
				parent: null
			);
	}
}