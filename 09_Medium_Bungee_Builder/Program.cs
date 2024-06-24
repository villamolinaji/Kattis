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

			lines.Add("10");
			lines.Add("3 5 1 4 2 3 7 2 2 5");	
						
			var mountains = new List<long>();
			var lineSplit = lines[1].Split(' ');
			foreach (var m in lineSplit)
			{
				mountains.Add(long.Parse(m));
			}

			var output = CalculateBridge(mountains);
			Console.WriteLine(output);
		}

		private static long CalculateBridge(List<long> mountains)
		{
			long height = 0L;

			for (int i = 0; i < mountains.Count - 2; i++)
			{
				var heightMountain = EvaluateMountain(mountains, i);

				height = Math.Max(height, heightMountain);
			}

			return height;
		}

		private static long EvaluateMountain(List<long> mountains, int index)
		{
			long heightMountain = 0L;			
			bool isBridge = false;

			for (int j = index + 1; j < mountains.Count; j++)
			{
				if (mountains[j] < mountains[index])
				{
					heightMountain = Math.Max(heightMountain, mountains[index] - mountains[j]);
				}
				else
				{
					isBridge = true;
					break;
				}
			}
			
			return isBridge 
				? heightMountain
				: 0L;
		}
	}
}