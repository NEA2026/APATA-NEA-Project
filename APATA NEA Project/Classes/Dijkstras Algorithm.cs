namespace APATA_NEA_Project.Classes;

internal class Dijkstras_Algorithm(Maze maze) : Pathfinding_Algorithms(maze)
{
    private MinHeapPriorityQueue priorityQueue = new();
    private List<Cell> visitedCells = new();

    private Dictionary<Cell, Cell> previous = new();
    private Dictionary<Cell, int> distance = new();

    private readonly Cell source = maze.Cells[0, 0];
    private readonly Cell target = maze.Cells[maze.Columns - 1, maze.Rows - 1];

    public override void FindShortestPath(Graphics graphics)
    {
        Initialise();

        while (priorityQueue.Count != 0)
        {
            Cell current = priorityQueue.ExtractMin();
            current.Visited = true;
            visitedCells.Add(current);

            current.PaintCurrentCell(graphics, currentCellColour);
            //Thread.Sleep(5);

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

            current.PaintVisitedCell(graphics, visitedCellColour);
        }

        ReconstructPath(graphics, previous, target);
    }

    protected override void Initialise()
    {
        source.Visited = false;
        distance[source] = 0;
        priorityQueue.Insert(source, 0);

        foreach (Cell cell in maze.Cells)
        {
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
