namespace APATA_NEA_Project.Classes;

internal class A_Star_Search_Algorithm : Pathfinding_Algorithms
{
    private MinHeapPriorityQueue openSet = new();
    private List<Cell> visitedCells = new();

    private Dictionary<Cell, Cell> cameFrom = new();
    private Dictionary<Cell, int> gScore = new();
    private Dictionary<Cell, int> fScore = new();

    private readonly Cell start;
    private readonly Cell goal;

    public A_Star_Search_Algorithm(Maze maze) : base(maze)
    {
        start = maze.Cells[0, 0];
        goal = maze.Cells[maze.Rows - 1, maze.Columns - 1];

        InitialiseAlgorithm();
    }
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

    private int Heuristic(Cell current)
    {
        int manhattanDistance = Math.Abs(current.Column - goal.Column) + Math.Abs(current.Row - goal.Row);

        return manhattanDistance;
    }
}
