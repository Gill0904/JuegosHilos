using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace paginaInicio
{
    public partial class Form2 : Form
    {
        WindowsMediaPlayer sonido;
        public Form2()
        {
            InitializeComponent();
            try
            {
                if (sonido == null)
                {
                    sonido = new WindowsMediaPlayer();
                    sonido.URL = Application.StartupPath + @"\SoundTrack\8BitTheme.mp3";
                    sonido.controls.play();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sonido.controls.stop();
            Juegos.Naves n=new Juegos.Naves();
            n.lblUser.Text = lblUser.Text;
            n.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sonido.controls.stop();
            Juegos.Tanks t=new Juegos.Tanks();
            t.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            sonido.controls.stop();
            Juegos.Cartas c = new Juegos.Cartas();
            
            c.Show();
            this.Hide();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\Retro Game - Title Screen.mp3";
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\Retro Game - Title Screen.mp3";
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\02_I_Said_Im_Naruto.mp3";
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\16_Need_To_Be_Strong.mp3";
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\16_Need_To_Be_Strong.mp3";
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\02_I_Said_Im_Naruto.mp3";
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            sonido.URL = Application.StartupPath + @"\SoundTrack\8BitTheme.mp3";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
