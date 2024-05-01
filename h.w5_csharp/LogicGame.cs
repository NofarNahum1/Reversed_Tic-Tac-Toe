using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace h.w5_csharp
{
    public partial class LogicGame : Form
    {
        private Player m_Player1;
        private Player m_Player2;
        private Player m_PlayerTurn;
        private int m_InputBoardSize;
        Button[,] m_ButtonMatrix;
        int m_WindowWidth = 0;
        int m_WindowHeight = 0;
        System.Windows.Forms.Label m_Score = new System.Windows.Forms.Label();

        public LogicGame(Player i_Player1, Player i_Player2)
        {
            this.m_Player1 = i_Player1;
            this.m_Player2 = i_Player2;
            this.m_InputBoardSize = i_Player1.GetBoardSize();
            this.m_ButtonMatrix = new Button[m_InputBoardSize, m_InputBoardSize];
            this.m_PlayerTurn = i_Player1;
            InitializeComponent();
            initBoard();
        }

        private void initBoard()
        {
            const int k_ButtonSize = 50;
            const int k_ButtonSpacing = 6;
            const int k_StartPosition = 10;
            const int k_LableHeight = 20;
            const int k_WindowPadding = 10;

            for (int i = 0; i < m_InputBoardSize; i++)
            {
                for (int j = 0; j < m_InputBoardSize; j++)
                {
                    Button newButton = new Button();
                    m_ButtonMatrix[i, j] = newButton;
                    newButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    newButton.Font = new Font(newButton.Font.FontFamily, 10);
                    newButton.Location = new Point(k_StartPosition + (k_ButtonSpacing + k_ButtonSize) * i, k_StartPosition + (k_ButtonSpacing + k_ButtonSize) * j);
                    newButton.Tag = i.ToString() + ";" + j.ToString();
                    newButton.Click += matrixButton_Click;
                    newButton.KeyPress += logicGame_KeyPress;
                    this.Controls.Add(newButton);
                }
            }

            int boardSize = (k_ButtonSize + k_ButtonSpacing) * m_InputBoardSize;
            m_WindowWidth = boardSize + k_ButtonSpacing + k_WindowPadding;
            m_WindowHeight = boardSize + (2 * k_ButtonSpacing) + k_WindowPadding + k_LableHeight; 
            this.ClientSize = new Size(m_WindowWidth, m_WindowHeight); 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            ShowPoints();
            }

        private void matrixButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = sender as Button;
                int boardRow = int.Parse(b.Tag.ToString().Split(";")[0]);
                int boardColm = int.Parse(b.Tag.ToString().Split(";")[1]);

                if (m_Player1.CellIsEmpty(boardRow, boardColm) && m_Player2.CellIsEmpty(boardRow, boardColm))
                {
                    m_PlayerTurn.m_PlayerChoosenSymbols[boardRow, boardColm] = m_PlayerTurn.PlayerSymbol();
                    DrawBoard();
                    CheckWinner();
                    if (m_PlayerTurn == m_Player1)
                    {
                        m_PlayerTurn = m_Player2;
                    }
                    else
                    {
                        m_PlayerTurn = m_Player1;
                    }
                    if (m_Player2.IsComputer())
                    {
                        computerTurn();
                        m_PlayerTurn = m_Player1;
                    }
                }
                else
                {
                    MessageBox.Show("Error", "Please click on emtpy button", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void logicGame_Load(object sender, EventArgs e)
        {
            ShowPoints();
        }

        public void DrawBoard()
        {
            for (int i = 0; i < m_InputBoardSize; i++)
            {
                for (int j = 0; j < m_InputBoardSize; j++)
                {
                    appendSquare(i, j);
                }
            }
        }

        private void appendSquare(int i_Row, int i_Col)
        {
            if (String.Equals(m_Player1.m_PlayerChoosenSymbols[i_Row, i_Col], m_Player1.PlayerSymbol()))
            {
                m_ButtonMatrix[i_Row, i_Col].Text = m_Player1.PlayerSymbol();
                m_ButtonMatrix[i_Row, i_Col].Enabled = false;
            }
            else if (String.Equals(m_Player2.m_PlayerChoosenSymbols[i_Row, i_Col], m_Player2.PlayerSymbol()))
            {
                m_ButtonMatrix[i_Row, i_Col].Text = m_Player2.PlayerSymbol();
                m_ButtonMatrix[i_Row, i_Col].Enabled = false;
            }
            else
            {
                m_ButtonMatrix[i_Row, i_Col].Text = "";
            }
        }

        public void CheckWinner()
        {
            int maxMoves = m_InputBoardSize * m_InputBoardSize;
            int countMoves = 0;

            for (int i = 0; i < m_InputBoardSize; i++)
            {
                for (int j = 0; j < m_InputBoardSize; j++)
                {
                    if (m_ButtonMatrix[i, j].Text != "")
                    {
                        countMoves++;
                    }
                }
            }
            if (m_Player1.CheckIfLose())
            {
                m_Player2.m_points++;
                DialogResult dr = MessageBox.Show("The winner is: " + m_Player2.PlayerSymbol() + "\n" + "Do you want to play again?"
      , "A Win!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    m_Player1.NewTable();
                    m_Player2.NewTable();
                    ResetButtons();
                    DrawBoard();
                }
                else
                {
                    Close();
                }
            }
            else if (m_Player2.CheckIfLose())
            {
                m_Player1.m_points++;
                DialogResult dr = MessageBox.Show("The winner is: " + m_Player1.PlayerSymbol() + "\n" + "Do you want to play again?",
"A Win!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    m_Player1.NewTable();
                    m_Player2.NewTable();
                    ResetButtons();
                    DrawBoard();
                }
                else
                {
                    Close();
                }
            }
            else if (countMoves == maxMoves)
            {
                DialogResult dr = MessageBox.Show("Tie!" + "\n" + "Do you want to play again?",
"A Tie!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    m_Player1.NewTable();
                    m_Player2.NewTable();
                    ResetButtons();
                    DrawBoard();
                }
                else
                {
                    Close();
                }
            }
            ShowPoints();
        }

        public void ResetButtons()
        {
            for (int i = 0; i < m_InputBoardSize; i++)
            {
                for (int j = 0; j < m_InputBoardSize; j++)
                {
                    m_ButtonMatrix[i, j].Enabled = true;
                    m_ButtonMatrix[i, j].Text = "";
                }
            }
        }

        public void ShowPoints()
        {
            m_Score.Name = "k_Score";
            m_Score.TextAlign = ContentAlignment.MiddleCenter;
            m_Score.Font = new Font(m_Score.Font.FontFamily, 8);
            m_Score.AutoSize = true;
            m_Score.Location = new Point((m_WindowWidth - m_Score.Width) / 2, m_WindowHeight - m_Score.Height);
            m_Score.Padding = new Padding(7);
            this.Controls.Add(m_Score);
            m_Score.Text = m_Player1.GetPlayerName() + " : " + m_Player1.GetPoints() + "    " + m_Player2.GetPlayerName() + " : " + m_Player2.GetPoints();
        }

        private void computerTurn()
        {
            Random random = new Random();
            bool playerTurn = true;
            int row = 0;
            int colm = 0;

            while (playerTurn)
            {
                row = random.Next(0, m_PlayerTurn.GetBoardSize());
                colm = random.Next(0, m_PlayerTurn.GetBoardSize());
                if (m_Player1.CellIsEmpty(row, colm) && m_Player2.CellIsEmpty(row, colm))
                {
                    m_PlayerTurn.m_PlayerChoosenSymbols[row, colm] = m_PlayerTurn.PlayerSymbol();
                    playerTurn = false;
                }
            }
            DrawBoard();
            CheckWinner();
        }

        private void logicGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q' || e.KeyChar == 'Q')
            {
                Close();
            }
        }
    }
}
