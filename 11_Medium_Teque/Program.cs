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
			
			lines.Add("9");
			lines.Add("push_back 9");
			lines.Add("push_front 3");
			lines.Add("push_middle 5");
			lines.Add("get 0");
			lines.Add("get 1");
			lines.Add("get 2");
			lines.Add("push_middle 1");
			lines.Add("get 1");
			lines.Add("get 2");

			var numberOfOperations = int.Parse(lines[0]);			
			
			List<int> l1 = new List<int>();
			List<int> l2 = new List<int>();

			for (int i = 0; i < numberOfOperations; i++)
			{
				var operationSplit = lines[i + 1].Split(' ');				
				var operationCommand = operationSplit[0];
				var operationValue = int.Parse(operationSplit[1]);

				if (operationCommand[0] == 'g')
				{
					if (l1.Count > operationValue)
					{
						Console.WriteLine(l1[operationValue]);
					}
					else
					{
						Console.WriteLine(l2[operationValue - l1.Count]);
					}
				}
				else if (operationCommand[5] == 'b')
				{
					l2.Add(operationValue);
					Balance(l1, l2);
				}
				else if (operationCommand[5] == 'f')
				{
					l1.Insert(0, operationValue);
					Balance(l1, l2);
				}
				else
				{
					l1.Add(operationValue);
					Balance(l1, l2);
				}											
			}
		}

		private static void Balance(List<int> l1, List<int> l2)
		{
			while (l1.Count > l2.Count + 1)
			{
				l2.Insert(0, l1[l1.Count - 1]);
				l1.RemoveAt(l1.Count - 1);
			}
			while (l2.Count > l1.Count)
			{
				l1.Add(l2[0]);
				l2.RemoveAt(0);
			}
		}
	}	
}