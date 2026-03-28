using APATA_NEA_Project.Forms;

namespace APATA_NEA_Project.Classes;

/// <summary>
/// Stores information about a maze, such as the number of rows in the maze, 
/// the number of columns in the maze, the width of every cell in the maze, 
/// the dynamically generated 2D array of cell instantiations (which stores every cell instantiation in the maze) 
/// and all the algorithms needed to generate the maze, such as the Randomised DFS algorithm.
/// </summary>
internal class Maze
{
    /// <summary>
    /// The MazeScreen form instantiation the maze is displayed on.
    /// </summary>
    public readonly MazeScreen MazeScreen;

    /// <summary>
    /// The number of rows in the maze, which the user inputted as the maze’s height on the Start Screen.
    /// </summary>
    public readonly int Rows;

    /// <summary>
    /// The number of columns in the maze, which the user inputted as the maze’s width on the Start Screen.
    /// </summary>
    public readonly int Columns;

    /// <summary>
    /// The width of the maze’s cells, which determines the size of each cell.
    /// </summary>
    public readonly int CellWidth;

    /// <summary>
    /// A 2D array of cell instantiations which stores a cell instantiation for every cell in the maze.
    /// </summary>
    public readonly Cell[,] Cells;

    /// <summary>
    /// The percentage of dead ends to remove from the maze, which the user inputted on the Start Screen.
    /// </summary>
    private readonly int percentage;

    /// <summary>
    /// The delay in milliseconds that determines the generation speed of the maze.
    /// </summary>
    public int generationDelay { get; set; }

    /// <summary>
    /// A Boolean flag that indicates when the randomised DFS algorithm has finished generating the maze.
    /// </summary>
    public bool generationFinished { get; private set; } = false; 

    /// <summary>
    /// The current cell that is being processed in the randomised DFS algorithm.
    /// </summary>
    private Cell current = null!;

    /// <summary>
    /// The stack of cell instantiations which is used in the randomised DFS algorithm.
    /// </summary>
    private Stack<Cell> cellStack = null!;

    /// <summary>
    /// The colour of cells, on the Maze Screen, which have not been visited by the randomised DFS algorithm.
    /// </summary>
    private readonly Color unvisitedCellColour = Color.White;

    /// <summary>
    /// The colour of the current cell, on the Maze Screen, being processed by the randomised DFS algorithm.
    /// </summary>
    private readonly Color currentCellColour = Color.Orange;

    /// <summary>
    /// The colour of cells, on the Maze Screen, which have been visited by the randomised DFS algorithm.
    /// </summary>
    private readonly Color visitedCellColour = Color.LightGreen;

