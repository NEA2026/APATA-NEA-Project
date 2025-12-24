namespace APATA_NEA_Project.Classes;

internal abstract class Pathfinding_Algorithms(Maze maze)
{
    protected readonly Maze maze = maze;

    public abstract void FindShortestPath(Graphics graphics);

    protected List<Cell> FindNeighbours(Cell cell)
    {
        List<Cell> neighbours = new();

        if (cell.Row != 0 && !cell.TopWall)
        {
            Cell top = maze.Cells[cell.Row - 1, cell.Column];
            neighbours.Add(top);
        }

        if (cell.Column != maze.Columns - 1 && !cell.RightWall)
        {
            Cell right = maze.Cells[cell.Row, cell.Column + 1];
            neighbours.Add(right);
        }

        if (cell.Row != maze.Rows - 1 && !cell.BottomWall)
        {
            Cell bottom = maze.Cells[cell.Row + 1, cell.Column];
            neighbours.Add(bottom);
        }

        if (cell.Column != 0 && !cell.LeftWall)
        {
            Cell left = maze.Cells[cell.Row, cell.Column - 1];
            neighbours.Add(left);
        }

        return neighbours;
    }

    protected void ReconstructPath(Graphics graphics, Dictionary<Cell, Cell> cameFrom, Cell current)
    {
        Stack<Cell> shortestPath = new();

        while (cameFrom.ContainsKey(current))
        {
            shortestPath.Push(current);
            current = cameFrom[current];
        }

        foreach (Cell cell in shortestPath)
        {
            using Brush path = new SolidBrush(Color.Blue);
            graphics.FillRectangle(path, cell.X + 1, cell.Y + 1, (float)(maze.CellWidth - 1.5), (float)(maze.CellWidth - 1.5));
        }
    }
}