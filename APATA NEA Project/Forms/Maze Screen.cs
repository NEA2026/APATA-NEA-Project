using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms;

public partial class MazeScreen : Form
{
    private readonly Maze maze;
    private readonly int percentage;
    private readonly int animationDelay;
   
    public MazeScreen(int rows, int columns, int percentage, int animationDelay)
    {
        InitializeComponent();
        const int guiWidth = 850;
        const int guiHeight = 630;

        const int defaultDpi = 96;

        double scaling = (double)DeviceDpi / defaultDpi;

        this.Width = (int)(guiWidth * scaling);
        this.Height = (int)(guiHeight * scaling);

        this.percentage = percentage;
        this.animationDelay = animationDelay;

        maze = new(rows, columns, scaling);
    }

    private void Maze_Paint(object sender, PaintEventArgs e)
    {
        maze.AddCells();
        maze.DrawCells(e.Graphics);
        maze.GenerateMaze(e.Graphics, animationDelay);

        if (percentage == 100)
        {
            maze.RemoveDeadEnds(e.Graphics);
        }

        else if (percentage > 0 && percentage < 100)
        {
            maze.RemoveDeadEnds(e.Graphics, percentage);
        }

        //Dijkstras_Algorithm dijkstra = new(maze);
        //dijkstra.FindShortestPath(e.Graphics);

        A_Star_Search_Algorithm aStar = new(maze);
        aStar.FindShortestPath(e.Graphics);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        StartScreen startScreen = new();
        Hide();
        startScreen.Show();
    }
}
