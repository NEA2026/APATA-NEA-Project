namespace APATA_NEA_Project.Classes;

internal class Dijkstras_Algorithm(Maze maze) : Pathfinding_Algorithms(maze)
{
    public override void FindShortestPath(Graphics graphics)
    {
        MinHeapPriorityQueue priorityQueue = new();
        List<Cell> visitedCells = new();

        Dictionary<Cell, Cell> previous = new();
        Dictionary<Cell, int> distance = new();
        
        Cell source = maze.Cells[0, 0];
        Cell target = maze.Cells[maze.Columns - 1, maze.Rows - 1];

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
}
