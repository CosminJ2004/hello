using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace masinute2
{
    public partial class Form1 : Form
    {
        int roadSpeed;
        int trafficSpeed;
        int playerSpeed=12;
        int score;
        int carImage;
        Random rand = new Random();
        Random carPosition=new Random();
        bool goleft, goright;


        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            { goleft = true; }
            if (e.KeyCode == Keys.Right)
            { goright = true; }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            { goleft = false; }
            if (e.KeyCode == Keys.Right)
            { goright = false; }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Scor:" + score;
            score++;
            if(goleft==true&&player.Left>5)
            { player.Left = player.Left-playerSpeed; }
             if(goright == true && player.Left <320)
            { player.Left = player.Left+playerSpeed; }
            drum1.Top += roadSpeed;
            drum2.Top += roadSpeed;
            if(drum2.Top>519)
            {
                drum2.Top = -519;
            }
            if (drum1.Top > 519)
            {
                drum1.Top = -519;
            }
            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;
            if(AI1.Top>530)
            { changeAIcars(AI1); }
            if (AI2.Top > 530)
            { changeAIcars(AI2); }
            if(player.Bounds.IntersectsWith(AI1.Bounds)||player.Bounds.IntersectsWith(AI2.Bounds))
            { gameOver(); }
            if(score>40&&score<500)
            { award.Image = Properties.Resources.bronze; }
            if (score > 500 && score < 2000)
            { award.Image = Properties.Resources.silver;
                roadSpeed = 20;
                trafficSpeed = 22;
            }
            if (score > 2000)
            { award.Image = Properties.Resources.gold;
                roadSpeed = 25;
                trafficSpeed = 27;
            }

        }
        private void changeAIcars(PictureBox tempCar)
        {
            carImage = rand.Next(1, 15);
            switch(carImage)
            {

                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
                case 10:
                    tempCar.Image = Properties.Resources.trashmaster;
                    break;
                case 11:
                    tempCar.Image = Properties.Resources.car2;
                    break;
                case 12:
                    tempCar.Image = Properties.Resources.jeep;
                    break;
                
            }

            tempCar.Top = carPosition.Next(100, 400) * -1;
            if ((string)tempCar.Tag=="carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(245, 422);
            }

        }
        private void gameOver()
        {
            PlaySound();
            timp.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor= Color.Transparent;

            award.Visible= true;
            award.BringToFront();
            btnStart.Enabled=true;
        }
        private void ResetGame() 
        {
            btnStart.Enabled = false;
            explosion.Visible= false;
            award.Visible= false;
            goleft=false;
            goright=false;
            score = 0;
            award.Image = Properties.Resources.bronze;
            roadSpeed = 12;
            trafficSpeed = 15;
            AI1.Top = carPosition.Next(200, 550) * -1;
            AI1.Left = carPosition.Next(5, 200);
            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(245, 422);
           timp.Start();
        }
        
        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void PlaySound()
        {
            System.Media.SoundPlayer playCrash=new System.Media.SoundPlayer(Properties.Resources.hit);
            playCrash.Play();
        }
    }
}
