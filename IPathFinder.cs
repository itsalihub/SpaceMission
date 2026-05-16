using System.Collections.Generic;

namespace SpaceMission
{
    interface IPathFinder
    {
        (int distance, List<(int, int)> path) FindShortestPath(int startRow, int startCol);
    }
}