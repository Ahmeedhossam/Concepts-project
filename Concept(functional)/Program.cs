namespace GameOfLifeLinqFunctional
{
    internal class Program
    {
        private const int ROWS = 20;
        private const int COLS = 40;

        private static void Main()
        {
            Console.CursorVisible = false;
            var rand = new Random();

            var initialGrid = Enumerable.Range(0, ROWS)
                .Select(x => Enumerable.Range(0, COLS)
                    .Select(y => rand.Next(2) == 0)
                    .ToArray())
                .ToArray();

            Func<bool, int, bool> conwayRules = (isAlive, neighbors) =>
            {
                if (isAlive) return neighbors == 2 || neighbors == 3;
                return neighbors == 3;
            };

            RunGameLoop(initialGrid, conwayRules);
        }

        private static void RunGameLoop(bool[][] currentGrid, Func<bool, int, bool> rule)
        {
            PrintGrid(currentGrid);

            Thread.Sleep(100);

            var nextGrid = GenerateNextGen(currentGrid, rule);

            RunGameLoop(nextGrid, rule);
        }

        private static bool[][] GenerateNextGen(bool[][] grid, Func<bool, int, bool> rule)
        {
            return Enumerable.Range(0, ROWS).Select(x =>

                Enumerable.Range(0, COLS).Select(y =>
                {
                    bool isAlive = grid[x][y];
                    int neighbors = CountNeighbors(grid, x, y);

                    return rule(isAlive, neighbors);
                })
                .ToArray()

            ).ToArray();
        }

        private static int CountNeighbors(bool[][] grid, int x, int y)
        {
            var range = Enumerable.Range(-1, 3);

            return range.SelectMany(i => range.Select(j => new { i, j }))

                .Where(offset => !(offset.i == 0 && offset.j == 0))

                .Count(offset =>
                {
                    int nx = x + offset.i;
                    int ny = y + offset.j;

                    return nx >= 0 && nx < ROWS &&
                           ny >= 0 && ny < COLS &&

                           grid[nx][ny];
                });
        }

        private static void PrintGrid(bool[][] grid)
        {
            Console.SetCursorPosition(0, 0);
            var output = grid.Aggregate("", (currentGridStr, row) =>
                currentGridStr + row.Aggregate("", (rowStr, cell) => rowStr + (cell ? "O" : " ")) + "\n");

            Console.Write(output);
        }
    }
}