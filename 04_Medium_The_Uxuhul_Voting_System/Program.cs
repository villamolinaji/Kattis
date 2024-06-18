using System;
using System.Collections.Generic;
using System.Linq;

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

			lines.Add("2");
			lines.Add("4");
			lines.Add("8 7 6 5 4 3 2 1");
			lines.Add("8 6 3 1 2 4 5 7");
			lines.Add("8 3 6 5 1 2 7 4");
			lines.Add("1 2 3 4 5 6 7 8");
			lines.Add("1");
			lines.Add("1 2 3 4 5 6 7 8");

			var indexLines = 0;
			var votingRounds = int.Parse(lines[indexLines]);

			var votings = new List<Voting>();

			for (int i = 0; i < votingRounds; i++)
			{
				indexLines++;
				var voting = new Voting();
				voting.NumberOfPriest = int.Parse(lines[indexLines]);

				for (int j = 0; j < voting.NumberOfPriest; j++)
				{
					indexLines++;

					voting.VotingPreferences.Add(lines[indexLines].Split(' ').Select(int.Parse).ToList());
				}

				votings.Add(voting);
			}

			foreach (var voting in votings)
			{
				var outcome = SimulateVoting(voting);
				Console.WriteLine(outcome);
			}
		}

		public static string SimulateVoting(Voting voting)
		{
			int initialState = 0;

			var memo = new Dictionary<(int, int), int>();

			var finalState = Simulate(initialState, voting, 0, memo);

			return ConvertStateToString(finalState);
		}	

		public static int Simulate(int currentState, Voting voting, int priestIndex, Dictionary<(int, int), int> memo)
		{
			if (priestIndex == voting.NumberOfPriest)
			{				
				return currentState;
			}

			var key = (currentState, priestIndex);
			if (memo.ContainsKey(key))
			{
				return memo[key];
			}

			int bestState = -1;
			int bestPreference = int.MaxValue;

			for (int i = 0; i < 3; i++)
			{				
				int nextState = currentState ^ (1 << i);

				int resultingState = Simulate(nextState, voting, priestIndex + 1, memo);
				
				int preference = voting.VotingPreferences[priestIndex][resultingState];
				
				if (preference < bestPreference)
				{
					bestPreference = preference;
					bestState = resultingState;
				}
			}

			memo[key] = bestState;

			return bestState;
		}

		public static string ConvertStateToString(int state)
		{
			char[] result = new char[3];
			for (int i = 0; i < 3; i++)
			{
				result[i] = (state & (1 << (2 - i))) != 0 ? 'Y' : 'N';
			}
			return new string(result);
		}
	}

	public class Voting
	{
		public int NumberOfPriest { get; set; }
		public List<List<int>> VotingPreferences { get; set; }

		public Voting()
		{
			VotingPreferences = new List<List<int>>();
		}
	}
}