namespace APATA_NEA_Project.Classes;

internal class Maze
{
    public readonly int Rows;
    public readonly int Columns;
    public readonly int CellWidth;
    public readonly Cell[,] Cells;

    private readonly Color currentCellColour = Color.Orange;
    private readonly Color visitedCellColour = Color.LightGreen;

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

    public void GenerateMaze(Graphics graphics, int animationDelay)
    {
        Cell current = Cells[0, 0];
        current.Visited = true;

        Stack<Cell> cellStack = new();
        cellStack.Push(current);

        CreateStartAndExit(graphics, current);

        while (cellStack.Count != 0)
        {
            current = cellStack.Pop();

            current.PaintCurrentCell(graphics, currentCellColour);
            Thread.Sleep(animationDelay);

            List<Cell> unvisitedNeighbours = FindUnvisitedNeighbours(current);

            if (unvisitedNeighbours.Count > 0)
            {
                cellStack.Push(current);

                Random random = new();
                int randomUnvisitedNeighbour = random.Next(0, unvisitedNeighbours.Count);
                Cell next = unvisitedNeighbours[randomUnvisitedNeighbour];

                RemoveWalls(current, next);

                next.Visited = true;
                cellStack.Push(next);
            }

            current.PaintVisitedCell(graphics, visitedCellColour);
        }
    }

    private void CreateStartAndExit(Graphics graphics, Cell current)
    {
        using Pen removeWall = new(Color.White, 1.5f);

        current.TopWall = false;
        graphics.DrawLine(removeWall, current.X + 1, current.Y, current.X + CellWidth - 1, current.Y);

        Cell end = Cells[Rows - 1, Columns - 1];
        end.BottomWall = false;
        graphics.DrawLine(removeWall, end.X + CellWidth - 1, end.Y + CellWidth, end.X + 1, end.Y + CellWidth);
    }

    private List<Cell> FindUnvisitedNeighbours(Cell current)
    {
        List<Cell> unvisitedNeighbours = new();

        if (current.Row != 0)
        {
            Cell top = Cells[current.Row - 1, current.Column];

            if (!top.Visited)
            {
                unvisitedNeighbours.Add(top);
            }
        }

        if (current.Column != Columns - 1)
        {
            Cell right = Cells[current.Row, current.Column + 1];

            if (!right.Visited)
            {
                unvisitedNeighbours.Add(right);
            }
        }

        if (current.Row != Rows - 1)
        {
            Cell bottom = Cells[current.Row + 1, current.Column];

            if (!bottom.Visited)
            {
                unvisitedNeighbours.Add(bottom);
            }
        }

        if (current.Column != 0)
        {
            Cell left = Cells[current.Row, current.Column - 1];

            if (!left.Visited)
            {
                unvisitedNeighbours.Add(left);
            }
        }

        return unvisitedNeighbours;
    }

    private void RemoveWalls(Cell current, Cell next)
    {
        int rowDifference = current.Row - next.Row;

        if (rowDifference == 1)
        {
            current.TopWall = false;
            next.BottomWall = false;
        }

        else if (rowDifference == -1)
        {
            current.BottomWall = false;
            next.TopWall = false;
        }

        int columnDifference = current.Column - next.Column;

        if (columnDifference == 1)
        {
            current.LeftWall = false;
            next.RightWall = false;
        }

        else if (columnDifference == -1)
        {
            current.RightWall = false;
            next.LeftWall = false;
        }
    }

    public void RemoveDeadEnds(Graphics graphics)
    {
        List<Cell> deadEnds = FindDeadEnds();
        Cell[] deadEndsArray = deadEnds.ToArray();
        RemoveWallsFromDeadEnds(graphics, deadEndsArray);
    }

    public void RemoveDeadEnds(Graphics graphics, double percentage)
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
        
        RemoveWallsFromDeadEnds(graphics, randomDeadEnds);
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

    private void RemoveWallsFromDeadEnds(Graphics graphics, Cell[] deadEnds)
    {
        using Pen path = new(visitedCellColour, 1.5f);

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
