using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOGame.Properties;

namespace XOGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;

        enum enPlayer
        {
            Player1, 
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        public void EndGame()
        {
            lblTurnPlayer.Text = "Game Over"; 
            
            switch(GameStatus.Winner)
            {

                case enWinner.Player1:

                        lblWinner.Text = "Player 1";
                        break;


                case enWinner.Player2:

                        lblWinner.Text = "Player 2";
                        break;


                default:

                        lblWinner.Text = "Draw";
                        break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CheckValue(Button btn1,  Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn2.Tag.ToString() == btn3.Tag.ToString()) 
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X") 
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                if (btn1.Tag.ToString() == "O")
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

        public void CheckWinner()
        {
            if (CheckValue(button1, button2, button3))
                return;

            if (CheckValue(button4, button5, button6))
                return;

            if (CheckValue(button7, button8, button9))
                return;

            if (CheckValue(button1, button4, button7))
                return;

            if (CheckValue(button2, button5, button8))
                return;

            if (CheckValue(button3, button6, button9))
                return;

            if (CheckValue(button1, button5, button9))
                return;

            if (CheckValue(button3, button5, button7))
                return;
        }

        public void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                GameStatus.PlayCount++;

                switch(PlayerTurn)
                {
                    case enPlayer.Player1:

                        btn.BackgroundImage = Resources.XX;
                        PlayerTurn = enPlayer.Player2;
                        lblTurnPlayer.Text = "Player 2";
                        btn.Tag = "X";
                        CheckWinner();
                        break;

                    case enPlayer.Player2:

                        btn.BackgroundImage = Resources.o;
                        PlayerTurn = enPlayer.Player1;
                        lblTurnPlayer.Text = "Player 1";
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(GameStatus.PlayCount == 9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();

            }
        }

        public void RestButton(Button btn)
        {
            btn.Tag = "?";
            btn.BackColor = Color.Black;
            btn.BackgroundImage = Resources.q;
        }

        public void RestartGame()
        {
            RestButton(button1);
            RestButton(button2);
            RestButton(button3);
            RestButton(button4);
            RestButton(button5);
            RestButton(button6);
            RestButton(button7);
            RestButton(button8);
            RestButton(button9);

            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";

            PlayerTurn = enPlayer.Player1;
            lblTurnPlayer.Text = "Player 1";

            GameStatus.PlayCount = 0;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color Wihte = Color.FromArgb(255, 255, 255, 255);
            Pen pen = new Pen(Wihte);

            pen.Width = 15;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 700, 150, 700, 500);
            e.Graphics.DrawLine(pen, 850, 150, 850, 500);


            e.Graphics.DrawLine(pen, 550, 267, 1000, 267);
            e.Graphics.DrawLine(pen, 550, 383, 1000, 383);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
