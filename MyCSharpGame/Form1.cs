using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCSharpGame
{
    public partial class Form1 : Form
    {
        bool shooterLeft, shooterRight;
        int score = 0;
        PictureBox[] rebel_list;
        bool isGameEnded;
        public Form1()
        {
            InitializeComponent();
            GameStart();
        }

        private void Timer_Event(object sender, EventArgs e)
        {
            label1.Text = nameof(score)+": " + score;

            if(shooterLeft)
            {
                shooter.Left = Math.Max(0, shooter.Left - 15);
            }
            if (shooterRight)
            {
                shooter.Left = Math.Min(845, shooter.Left + 15);
            }

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "rebels")
                {
                    if (x.Top == shooter.Top - 40)
                    {
                        GameEnd("Game Over");
                    }
                    x.Top += 3;
                    if (x.Bounds.IntersectsWith(shooter.Bounds))
                     {
                        GameEnd("Game Over");
                     }
                    foreach(Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "missile")
                        {
                            if(y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                x.Dispose();
                                this.Controls.Remove(y);
                                score = score + 10;
                            }
                        }
                    }
                }
            if(x is PictureBox && (string)x.Tag == "missile")
                {
                    x.Top -= 20;
                    if(x.Top < 15)
                    {
                        this.Controls.Remove(x);
                    }
                }
            if(x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Top += 20;
                    if(x.Top > 620)
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        private void DownKey(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                shooterLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                shooterRight = true;
            }
        }

        private void UpKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                shooterLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                shooterRight = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                Missile();
            }
            if(e.KeyCode == Keys.Enter && isGameEnded == true)
            {
                GameReset();
                GameStart();
            }
        }

        private void Rebels()
        {
            RebelLine1();
            RebelLine2();
        }

        private void RebelLine2()
        {
            rebel_list ??= new PictureBox[10];
            int left = this.Left;
            int top = this.Top;
            for (int i = 0; i < rebel_list?.Length; i++)
            {
                rebel_list[i] = new PictureBox();
                rebel_list[i].Size = new Size(60, 50);
                rebel_list[i].Image = Properties.Resources.rebel;
                rebel_list[i].Top = top + 120;
                rebel_list[i].Tag = "rebels";
                rebel_list[i].Left = left + 30;
                rebel_list[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(rebel_list[i]);
                left = left + 140;
            }
        }
        private void RebelLine1()
        {
            rebel_list ??= new PictureBox[10];
            int left = this.Left;
            int top = this.Top;
            for (int i = 0; i < rebel_list?.Length; i++)
            {
                rebel_list[i] = new PictureBox();
                rebel_list[i].Size = new Size(60, 50);
                rebel_list[i].Image = Properties.Resources.rebel;
                rebel_list[i].Top = top + 20;
                rebel_list[i].Tag = "rebels";
                rebel_list[i].Left = left;
                rebel_list[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(rebel_list[i]);
                left = left + 140;
            }
        }

        private void GameStart()
        {
            label1.Text = "Score: 0";
            score = 0;
            isGameEnded = false;
            Rebels();
            Mygametimer.Start();
        }
        private void GameEnd(string message)
        {
            isGameEnded = true;
            Mygametimer.Stop();
            label1.Text = "Score: " + score;
            label2.Visible = true;
            label2.Text = message;
        }

        private void GameReset()
        {
            foreach (PictureBox i in rebel_list)
            {
                this.Controls.Remove(i);
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((String)x.Tag == "missile" || (String)x.Tag == "rebel")
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }
        private void Missile()
        {
            PictureBox missile = new PictureBox();
            missile = new PictureBox();
            missile.Image = Properties.Resources.bullet1;
            missile.Size = new Size(5, 20);
            missile.Tag = "missile";
            missile.Left = shooter.Left + shooter.Width / 2;
            missile.Top = shooter.Top - 20;
            this.Controls.Add(missile);
            missile.BringToFront();
        }
    }
}
