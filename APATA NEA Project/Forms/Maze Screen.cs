using APATA_NEA_Project.Classes;

namespace APATA_NEA_Project.Forms;

public partial class MazeScreen : Form
{
    /// <summary>
    /// The bitmap that the maze is drawn and rendered to. 
    /// This is what is drawn on the form.
    /// </summary>
    public readonly Bitmap MazeBitmap;

    /// <summary>
    /// Stores a reference to the Maze instantiation of the maze.
    /// </summary>
    private Maze maze;

    /// <summary>
    /// The number of rows in the maze, which the user inputted as the maze’s height on the Start Screen.
    /// </summary>
    private readonly int rows;

    /// <summary>
    /// The number of columns in the maze, which the user inputted as the maze’s width on the Start Screen.
    /// </summary>
    private readonly int columns;

    /// <summary>
    /// The percentage of dead ends to remove from the maze, which the user inputted on the Start Screen.
    /// </summary>
    private readonly int percentage;

    /// <summary>
    /// Maximum sixe of the maze (in pixels).
    /// </summary>
    private readonly int scaledMazeSize;

    /// <summary>
    /// Signals to a cancellation token that it should be cancelled.
    /// </summary>
    private CancellationTokenSource? tokenSource;

    /// <summary>
    /// The instantiation of Dijkstra's algorithm used in pathfinding.
    /// </summary>
    private Dijkstras_Algorithm dijkstra = null!;

    /// <summary>
    /// The instantiation of the A* Search algorithm used in pathfinding.
    /// </summary>
    private A_Star_Search_Algorithm aStar = null!;

    public MazeScreen(int rows, int columns, int percentage)
    {
        InitializeComponent();

        // Makes the form redraw its surface using a secondary buffer to prevent flicker.
        this.DoubleBuffered = true;

        // Sets the default sizes of the form, and the maximum size of the maze (in pixels).
        const int formWidth = 850;
        const int formHeight = 630;
        const int mazeSize = 520;

        // The default DPI for a 100% display scale on a Windows device.
        const int defaultDpi = 96;

        // Changes the forms size based on the users display scale in their Windows settings.
        double scaling = (double)DeviceDpi / defaultDpi;
        this.Width = (int)(formWidth * scaling);
        this.Height = (int)(formHeight * scaling);

        // Changes the maze's maximum size (in pixels) based on the users display scale in their Windows settings.
        int scaledMazeSize = (int)(mazeSize * scaling);

        // Intialises the fields in this class.
        MazeBitmap = new Bitmap(scaledMazeSize + 1, scaledMazeSize + 1);
        maze = new(this, rows, columns, percentage, scaledMazeSize);
        this.rows = rows;
        this.columns = columns;
        this.percentage = percentage;
        this.scaledMazeSize = scaledMazeSize;
    }

    /// <summary>
    /// Draws the bitmap of the maze displayed on the form everytime the paint event is triggered.
    /// </summary>
    private void MazeScreen_Paint(object sender, PaintEventArgs e)
    {
        int padding = 22;
        e.Graphics.DrawImage(MazeBitmap, padding, padding);
    }

    /// <summary>
    /// Takes the user back to the Start Screen.
    /// </summary>
    private void btnBack_Click(object sender, EventArgs e)
    {
        StartScreen startScreen = new();
        Hide();
        startScreen.Show();
    }

    /// <summary>
    /// Generates the maze when the checkbox is checked and pauses/cancels the generation when unchecked.
    /// Also runs the method MazeFinished when the maze has finished generating.
    /// </summary>
    private async void chkGenerateMaze_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGenerateMaze.Checked && !maze.generationFinished)
        {
            btnResetGeneration.Enabled = true;

            tokenSource = new();
            await maze.Generate(false, tokenSource.Token);
        }

        else if (!chkGenerateMaze.Checked && !maze.generationFinished)
        {
            tokenSource?.Cancel();
        }

        if (maze.generationFinished)
        {
            MazeFinished();
        }
    }

    /// <summary>
    /// Sets the maze generation delay to the value of the generation delay trackbar.
    /// Also displays the value of the trackbar in the generation delay label that appears next to the trackbar.
    /// </summary>
    private void tbGenerationDelay_Scroll(object sender, EventArgs e)
    {
        lblGenerationDelayValue.Text = tbGenerationDelay.Value.ToString() + " ms";
        maze.generationDelay = tbGenerationDelay.Value;
    }

    /// <summary>
    /// Resets the maze bitmap back to a grid of cells, so another maze can be generated.
    /// </summary>
    private async void btnResetGeneration_Click(object sender, EventArgs e)
    {
        DisablePathfindingControls();
        chkGenerateMaze.Checked = false;
        btnResetGeneration.Enabled = false;

        tokenSource?.Cancel();

        // Allows the form some time to finish processing the current iteration of the Randomised DFS algorithm.
        await Task.Delay(50);

        maze = new(this, rows, columns, percentage, scaledMazeSize)
        {
            generationDelay = tbGenerationDelay.Value
        };

        chkGenerateMaze.Enabled = true;
        btnStepGeneration.Enabled = true;
        tbGenerationDelay.Enabled = true;
    }

    /// <summary>
    /// Disables all the pathfinding controls in the form's control panel.
    /// </summary>
    private void DisablePathfindingControls()
    {
        cboPathfindingAlgorithm.Enabled = false;
        chkSolveShortestPath.Enabled = false;
        btnResetPathfinding.Enabled = false;
        btnStepPathfinding.Enabled = false;
        tbPathfindingDelay.Enabled = false;
    }

