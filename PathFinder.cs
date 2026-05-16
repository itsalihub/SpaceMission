using System.Collections.Generic;

namespace SpaceMission
{
    class PathFinder : IPathFinder
    {
        private Grid _grid;

        public PathFinder(Grid grid)
        {
            _grid = grid;
        }

        public (int distance, List<(int, int)> path) FindShortestPath(int startRow, int startCol)
        {
            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };

            var visited = new bool[_grid.Rows, _grid.Cols];
            var parent = new (int, int)[_grid.Rows, _grid.Cols];

            for (int r = 0; r < _grid.Rows; r++)
                for (int c = 0; c < _grid.Cols; c++)
                    parent[r, c] = (-1, -1);

            var queue = new Queue<(int row, int col, int dist)>();
            queue.Enqueue((startRow, startCol, 0));
            visited[startRow, startCol] = true;

            while (queue.Count > 0)
            {
                var (row, col, dist) = queue.Dequeue();

                if (_grid.Map[row, col] == "F")
                {
                    var path = new List<(int, int)>();
                    int cr = row, cc = col;

                    while (cr != -1 && cc != -1)
                    {
                        path.Add((cr, cc));
                        var (pr, pc) = parent[cr, cc];
                        cr = pr;
                        cc = pc;
                    }

                    path.Reverse();
                    return (dist, path);
                }

                for (int d = 0; d < 4; d++)
                {
                    int nr = row + dr[d];
                    int nc = col + dc[d];

                    if (_grid.IsValid(nr, nc) && !visited[nr, nc] && _grid.IsPassable(nr, nc))
                    {
                        visited[nr, nc] = true;
                        parent[nr, nc] = (row, col);
                        queue.Enqueue((nr, nc, dist + 1));
                    }
                }
            }

            return (-1, new List<(int, int)>());
        }
    }
}