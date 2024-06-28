using System;
using System.Collections.Generic;
using System.Numerics;

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
			
			lines.Add("0");
			lines.Add("1");
			lines.Add("4");
			lines.Add("7");
			lines.Add("18");
			lines.Add("49");
			lines.Add("51");
			lines.Add("768");			
			lines.Add("-1");

			foreach (var line in lines)
			{
				if (line == "-1")
				{
					break;
				}

				var numberString = line;

				var persistence = FindSmallestNumberWithPersistence(numberString);
				Console.WriteLine(persistence);
			}
		}

		private static string FindSmallestNumberWithPersistence(string numberString)
		{		
			var targetPersistence = BigInteger.Parse(numberString);
			if (targetPersistence < 10)
			{
				return $"1{targetPersistence}";
			}

			List<int> factors = new List<int>();
			if (!Factorize(targetPersistence, factors))
			{
				return "There is no such number.";
			}

			factors.Sort();
			string result = string.Join("", factors);
			return result;
		}

		private static bool Factorize(BigInteger n, List<int> factors)
		{
			for (int i = 9; i > 1; i--)
			{
				while (n % i == 0)
				{
					factors.Add(i);
					n /= i;
				}
			}
			return n == 1;
		}
	}
}