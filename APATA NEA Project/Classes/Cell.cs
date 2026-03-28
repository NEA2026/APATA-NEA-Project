namespace APATA_NEA_Project.Classes;

/// <summary>
/// Identifies a cell in the maze, storing integer values for each cells’ row and column in the maze, 
/// as well as having expression bodied properties to calculate and return a cell’s x and y coordinates on the Maze Screen. 
/// The Cell class also contains methods to colour the cell, and to colour the cell including its walls. 
/// These methods are necessary for the visualisation of the randomised DFS algorithm, as well as Dijkstra’s algorithm and the A* Search algorithm.
/// </summary>
internal class Cell(Maze maze, int row, int column)
{
    /// <summary>
    /// Stores a Maze instantiation for the maze the cell is in.
    /// </summary>
    private readonly Maze maze = maze;

    /// <summary>
    /// Stores the cell’s row number in the maze. The row number is zero-indexed (begins at 0).
    /// </summary>
    public readonly int Row = row;

    /// <summary>
    /// Stores the cell’s column number in the maze. The column number is zero-indexed (begins at 0).
    /// </summary>
    public readonly int Column = column;

    /// <summary>
    /// An expression bodied property that when called calculates and returns the cell’s x coordinate in the maze. 
    /// The x coordinate returned will be the x coordinate of the cells’ top left corner.
    /// </summary>
    public int X => Column * maze.CellWidth;

    /// <summary>
    /// An expression bodied property that when called calculates and returns the cell’s y coordinate in the maze. 
    /// The y coordinate returned will be the y coordinate of the cells’ top left corner.
    /// </summary>
    public int Y => Row * maze.CellWidth;

    /// <summary>
    /// Stores a Boolean value that indicates whether the cell has a top wall or not.
    /// </summary>
    public bool TopWall { get; set; } = true;

    /// <summary>
    /// Stores a Boolean value that indicates whether the cell has a right wall or not.
    /// </summary>
    public bool RightWall { get; set; } = true;

    /// <summary>
    /// Stores a Boolean value that indicates whether the cell has a bottom wall or not.
    /// </summary>
    public bool BottomWall { get; set; } = true;

    /// <summary>
    /// Stores a Boolean value that indicates whether the cell has a left wall or not.
    /// </summary>
    public bool LeftWall { get; set; } = true;

    /// <summary>
    /// Stores a Boolean value that indicates whether the cell has been visited or not.
    /// </summary>
    public bool Visited { get; set; } = false;

    /// <summary>
    /// Takes a colour as a parameter and fills the cell with that colour. 
    /// The value of the delay parameter controls the animation speed of the algorithm that calls this method.
    /// </summary>
    public async Task PaintCell(Color colour, int delay)
    {
        using Graphics graphics = Graphics.FromImage(maze.MazeScreen.MazeBitmap);
        
        using Brush currentCellBrush = new SolidBrush(colour);
        graphics.FillRectangle(currentCellBrush, X + 1, Y + 1, maze.CellWidth - 1, maze.CellWidth - 1);

        maze.MazeScreen.Invalidate();
        await Task.Delay(delay);
    }

    /// <summary>
    /// Takes a colour a colour as a parameter and fills the cell, including its walls, with that colour. 
    /// The value of the delay parameter controls the animation speed of the algorithm that calls this method.
    /// </summary>
    public async Task PaintCellWithWalls(Color colour, int delay)
    {
        using Graphics graphics = Graphics.FromImage(maze.MazeScreen.MazeBitmap);

        using Brush brush = new SolidBrush(colour);
        graphics.FillRectangle(brush, X + 1, Y + 1, maze.CellWidth - 1, maze.CellWidth - 1);

        using Pen path = new(brush, 1);
        int CellWidth = maze.CellWidth;

        if (!TopWall)
        {
            graphics.DrawLine(path, X + 1, Y, X + CellWidth - 1, Y);
        }

        if (!RightWall)
        {
            graphics.DrawLine(path, X + CellWidth, Y + 1, X + CellWidth, Y + CellWidth - 1);
        }

        if (!BottomWall)
        {
            graphics.DrawLine(path, X + CellWidth - 1, Y + CellWidth, X + 1, Y + CellWidth);
        }

        if (!LeftWall)
        {
            graphics.DrawLine(path, X, Y + CellWidth - 1, X, Y + 1);
        }

        maze.MazeScreen.Invalidate();
        await Task.Delay(delay);
    }
}