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

			lines.Add("4 3 4 0");
			lines.Add("0 1 2");
			lines.Add("1 2 2");
			lines.Add("3 0 2");
			lines.Add("0");
			lines.Add("1");
			lines.Add("2");
			lines.Add("3");
			lines.Add("2 1 1 0");
			lines.Add("0 1 100");
			lines.Add("1");
			lines.Add("0 0 0 0");

			int contLine = 0;
			var cases = new List<Case>();

			while (lines[contLine] != "0 0 0 0")
			{
				var firstLineSplit = lines[contLine].Split(' ');
				int n = int.Parse(firstLineSplit[0]);
				int m = int.Parse(firstLineSplit[1]);
				int q = int.Parse(firstLineSplit[2]);
				int s = int.Parse(firstLineSplit[3]);

				var graph = new List<(int, int)>[n];
				for (int i = 0; i < n; i++)
				{
					graph[i] = new List<(int, int)>();
				}

				var listM = new List<M>();
				for (int i = 0; i < m; i++)
				{
					contLine++;
					var mSplit = lines[contLine].Split(' ');
					var mObj = new M();
					mObj.U = int.Parse(mSplit[0]);
					mObj.V = int.Parse(mSplit[1]);
					mObj.W = int.Parse(mSplit[2]);
					listM.Add(mObj);

					graph[mObj.U].Add((mObj.V, mObj.W));
				}

				var listQ = new List<int>();
				for (int i = 0; i < q; i++)
				{
					contLine++;
					listQ.Add(int.Parse(lines[contLine]));
				}

				var caseObj = new Case(n);				
				caseObj.M = m;
				caseObj.Q = q;
				caseObj.S = s;
				caseObj.ListM = listM;
				caseObj.ListQ = listQ;
				caseObj.Graph = graph;

				cases.Add(caseObj);

				contLine++;
			}

			foreach (var caseItem in cases)
			{
				var distances = CalculateDistanceDijkstra(caseItem.Graph, caseItem.S, caseItem.N);
				
				foreach (var query in caseItem.ListQ)
				{
					if (distances[query] == int.MaxValue)
					{
						Console.WriteLine("Impossible");
					}
					else
					{
						Console.WriteLine(distances[query]);
					}
				}

				Console.WriteLine();
			}				
		}

		private static int[] CalculateDistanceDijkstra(List<(int, int)>[] graph, int start, int n)
		{
			var distances = new int[n];
			for (int i = 0; i < n; i++)
			{
				distances[i] = int.MaxValue;
			}
			distances[start] = 0;

			var priorityQueue = new SortedSet<(int, int)>();
			priorityQueue.Add((0, start));

			while (priorityQueue.Count > 0)
			{
				var (currentDistance, currentNode) = priorityQueue.Min;
				priorityQueue.Remove(priorityQueue.Min);

				if (currentDistance > distances[currentNode])
				{
					continue;
				}

				foreach (var (neighbor, weight) in graph[currentNode])
				{
					int newDist = currentDistance + weight;
					if (newDist < distances[neighbor])
					{
						priorityQueue.Remove((distances[neighbor], neighbor));
						distances[neighbor] = newDist;
						priorityQueue.Add((newDist, neighbor));
					}
				}
			}

			return distances;
		}
	}

	public class M
	{

		public int U { get; set; }
		public int V { get; set; }
		public int W { get; set; }
	}

	public class Case
	{
		public int N { get; set; }
		public int M { get; set; }
		public int Q { get; set; }
		public int S { get; set; }
		public List<M> ListM { get; set; }
		public List<int> ListQ { get; set; }

		public List<(int, int)>[] Graph { get; set; }

		public Case(int n)
		{
			N = n;
			ListM = new List<M>();
			ListQ = new List<int>();
			Graph = new List<(int, int)>[n];
		}
	}

}