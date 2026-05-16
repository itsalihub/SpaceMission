using System;
using System.Collections.Generic;

namespace SpaceMission
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Space Mission!");
            Console.WriteLine("1. Enter map manually");
            Console.WriteLine("2. Generate random map");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine()!;

            string[,] map;
            int rows, cols;

            if (choice == "2")
            {
                Console.Write("Enter number of rows (2-100): ");
                rows = int.Parse(Console.ReadLine()!);

                Console.Write("Enter number of columns (2-100): ");
                cols = int.Parse(Console.ReadLine()!);

                Console.Write("Enter number of asteroids: ");
                int asteroids = int.Parse(Console.ReadLine()!);

                Console.Write("Enter number of astronauts (1-3): ");
                int numAstronauts = int.Parse(Console.ReadLine()!);

                map = GenerateRandomMap(rows, cols, asteroids, numAstronauts);

                Console.WriteLine("\nGenerated map:");
                PrintRawMap(map, rows, cols);
            }
            else
            {
                Console.WriteLine("Map rows:");
                rows = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Map columns:");
                cols = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Cosmic map:");
                map = new string[rows, cols];

                for (int r = 0; r < rows; r++)
                {
                    string[] line = Console.ReadLine()!.Split(' ');
                    for (int c = 0; c < cols; c++)
                    {
                        map[r, c] = line[c];
                    }
                }
            }

            var astronauts = new List<Astronaut>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (map[r, c] == "S1" || map[r, c] == "S2" || map[r, c] == "S3")
                    {
                        astronauts.Add(new Astronaut(map[r, c], r, c));
                    }
                }
            }

            Grid grid = new Grid(rows, cols, map);
            IPathFinder pathFinder = new PathFinder(grid);

            var failed = new List<Astronaut>();
            var succeeded = new List<Astronaut>();

            foreach (var astronaut in astronauts)
            {
                var result = pathFinder.FindShortestPath(astronaut.StartRow, astronaut.StartCol);
                astronaut.ShortestPath = result.distance;
                astronaut.Path = result.path;

                if (result.distance == -1)
                    failed.Add(astronaut);
                else
                    succeeded.Add(astronaut);
            }

            succeeded.Sort((a, b) => a.ShortestPath.CompareTo(b.ShortestPath));

            foreach (var astronaut in failed)
            {
                Console.WriteLine($"Mission failed — Astronaut {astronaut.Name} lost in space!");
            }

            foreach (var astronaut in succeeded)
            {
                Console.WriteLine($"\nAstronaut {astronaut.Name} - Shortest path: {astronaut.ShortestPath} steps");
                grid.PrintMap(astronaut.Path);
            }
        }

        static string[,] GenerateRandomMap(int rows, int cols, int asteroids, int numAstronauts)
        {
            var random = new Random();
            string[,] map = new string[rows, cols];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    map[r, c] = "O";

            map[rows - 1, cols - 1] = "F";

            string[] astronautNames = { "S1", "S2", "S3" };
            var usedPositions = new HashSet<(int, int)>();
            usedPositions.Add((rows - 1, cols - 1));

            for (int i = 0; i < numAstronauts; i++)
            {
                int r, c;
                do
                {
                    r = random.Next(0, rows);
                    c = random.Next(0, cols);
                } while (usedPositions.Contains((r, c)));

                map[r, c] = astronautNames[i];
                usedPositions.Add((r, c));
            }

            int placed = 0;
            while (placed < asteroids)
            {
                int r = random.Next(0, rows);
                int c = random.Next(0, cols);

                if (!usedPositions.Contains((r, c)))
                {
                    map[r, c] = "X";
                    usedPositions.Add((r, c));
                    placed++;
                }
            }

            return map;
        }

        static void PrintRawMap(string[,] map, int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(map[r, c]);
                    if (c < cols - 1) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}