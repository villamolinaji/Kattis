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
			
			lines.Add("p");
			lines.Add("Popup");
			lines.Add("helo");
			lines.Add("Hello there!");
			lines.Add("peek a boo");
			lines.Add("you speek a bootiful language");
			lines.Add("anas");
			lines.Add("bananananaspaj");

			for (int i = 0; i < lines.Count; i += 2)
			{
				string pattern = lines[i];
				string text = lines[i + 1];

				var positions = KmpSearch(pattern, text);

				Console.WriteLine(string.Join(" ", positions));
			}
		}

		private static List<int> KmpSearch(string pattern, string text)
		{
			List<int> positions = new List<int>();
			int[] lps = ComputeLpsArray(pattern);
			int indexText = 0;
			int indexPattern = 0;

			while (indexText < text.Length)
			{
				if (pattern[indexPattern] == text[indexText])
				{
					indexPattern++;
					indexText++;
				}

				if (indexPattern == pattern.Length)
				{
					positions.Add(indexText - indexPattern);
					indexPattern = lps[indexPattern - 1];
				}
				else if (indexText < text.Length && pattern[indexPattern] != text[indexText])
				{
					if (indexPattern != 0)
					{
						indexPattern = lps[indexPattern - 1];
					}
					else
					{
						indexText++;
					}
				}
			}

			return positions;
		}

		private static int[] ComputeLpsArray(string pattern)
		{
			int length = 0;
			int i = 1;
			int[] lps = new int[pattern.Length];
			lps[0] = 0;

			while (i < pattern.Length)
			{
				if (pattern[i] == pattern[length])
				{
					length++;
					lps[i] = length;
					i++;
				}
				else
				{
					if (length != 0)
					{
						length = lps[length - 1];
					}
					else
					{
						lps[i] = 0;
						i++;
					}
				}
			}

			return lps;
		}
	}
}