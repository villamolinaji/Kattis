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
			lines.Add("too high");
			lines.Add("3");
			lines.Add("too low");
			lines.Add("4");
			lines.Add("too high");
			lines.Add("2");
			lines.Add("right on");
			lines.Add("5");
			lines.Add("too low");
			lines.Add("7");
			lines.Add("too high");
			lines.Add("6");
			lines.Add("right on");
			lines.Add("0");			

			List<string> results = new List<string>();
			List<int> guesses = new List<int>();
			List<string> responses = new List<string>();

			int contLines = 0;
			while (lines[contLines] != "0")
			{

				int guess = int.Parse(lines[contLines]);
				contLines++;
				string response = lines[contLines];
				contLines++;

				if (response == "right on")
				{
					// Process the collected guesses and responses
					if (IsStanHonest(guesses, responses, guess))
					{
						results.Add("Stan may be honest");
					}
					else
					{
						results.Add("Stan is dishonest");
					}

					// Clear the lists for the next game
					guesses.Clear();
					responses.Clear();
				}
				else
				{
					guesses.Add(guess);
					responses.Add(response);
				}
			}

			// Output the results
			foreach (var result in results)
			{
				Console.WriteLine(result);
			}			
		}

		private static bool IsStanHonest(List<int> guesses, List<string> responses, int finalGuess)
		{
			int minBound = 1;
			int maxBound = 10;

			for (int i = 0; i < guesses.Count; i++)
			{
				int guess = guesses[i];
				string response = responses[i];

				if (response == "too high")
				{
					if (guess <= minBound || guess <= finalGuess)
					{
						return false;
					}
					maxBound = Math.Min(maxBound, guess - 1);
				}
				else if (response == "too low")
				{
					if (guess >= maxBound || guess >= finalGuess)
					{
						return false;
					}
					minBound = Math.Max(minBound, guess + 1);
				}
			}

			return finalGuess >= minBound && 
				finalGuess <= maxBound;
		}
	}
}