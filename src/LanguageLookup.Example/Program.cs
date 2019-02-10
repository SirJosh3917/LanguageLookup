using System;
using System.Linq;
using System.Reflection;

namespace LanguageLookup.Example
{
	public sealed class ExampleAttribute : Attribute
	{
		public ExampleAttribute(int exampleId) => ExampleId = exampleId;

		public int ExampleId { get; }
	}

	public interface IExample
	{
		void Run();
	}

	internal class Program
	{
		private static void Main(string[] args)
		{
			var examples = typeof(Program).Assembly
				.GetTypes()
				.Where(type => type.GetCustomAttribute<ExampleAttribute>() != null)
				.OrderBy(x => x.GetCustomAttribute<ExampleAttribute>().ExampleId)
				.Select(type => (IExample)Activator.CreateInstance(type))
				.ToArray();

			for (var i = 0; i < examples.Length; i++)
			{
				Console.WriteLine($"\tRunning example {i}:");

				examples[i].Run();

				Console.WriteLine();
			}

			Console.ReadLine();
		}
	}
}