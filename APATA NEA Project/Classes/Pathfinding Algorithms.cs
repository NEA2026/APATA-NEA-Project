namespace APATA_NEA_Project.Classes;

/// <summary>
/// The parent class that each pathfinding algorithm inherits from. 
/// This class encapsulates methods that have common functionality in pathfinding algorithms such as FindNeighbours and ReconstructPath. 
/// In my program specifically, there is also a ResetColour method for when the user wants to rerun a pathfinding algorithm on the same maze.
/// </summary>
/// <param name="maze"></param>
internal abstract class Pathfinding_Algorithms(Maze maze)
{
    /// <summary>
    /// Stores a reference to the Maze instantiation of the maze.
    /// </summary>
    protected readonly Maze maze = maze;

    /// <summary>
    /// The delay in milliseconds that determines the animation speed of the selected pathfinding algorithm on the Maze Screen.
    /// </summary>
    public int pathfindingDelay { protected get; set; }

    /// <summary>
    /// A Boolean flag that indicates when the pathfinding algorithm has finished solving the shortest path from the maze’s start to its exit.
    /// </summary>
    public bool pathfindingFinished { get; set; } = false;

    /// <summary>
    /// The colour of cells, on the Maze Screen, which have not been visited by the pathfinding algorithm the user has selected.
    /// </summary>
    private readonly Color unvisitedCellColour = Color.LightGreen;

    /// <summary>
    /// The colour of the current cell, on the Maze Screen, being processed by the pathfinding algorithm the user has selected.
    /// </summary>
    protected readonly Color currentCellColour = Color.Orange;

    /// <summary>
    /// The colour of cells, on the Maze Screen, which have been visited by the pathfinding algorithm that the user has selected.
    /// </summary>
    protected readonly Color visitedCellColour = Color.PaleVioletRed;

    /// <summary>
    /// The colour of cells, on the Maze Screen, which indicate the shortest path from the maze’s start to its exit.
    /// </summary>
    private readonly Color shortestPathColour = Color.LightBlue;

    /// <summary>
    /// This method is marked as abstract so that that any classes that inherit from this abstract base class must override this method, 
    /// implementing its pathfinding algorithm.
    /// Each implementation of this method must take parameters for a Boolean flag called stepping and a cancellation token called token.
    /// </summary>
    /// <param name="stepping"> A Boolean flag that determines whether the algorithm stops after one iteration/step (when true) or runs continuously (when false). </param>
    /// <param name="token"> A cancellation token that, if a cancellation is requested, stops the algorithm after completing the current iteration. </param>
    public abstract Task FindShortestPath(bool stepping, CancellationToken token);

    /// <summary>
    /// This method is marked as abstract so that that any classes that inherit from this abstract base class must override this method, 
    /// implementing the initialisation of its pathfinding algorithm.
    /// </summary>
    protected abstract void InitialiseAlgorithm();

    /// <summary>
    /// Takes a cell as a parameter and fills that cell (including walls) with the colour that is assigned to the field unvisitedCellColour.
    /// </summary>
    protected void ResetColour(Cell cell)
    {
        _ = cell.PaintCellWithWalls(unvisitedCellColour, 0);
    }

    /// <summary>
    /// Finds the neighbouring cells to the top, right, bottom or left of the current cell and returns a list of them.
    /// </summary>
    protected List<Cell> FindNeighbours(Cell cell)
    {
        List<Cell> neighbours = new();

        // If there is a cell to the top of the cell (passed in as a parameter), and there is no wall between the cells, add that cell as a neighbour.
        if (cell.Row != 0 && !cell.TopWall)
        {
            Cell top = maze.Cells[cell.Row - 1, cell.Column];
            neighbours.Add(top);
        }

        // If there is a cell to the right of the cell (passed in as a parameter), and there is no wall between the cells, add that cell as a neighbour.
        if (cell.Column != maze.Columns - 1 && !cell.RightWall)
        {
            Cell right = maze.Cells[cell.Row, cell.Column + 1];
            neighbours.Add(right);
        }

        // If there is a cell to the bottom of the cell (passed in as a parameter), and there is no wall between the cells, add that cell as a neighbour.
        if (cell.Row != maze.Rows - 1 && !cell.BottomWall)
        {
            Cell bottom = maze.Cells[cell.Row + 1, cell.Column];
            neighbours.Add(bottom);
        }

        // If there is a cell to the left of the cell (passed in as a parameter), and there is no wall between the cells, add that cell as a neighbour.
        if (cell.Column != 0 && !cell.LeftWall)
        {
            Cell left = maze.Cells[cell.Row, cell.Column - 1];
            neighbours.Add(left);
        }

        return neighbours;
    }

    /// <summary>
    /// Reconstructs the shortest path from the maze’s start to its exit by backtracking from the goal cell via the cameFrom dictionary,
    /// storing the path in a stack to reverse the order, 
    /// and colouring each cell with the colour assigned to the field shortestPathColour, to visually display the path found by the algorithm.
    /// </summary>
    protected async Task ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
    {
        Stack<Cell> shortestPath = new();
        shortestPath.Push(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            shortestPath.Push(current);
        }

        foreach (Cell cell in shortestPath)
        {
            await cell.PaintCellWithWalls(shortestPathColour, pathfindingDelay);
        }

        pathfindingFinished = true;
    }  
}