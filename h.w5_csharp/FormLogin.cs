using System.Numerics;

namespace h.w5_csharp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.Text = "";
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Text = "[Computer]";
                textBox2.Enabled = false;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter player 1 name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter player 2 name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numericUpDown1.Value < 4 || numericUpDown1.Value > 10)
            {
                MessageBox.Show("Some text", "Some title",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool against_computer = !checkBox1.Checked;
                String firstPlayerSymbol = "X";
                String secondPlayerSymbol = "O";
                int boardSize = (int)numericUpDown1.Value;
                Player player1 = new Player(textBox1.Text, boardSize, false, 0, true, firstPlayerSymbol);
                Player player2 = new Player(textBox2.Text, boardSize, against_computer, 0, false, secondPlayerSymbol);
                this.Hide();
                LogicGame form = new LogicGame(player1, player2);
                form.ShowDialog();
                Close();
            }
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            numericUpDown2.Value = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown2.Value;
        }
    }
}