using System;
using System.Collections.Generic;

namespace Program
{
	public static class Program
	{
		private const char EmptyCell = ' ';

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
			lines.Add("01011");
			lines.Add("110 1");
			lines.Add("01110");
			lines.Add("01010");
			lines.Add("00100");
			lines.Add("10110");
			lines.Add("01 11");
			lines.Add("10111");
			lines.Add("01001");
			lines.Add("00000");

			var numberInputs = int.Parse(lines[0]);

			var games = new List<Game>();

			int contLine = 1;

			for (int i = 0; i < numberInputs; i++)
			{
				var game = new Game();

				for (int j = 0; j < 5; j++)
				{
					var line = lines[contLine];
					contLine++;

					for (int k = 0; k < 5; k++)
					{
						game.Board[j, k] = line[k];
					}
				}

				games.Add(game);
			}

			var tartgetBoard = new char[,]
			{
					{'1', '1', '1', '1', '1'},
					{'0', '1', '1', '1', '1'},
					{'0', '0', EmptyCell, '1', '1'},
					{'0', '0', '0', '0', '1'},
					{'0', '0', '0', '0', '0'}
			};

			int[] dRow = { -2, -2, -1, 1, 2, 2, 1, -1 };
			int[] dCol = { -1, 1, 2, 2, 1, -1, -2, -2 };

			foreach (var game in games)
			{
				var movements = SolveGame(game, tartgetBoard, dRow, dCol);
				var output = movements > 10
					? "Unsolvable in less than 11 move(s)."
					: $"Solvable in {movements} move(s).";
				Console.WriteLine(output);
			}
		}

		private static int SolveGame(Game game, char[,] tartgetBoard, int[] dRow, int[] dCol)
		{
			int minMovements = int.MaxValue;

			var queue = new PriorityQueue<QueueItem, int>();

			var initialBoard = CopyBoard(game.Board);
			var (emptyPositionRow, emptyPositionCol) = FindEmptySpace(initialBoard);
			queue.Enqueue(new QueueItem(initialBoard, 0, emptyPositionRow, emptyPositionCol), 0);

			var visited = new HashSet<string> { BoardToString(initialBoard) };

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();

				if (item.Movements > 10)
				{
					continue;
				}

				var wrongKnights = GetNumberOfWrongKnights(item.Board, tartgetBoard);
				if (wrongKnights + item.Movements > 10)
				{
					continue;
				}

				if (wrongKnights == 0)
				{
					minMovements = Math.Min(minMovements, item.Movements);
					continue;
				}

				foreach (var movement in GetPossibleMovements(item.EmptyPositionRow, item.EmptyPositionCol, dRow, dCol))
				{
					var newBoard = CopyBoard(item.Board);
					newBoard[item.EmptyPositionRow, item.EmptyPositionCol] = newBoard[movement.Item1, movement.Item2];
					newBoard[movement.Item1, movement.Item2] = EmptyCell;

					var boardString = BoardToString(newBoard);
					if (!visited.Contains(boardString))
					{
						visited.Add(boardString);
						var nextMovements = item.Movements + 1;
						queue.Enqueue(
							new QueueItem(newBoard, nextMovements, movement.Item1, movement.Item2),
							nextMovements);
					}
				}
			}


			return minMovements;
		}

		private static (int, int) FindEmptySpace(char[,] board)
		{
			for (int r = 0; r < 5; r++)
			{
				for (int c = 0; c < 5; c++)
				{
					if (board[r, c] == ' ')
					{
						return (r, c);
					}
				}
			}

			return (-1, -1);
		}		

		private static int GetNumberOfWrongKnights(char[,] board, char[,] tartgetBoard)
		{
			int wrongKnights = 0;

			for (int r = 0; r < 5; r++)
			{
				for (int c = 0; c < 5; c++)
				{
					if (board[r, c] != tartgetBoard[r, c] &&
						board[r, c] != EmptyCell)
					{
						wrongKnights++;
					}
				}
			}
			return wrongKnights;
		}

		private static IEnumerable<(int, int)> GetPossibleMovements(int emptyPositionRow, int emptyPositionCol, int[] dRow, int[] dCol)
		{						
			for (int i = 0; i < 8; i++)
			{
				int nRow = emptyPositionRow + dRow[i];
				int nCol = emptyPositionCol + dCol[i];

				if (IsValidCell(nRow, nCol))
				{
					yield return (nRow, nCol);					
				}
			}
		}		

		private static char[,] CopyBoard(char[,] board)
		{
			char[,] newBoard = new char[5, 5];
			Array.Copy(board, newBoard, board.Length);
			return newBoard;
		}

		private static string BoardToString(char[,] board)
		{
			char[] chars = new char[25];
			for (int r = 0; r < 5; r++)
			{
				for (int c = 0; c < 5; c++)
				{
					char cell = board[r, c];
					chars[r * 5 + c] = cell;
				}
			}
			return new string(chars);
		}

		private static bool IsValidCell(int row, int col)
		{
			return row >= 0 && row < 5 && col >= 0 && col < 5;
		}
	}

	public class Game
	{
		public char[,] Board { get; set; }

		public Game()
		{
			Board = new char[5, 5];
		}
	}

#pragma warning disable S2368 // Public methods should not have multidimensional array parameters
	public class QueueItem(char[,] board, int movements, int emptyPositionRow, int emptyPositionCol)
#pragma warning restore S2368 // Public methods should not have multidimensional array parameters
	{
		public char[,] Board { get; set; } = board;
		public int Movements { get; set; } = movements;

		public int EmptyPositionRow { get; set; } = emptyPositionRow;
		public int EmptyPositionCol { get; set; } = emptyPositionCol;
	}
}