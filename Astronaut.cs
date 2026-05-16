using System.Collections.Generic;

namespace SpaceMission
{
    class Astronaut
    {
        public string Name { get; private set; }
        public int StartRow { get; private set; }
        public int StartCol { get; private set; }
        public int ShortestPath { get; set; }
        public List<(int, int)> Path { get; set; }

        public Astronaut(string name, int startRow, int startCol)
        {
            Name = name;
            StartRow = startRow;
            StartCol = startCol;
            ShortestPath = -1;
            Path = new List<(int, int)>();
        }
    }
}