namespace APATA_NEA_Project.Classes
{
    internal abstract class Pathfinding_Algorithms(Maze maze)
    {
        protected readonly Maze maze = maze;

        public abstract void FindShortestPath(Graphics graphics);

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
    }
}
