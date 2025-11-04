using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms
{
    public partial class MazeScreen : Form
    {
        private readonly int rows;
        private readonly int columns;
        private readonly int percentage;
        private readonly Graph maze;

        public MazeScreen(int rows, int columns, int percentage)
        {
            InitializeComponent();
            this.rows = rows;
            this.columns = columns;
            this.percentage = percentage;
            maze = new(rows, columns);
        }

        private void Maze_Paint(object sender, PaintEventArgs e)
        {
            maze.AddCells();
            maze.DrawCells(e.Graphics);
            maze.GenerateMaze(e.Graphics);

            if (percentage == 100)
            {
                maze.RemoveDeadEnds(e.Graphics);
            }

            else if (percentage > 0 && percentage < 100)
            {
                maze.RemoveDeadEnds(e.Graphics, percentage);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StartScreen startScreen = new();
            Hide();
            startScreen.Show();
        }
    }
}
