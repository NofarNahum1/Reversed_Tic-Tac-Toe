using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace h.w5_csharp
{
    public class Player
    {
        private readonly int r_BoardSize;
        private readonly bool r_IsComputer;
        private readonly string r_PlayerSymble;
        public int m_points = 0;
        public bool m_IsTurn;
        private string m_player_name;
        public String[,] m_PlayerChoosenSymbols;

        public Player(string i_player_name, int i_BoardSize, bool i_IsComputer, int i_points, bool i_IsTurn, string i_PlayerSymble)
        {
            this.r_BoardSize = i_BoardSize;
            this.r_IsComputer = i_IsComputer;
            this.r_PlayerSymble = i_PlayerSymble;
            this.m_points = i_points;
            this.m_IsTurn = i_IsTurn;
            this.m_PlayerChoosenSymbols = new string[i_BoardSize, i_BoardSize];
            this.m_player_name = i_player_name;
        }

        public String GetPlayerName()
        {
            return this.m_player_name;
        }

        public void NewTable()
        {
            this.m_PlayerChoosenSymbols = new string[this.r_BoardSize, this.r_BoardSize];
        }

        public int GetBoardSize()
        {
            return this.r_BoardSize;
        }

        public bool IsComputer()
        {
            return this.r_IsComputer;
        }

        public bool IsTurn()
        {
            return this.m_IsTurn;
        }

        public string PlayerSymbol()
        {
            return this.r_PlayerSymble;
        }

        public int GetPoints()
        {
            return this.m_points;
        }

        public void IncreasePoints()
        {
            this.m_points++;
        }

        public void SetPlayerTurn()
        {
            if (this.m_IsTurn)
            {
                this.m_IsTurn = false;
            }
            else
            {
                this.m_IsTurn = true;
            }
        }

        public bool CellIsEmpty(int i_Row, int i_Col)
        {
            bool valueToReturn = i_Row < 0 || i_Row >= r_BoardSize || i_Col < 0 || i_Col >= r_BoardSize || this.m_PlayerChoosenSymbols[i_Row, i_Col] != null;

            return !valueToReturn;
        }

        public bool CheckRowSequence()
        {
            bool rowSequence = false;
            int sequence = 0;
            int j = 0;
            for (int i = 0; i < this.GetBoardSize(); i++)
            {
                sequence = 0;
                j = 0;
                while (!this.CellIsEmpty(i, j))
                {
                    sequence++;
                    j++;
                    if (sequence == this.r_BoardSize)
                    {
                        rowSequence = true;
                        break;
                    }
                }
            }

            return rowSequence;
        }

        public bool CheckColSequence()
        {
            bool colSequence = false;
            int sequence = 0;
            int j = 0;
            for (int i = 0; i < this.GetBoardSize(); i++)
            {
                sequence = 0;
                j = 0;
                while (!this.CellIsEmpty(j, i))
                {
                    sequence++;
                    j++;
                    if (sequence == this.r_BoardSize)
                    {
                        colSequence = true;
                        break;
                    }
                }
            }

            return colSequence;
        }

        public bool CheckDiagonalSequence()
        {
            bool diagonalSequence = false;
            int sequence = 0;

            for (int i = 0; i < this.GetBoardSize(); i++)
            {
                if (!this.CellIsEmpty(i, i))
                {
                    sequence++;
                    if (sequence == this.r_BoardSize)
                    {
                        diagonalSequence = true;
                        goto end;
                    }
                }
            }
            sequence = 0;
            int row = this.GetBoardSize() - 1;
            for (int col = 0; col < this.GetBoardSize(); col++)
            {
                if (!this.CellIsEmpty(row, col))
                {
                    sequence++;
                    if (sequence == this.r_BoardSize)
                    {
                        diagonalSequence = true;
                        goto end;
                    }
                }
                row--;
            }
        end:

            return diagonalSequence;
        }

        public bool CheckIfLose()
        {
            bool loser = false;
            if (this.CheckRowSequence() || this.CheckColSequence() || this.CheckDiagonalSequence())
            {
                loser = true;
            }

            return loser;
        }

        public static void ShowPoints(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            System.Console.WriteLine("first player ({0}) points: {1}", i_FirstPlayer.r_PlayerSymble, i_FirstPlayer.m_points);
            System.Console.WriteLine("second player ({0}) points: {1}", i_SecondPlayer.r_PlayerSymble, i_SecondPlayer.m_points);
        }
    }
}
