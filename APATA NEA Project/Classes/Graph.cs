namespace APATA_NEA_Project.Classes
{
    internal class Graph(int rows, int columns)
    {
        public const int cellWidth = 25;
        private readonly Cell[,] nodes = new Cell[rows, columns];

        public void AddCells()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Cell cell = new(row, column);
                    nodes[row, column] = cell;

                    int x = 25 + column * cellWidth;
                    int y = 25 + row * cellWidth;
                    cell.x = x;
                    cell.y = y;
                }
            }
        }

        public void AddWalls(Graphics graphics)
        {
            using Pen wall = new(Color.Black, 1.5f);
            foreach (Cell cell in nodes)
            {
                int x = cell.x;
                int y = cell.y;

                if (cell.topWall)
                {
                    graphics.DrawLine(wall, x, y, x + cellWidth, y);
                }

                if (cell.rightWall)
                {
                    graphics.DrawLine(wall, x + cellWidth, y, x + cellWidth, y + cellWidth);
                }

                if (cell.bottomWall)
                {
                    graphics.DrawLine(wall, x + cellWidth, y + cellWidth, x, y + cellWidth);
                }

                if (cell.leftWall)
                {
                    graphics.DrawLine(wall, x, y + cellWidth, x, y);
                }
            }
        }

        public void GenerateMaze(Graphics graphics)
        {
            Cell current = nodes[0, 0];
            current.visited = true;

            Stack<Cell> cellStack = new();
            cellStack.Push(current);

            CreateStartAndEnd(current, graphics);

            while (cellStack.Count > 0)
            {
                current = cellStack.Pop();

                List<Cell> unvisitedNeighbours = current.FindUnvisitedNeighbours(nodes, rows, columns);

                if (unvisitedNeighbours.Count > 0)
                {
                    cellStack.Push(current);

                    Random random = new();
                    int randomUnvisitedNeighbour = random.Next(0, unvisitedNeighbours.Count);
                    Cell next = unvisitedNeighbours[randomUnvisitedNeighbour];

                    current.RemoveWalls(next, graphics);

                    next.visited = true;
                    cellStack.Push(next);
                }
            }
        }

        private void CreateStartAndEnd(Cell current, Graphics graphics)
        {
            using Pen removeWall = new(Color.White, 1.5f);
            current.topWall = false;
            graphics.DrawLine(removeWall, current.x + 1, current.y, current.x + cellWidth - 1, current.y);

            Cell end = nodes[rows - 1, columns - 1];
            end.bottomWall = false;
            graphics.DrawLine(removeWall, end.x + cellWidth - 1, end.y + cellWidth, end.x + 1, end.y + cellWidth);
        }
    }
}
