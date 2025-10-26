using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms
{
    public partial class MazeScreen : Form
    {
        private readonly int rows;
        private readonly int columns;

        public MazeScreen(int rows, int columns)
        {
            InitializeComponent();
            this.rows = rows;
            this.columns = columns;
        }

        private void Maze_Paint(object sender, PaintEventArgs e)
        {
            Graph graph = new(rows, columns);
            graph.AddCells();
            graph.AddWalls(e.Graphics);
            graph.GenerateMaze(e.Graphics);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StartScreen startScreen = new();
            Hide();
            startScreen.Show();
        }
    }
}
