namespace APATA_NEA_Project.Classes;

internal abstract class Pathfinding_Algorithms(Maze mazeParam)
{
    protected readonly Maze maze = mazeParam;

    protected readonly Color currentCellColour = Color.Orange;
    protected readonly Color visitedCellColour = Color.PaleVioletRed;
    private readonly Color shortestPathColour = Color.RebeccaPurple;

    public abstract Task FindShortestPath();

    protected abstract void InitialiseAlgorithm();

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

    protected async Task ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
    {
        Stack<Cell> shortestPath = new();

        while (cameFrom.ContainsKey(current))
        {
            shortestPath.Push(current);
            current = cameFrom[current];
        }

        Cell start = maze.Cells[0, 0];
        shortestPath.Push(start);

        foreach (Cell cell in shortestPath)
        {
            cell.PaintCell(shortestPathColour);
            await Task.Delay(0);
        }
    }  
}