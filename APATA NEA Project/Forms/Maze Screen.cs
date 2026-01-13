using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms;

public partial class MazeScreen : Form
{
    public readonly Bitmap MazeBitmap;
    private Maze maze;
    private readonly int rows;
    private readonly int columns;
    private readonly int percentage;
    private readonly int scaledMazeSize;

    private CancellationTokenSource? tokenSource;

    private Dijkstras_Algorithm? dijkstra;
    private A_Star_Search_Algorithm? aStar;

    public MazeScreen(int rows, int columns, int percentage)
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

        MazeBitmap = new Bitmap(scaledMazeSize + 1, scaledMazeSize + 1);
        maze = new(this, rows, columns, percentage, scaledMazeSize);
        this.rows = rows;
        this.columns = columns;
        this.percentage = percentage;
        this.scaledMazeSize = scaledMazeSize;
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

    private async void chkGenerateMaze_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGenerateMaze.Checked && !maze.finished)
        {
            btnResetGeneration.Enabled = true;

            tokenSource = new();
            await maze.Generate(false, tokenSource.Token);
        }

        else if (!chkGenerateMaze.Checked && !maze.finished)
        {
            tokenSource?.Cancel();
        }

        if (maze.finished)
        {
            MazeFinished();
        }
    }

    private void tbGenerationDelay_Scroll(object sender, EventArgs e)
    {
        lblGenerationDelayValue.Text = tbGenerationDelay.Value.ToString() + " ms";
        maze.generationDelay = tbGenerationDelay.Value;
    }

    private async void btnResetGeneration_Click(object sender, EventArgs e)
    {
        chkGenerateMaze.Checked = false;
        btnResetGeneration.Enabled = false;

        tokenSource?.Cancel();
        await Task.Delay(50);

        maze = new(this, rows, columns, percentage, scaledMazeSize)
        {
            generationDelay = tbGenerationDelay.Value
        };

        chkGenerateMaze.Enabled = true;
        btnStepGeneration.Enabled = true;
        tbGenerationDelay.Enabled = true;
    }

    private async void btnStepGeneration_Click(object sender, EventArgs e)
    {
        if (!maze.finished)
        {
            chkGenerateMaze.Checked = false;
            btnStepGeneration.Enabled = false;
            btnResetGeneration.Enabled = false;

            tokenSource?.Cancel();
            await Task.Delay(50);

            tokenSource = new();
            await maze.Generate(true, tokenSource.Token);

            btnStepGeneration.Enabled = true;
        }

        else if (maze.finished)
        {
            MazeFinished();
        }

        btnResetGeneration.Enabled = true;
    }

    private void MazeFinished()
    {
        chkGenerateMaze.Checked = false;
        chkGenerateMaze.Enabled = false;
        btnStepGeneration.Enabled = false;
        tbGenerationDelay.Enabled = false;
        tokenSource = null;

        dijkstra = new(maze);
        aStar = new(maze);

        cboPathfindingAlgorithm.Enabled = true;
        chkSolveShortestPath.Enabled = true;
        tbPathfindingDelay.Enabled = true;
        btnStepPathfinding.Enabled = true;

        cboPathfindingAlgorithm.SelectedIndex = 0;
    }

    private async void chkSolveShortestPath_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSolveShortestPath.Checked && (!dijkstra!.finished && !aStar!.finished))
        {
            cboPathfindingAlgorithm.Enabled = false;
            btnResetPathfinding.Enabled = true;

            tokenSource = new();

            if (cboPathfindingAlgorithm.SelectedIndex == 0)
            {
                await dijkstra.FindShortestPath(false, tokenSource.Token);

            }

            else if (cboPathfindingAlgorithm.SelectedIndex == 1)
            {
                await aStar.FindShortestPath(false, tokenSource.Token);
            }
        }

        else if (!chkSolveShortestPath.Checked && (!dijkstra!.finished && !aStar!.finished))
        {
            tokenSource?.Cancel();
        }

        if (dijkstra!.finished || aStar!.finished)
        {
            PathfindingFinished();
        }
    }

    private void tbPathfindingDelay_Scroll(object sender, EventArgs e)
    {
        lblPathfindingDelayValue.Text = tbPathfindingDelay.Value.ToString() + " ms";

        if (cboPathfindingAlgorithm.SelectedIndex == 0)
        {
            dijkstra!.pathfindingDelay = tbPathfindingDelay.Value;
        }

        else if (cboPathfindingAlgorithm.SelectedIndex == 1)
        {
            aStar!.pathfindingDelay = tbPathfindingDelay.Value;
        }
    }

    private async void btnResetPathfinding_Click(object sender, EventArgs e)
    {
        chkSolveShortestPath.Checked = false;
        btnResetPathfinding.Enabled = false;

        tokenSource?.Cancel();
        await Task.Delay(100);

        dijkstra = new(maze)
        {
            pathfindingDelay = tbPathfindingDelay.Value
        };

        aStar = new(maze)
        {
            pathfindingDelay = tbPathfindingDelay.Value
        };

        chkSolveShortestPath.Enabled = true;
        btnStepPathfinding.Enabled = true;
        cboPathfindingAlgorithm.Enabled = true;
    }

    private async void btnStepPathfinding_Click(object sender, EventArgs e)
    {
        chkSolveShortestPath.Checked = false;

        if (!dijkstra!.finished && !aStar!.finished)
        {
            btnStepPathfinding.Enabled = false;
            btnResetPathfinding.Enabled = false;

            tokenSource?.Cancel();
            await Task.Delay(50);

            tokenSource = new();

            if (cboPathfindingAlgorithm.SelectedIndex == 0)
            {
                await dijkstra.FindShortestPath(true, tokenSource.Token);
            }

            if (cboPathfindingAlgorithm.SelectedIndex == 1)
            {
                await aStar.FindShortestPath(true, tokenSource.Token);
            }

            btnStepPathfinding.Enabled = true;
        }

        else if (dijkstra.finished || aStar.finished)
        {
            PathfindingFinished();
        }

        btnResetPathfinding.Enabled = true;
    }

    private void PathfindingFinished()
    {
        chkSolveShortestPath.Checked = false;
        chkSolveShortestPath.Enabled = false;
        btnStepPathfinding.Enabled = false;
        tbGenerationDelay.Enabled = false;
    }
}
