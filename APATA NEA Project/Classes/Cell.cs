using System.Drawing.Text;

namespace APATA_NEA_Project.Classes
{
    internal class Cell(int row, int column)
    {
        private readonly int row = row;
        private readonly int column = column;
        public int x;
        public int y;

        public bool topWall = true;
        public bool rightWall = true;
        public bool bottomWall = true;
        public bool leftWall = true;

        public bool visited = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization")]

        public List<Cell> FindUnvisitedNeighbours(Cell[,] nodes, int rows, int columns)
        {
            List<Cell> unvisitedNeighbours = new();

            if (row > 0)
            {
                Cell top = nodes[row - 1, column];

                if (!top.visited)
                {
                    unvisitedNeighbours.Add(top);
                }
            }

            if (column < columns - 1)
            {
                Cell right = nodes[row, column + 1];

                if (!right.visited)
                {
                    unvisitedNeighbours.Add(right);
                }
            }

            if (row < rows - 1)
            {
                Cell bottom = nodes[row + 1, column];

                if (!bottom.visited)
                {
                    unvisitedNeighbours.Add(bottom);
                }
            }

            if (column > 0)
            {
                Cell left = nodes[row, column - 1];

                if (!left.visited)
                {
                    unvisitedNeighbours.Add(left);
                }
            }

            return unvisitedNeighbours;
        }

        public void RemoveWalls(Cell next, Graphics graphics)
        {
            int rowDifference = row - next.row;

            if (rowDifference == 1)
            {
                topWall = false;
                next.bottomWall = false;
            }

            else if (rowDifference == -1)
            {
                bottomWall = false;
                next.topWall = false;
            }

            int columnDifference = column - next.column;

            if (columnDifference == 1)
            {
                leftWall = false;
                next.rightWall = false;
            }

            else if (columnDifference == -1)
            {
                rightWall = false;
                next.leftWall = false;
            }

            using Pen path = new(Color.White, 1.5f);
            const int cellWidth = Graph.cellWidth;

            if (!topWall && !next.bottomWall)
            {
                graphics.DrawLine(path, x + 1, y, x + cellWidth - 1, y);
            }

            if (!rightWall && !next.leftWall)
            {
                graphics.DrawLine(path, x + cellWidth, y + 1, x + cellWidth, y + cellWidth - 1);
            }

            if (!bottomWall && !next.topWall)
            {
                graphics.DrawLine(path, x + cellWidth - 1, y + cellWidth, x + 1, y + cellWidth);
            }

            if (!leftWall && !next.rightWall)
            {
                graphics.DrawLine(path, x, y + cellWidth - 1, x, y + 1);
            }
        }
    }
}