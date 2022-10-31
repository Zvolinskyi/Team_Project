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

namespace TeamProject
{
    
    public partial class HorrorQuest : Form
    {
        SoundPlayer player = new SoundPlayer();
        SoundPlayer scare = new SoundPlayer();
        public HorrorQuest()
        {
            InitializeComponent();
        }

        private void StartButtonEvent(object sender, EventArgs e)
        {
            StartGame();
        }

        private void FormMousEvent(object sender, EventArgs e)
        {

            EndGame();
        }

        private void ShowAnimDoor(object sender, EventArgs e)
        {
            
            ScarePict scarePict = new ScarePict();
            scarePict.Show();
            player.Stop();
            scare.Play();
        }
        private void StartGame()
        {
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
                    if(x.Tag == null)
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
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Enabled = false;

                    if (x.Tag == null)
                    {
                        x.BackColor = Color.Black;
                    }
                }
            }
        
        }
    }
}
