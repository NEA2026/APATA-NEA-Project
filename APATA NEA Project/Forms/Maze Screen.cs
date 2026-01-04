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
        if (chkGenerateMaze.Checked)
        {
            btnReset.Enabled = true;

            tokenSource = new();
            await maze.Generate(false, tokenSource.Token);

            if (maze.finished)
            {
                chkGenerateMaze.Checked = false;
                chkGenerateMaze.Enabled = false;
                btnStep.Enabled = false;
                tbGenerationDelay.Enabled = false;

                chkSolveShortestPath.Enabled = true;
                tokenSource = null;

                dijkstra = new(maze);
                aStar = new(maze);
            }
        }

        else if (!chkGenerateMaze.Checked)
        {
            tokenSource?.Cancel();
        }
    }

    private void tbGenerationDelay_Scroll(object sender, EventArgs e)
    {
        lblGenerationDelayValue.Text = tbGenerationDelay.Value.ToString() + " ms";
        maze.generationDelay = tbGenerationDelay.Value;
    }

    private async void btnReset_Click(object sender, EventArgs e)
    {
        chkGenerateMaze.Checked = false;

        tokenSource?.Cancel();
        await Task.Delay(50);

        maze = new(this, rows, columns, percentage, scaledMazeSize)
        {
            generationDelay = tbGenerationDelay.Value
        };

        btnReset.Enabled = false;

        chkGenerateMaze.Enabled = true;
        btnStep.Enabled = true;
        tbGenerationDelay.Enabled = true;
    }

    private async void btnStep_Click(object sender, EventArgs e)
    {
        chkGenerateMaze.Checked = false;

        btnStep.Enabled = false;
        btnReset.Enabled = false;

        tokenSource?.Cancel();
        await Task.Delay(50);

        tokenSource = new();
        await maze.Generate(true, tokenSource.Token);

        btnStep.Enabled = true;
        btnReset.Enabled = true;

        if (maze.finished)
        {
            chkGenerateMaze.Enabled = false;
            btnStep.Enabled = false;
            tbGenerationDelay.Enabled = false;

            chkSolveShortestPath.Enabled = true;
            tokenSource = null;

            dijkstra = new(maze);
            aStar = new(maze);
        }
    }

    private async void chkSolveShortestPath_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSolveShortestPath.Checked)
        {
            cboPathfindingAlgorithm.Enabled = false;

            tokenSource = new();

            if (cboPathfindingAlgorithm.SelectedIndex == 0 && !dijkstra!.finished)
            {
                await dijkstra.FindShortestPath(false, tokenSource.Token);

                if (dijkstra.finished)
                {
                    chkSolveShortestPath.Checked = false;
                    chkSolveShortestPath.Enabled = false;
                    tbGenerationDelay.Enabled = false;
                }
            }

            else if (cboPathfindingAlgorithm.SelectedIndex == 1 && !aStar!.finished)
            {
                await aStar.FindShortestPath(false, tokenSource.Token);

                if (aStar.finished)
                {
                    chkSolveShortestPath.Checked = false;
                    chkSolveShortestPath.Enabled = false;
                    tbGenerationDelay.Enabled = false;
                }
            }
        }

        else if (!chkSolveShortestPath.Checked)
        {
            tokenSource?.Cancel();
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
}
