using APATA_NEA_Project.Forms;

namespace APATA_NEA_Project.Classes;

internal class Maze
{
    public readonly MazeScreen MazeScreen;
    public readonly int Rows;
    public readonly int Columns;
    public readonly int CellWidth;
    public readonly Cell[,] Cells;
    private readonly int percentage;

    public int generationDelay;
    public bool finished = false;
    private Cell current;
    private Stack<Cell> cellStack;

    private readonly Color unvisitedCellColour = Color.White;
    private readonly Color currentCellColour = Color.Orange;
    private readonly Color visitedCellColour = Color.LightGreen;
    
    public Maze(MazeScreen mazeScreen, int rows, int columns, int percentage, int scaledMazeSize)
    {
        this.MazeScreen = mazeScreen;
        this.Rows = rows;
        this.Columns = columns;
        
        if (rows >= columns)
        {
            CellWidth = scaledMazeSize / rows;
        }

        else
        {
            CellWidth = scaledMazeSize / columns;
        }

        Cells = new Cell[rows, columns];

        this.percentage = percentage;

        AddAndDrawCells();
        CreateStartAndExit();

        current = Cells[0, 0];
        cellStack = new Stack<Cell>();
        current.Visited = true;
        cellStack.Push(current);
    }

    public async Task Generate(bool stepping, CancellationToken token)
    {
        await RunRandomisedDFS(stepping, token);

        if (percentage == 100 && finished)
        {
            RemoveDeadEnds();
        }

        else if (finished)
        {
            RemoveDeadEnds(percentage);
        }
    }

    private void AddAndDrawCells()
    {
        using Graphics graphics = Graphics.FromImage(MazeScreen.MazeBitmap);
        using Pen wall = new(Color.Black, 1);

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Cell cell = new(this, row, column);
                Cells[row, column] = cell;

                graphics.DrawRectangle(wall, cell.X, cell.Y, CellWidth, CellWidth);
                Task task = cell.PaintCurrentCell(unvisitedCellColour, 0);
            }
        }
    }

    private void CreateStartAndExit()
    {
        using Graphics graphics = Graphics.FromImage(MazeScreen.MazeBitmap);
        using Pen removeWall = new(unvisitedCellColour, 1);

        Cell start = Cells[0, 0];
        start.TopWall = false;
        graphics.DrawLine(removeWall, start.X + 1, start.Y, start.X + CellWidth - 1, start.Y);

        Cell exit = Cells[Rows - 1, Columns - 1];
        exit.BottomWall = false;
        graphics.DrawLine(removeWall, exit.X + CellWidth - 1, exit.Y + CellWidth, exit.X + 1, exit.Y + CellWidth);
    }

    private async Task RunRandomisedDFS(bool stepping, CancellationToken token)
    {
        while (cellStack.Count != 0)
        {
            if (token.IsCancellationRequested) 
            {
                return;
            }

            current = cellStack.Pop();
            await current.PaintCurrentCell(currentCellColour, generationDelay);

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

            await current.PaintCell(visitedCellColour, generationDelay);

            if (stepping)
            {
                return;
            }
        }

        finished = true;
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

    private static void RemoveWalls(Cell current, Cell next)
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

    private void RemoveDeadEnds()
    {
        List<Cell> deadEnds = FindDeadEnds();
        Cell[] deadEndsArray = deadEnds.ToArray();
        RemoveWallsFromDeadEnds(deadEndsArray);
    }

    private void RemoveDeadEnds(double percentage)
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
        
        RemoveWallsFromDeadEnds(randomDeadEnds);
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

    private void RemoveWallsFromDeadEnds(Cell[] deadEnds)
    {
        using Graphics graphics = Graphics.FromImage(MazeScreen.MazeBitmap);
        using Pen removeWall = new(visitedCellColour, 1);

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
                            graphics.DrawLine(removeWall, x + 1, y, x + CellWidth - 1, y);
                        }
                        break;

                    case 1:
                        if (cell.RightWall && cell.Column != Columns - 1)
                        {
                            cell.RightWall = false;
                            removed = true;
                            graphics.DrawLine(removeWall, x + CellWidth, y + 1, x + CellWidth, y + CellWidth - 1);
                        }
                        break;

                    case 2:
                        if (cell.BottomWall && cell.Row != Rows - 1)
                        {
                            cell.BottomWall = false;
                            removed = true;
                            graphics.DrawLine(removeWall, x + CellWidth - 1, y + CellWidth, x + 1, y + CellWidth);
                        }
                        break;

                    case 3:
                        if (cell.LeftWall && cell.Column != 0)
                        {
                            cell.LeftWall = false;
                            removed = true;
                            graphics.DrawLine(removeWall, x, y + CellWidth - 1, x, y + 1);
                        }
                        break;
                }
            }
        }

        MazeScreen.Invalidate();
    }
}
