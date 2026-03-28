namespace APATA_NEA_Project.Classes;

/// <summary>
/// This subclass, which inherits from Pathfinding Algorithms, contains the code for the A* Search Algorithm, 
/// encapsulated in the overridden abstract methods FindShortestPath and InitialiseAlgorithm. 
/// This subclass also contains a method for the Manhattan Distance heuristic.
/// </summary>
internal class A_Star_Search_Algorithm : Pathfinding_Algorithms
{
    /// <summary>
    /// The binary min-heap priority queue which is used in the A* Search algorithm.
    /// </summary>
    private BinaryMinHeapPriorityQueue openSet = new();

    /// <summary>
    /// A list of all the cells that have been visited by the A* Search algorithm.
    /// </summary>
    private List<Cell> visitedCells = new();

    /// <summary>
    /// A dictionary that maps each cell with its predecessor in the A* Search algorithm.
    /// </summary>
    private Dictionary<Cell, Cell> cameFrom = new();

    /// <summary>
    /// A dictionary that maps each cell with the currently known cost (distance) of the cheapest path from the start to that cell.
    /// </summary>
    private Dictionary<Cell, int> gScore = new();

    /// <summary>
    /// A dictionary that maps each cell with the current best guess as to how cheap a path could be from the maze’s start to its exit, going through that cell. 
    /// For node n, fScore[n] = gScore[n] + h(n).
    /// </summary>
    private Dictionary<Cell, int> fScore = new();

    /// <summary>
    /// The cell that the A* Search algorithm starts from. 
    /// This is always the top-left cell in my program, as that is where the maze always starts from.
    /// </summary>
    private readonly Cell start;

    /// <summary>
    /// The cell that the A* Search algorithm is trying to find the shortest path to. 
    /// This is always the bottom-right cell in my program, as that is where the exit of the maze is always located.
    /// </summary>
    private readonly Cell goal;

    public A_Star_Search_Algorithm(Maze maze) : base(maze)
    {
        start = maze.Cells[0, 0];
        goal = maze.Cells[maze.Rows - 1, maze.Columns - 1];

        InitialiseAlgorithm();
    }

    /// <summary>
    /// Solves the shortest path from the maze’s start to its exit using the A* Search algorithm which is encapsulated inside this overridden method. 
    /// </summary>
    /// <param name="stepping"> A Boolean flag that determines whether the algorithm stops after one iteration/step (when true) or runs continuously (when false). </param>
    /// <param name="token"> A cancellation token that, if a cancellation is requested, stops the algorithm after completing the current iteration. </param>
    public override async Task FindShortestPath(bool stepping, CancellationToken token)
    {
        while (openSet.Count != 0)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            Cell current = openSet.ExtractMin();
            current.Visited = true;
            visitedCells.Add(current);

            await current.PaintCell(currentCellColour, pathfindingDelay);

            if (current == goal)
            {
                await ReconstructPath(cameFrom, current);
                break;
            }

            List<Cell> neighbours = FindNeighbours(current);

            foreach (Cell neighbour in neighbours)
            {
                int tentativeGScore = gScore[current] + 1;

                if (tentativeGScore < gScore[neighbour])
                {
                    cameFrom[neighbour] = current;
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = tentativeGScore + Heuristic(neighbour);

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Insert(neighbour, fScore[neighbour]);
                    }

                    else
                    {
                        openSet.DecreaseKey(neighbour, fScore[neighbour]);
                    }
                }
            }

            await current.PaintCellWithWalls(visitedCellColour, pathfindingDelay);

            if (stepping)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Initialises the A* Search algorithm. 
    /// This method utilises polymorphism by overriding an abstract base method, ensuring that the algorithm-specific initialisation is properly implemented.
    /// </summary>
    protected override void InitialiseAlgorithm()
    {
        start.Visited = false;
        gScore[start] = 0;
        fScore[start] = Heuristic(start);
        openSet.Insert(start, 0);

        foreach (Cell cell in maze.Cells)
        {
            ResetColour(cell);

            if (cell != start)
            {
                cell.Visited = false;
                gScore[cell] = int.MaxValue;
                fScore[cell] = int.MaxValue;
            }
        }
    }

    /// <summary>
    /// The Manhattan Distance approximation heuristic used in the A* Search algorithm in my program.
    /// </summary>
    private int Heuristic(Cell current)
    {
        int manhattanDistance = Math.Abs(current.Column - goal.Column) + Math.Abs(current.Row - goal.Row);

        return manhattanDistance;
    }
}
