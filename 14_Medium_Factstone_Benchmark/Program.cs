using System;
using System.Collections.Generic;
using System.Linq;
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

			lines.Add("1960");
			lines.Add("1981");
			lines.Add("2050");
			lines.Add("2160");
			lines.Add("0");
						
			Dictionary<int, int> yearToFactstoneRating = new Dictionary<int, int>();
			int startYear = 1960;
			int endYear = lines.Max(l => int.Parse(l));
			int initialWordSize = 4;

			int currentWordSize = initialWordSize;
			BigInteger factorial = 1;
			int n = 1;
			for (int year = startYear; year <= endYear; year += 10)
			{
				BigInteger maxValue = (BigInteger.One << currentWordSize) - 1;

				while (factorial <= maxValue)
				{
					factorial *= n;
					n++;
				}

				yearToFactstoneRating[year] = n - 2;
				currentWordSize *= 2;
			}

			int indexLines = 0;

			while (lines[indexLines] != "0")
			{
				int year = int.Parse(lines[indexLines]);
				indexLines++;

				int wordSizeYear = ((year - startYear) / 10) * 10 + startYear;
				Console.WriteLine(yearToFactstoneRating[wordSizeYear]);
			}			
		}
	}
}