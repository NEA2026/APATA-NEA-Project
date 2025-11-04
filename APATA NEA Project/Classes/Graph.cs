namespace APATA_NEA_Project.Classes
{
    internal class Graph
    {
        private readonly int rows;
        private readonly int columns;
        public readonly int CellWidth;
        public readonly Node[,] Cells;

        public Graph(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            if (rows > columns)
            {
                CellWidth = 650 / rows;
            }

            else
            {
                CellWidth = 650 / columns;
            }

            Cells = new Node[rows, columns];
        }

        public void AddCells()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Node cell = new(this, row, column);
                    Cells[row, column] = cell;

                    int x = 25 + column * CellWidth;
                    int y = 25 + row * CellWidth;
                    cell.X = x;
                    cell.Y = y;
                }
            }
        }

        public void DrawCells(Graphics graphics)
        {
            using Pen wall = new(Color.Black, 1.5f);
            foreach (Node cell in Cells)
            {
                graphics.DrawRectangle(wall, cell.X, cell.Y, CellWidth, CellWidth);
            }
        }

        public void GenerateMaze(Graphics graphics)
        {
            Node current = Cells[0, 0];
            current.Visited = true;

            Stack<Node> cellStack = new();
            cellStack.Push(current);

            CreateStartAndEnd(current, graphics);

            while (cellStack.Count > 0)
            {
                current = cellStack.Pop();

                List<Node> unvisitedNeighbours = current.FindUnvisitedNeighbours(Cells, rows, columns);

                if (unvisitedNeighbours.Count > 0)
                {
                    cellStack.Push(current);

                    Random random = new();
                    int randomUnvisitedNeighbour = random.Next(0, unvisitedNeighbours.Count);
                    Node next = unvisitedNeighbours[randomUnvisitedNeighbour];

                    current.RemoveWalls(next, graphics);

                    next.Visited = true;
                    cellStack.Push(next);
                }
            }
        }

        private void CreateStartAndEnd(Node current, Graphics graphics)
        {
            using Pen removeWall = new(Color.White, 1.5f);
            current.TopWall = false;
            graphics.DrawLine(removeWall, current.X + 1, current.Y, current.X + CellWidth - 1, current.Y);

            Node end = Cells[rows - 1, columns - 1];
            end.BottomWall = false;
            graphics.DrawLine(removeWall, end.X + CellWidth - 1, end.Y + CellWidth, end.X + 1, end.Y + CellWidth);
        }

        public void RemoveDeadEnds(Graphics graphics)
        {
            List<Node> deadEnds = FindDeadEnds();
            Node[] deadEndsArray = deadEnds.ToArray();
            RemoveDeadEndWalls(deadEndsArray, graphics);
        }

        public void RemoveDeadEnds(Graphics graphics, double percentage)
        {
            List<Node> deadEnds = FindDeadEnds();
            double multiplier = percentage / 100;
            int deadEndsToRemove = (int)Math.Round(multiplier * deadEnds.Count, MidpointRounding.AwayFromZero);

            Node[] randomDeadEnds = new Node[deadEndsToRemove];
            Random random = new();

            for (int i = 0; i < deadEndsToRemove; i++)
            {
                int randomDeadEnd = random.Next(0, deadEnds.Count);
                randomDeadEnds[i] = deadEnds[randomDeadEnd];
                deadEnds.Remove(deadEnds[randomDeadEnd]);
            }
            
            RemoveDeadEndWalls(randomDeadEnds, graphics);
        }

        private List<Node> FindDeadEnds()
        {
            List<Node> deadEnds = new();
            
            foreach (Node cell in Cells)
            {
                int[] walls =
                {
                    Convert.ToInt32(cell.TopWall),
                    Convert.ToInt32(cell.RightWall),
                    Convert.ToInt32(cell.BottomWall),
                    Convert.ToInt32(cell.LeftWall)
                };

                if (walls.Sum() == 3)
                {
                    deadEnds.Add(cell);
                }
            }

            return deadEnds;
        }

        private void RemoveDeadEndWalls(Node[] deadEnds, Graphics graphics)
        {
            using Pen path = new(Color.White, 1.5f);

            foreach (Node cell in deadEnds)
            {
                int x = cell.X;
                int y = cell.Y;

                bool removed = false;
                while (!removed)
                {
                    Random random = new();
                    int randomWall = random.Next(0, 3);

                    switch (randomWall)
                    {
                        case 0:
                            if (cell.TopWall && cell.Row != 0)
                            {
                                cell.TopWall = false;
                                removed = true;
                                graphics.DrawLine(path, x + 1, y, x + CellWidth - 1, y);
                            }
                            break;

                        case 1:
                            if (cell.RightWall && cell.Column != columns - 1)
                            {
                                cell.RightWall = false;
                                removed = true;
                                graphics.DrawLine(path, x + CellWidth, y + 1, x + CellWidth, y + CellWidth - 1);
                            }
                            break;

                        case 2:
                            if (cell.BottomWall && cell.Row != rows - 1)
                            {
                                cell.BottomWall = false;
                                removed = true;
                                graphics.DrawLine(path, x + CellWidth - 1, y + CellWidth, x + 1, y + CellWidth);
                            }
                            break;

                        case 3:
                            if (cell.LeftWall && cell.Column != 0)
                            {
                                cell.LeftWall = false;
                                removed = true;
                                graphics.DrawLine(path, x, y + CellWidth - 1, x, y + 1);
                            }
                            break;
                    }
                }
            }
        }
    }
}
