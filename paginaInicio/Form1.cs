using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace paginaInicio
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer sonido;
        public Form1()
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sonido.close();
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread.EndThreadAffinity();
            Application.Exit();
        }
    }
}
