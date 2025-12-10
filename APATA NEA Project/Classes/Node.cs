namespace APATA_NEA_Project.Classes
{
    internal class Node(Graph maze, int Row, int Column)
    {
        private readonly Graph maze = maze;
        public readonly int Row = Row;
        public readonly int Column = Column;

        public int X;
        public int Y;

        public bool TopWall = true;
        public bool RightWall = true;
        public bool BottomWall = true;
        public bool LeftWall = true;

        public bool Visited = false;

        public List<Node> FindUnvisitedNeighbours(Node[,] Cells, int rows, int columns)
        {
            List<Node> unvisitedNeighbours = new();

            if (Row != 0)
            {
                Node top = Cells[Row - 1, Column];

                if (!top.Visited)
                {
                    unvisitedNeighbours.Add(top);
                }
            }

            if (Column != columns - 1)
            {
                Node right = Cells[Row, Column + 1];

                if (!right.Visited)
                {
                    unvisitedNeighbours.Add(right);
                }
            }

            if (Row != rows - 1)
            {
                Node bottom = Cells[Row + 1, Column];

                if (!bottom.Visited)
                {
                    unvisitedNeighbours.Add(bottom);
                }
            }

            if (Column != 0)
            {
                Node left = Cells[Row, Column - 1];

                if (!left.Visited)
                {
                    unvisitedNeighbours.Add(left);
                }
            }

            return unvisitedNeighbours;
        }

        public void RemoveWalls(Node next, Graphics graphics)
        {
            int rowDifference = Row - next.Row;

            if (rowDifference == 1)
            {
                TopWall = false;
                next.BottomWall = false;
            }

            else if (rowDifference == -1)
            {
                BottomWall = false;
                next.TopWall = false;
            }

            int columnDifference = Column - next.Column;

            if (columnDifference == 1)
            {
                LeftWall = false;
                next.RightWall = false;
            }

            else if (columnDifference == -1)
            {
                RightWall = false;
                next.LeftWall = false;
            }
            
            int CellWidth = maze.CellWidth;
            using Pen path = new(Color.LightGreen, 1.5f);
       
            if (!TopWall && !next.BottomWall)
            {
                graphics.DrawLine(path, X + 1, Y, X + CellWidth - 1, Y);
            }

            if (!RightWall && !next.LeftWall)
            {
                graphics.DrawLine(path, X + CellWidth, Y + 1, X + CellWidth, Y + CellWidth - 1);
            }

            if (!BottomWall && !next.TopWall)
            {
                graphics.DrawLine(path, X + CellWidth - 1, Y + CellWidth, X + 1, Y + CellWidth);
            }

            if (!LeftWall && !next.RightWall)
            {
                graphics.DrawLine(path, X, Y + CellWidth - 1, X, Y + 1);
            }
        }
    }
}