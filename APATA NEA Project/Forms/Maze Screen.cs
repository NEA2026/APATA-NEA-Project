using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms;

public partial class MazeScreen : Form
{
    public readonly Bitmap MazeBitmap;
    private readonly Maze maze;

    public MazeScreen(int rows, int columns, int generationDelay, int percentage)
    {
        InitializeComponent();
        this.DoubleBuffered = true;

        const int formWidth = 850;
        const int formHeight = 630;
        const int mazeSize = 520;
        const int defaultDpi = 96;

        double scaling = (double)DeviceDpi / defaultDpi;
        this.Width = (int)(formWidth * scaling);
        this.Height = (int)(formHeight * scaling);

        int scaledMazeSize = (int)(mazeSize * scaling);
        int padding = 1;
        this.MazeBitmap = new Bitmap(scaledMazeSize + padding, scaledMazeSize + padding);

        maze = new(this, rows, columns, generationDelay, percentage, scaledMazeSize);
    }

    private async void MazeScreen_Shown(object sender, EventArgs e)
    {
        await maze.GenerateMaze();

        //Dijkstras_Algorithm dijkstra = new(maze);
        //await dijkstra.FindShortestPath();

        A_Star_Search_Algorithm aStar = new(maze);
        await aStar.FindShortestPath();
    }

    private void MazeScreen_Paint(object sender, PaintEventArgs e)
    {
        int padding = 25;
        e.Graphics.DrawImage(MazeBitmap, padding, padding);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        StartScreen startScreen = new();
        Hide();
        startScreen.Show();
    }
}
