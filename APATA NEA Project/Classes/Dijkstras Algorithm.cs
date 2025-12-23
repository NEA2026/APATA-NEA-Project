namespace APATA_NEA_Project.Classes
{
    internal class Dijkstras_Algorithm(Maze maze) : Pathfinding_Algorithms(maze)
    {
        public override void FindShortestPath(Graphics graphics)
        {
            Dictionary<Cell, int> distance = new();
            Dictionary<Cell, Cell> previous = new();

            List<Cell> visitedCells = new();
            IndexedPriorityQueue priorityQueue = new();

            Cell source = maze.Cells[0, 0];
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
                using Brush currentCell = new SolidBrush(Color.Orange);

                Cell current = priorityQueue.ExtractMin();
                current.Visited = true;
                visitedCells.Add(current);
                graphics.FillRectangle(currentCell, current.X + 1, current.Y + 1, (float)(maze.CellWidth - 1.5), (float)(maze.CellWidth - 1.5));
                //Thread.Sleep(20);

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
            }

            Stack<Cell> shortestPath = new();
            Cell target = maze.Cells[maze.Columns - 1, maze.Rows - 1];

            while (previous.ContainsKey(target))
            {
                shortestPath.Push(target);
                target = previous[target]!;
            }

            foreach (Cell cell in shortestPath)
            {
                using Brush path = new SolidBrush(Color.Blue);
                graphics.FillRectangle(path, cell.X + 1, cell.Y + 1, (float)(maze.CellWidth - 1.5), (float)(maze.CellWidth - 1.5));
            }
        }   
    }
}
