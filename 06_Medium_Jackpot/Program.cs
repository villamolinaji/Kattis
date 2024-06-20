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
			lines.Add("3");
			lines.Add("10 6 15");
			lines.Add("5");
			lines.Add("1 2 3 4 5");

			var jackpot = new Jackpot();
			int contLine = 0;
			jackpot.NumberOfMachines = int.Parse(lines[contLine]);
			contLine++;

			for (int i = 0; i < jackpot.NumberOfMachines; i++)
			{
				var machine = new Machine();
				machine.NumberOfWheels = int.Parse(lines[contLine]);
				contLine++;
				machine.WeelPeriodicies = lines[contLine].Split().Select(long.Parse).ToList();
				contLine++;

				jackpot.Machines.Add(machine);
			}

			foreach (var machine in jackpot.Machines)
			{
				SimulateJackpot(machine);				
			}
		}

		private static void SimulateJackpot(Machine machine)
		{
			long lcm = machine.WeelPeriodicies.Aggregate((a, b) => CalculateLeastCommonMultiple(a, b));

			if (lcm <= 1_000_000_000)
			{
				Console.WriteLine(lcm);
			}
			else
			{
				Console.WriteLine("More than a billion.");
			}
		}

		private static long CalculateLeastCommonMultiple(long a, long b)
		{
			return (a / CalculateGreatestCommonDivisor(a, b)) * b;
		}

		private static long CalculateGreatestCommonDivisor(long a, long b)
		{
			while (b != 0)
			{
				long temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}
	}

	public class Jackpot
	{
		public int NumberOfMachines { get; set; }
		public List<Machine> Machines { get; set; }

		public Jackpot()
		{
			Machines = new List<Machine>();
		}
	}

	public class Machine
	{
		public int NumberOfWheels { get; set; }
		public List<long> WeelPeriodicies { get; set; }

		public Machine()
		{
			WeelPeriodicies = new List<long>();
		}
	}
}