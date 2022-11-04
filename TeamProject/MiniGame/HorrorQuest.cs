using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Windows;
using Point = System.Drawing.Point;
using System.Diagnostics;

namespace TeamProject
{
    public partial class HorrorQuest : Form
    {
        bool buton = false;
        SoundPlayer player = new SoundPlayer();
        SoundPlayer scare = new SoundPlayer();
        public HorrorQuest()
        {
            InitializeComponent();
            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(Cursor.Position.X - -1300,Cursor.Position.Y - -1400);
            //Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        private void StartButtonEvent(object sender, EventArgs e)
        {
            StartGame();
        }
        private void Change(object sender, EventArgs e)
        {
                Score.EndGame_ = true;
                MainWindow game = new MainWindow();
                game.ShowDialog();
        }
        private void FormMouseEvent(object sender, EventArgs e)
        { 
          EndGame();
        }

        private void ShowGoOut(object sender, EventArgs e)
        {
            ScarePict scarePict = new ScarePict();
            scarePict.Show();
            player.Stop();
            scare.Play();

        }
        private void StartGame()
        {
            Score.death = true;
            StartButton.Enabled = false;
            player.Stream = Properties.Resources.song;
            scare.Stream = Properties.Resources.scary__1_;
            player.PlayLooping();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {


                    x.Enabled = true;
                    x.MouseDown += X_MouseDown;
                    if (x.Tag == null)
                    {
                        x.BackColor = Color.Orange;
                       
                    }
                }
            }
        }
        private void X_MouseDown(object sender, MouseEventArgs e)
        {
            EndGame();
        }
        private void EndGame()
        {
            StartButton.Enabled = true;
           
            if(buton)
            {

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    
                    x.Enabled = false;
                  

                    if (x.Tag == null)
                    {
                            HorrorQuest horror = new HorrorQuest();
                        x.BackColor = Color.Black;
                        ScarePict scarePict = new ScarePict();
                        scarePict.Show();
                        player.Stop();
                        scare.Play();
                        this.Close();
                        scarePict.Close();

                    }
                  

                }
            }
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buton = true;
        }

        private void HorrorQuest_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }
    }
}
