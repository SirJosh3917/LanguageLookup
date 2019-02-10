using System;

namespace LanguageLookup
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public sealed class ValueAttribute : Attribute
	{
		public ValueAttribute(string value, string group = null)
		{
			Value = value;
			Group = group;
		}

		public string Value { get; }
		public string Group { get; }
	}
}