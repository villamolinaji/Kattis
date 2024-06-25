
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
			lines.Add("6 5");
			lines.Add("##### ");
			lines.Add("#A#A##");
			lines.Add("# # A#");
			lines.Add("#S  ##");
			lines.Add("##### ");
			lines.Add("7 7");
			lines.Add("#####  ");
			lines.Add("#AAA###");
			lines.Add("#    A#");
			lines.Add("# S ###");
			lines.Add("#     #");
			lines.Add("#AAA###");
			lines.Add("#####  ");

			var lineIndex = 0;
			var numberTestCases = int.Parse(lines[lineIndex]);
			lineIndex++;

			var testCases = new List<TestCase>();

			for (int i = 0; i < numberTestCases; i++)
			{
				var lineSplit = lines[lineIndex].Split(' ');
				lineIndex++;
				int x = int.Parse(lineSplit[0]);
				int y = int.Parse(lineSplit[1]);

				var testCase = new TestCase(y, x);

				for (int j = 0; j < y; j++)
				{
					var line = lines[lineIndex];
					lineIndex++;

					for (int k = 0; k < x; k++)
					{
						testCase.Maze[j, k] = line[k];
					}
				}

				testCases.Add(testCase);
			}

			foreach (var testCase in testCases)
			{
				var output = SolveTestCase(testCase);
				Console.WriteLine(output);
			}
		}

		private static int SolveTestCase(TestCase testCase)
		{
			var points = new List<(int, int)>();

			// Find all endpoints
			for (int i = 0; i < testCase.LengthY; i++)
			{
				for (int j = 0; j < testCase.LengthX; j++)
				{
					if (testCase.Maze[i, j] == 'A' || testCase.Maze[i, j] == 'S')
					{
						points.Add((i, j));
					}
				}
			}

			// For each endpoint, find dist to all other endpoints, add to list
			var edges = new List<Edge>();
			foreach (var point in points)
			{				
				BFS(testCase.Maze, edges, point, testCase.LengthY, testCase.LengthX);
			}

			// Run min spanning tree
			edges.Sort((e1, e2) => e1.Steps.CompareTo(e2.Steps));
			int total = 0;
			var d = new List<int>(new int[testCase.LengthY * testCase.LengthX]);
			for (int i = 0; i < d.Count; i++)
			{
				d[i] = -1;
			}

			foreach (var edge in edges)
			{
				if (Find(d, Spot(edge.P1, testCase.LengthX)) != Find(d, Spot(edge.P2, testCase.LengthX)))
				{
					Join(d, Spot(edge.P1, testCase.LengthX), Spot(edge.P2, testCase.LengthX));
					total += edge.Steps;
				}
			}

			return total;
		}

		private static int Spot((int y, int x) p, int length)
		{
			return p.y * length + p.x;
		}

		private static int Find(List<int> d, int a)
		{
			if (d[a] == -1)
			{
				return a;
			}
			return d[a] = Find(d, d[a]);
		}

		private static void Join(List<int> d, int a, int b)
		{
			a = Find(d, a);
			b = Find(d, b);

			if (a == b)
			{
				return;
			}

			d[a] = b;
		}

		private static void BFS(char[,] v, List<Edge> edges, (int y, int x) p, int lengthY, int lengthX)
		{
			var dist = new List<List<int>>(lengthY);
			for (int i = 0; i < lengthY; i++)
			{
				dist.Add(Enumerable.Repeat(int.MaxValue, lengthX).ToList());
			}

			var q = new Queue<(int, int)>();
			q.Enqueue(p);
			dist[p.y][p.x] = 0;

			var movement = new List<(int, int)> { (1, 0), (-1, 0), (0, 1), (0, -1) };

			// Run BFS
			while (q.Count > 0)
			{
				var (currY, currX) = q.Dequeue();

				// If this is an alien or character, add an edge
				if (v[currY, currX] == 'S' || v[currY, currX] == 'A')
				{
					edges.Add(new Edge
					{
						P1 = p,
						P2 = (currY, currX),
						Steps = dist[currY][currX]
					});
				}

				// Try all movement
				foreach (var (dy, dx) in movement)
				{
					int nextY = currY + dy;
					int nextX = currX + dx;
					if (InRange(nextY, nextX, lengthY, lengthX))
					{
						if (v[nextY, nextX] == '#')
						{
							continue;
						}
						if (dist[nextY][nextX] > dist[currY][currX] + 1)
						{
							dist[nextY][nextX] = dist[currY][currX] + 1;
							q.Enqueue((nextY, nextX));
						}
					}
				}
			}
		}

		private static bool InRange(int pY, int pX,  int lengthY, int lengthX)
		{
			return pY >= 0 && pY < lengthY && pX >= 0 && pX < lengthX;
		}
	}		

	public class TestCase
	{
		public int LengthY { get; set; }
		public int LengthX { get; set; }
		public char[,] Maze { get; set; }

		public TestCase(int lengthY, int lengthX)
		{
			LengthY = lengthY;
			LengthX = lengthX;
			Maze = new char[lengthY, lengthX];
		}
	}

	public class Edge
	{
		public (int y, int x) P1 { get; set; }
		public (int y, int x) P2 { get; set; }
		public int Steps { get; set; }
	}			
}