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

			lines.Add("3 5 2 5 2");
			lines.Add("4 1 1 1 2 1 5 2 1");
			lines.Add("3 2 5 3 3 3 5");
			lines.Add("1 1 1 1");
			lines.Add("1 3 2 1");
			lines.Add("1 5 3 1");
			lines.Add("2 5 4 2");
			lines.Add("3 3 5 2");
			lines.Add("3 2");		
			
#pragma warning disable S125 // Sections of code should not be commented out			
			/*
			lines.Add("3 5 1 2 1");
			lines.Add("2 1 1 1 2");
			lines.Add("3 5 1 1");
			lines.Add("1 3 2 1");
			lines.Add("1");	
			*/
#pragma warning restore S125 // Sections of code should not be commented out
			
			int contLine = 0;
			var firstLineSplit = lines[contLine].Split(' ');
			int R = int.Parse(firstLineSplit[0]);
			int C = int.Parse(firstLineSplit[1]);
			int F = int.Parse(firstLineSplit[2]);
			int S = int.Parse(firstLineSplit[3]);
			int G = int.Parse(firstLineSplit[4]);

			contLine++;
			
			var faculties = new List<Faculty>();
			var students = new List<Student>();

			for (int i = 0; i < F; i++)
			{
				var faculty = new Faculty();
				faculty.FacultyNumber = i + 1;
				var lineSplit = lines[contLine].Split(' ');
				int numberCells = int.Parse(lineSplit[0]);

				faculty.NumberCells = numberCells;
				for (int j = 0; j < numberCells; j++)
				{					
					var cellRow = long.Parse(lineSplit[(j * 2) + 1]);
					var cellCol = long.Parse(lineSplit[(j * 2) + 2]);

					faculty.Cells.Add((cellRow, cellCol));
				}

				faculties.Add(faculty);

				contLine++;
			}

			for (int i = 0; i < S; i++)
			{
				var student = new Student();				

				var lineSplit = lines[contLine].Split(' ');
				student.RowCurrent = long.Parse(lineSplit[0]);
				student.ColCurrent = long.Parse(lineSplit[1]);
				student.StudentNumber = long.Parse(lineSplit[2]);
				student.FacultyNumber = long.Parse(lineSplit[3]);				

				students.Add(student);

				contLine++;
			}

			var lastLineSplit = lines[contLine].Split(' ');
			int contFaculties = 0;
			foreach (var faculty in faculties)
			{
				faculty.NumberStudents = int.Parse(lastLineSplit[contFaculties]);
				contFaculties++;
			}

			// Allocate students to their faculty cells
			var facultyAssignedCells = new Dictionary<long, SortedSet<(long row, long col)>>();
			foreach (var faculty in faculties)
			{
				facultyAssignedCells[faculty.FacultyNumber] = new SortedSet<(long row, long col)>(faculty.Cells);
			}
			
			foreach (var student in students.OrderBy(s => s.StudentNumber))
			{				
				var assignedCell = facultyAssignedCells[student.FacultyNumber].First();
				facultyAssignedCells[student.FacultyNumber].Remove(assignedCell);
				student.RowTarget = assignedCell.row;
				student.ColTarget = assignedCell.col;				
			}

			var minMovements = MoveStudents(students, faculties, R, C, G);
			Console.WriteLine(minMovements);
		}

		public static long MoveStudents(
			List<Student> students, 
			List<Faculty> faculties,			
			int R, 
			int C, 
			int G)
		{						
			foreach (var student in students)
			{								
				student.TotalSteps = Math.Abs(student.RowCurrent - student.RowTarget) + Math.Abs(student.ColCurrent - student.ColTarget);
			}

			foreach (var faculty in faculties)
			{
				faculty.TotalSteps = 
					students
						.Where(s => s.FacultyNumber == faculty.FacultyNumber)
						.OrderBy(s => s.TotalSteps)
						.Take(faculty.NumberStudents)
						.Sum(s => s.TotalSteps);
			}

			var minSteps = 
				faculties
					.OrderBy(s => s.TotalSteps)
					.Take(G)
					.Sum(s => s.TotalSteps);

			return minSteps;
		}
	}	

	public class Student
	{
		public long StudentNumber { get; set; }
		public long FacultyNumber { get; set; }			
		public long RowCurrent { get; set; }
		public long ColCurrent { get; set; }
		public long RowTarget { get; set; }
		public long ColTarget { get; set; }
		public long TotalSteps { get; set; }
	}

	public class Faculty
	{
		public long FacultyNumber { get; set; }
		public long NumberCells { get; set; }
		public List<(long row, long col)> Cells { get; set; }
		public int NumberStudents { get; set; }
		public long TotalSteps { get; set; }

		public Faculty()
		{
			Cells = new List<(long row, long col)>();			
		}
	}	
}