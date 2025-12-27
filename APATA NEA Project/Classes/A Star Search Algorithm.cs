using System.Collections.Generic;

namespace APATA_NEA_Project.Classes;

internal class A_Star_Search_Algorithm(Maze maze) : Pathfinding_Algorithms(maze)
{
    public override void FindShortestPath(Graphics graphics)
    {
        MinHeapPriorityQueue openSet = new();
        List<Cell> visitedCells = new();

        Dictionary<Cell, Cell> cameFrom = new();
        Dictionary<Cell, int> gScore = new();
        Dictionary<Cell, int> fScore = new();

        Cell start = maze.Cells[0, 0];
        Cell goal = maze.Cells[maze.Columns - 1, maze.Rows - 1];

        start.Visited = false;
        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);
        openSet.Insert(start, 0);

        foreach (Cell cell in maze.Cells)
        {
            if (cell != start)
            {
                cell.Visited = false;
                gScore[cell] = int.MaxValue;
                fScore[cell] = int.MaxValue;
            }
        }

        while (openSet.Count != 0)
        {
            using Brush currentCell = new SolidBrush(Color.DarkRed);

            Cell current = openSet.ExtractMin();
            current.Visited = true;
            visitedCells.Add(current);

            current.PaintCurrentCell(graphics, currentCellColour);
            //Thread.Sleep(5);

            if (current == goal)
            {
                ReconstructPath(graphics, cameFrom, current);
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
                    fScore[neighbour] = tentativeGScore + Heuristic(neighbour, goal);

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

            current.PaintVisitedCell(graphics, visitedCellColour);
        }
    }

    private int Heuristic(Cell current, Cell goal)
    {
        int manhattanDistance = Math.Abs(current.Column - goal.Column) + Math.Abs(current.Row - goal.Row);

        return manhattanDistance;
    }
}
