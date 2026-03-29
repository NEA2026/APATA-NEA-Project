namespace APATA_NEA_Project.Classes;

/// <summary>
/// This subclass, which inherits from Pathfinding Algorithms, contains the code for Dijkstra’s Algorithm, 
/// encapsulated in the overridden abstract methods FindShortestPath and InitialiseAlgorithm.
/// </summary>
internal class Dijkstras_Algorithm : Pathfinding_Algorithms
{
    /// <summary>
    /// The binary min-heap priority queue which is used in Dijkstra’s algorithm.
    /// </summary>
    private BinaryMinHeapPriorityQueue priorityQueue = new();

    /// <summary>
    /// A list of all the cells that have been visited by Dijkstra’s algorithm.
    /// </summary>
    private List<Cell> visitedCells = new();

    /// <summary>
    /// A dictionary that maps each cell with its predecessor in Dijkstra’s algorithm.
    /// </summary>
    private Dictionary<Cell, Cell> previous = new();

    /// <summary>
    /// A dictionary that maps each cell with its associated distance (as used in Dijkstra’s algorithm).
    /// </summary>
    private Dictionary<Cell, int> distance = new();

    /// <summary>
    /// The cell that Dijkstra’s algorithm starts from. 
    /// This is always the top-left cell in my program, as that is where the maze always starts from.
    /// </summary>
    private readonly Cell source;

    /// <summary>
    /// The cell that Dijkstra’s algorithm is trying to find the shortest path to. 
    /// This is always the bottom-right cell in my program, as that is where the exit of the maze is always located.
    /// </summary>
    private readonly Cell target;

    public Dijkstras_Algorithm(Maze maze) : base(maze)
    {
        source = maze.Cells[0, 0];
        target = maze.Cells[maze.Rows - 1, maze.Columns - 1];

        InitialiseAlgorithm();
    }

    /// <summary>
    /// Solves the shortest path from the maze’s start to its exit using Dijkstra’s algorithm which is encapsulated inside this overridden method.
    /// </summary>
    /// <param name="stepping"> A Boolean flag that determines whether the algorithm stops after one iteration/step (when true) or runs continuously (when false). </param>
    /// <param name="token"> A cancellation token that, if a cancellation is requested, stops the algorithm after completing the current iteration. </param>
    /// <returns></returns>
    public override async Task FindShortestPath(bool stepping, CancellationToken token)
    {
        while (priorityQueue.Count != 0)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            // Remove and return the cell with the shortest distance from the source.
            Cell current = priorityQueue.ExtractMin();

            current.Visited = true;
            visitedCells.Add(current);

            await current.PaintCell(currentCellColour, pathfindingDelay);

            List<Cell> neighbours = FindNeighbours(current);

            foreach (Cell neighbour in neighbours)
            {
                // The distance to a neighbouring cell through the current cell is always the distance to current cell + 1.
                int alternateDistance = distance[current] + 1;

                // If this path to the neighbouring cell is shorter than any previous one, record it!
                if (alternateDistance < distance[neighbour])
                {
                    previous[neighbour] = current;
                    distance[neighbour] = alternateDistance;
                    priorityQueue.DecreaseKey(neighbour, alternateDistance);
                }
            }

            await current.PaintCellWithWalls(visitedCellColour, pathfindingDelay);

            if (stepping)
            {
                return;
            }
        }

        await ReconstructPath(previous, target);
    }

    /// <summary>
    /// Initialises Dijkstra’s algorithm. 
    /// This method utilises polymorphism by overriding an abstract base method, ensuring that the algorithm-specific initialisation is properly implemented.
    /// </summary>
    protected override void InitialiseAlgorithm()
    {
        source.Visited = false;
        distance[source] = 0;
        priorityQueue.Insert(source, 0);

        foreach (Cell cell in maze.Cells)
        {
            ResetColour(cell);

            if (cell != source)
            {
                cell.Visited = false;
                previous[cell] = null!;
                distance[cell] = int.MaxValue;
                priorityQueue.Insert(cell, int.MaxValue);
            }
        }
    }
}