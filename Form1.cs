using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tic___Tac___Toe_Game.Properties;

namespace Tic___Tac___Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;

        enPlayer PlayerTurn = enPlayer.Player1;

        enum enWinner { Player1 , Player2 , InProgress , Draw};

        enum enPlayer { Player1, Player2 };

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public int PlayCount;
        }
    
        

        void EndGame()
        {
            txtTurn.Text = "Game Over";


            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    {
                        txtWinner.Text = "Player1";
                        MessageBox.Show("Game Over , Player 1 Win", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case enWinner.Player2:
                    {
                        txtWinner.Text = "Player2";
                        MessageBox.Show("Game Over , Player 2 Win", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                default:
                    {
                        txtWinner.Text = "Draw";
                        MessageBox.Show("Game Over , Draw", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
        }

        bool CheckLine(Button B1, Button B2, Button B3)
        {
            if (B1.Tag.ToString() != "?" && B1.Tag == B2.Tag && B1.Tag == B3.Tag)
            {
                B1.BackgroundImage = Resources.trophy;
                B2.BackgroundImage = Resources.trophy;
                B3.BackgroundImage = Resources.trophy;

                if(B1.Tag.ToString() == "x")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                
            }
                GameStatus.GameOver = false;
                return false;
        }
        void CheckWinner()
        {
            //Check Row 1
            if (CheckLine(btn10, btn20, btn30))
                return;

            //Check Row 2
            if(CheckLine(btn40, btn50, btn60))
                return;

            //Check Row 3
            if (CheckLine(btn70, btn80, btn90))
                return;

            //Check Col 1
            if (CheckLine(btn10, btn40, btn70))
                return;

            //Check Col 2
            if(CheckLine(btn20, btn50, btn80))
                return;

            //Check Col 3
            if (CheckLine(btn30, btn60, btn90))
                return;

            //Check Diagnol 1
            if (CheckLine(btn10, btn50, btn90))
                return;

            //Check Diagnol 2
            if (CheckLine(btn30, btn50, btn70))
                return;

        }
        void UpdateImage(Button Btn)
        {
            if (Btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        {
                            Btn.BackgroundImage = Resources.x;
                            PlayerTurn = enPlayer.Player2;
                            txtTurn.Text = "Player 2";
                            GameStatus.PlayCount++;
                            Btn.Tag = "x";
                            CheckWinner();
                            break;
                        }
                    case enPlayer.Player2:
                        {
                            Btn.BackgroundImage = Resources.o;
                            PlayerTurn = enPlayer.Player1;
                            txtTurn.Text = "Player 1";
                            GameStatus.PlayCount++;
                            Btn.Tag = "o";
                            CheckWinner();
                            break;
                        }
                }

            }
            else
                MessageBox.Show("Wrong choice", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (GameStatus.PlayCount == 9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
        }

        void ResetButton(Button Btn)
        {
            Btn.BackgroundImage = Resources.chia;
            Btn.Tag = "?";
        }
        void StartTheGame()
        {
            ResetButton(btn10);
            ResetButton(btn20);
            ResetButton(btn30);
            ResetButton(btn40);
            ResetButton(btn50);
            ResetButton(btn60);
            ResetButton(btn70);
            ResetButton(btn80);
            ResetButton(btn90);

            PlayerTurn = enPlayer.Player1;
            GameStatus.Winner = enWinner.InProgress;
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            txtTurn.Text = "Player1";
            txtWinner.Text = "In Progress";
        }

        private void button_Click(object sender, EventArgs e)
        {
            UpdateImage((Button)sender);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            UpdateImage(btn10);
        }
        
        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartTheGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color Black = Color.FromArgb(63, 61, 61);

            Pen Pen = new Pen(Black);

            Pen.Width = 10;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 554, 70, 554, 300);
            e.Graphics.DrawLine(Pen, 646, 70, 646, 300);
            e.Graphics.DrawLine(Pen, 480, 134, 710, 134);
            e.Graphics.DrawLine(Pen, 480, 226, 710, 226);

        }

    }
}
