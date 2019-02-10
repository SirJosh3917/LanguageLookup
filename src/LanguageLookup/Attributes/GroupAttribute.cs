using System;

namespace LanguageLookup
{
	// TODO: implement inaccessible interface

	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class GroupAttribute : Attribute
	{
		public GroupAttribute(string group)
		{
			// null checking this will break it
			Group = group;
		}

		public string Group { get; set; }
	}
}