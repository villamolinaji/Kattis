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

			lines.Add("4");
			lines.Add("1 3 + 1 2");
			lines.Add("1 3 - 1 2");
			lines.Add("123 287 / 81 -82");
			lines.Add("12 -3 * -1 -1");

			var numberOfOperations = int.Parse(lines[0]);
			var operations = new List<Operation>();

			for (int i = 0; i < numberOfOperations; i++)
			{
				var splitLine = lines[i + 1].Split(" ");
				var operation = new Operation();
				operation.FirstNumerator = long.Parse(splitLine[0]);
				operation.FirstDenominator = long.Parse(splitLine[1]);
				operation.Operator = splitLine[2];
				operation.SecondNumerator = long.Parse(splitLine[3]);
				operation.SecondDenominator = long.Parse(splitLine[4]);

				operations.Add(operation);
			}

			foreach (var operation in operations)
			{
				var output = DoOperation(operation);
				Console.WriteLine(output);
			}
		}

		private static string DoOperation(Operation operation)
		{
			var operationResult = new OperationResult();
			switch (operation.Operator)
			{
				case "+":
					operationResult.ResultNumerator = (operation.FirstNumerator * operation.SecondDenominator) + (operation.SecondNumerator * operation.FirstDenominator);
					operationResult.ResultDenominator = operation.FirstDenominator * operation.SecondDenominator;
					break;
				case "-":
					operationResult.ResultNumerator = (operation.FirstNumerator * operation.SecondDenominator) - (operation.SecondNumerator * operation.FirstDenominator);
					operationResult.ResultDenominator = operation.FirstDenominator * operation.SecondDenominator;
					break;
				case "*":
					operationResult.ResultNumerator = operation.FirstNumerator * operation.SecondNumerator;
					operationResult.ResultDenominator = operation.FirstDenominator * operation.SecondDenominator;
					break;
				case "/":
					operationResult.ResultNumerator = operation.FirstNumerator * operation.SecondDenominator;
					operationResult.ResultDenominator = operation.FirstDenominator * operation.SecondNumerator;
					break;
			}

			(long simplifiedNumerator, long simplifiedDenominator) = SimplifyFraction(operationResult.ResultNumerator, operationResult.ResultDenominator);
			
			return $"{simplifiedNumerator} / {simplifiedDenominator}";
		}

		private static(long, long) SimplifyFraction(long numerator, long denominator)
		{
			long gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));

			numerator = numerator / gcd;
			denominator = denominator / gcd;


			if (denominator < 0)
			{
				numerator = -numerator;
				denominator = -denominator;
			}

			return (numerator, denominator);
		}

		private static long GCD(long a, long b)
		{
			while (b != 0)
			{
				long temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}

		public class Operation
		{
			public long FirstNumerator { get; set; }
			public long FirstDenominator { get; set; }
			public string Operator { get; set; }
			public long SecondNumerator { get; set; }
			public long SecondDenominator { get; set; }

			public Operation()
			{
				Operator = string.Empty;
			}
		}

		public class OperationResult
		{
			public long ResultNumerator { get; set; }
			public long ResultDenominator { get; set; }

			public OperationResult()
			{
				ResultNumerator = 0;
				ResultDenominator = 1;
			}
		}
	}
}