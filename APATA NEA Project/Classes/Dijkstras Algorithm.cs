namespace APATA_NEA_Project.Classes;

internal class Dijkstras_Algorithm : Pathfinding_Algorithms
{
    private MinHeapPriorityQueue priorityQueue = new();
    private List<Cell> visitedCells = new();

    private Dictionary<Cell, Cell> previous = new();
    private Dictionary<Cell, int> distance = new();

    private readonly Cell source;
    private readonly Cell target;

    public Dijkstras_Algorithm(Maze maze) : base(maze)
    {
        source = maze.Cells[0, 0];
        target = maze.Cells[maze.Rows - 1, maze.Columns - 1];

        InitialiseAlgorithm();
    }

    public override async Task FindShortestPath(bool stepping, CancellationToken token)
    {
        while (priorityQueue.Count != 0)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            Cell current = priorityQueue.ExtractMin();
            current.Visited = true;
            visitedCells.Add(current);

            await current.PaintCell(currentCellColour, pathfindingDelay);

            List<Cell> neighbours = FindNeighbours(current);

            foreach (Cell neighbour in neighbours)
            {
                int alternateDistance = distance[current] + 1;

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