    /// <summary>
    /// Stores information about a maze, such as the number of rows in the maze,
    /// the number of columns in the maze, the width of every cell in the maze,
    /// the dynamically generated 2D array of cell instantiations (which stores every cell instantiation in the maze)
    /// and all the algorithms needed to generate the maze, such as the Randomised DFS algorithm.
    /// </summary>
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
        InitialiseRandomisedDFS();
    }

    /// <summary>
    /// Generates the maze, by calling RunRandomisedDFS and calling the appropriate dead end removal algorithm. 
    /// </summary>
    /// <param name="stepping"> A Boolean flag which is passed into the RunRandomisedDFS method. 
    /// It determines whether the Randomised DFS algorithm runs continuously or executes one iteration (step) at a time. </param>
    /// <param name="token"> A cancellation token which is passed into the RunRandomisedDFS method. 
    /// It signals when the algorithm’s execution has been paused by the user and should stop executing. </param>
    /// <returns></returns>
    public async Task Generate(bool stepping, CancellationToken token)
    {
        bool finishedDFS = await RunRandomisedDFS(stepping, token);

        if (!finishedDFS)
        {
            return;
        }

        if (percentage == 100)
        {
            RemoveDeadEnds();
        }

        else if (percentage != 0)
        {
            RemoveDeadEnds(percentage);
        }

        generationFinished = true;
    }

    /// <summary>
    /// Dynamically populates the 2D array called Cells with a cell instantiation for each index in Cells, 
    /// based on the values for the number of columns and rows in the maze that the user inputted as width and height on the Start Screen. 
    /// Each cell is drawn onto the bitmap of the maze, and this bitmap is what is displayed on the Maze Screen.
    /// </summary>
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
                _ = cell.PaintCell(unvisitedCellColour, 0);
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

    private void InitialiseRandomisedDFS()
    {
        current = Cells[0, 0];
        cellStack = new Stack<Cell>();
        current.Visited = true;
        cellStack.Push(current);
    }

    /// <summary>
    /// The encapsulated randomised depth first search (DFS) maze-generation algorithm. 
    /// This algorithm generates a maze by randomly choosing an unvisited neighbouring cell of the current cell, 
    /// removing the wall between them, and then setting that neighbouring cell as the current cell. 
    /// This process repeats until every cell in the maze has been visited.
    /// </summary>
    /// <param name="stepping"> A Boolean flag that determines whether the algorithm stops after one iteration/step (when true) or runs continuously (when false). </param>
    /// <param name="token"> A cancellation token that, if a cancellation is requested, stops the algorithm after completing the current iteration. </param>
    /// <returns> Returns false if the Randomised DFS algorithm is requested to cancel or stepping.
    /// Returns true when the algorithm has finished generating the maze. </returns>
    private async Task<bool> RunRandomisedDFS(bool stepping, CancellationToken token)
    {
        while (cellStack.Count != 0)
        {
            if (token.IsCancellationRequested)
            {
                return false;
            }

            current = cellStack.Pop();
            await current.PaintCell(currentCellColour, generationDelay);

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

            await current.PaintCellWithWalls(visitedCellColour, generationDelay);

            if (stepping)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Finds any unvisited neighbouring cells to the top, right, bottom or left of the current cell and returns a list of them.
    /// </summary>
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

    /// <summary>
    /// Identifies the two walls that must be removed to create a path between the current cell and the next cell to be visited, 
    /// then sets the corresponding Boolean values to false for both cells.
    /// </summary>
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

    /// <summary>
    /// Called if the user inputted to remove all (a percentage of 100%) the dead ends from the maze, on the Start Screen. 
    /// This will create a plethora of solutions to the maze.
    /// </summary>
    private void RemoveDeadEnds()
    {
        List<Cell> deadEnds = FindDeadEnds();
        Cell[] deadEndsArray = deadEnds.ToArray();
        RemoveWallsFromDeadEnds(deadEndsArray);
    }

    /// <summary>
    /// Called if the user inputted to remove a percentage (greater than 0 and less than 100) of dead ends from the maze, on the Start Screen. 
    /// This will create many solutions to the maze.
    /// </summary>
    private void RemoveDeadEnds(double percentage)
    {
        List<Cell> deadEnds = FindDeadEnds();
        double multiplier = percentage / 100;
        int numOfDeadEndsToRemove = (int)Math.Round(multiplier * deadEnds.Count, MidpointRounding.AwayFromZero);

        Cell[] randomDeadEnds = new Cell[numOfDeadEndsToRemove];
        Random random = new();

        for (int i = 0; i < numOfDeadEndsToRemove; i++)
        {
            int randomDeadEnd = random.Next(0, deadEnds.Count);
            randomDeadEnds[i] = deadEnds[randomDeadEnd];
            deadEnds.Remove(deadEnds[randomDeadEnd]);
        }

        RemoveWallsFromDeadEnds(randomDeadEnds);
    }

    /// <summary>
    /// Finds all the dead ends in the maze and returns a list of them.
    /// </summary>
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

    /// <summary>
    /// Iterates through each dead end in the parameter deadEndsToRemove, 
    /// randomly chooses a wall to remove, sets the wall’s Boolean value to false and graphically removes it from the maze.
    /// </summary>
    private void RemoveWallsFromDeadEnds(Cell[] deadEndsToRemove)
    {
        using Graphics graphics = Graphics.FromImage(MazeScreen.MazeBitmap);
        using Pen removeWall = new(visitedCellColour, 1);
        Random random = new();

        foreach (Cell cell in deadEndsToRemove)
        {
            int x = cell.X;
            int y = cell.Y;

            bool removed = false;
            while (!removed)
            {
                int randomWall = random.Next(0, 3);

                switch (randomWall)
                {
                    case 0:
                        if (cell.TopWall && cell.Row != 0)
                        {
                            Cell top = Cells[cell.Row - 1, cell.Column];
                            RemoveWalls(cell, top);
                            graphics.DrawLine(removeWall, x + 1, y, x + CellWidth - 1, y);
                            removed = true;
                        }
                        break;

                    case 1:
                        if (cell.RightWall && cell.Column != Columns - 1)
                        {
                            Cell right = Cells[cell.Row, cell.Column + 1];
                            RemoveWalls(cell, right);
                            graphics.DrawLine(removeWall, x + CellWidth, y + 1, x + CellWidth, y + CellWidth - 1);
                            removed = true;
                        }
                        break;

                    case 2:
                        if (cell.BottomWall && cell.Row != Rows - 1)
                        {
                            Cell bottom = Cells[cell.Row + 1, cell.Column];
                            RemoveWalls(cell, bottom);
                            graphics.DrawLine(removeWall, x + CellWidth - 1, y + CellWidth, x + 1, y + CellWidth);
                            removed = true;
                        }
                        break;

                    case 3:
                        if (cell.LeftWall && cell.Column != 0)
                        {
                            Cell left = Cells[cell.Row, cell.Column - 1];
                            RemoveWalls(cell, left);
                            graphics.DrawLine(removeWall, x, y + CellWidth - 1, x, y + 1);
                            removed = true;
                        }
                        break;
                }
            }
        }

        MazeScreen.Invalidate();
    }
}