/// <summary>
/// Allows the user to step through the Randomised DFS maze generation algorithm one iteration (step) at a time.
/// </summary>
    private async void btnStepGeneration_Click(object sender, EventArgs e)
    {
        if (!maze.generationFinished)
        {
            chkGenerateMaze.Checked = false;
            btnStepGeneration.Enabled = false;
            btnResetGeneration.Enabled = false;

            tokenSource?.Cancel();

            // Allows the form some time to finish processing the current iteration of the Randomised DFS algorithm.
            await Task.Delay(50);

            tokenSource = new();
            await maze.Generate(true, tokenSource.Token);

            btnStepGeneration.Enabled = true;
        }

        else if (maze.generationFinished)
        {
            MazeFinished();
        }

        btnResetGeneration.Enabled = true;
    }

    /// <summary>
    /// Disables the maze generation controls when the maze has finished generating.
    /// Also instantiates objects for Dijkstra's algorithm and the A* Search algorithm for pathfinding.
    /// </summary>
    private void MazeFinished()
    {
        chkGenerateMaze.Checked = false;
        chkGenerateMaze.Enabled = false;
        btnStepGeneration.Enabled = false;
        tbGenerationDelay.Enabled = false;
        tokenSource = null;

        dijkstra = new(maze)
        {
            pathfindingDelay = tbPathfindingDelay.Value
        };
        aStar = new(maze)
        {
            pathfindingDelay = tbPathfindingDelay.Value
        };

        cboPathfindingAlgorithm.Enabled = true;
        chkSolveShortestPath.Enabled = true;
        tbPathfindingDelay.Enabled = true;
        btnStepPathfinding.Enabled = true;

        // Sets the default pathfinding algorithm selected to Dijkstra's algorithm
        cboPathfindingAlgorithm.SelectedIndex = 0;
    }

    /// <summary>
    /// Solves the maze with the selected pathfinding algorithm, when the checkbox is checked, 
    /// and pauses/cancels the generation when unchecked.
    /// Also runs the method PathfindingFinished when the maze has finished generating.
    /// </summary>
    private async void chkSolveShortestPath_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSolveShortestPath.Checked && (!dijkstra.pathfindingFinished && !aStar.pathfindingFinished))
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

        else if (!chkSolveShortestPath.Checked && (!dijkstra.pathfindingFinished && !aStar.pathfindingFinished))
        {
            tokenSource?.Cancel();
        }

        if (dijkstra.pathfindingFinished || aStar.pathfindingFinished)
        {
            PathfindingFinished();
        }
    }

    /// <summary>
    /// Sets the pathfinding delay to the value of the pathfinding delay trackbar.
    /// Also displays the value of the trackbar in the pathfinding delay label that appears next to the trackbar.
    /// </summary>
    private void tbPathfindingDelay_Scroll(object sender, EventArgs e)
    {
        lblPathfindingDelayValue.Text = tbPathfindingDelay.Value.ToString() + " ms";

        if (cboPathfindingAlgorithm.SelectedIndex == 0)
        {
            dijkstra.pathfindingDelay = tbPathfindingDelay.Value;
        }

        else if (cboPathfindingAlgorithm.SelectedIndex == 1)
        {
            aStar.pathfindingDelay = tbPathfindingDelay.Value;
        }
    }

    /// <summary>
    /// Resets the maze bitmap back to the generated maze, so a pathfinding algorithm can be run again.
    /// </summary>
    private async void btnResetPathfinding_Click(object sender, EventArgs e)
    {
        chkSolveShortestPath.Checked = false;
        btnResetPathfinding.Enabled = false;

        tokenSource?.Cancel();

        // Allows the form some time to finish processing the current iteration of the selected pathfinding algorithm.
        await Task.Delay(50);

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

    /// <summary>
    /// Allows the user to step through the selected pathfinding algorithm one iteration (step) at a time.
    /// </summary>
    private async void btnStepPathfinding_Click(object sender, EventArgs e)
    {
        chkSolveShortestPath.Checked = false;

        if (!dijkstra.pathfindingFinished && !aStar.pathfindingFinished)
        {
            btnStepPathfinding.Enabled = false;
            btnResetPathfinding.Enabled = false;

            tokenSource?.Cancel();

            // Allows the form some time to finish processing the current iteration of the selected pathfinding algorithm.
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

        else if (dijkstra.pathfindingFinished || aStar.pathfindingFinished)
        {
            PathfindingFinished();
        }

        btnResetPathfinding.Enabled = true;
    }

    /// <summary>
    /// Disables the pathfinding controls when the selected pathfinding algorithm has solved the shortest path from the maze's start to its exit.
    /// </summary>
    private void PathfindingFinished()
    {
        chkSolveShortestPath.Checked = false;
        chkSolveShortestPath.Enabled = false;
        btnStepPathfinding.Enabled = false;
        tbGenerationDelay.Enabled = false;
    }
}
