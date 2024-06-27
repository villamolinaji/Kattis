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
			
			lines.Add("8 20 2");
			lines.Add("5 3");
			lines.Add("4 1");
			lines.Add("1 2");
			lines.Add("7 2");
			lines.Add("10 2");
			lines.Add("13 3");
			lines.Add("16 2");
			lines.Add("19 4");
			lines.Add("3 10 1");
			lines.Add("3 5");
			lines.Add("9 3");
			lines.Add("6 1");
			lines.Add("3 10 1");
			lines.Add("5 3");
			lines.Add("1 1");
			lines.Add("9 1");

			int linesIndex = 0;

			var wateringGrassCases = new List<WateringGrassCase>();

			while (linesIndex < lines.Count)
			{
				var wateringGrassCase = new WateringGrassCase();

				var firstLineSplit = lines[linesIndex].Split(' ');
				linesIndex++;

				wateringGrassCase.N = int.Parse(firstLineSplit[0]);
				wateringGrassCase.L = int.Parse(firstLineSplit[1]);
				wateringGrassCase.W = int.Parse(firstLineSplit[2]);

				for (int i = 0; i < wateringGrassCase.N; i++)
				{
					var sprinklerSplit = lines[linesIndex].Split(' ');
					linesIndex++;

					var sprinkler = new Splrinkler
					{
						X = int.Parse(sprinklerSplit[0]),
						R = int.Parse(sprinklerSplit[1])
					};

					wateringGrassCase.Sprinklers.Add(sprinkler);

					if (sprinkler.R > wateringGrassCase.W / 2.0)
					{
						double dx = Math.Sqrt(sprinkler.R * sprinkler.R - (wateringGrassCase.W / 2.0) * (wateringGrassCase.W / 2.0));
						wateringGrassCase.Intervals.Add((sprinkler.X - dx, sprinkler.X + dx));
					}
				}

				wateringGrassCases.Add(wateringGrassCase);
			}

			foreach (var wateringGrassCase in wateringGrassCases)
			{
				var output = CalculateWateringGrassCase(wateringGrassCase);
				Console.WriteLine(output);
			}
		}

		private static int CalculateWateringGrassCase(WateringGrassCase wateringGrassCase)
		{
			int result = 0;
			double currentEnd = 0;
			int i = 0;

			wateringGrassCase.Intervals.Sort((a, b) => a.left.CompareTo(b.left));

			while (currentEnd < wateringGrassCase.L)
			{
				double farthest = currentEnd;
				while (i < wateringGrassCase.Intervals.Count && 
					wateringGrassCase.Intervals[i].left <= currentEnd)
				{
					if (wateringGrassCase.Intervals[i].right > farthest)
					{
						farthest = wateringGrassCase.Intervals[i].right;
					}
					i++;
				}

#pragma warning disable S1244 // Floating point numbers should not be tested for equality
				if (farthest == currentEnd)
				{
					result = -1;
					break;
				}
#pragma warning restore S1244 // Floating point numbers should not be tested for equality

				currentEnd = farthest;
				result++;

				if (currentEnd >= wateringGrassCase.L)
				{
					break;
				}
			}

			if (currentEnd < wateringGrassCase.L)
			{
				result = -1;
			}

			return result;
		}
	}

	public class WateringGrassCase
	{
		public int N { get; set; }
		public int L { get; set; }
		public int W { get; set; }
		public List<Splrinkler> Sprinklers { get; set; }
		public List<(double left, double right)> Intervals { get; set; }

		public WateringGrassCase()
		{
			Sprinklers = new List<Splrinkler>();
			Intervals = new List<(double left, double right)>();
		}
	}

	public class Splrinkler
	{
		public int X { get; set; }
		public int R { get; set; }
	}
}