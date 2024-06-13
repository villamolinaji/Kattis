using System;
using System.Collections.Generic;

namespace Program
{
	public static class Program
	{
		public static void Main()
		{
			var lines = new List<string>();

#pragma warning disable S125 // Sections of code should not be commented out
			/*
			string lineInput;
			while ((lineInput = Console.ReadLine()) != null)
			{
				lines.Add(lineInput);
			}
			*/
#pragma warning restore S125 // Sections of code should not be commented out

			lines.Add("10 12");
			lines.Add("71293781758123 72784");
			lines.Add("1 12345677654321");		

			var outputs = new List<long>();

			foreach (var line in lines)
			{
				var lineSplit = line.Split(" ");
				var number1 = long.Parse(lineSplit[0]);
				var number2 = long.Parse(lineSplit[1]);

				outputs.Add(Math.Abs(number1 - number2));
			}

			foreach (var output in outputs)
			{
				Console.WriteLine(output);
			}
		}
	}
}