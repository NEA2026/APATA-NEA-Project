namespace APATA_NEA_Project.Classes;

internal class Cell(Maze mazeParam, int row, int column)
{
    private readonly Maze maze = mazeParam;
    public readonly int Row = row;
    public readonly int Column = column;

    public int X;
    public int Y;

    public bool TopWall = true;
    public bool RightWall = true;
    public bool BottomWall = true;
    public bool LeftWall = true;

    public bool Visited = false;

    public void PaintCurrentCell(Graphics graphics, Color currentCellColour)
    {
        using Brush currentCellBrush = new SolidBrush(currentCellColour);
        graphics.FillRectangle(currentCellBrush, X + 1, Y + 1, (float)(maze.CellWidth - 1.5), (float)(maze.CellWidth - 1.5));
    }

    public void PaintVisitedCell(Graphics graphics, Color visitedCellColour)
    {
        using Brush visitedCellBrush = new SolidBrush(visitedCellColour);
        graphics.FillRectangle(visitedCellBrush, X + 1, Y + 1, (float)(maze.CellWidth - 1.5), (float)(maze.CellWidth - 1.5));

        int CellWidth = maze.CellWidth;
        using Pen visitedCellPen = new(visitedCellBrush, 1.5f);

        if (!TopWall)
        {
            graphics.DrawLine(visitedCellPen, X + 1, Y, X + CellWidth - 1, Y);
        }

        if (!RightWall)
        {
            graphics.DrawLine(visitedCellPen, X + CellWidth, Y + 1, X + CellWidth, Y + CellWidth - 1);
        }

        if (!BottomWall)
        {
            graphics.DrawLine(visitedCellPen, X + CellWidth - 1, Y + CellWidth, X + 1, Y + CellWidth);
        }

        if (!LeftWall)
        {
            graphics.DrawLine(visitedCellPen, X, Y + CellWidth - 1, X, Y + 1);
        }
    }
}