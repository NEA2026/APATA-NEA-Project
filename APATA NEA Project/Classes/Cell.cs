namespace APATA_NEA_Project.Classes;

internal class Cell(Maze mazeParam, int row, int column)
{
    private readonly Maze maze = mazeParam;
    public readonly int Row = row;
    public readonly int Column = column;

    public int X => Column * maze.CellWidth;
    public int Y => Row * maze.CellWidth;

    public bool TopWall = true;
    public bool RightWall = true;
    public bool BottomWall = true;
    public bool LeftWall = true;

    public bool Visited = false;

    public void PaintCurrentCell(Color colour)
    {
        using Graphics graphics = Graphics.FromImage(maze.MazeScreen.MazeBitmap);
        
        using Brush currentCellBrush = new SolidBrush(colour);
        graphics.FillRectangle(currentCellBrush, X + 1, Y + 1, maze.CellWidth - 1, maze.CellWidth - 1);

        maze.MazeScreen.Invalidate();
    }

    public void PaintCell(Color colour)
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
    }
}