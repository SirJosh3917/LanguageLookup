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
		void Run(IPrinter printer);
	}

	public interface IPrinter
	{
		void Print(string str);
	}

	public class ConsolePrinter : IPrinter
	{
		public void Print(string str) => Console.WriteLine(str);
	}

	internal class Program
	{
		private static void Main()
		{
			var examples = typeof(Program).Assembly
				.GetTypes()
				.Where(type => type.GetCustomAttribute<ExampleAttribute>() != null)
				.OrderBy(x => x.GetCustomAttribute<ExampleAttribute>().ExampleId)
				.Select(type => (IExample)Activator.CreateInstance(type))
				.ToArray();

			var cp = new ConsolePrinter();

			for (var i = 0; i < examples.Length; i++)
			{
				Console.WriteLine($"\tRunning example {i}:");

				examples[i].Run(cp);

				Console.WriteLine();
			}

			Console.ReadLine();
		}
	}
}