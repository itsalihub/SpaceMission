using System;
using System.Collections.Generic;

namespace SpaceMission
{
    class Grid
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public string[,] Map { get; private set; }

        public Grid(int rows, int cols, string[,] map)
        {
            Rows = rows;
            Cols = cols;
            Map = map;
        }

        public bool IsValid(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Cols;
        }

        public bool IsPassable(int row, int col)
        {
            return Map[row, col] != "X";
        }

        public void PrintMap(List<(int, int)> path)
        {
            string[,] display = (string[,])Map.Clone();

            foreach (var (r, c) in path)
            {
                if (display[r, c] == "O" || display[r, c] == "D")
                    display[r, c] = "*";
            }

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Console.Write(display[r, c]);
                    if (c < Cols - 1) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}