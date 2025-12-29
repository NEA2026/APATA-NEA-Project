using APATA_NEA_Project.Classes;
using System.Diagnostics;
using System.Threading.Tasks;

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
        if (chkGenerateMaze.Checked && !maze.finishedDFS)
        {
            btnReset.Enabled = true;
            btnStep.Enabled = true;
            tokenSource = new();
            await maze.Generate(tokenSource.Token);
        }

        else if (!chkGenerateMaze.Checked && !maze.finishedDFS)
        {
            tokenSource!.Cancel();
        }
    }

    private void tbGenerationDelay_Scroll(object sender, EventArgs e)
    {
        lblGenerationDelayValue.Text = tbGenerationDelay.Value.ToString();
        maze.generationDelay = tbGenerationDelay.Value;
    }

    private async void btnReset_Click(object sender, EventArgs e)
    {
        chkGenerateMaze.Checked = false;
        tokenSource!.Cancel();
        await Task.Delay(50);

        maze = new(this, rows, columns, percentage, scaledMazeSize)
        {
            generationDelay = tbGenerationDelay.Value
        };

        btnReset.Enabled = false;
        chkGenerateMaze.Enabled = true;
    }
}
