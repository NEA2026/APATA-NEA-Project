namespace APATA_NEA_Project.Forms
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void btnGenerateMaze_Click(object sender, EventArgs e)
        {
            int rows;
            int columns;

            try
            {
                rows = Convert.ToInt32(txtHeight.Text.Trim());
                columns = Convert.ToInt32(txtWidth.Text.Trim());
            }

            catch
            {
                MessageBox.Show("Input error: Please enter integers greater than 5 and less than 25.", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rows > 25 || rows < 5 || columns > 25 || columns < 5)
            {
                MessageBox.Show("Input error: Height or width must be greater than 5 and less than 25.", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MazeScreen mazeScreen = new(rows, columns);
            Hide();
            mazeScreen.Show();
        }
    }
}