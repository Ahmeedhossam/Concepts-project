namespace GameOfLifeImperative
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int rows = 30;
            int cols = 60;

            bool[,] grid = new bool[rows, cols];

            //RandomGrid(grid);
            gliderpattern(grid);

            Console.CursorVisible = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                PrintGrid(grid);

                grid = GetNextGeneration(grid, Gamerules); //Variable Reassignment (mutability) // hna we are changing the grid to be the next generation grid

                Thread.Sleep(200);
            }
        }

        private static bool Gamerules(bool LiveorDead, int neighbors) // hna bashof el cell fe el next generation hayb2a 3ayesh wla mayet based on el rules wl return boolean 3la 7sb mmkn msln tb2a 3aysha w tmoot b3dha lw el return false
        {
            if (LiveorDead)
            {
                return (neighbors == 2 || neighbors == 3);
            }
            else
            {
                return (neighbors == 3);
            }
        }

        public static bool[,] GetNextGeneration(bool[,] currentGrid, Func<bool, int, bool> cellrule) //Func<bool, int, bool> cellrule means if cell is alive or dead and number of neighbors return if cell will be alive or dead in next generation and this is a delegate
        {                                                                                            //→ يأخذ bool و int                         ويرجع     bool .
            int rows = currentGrid.GetLength(0);
            int cols = currentGrid.GetLength(1);

            bool[,] newGrid = new bool[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int liveNeighbors = CountNeighbors(currentGrid, i, j);

                    bool LiveorDead = currentGrid[i, j];

                    newGrid[i, j] = cellrule(LiveorDead, liveNeighbors); //bb3t each cell live or dead and neighbors to the rule function to know the cell in next generation will be alive or not w b3d m a5ls kol el cells b return el grid kolha bl cells kolha
                }                                                        //here is the mutation the newGrid is being changed
            }

            return newGrid; // hna el grid 3la b3dha b2a fel next generation el current
        }

        private static int CountNeighbors(bool[,] grid, int x, int y)
        {
            int count = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                    {
                        if (grid[newX, newY])
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static void RandomGrid(bool[,] grid) //Pass By Reference behavior (mutability) in grid
        {
            Random rand = new Random();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (rand.Next(5) == 0) //generates number between 0 and 4 lw 0 yb2a 20% chance etb3t true
                    {
                        grid[i, j] = true;
                    }
                    else
                    {
                        grid[i, j] = false;
                    }
                }
            }
        }

        private static void gliderpattern(bool[,] grid)
        {
            if (grid.GetLength(0) > 5 && grid.GetLength(1) > 5)
            {
                grid[1, 2] = true;
                grid[2, 3] = true;
                grid[3, 1] = true;
                grid[3, 2] = true;
                grid[3, 3] = true;
            }
        }

        private static void PrintGrid(bool[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j] ? "O" : "."); //lw true etb333 O else print .
                }
                Console.WriteLine();
            }
        }
    }
}