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
                MessageBox.Show("Input error: Please enter integers between 5 and 50 (inclusive).", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rows > 50 || rows < 5 || columns > 50 || columns < 5)
            {
                MessageBox.Show("Input error: Height or width must be between 5 and 50 (inclusive).", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int percentage;

            try
            {
                percentage = Convert.ToInt32(txtRemoveDeadends.Text.Trim());
            }

            catch
            {
                MessageBox.Show("Input error: Please enter an integer between 0 and 100 (inclusive).", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (percentage < 0 || percentage > 100)
            {
                MessageBox.Show("Input error: Remove Deadends must be between 0 and 100 (inclusive).", "APATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MazeScreen mazeScreen = new(rows, columns, percentage);
            Hide();
            mazeScreen.Show();
        }
    }
}