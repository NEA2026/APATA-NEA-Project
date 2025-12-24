namespace APATA_NEA_Project.Classes;

internal class Maze
{
    public readonly int Rows;
    public readonly int Columns;
    public readonly int CellWidth;
    public readonly Cell[,] Cells;

    public Maze(int rows, int columns, double scaling)
    {
        this.Rows = rows;
        this.Columns = columns;

        double guiSize = 520 * scaling;

        if (rows >= columns)
        {
            CellWidth = (int)guiSize / rows;
        }

        else
        {
            CellWidth = (int)guiSize / columns;
        }

        Cells = new Cell[rows, columns];
    }

    public void AddCells()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Cell cell = new(this, row, column);
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
        foreach (Cell cell in Cells)
        {
            graphics.DrawRectangle(wall, cell.X, cell.Y, CellWidth, CellWidth);
        }
    }

    public void GenerateMaze(int animationDelay, Graphics graphics)
    {
        Cell current = Cells[0, 0];
        current.Visited = true;

        Stack<Cell> cellStack = new();
        cellStack.Push(current);

        CreateStartAndExit(current, graphics);

        while (cellStack.Count != 0)
        {
            using Brush currentCell = new SolidBrush(Color.Orange);
            using Brush visitedCell = new SolidBrush(Color.LightGreen);

            current = cellStack.Pop();

            graphics.FillRectangle(currentCell, current.X + 1, current.Y + 1, (float)(CellWidth - 1.5), (float)(CellWidth - 1.5));
            Thread.Sleep(animationDelay);

            List<Cell> unvisitedNeighbours = current.FindUnvisitedNeighbours();

            if (unvisitedNeighbours.Count > 0)
            {
                cellStack.Push(current);

                Random random = new();
                int randomUnvisitedNeighbour = random.Next(0, unvisitedNeighbours.Count);
                Cell next = unvisitedNeighbours[randomUnvisitedNeighbour];

                current.RemoveWalls(next, graphics);

                next.Visited = true;
                cellStack.Push(next);

                graphics.FillRectangle(visitedCell, current.X + 1, current.Y + 1, (float)(CellWidth - 1.5), (float)(CellWidth - 1.5));
                graphics.FillRectangle(currentCell, next.X + 1, next.Y + 1, (float)(CellWidth - 1.5), (float)(CellWidth - 1.5));
            }

            else if (unvisitedNeighbours.Count == 0)
            {
                graphics.FillRectangle(visitedCell, current.X + 1, current.Y + 1, (float)(CellWidth - 1.5), (float)(CellWidth - 1.5));
            }
        }
    }

    private void CreateStartAndExit(Cell current, Graphics graphics)
    {
        using Pen removeWall = new(Color.White, 1.5f);
        current.TopWall = false;
        graphics.DrawLine(removeWall, current.X + 1, current.Y, current.X + CellWidth - 1, current.Y);

        Cell end = Cells[Rows - 1, Columns - 1];
        end.BottomWall = false;
        graphics.DrawLine(removeWall, end.X + CellWidth - 1, end.Y + CellWidth, end.X + 1, end.Y + CellWidth);
    }

    public void RemoveDeadEnds(Graphics graphics)
    {
        List<Cell> deadEnds = FindDeadEnds();
        Cell[] deadEndsArray = deadEnds.ToArray();
        RemoveWallsFromDeadEnds(deadEndsArray, graphics);
    }

    public void RemoveDeadEnds(double percentage, Graphics graphics)
    {
        List<Cell> deadEnds = FindDeadEnds();
        double multiplier = percentage / 100;
        int deadEndsToRemove = (int)Math.Round(multiplier * deadEnds.Count, MidpointRounding.AwayFromZero);

        Cell[] randomDeadEnds = new Cell[deadEndsToRemove];
        Random random = new();

        for (int i = 0; i < deadEndsToRemove; i++)
        {
            int randomDeadEnd = random.Next(0, deadEnds.Count);
            randomDeadEnds[i] = deadEnds[randomDeadEnd];
            deadEnds.Remove(deadEnds[randomDeadEnd]);
        }
        
        RemoveWallsFromDeadEnds(randomDeadEnds, graphics);
    }

    private List<Cell> FindDeadEnds()
    {
        List<Cell> deadEnds = new();
        
        foreach (Cell cell in Cells)
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

    private void RemoveWallsFromDeadEnds(Cell[] deadEnds, Graphics graphics)
    {
        using Pen path = new(Color.LightGreen, 1.5f);

        foreach (Cell cell in deadEnds)
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
                        if (cell.RightWall && cell.Column != Columns - 1)
                        {
                            cell.RightWall = false;
                            removed = true;
                            graphics.DrawLine(path, x + CellWidth, y + 1, x + CellWidth, y + CellWidth - 1);
                        }
                        break;

                    case 2:
                        if (cell.BottomWall && cell.Row != Rows - 1)
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
