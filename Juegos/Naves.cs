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
using dll_videojuegos;

namespace Juegos
{
    public partial class Naves : Form
    {
        WindowsMediaPlayer sonido;
        PictureBox pbPlay = new PictureBox();
        PictureBox pbHome = new PictureBox();
        PictureBox pbGameOver = new PictureBox();
        public delegate void d_mover(Control c, int x, int y);
        public delegate void d_moverBalaEnemiga(Control c, int x, int y);
        public delegate void d_agregarBalaEnemiga(Control c, int x, int y);
        public delegate void d_vida(Control c);
        public delegate void d_vidaAliada(Control c);
        bool movimiento = true, genBal = true;
        int i = 5, puntuacion = 0;
        byte vidas = 5;
        string usuario ;
        public Naves()
        {
            InitializeComponent();
            this.KeyPress += Form1_KeyPress;
            i = 5;
            movimiento = true;
            genBal = true;
            Thread t = new Thread(mover);
            t.Start();
            Thread t4 = new Thread(balaEnemiga);
            t4.Start();
            Thread t5 = new Thread(moverVida);
            t5.Start();
            try
            {
                if (sonido == null)
                {
                    sonido = new WindowsMediaPlayer();
                    sonido.URL = Application.StartupPath+ @"\SoundTrack\Level 1.mp3";
                    sonido.controls.play();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        private void m_mover(Control c, int x, int y)
        {
            try
            {
                if (InvokeRequired)
                {
                    d_mover dm = new d_mover(m_mover);
                    object[] parametros = new object[] { c, x, y };
                    Invoke(dm, parametros);
                }
                else
                {
                    c.Location = new Point(x, y);
                    if (c.Tag != null)
                    {
                        if (!(bool)c.Tag)
                        {
                            c.Dispose();
                        }
                        else
                            if (c.Location.Y < -20)
                        {
                            c.Dispose();
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public void mover()
        {
            try
            {
                bool der = true;
                Control c = pictureBox1;
                int x = pictureBox1.Location.X;
                int y = pictureBox1.Location.Y;
                while (movimiento)
                {
                    if (der)
                    {
                        x += 10;
                        m_mover(this.pictureBox1, x, y);
                    }
                    else
                    {
                        x -= 10;
                        m_mover(this.pictureBox1, x, y);
                    }
                    Thread.Sleep(50);
                    if (x + pictureBox1.Width >= this.Width)
                    {
                        der = false;
                    }
                    if (x <= 0)
                    {
                        der = true;
                    }
                }
            }
            catch
            {

            }
        }
        public void moverVida()
        {
            bool der = true;
            Control c = progressBar1;
            int x = progressBar1.Location.X;
            int y = progressBar1.Location.Y;
            while (movimiento)
            {
                if (der)
                {
                    x += 10;
                    m_mover(this.progressBar1, x, y);
                }
                else
                {
                    x -= 10;
                    m_mover(this.progressBar1, x, y);
                }
                Thread.Sleep(50);
                if (x + progressBar1.Width >= this.Width)
                {
                    der = false;
                }
                if (x <= 0)
                {
                    der = true;
                }
            }
        }

        public void moverBala(Object pb)
        {
            Control c = pb as Control;
            int x = c.Location.X;
            int y = c.Location.Y;
            while ((bool)c.Tag)
            {
                y -= 6;
                if (c.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    m_vida(pictureBox1);
                    c.Tag = false;
                }
                else
                if (c.Location.Y < -20)
                {
                    c.Tag = false;
                }
                m_mover(c, x, y);
                Thread.Sleep(20);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            movimiento = false;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.A)
            {
                    pictureBox2.Location = new Point(pictureBox2.Location.X - 10, pictureBox2.Location.Y);
            }
            if (e.KeyChar == (char)Keys.D)
            {
                    pictureBox2.Location = new Point(pictureBox2.Location.X + 10, pictureBox2.Location.Y);
                
            }
            if (e.KeyChar == (char)Keys.Space)
            {
                puntuacion -= 1;
                PictureBox pb = new PictureBox();
                pb.Image = Properties.Resources.balaAliada;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.Size = new Size(8, 20);
                pb.Tag = true;
                int x = pictureBox2.Location.X + (pictureBox2.Width / 2);
                int y = pictureBox2.Location.Y - 20;
                pb.Location = new Point(x, y);
                this.Controls.Add(pb);
                Thread t2 = new Thread(moverBala);
                t2.Start(pb);
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                gameOver();
            }

        }
        private void balaEnemiga()
        {
            while (genBal)
            {
                Random rnd = new Random();
                int i = rnd.Next(100, 2000);
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Transparent;
                pb.Image = Properties.Resources.balaEnemiga;
                pb.SizeMode =PictureBoxSizeMode.CenterImage;
                pb.Size = new Size(8, 20);
                pb.Tag = true;
                int x = pictureBox1.Location.X + (pictureBox1.Width / 2);
                int y = pictureBox1.Location.Y + (pictureBox1.Height);
                pb.Location = new Point(x, y);
                m_agregarBalaEnemiga(pb, x, y);
                Thread t3 = new Thread(moverBalaEnemiga);
                t3.Start(pb);
                Thread.Sleep(i);
            }
        }
        public void moverBalaEnemiga(Object pb)
        {
            Control c = pb as Control;
            int x = c.Location.X;
            int y = c.Location.Y;
            while ((bool)c.Tag)
            {
                y += 6;
                if (c.Bounds.IntersectsWith(pictureBox2.Bounds))
                {
                    switch (i)
                    {
                        case 1:
                            m_vidaAliada(vida1);
                            break;
                        case 2:
                            m_vidaAliada(vida2);
                            break;
                        case 3:
                            m_vidaAliada(vida3); 
                            break;
                        case 4:
                            m_vidaAliada(vida4);
                            break;
                        case 5:
                            m_vidaAliada(vida5);
                            break;
                    }
                    vidas--;
                    i--;
                    c.Tag = false;
                }
                else
               if (c.Location.Y > this.Height + 20)
                {
                    c.Tag = false;
                }
                m_moverBalaEnemiga(c, x, y);
                Thread.Sleep(20);
            }
        }
        private void m_moverBalaEnemiga(Control c, int x, int y)
        {
            try
            {
                if (InvokeRequired)
                {
                    d_moverBalaEnemiga dm = new d_moverBalaEnemiga(m_moverBalaEnemiga);
                    object[] parametros = new object[] { c, x, y };
                    Invoke(dm, parametros);
                }
                else
                {
                    c.Location = new Point(x, y);
                    if (!(bool)c.Tag)
                    {
                        c.Dispose();
                    }
                    else
                        if (c.Location.Y < -20)
                    {
                        c.Dispose();
                    }
                }
            }
            catch
            {

            }
        }
        private void m_agregarBalaEnemiga(Control c, int x, int y)
        {
            if (InvokeRequired)
            {
                d_agregarBalaEnemiga dm = new d_agregarBalaEnemiga(m_agregarBalaEnemiga);
                object[] parametros = new object[] { c, x, y };
                Invoke(dm, parametros);
            }
            else
            {
                this.Controls.Add(c);
            }
        }
        private void m_vida(Control c)
        {
            if (InvokeRequired)
            {
                d_vida dm = new d_vida(m_vida);
                Invoke(dm, c);
            }
            else
            {
                PictureBox pb = c as PictureBox;
                if (progressBar1.Value > 0)
                {
                    progressBar1.Value -= 5;
                    puntuacion += 10;
                }
                else if (progressBar1.Value == 0)
                {

                    pb.Image = Properties.Resources.Boom;
                    gameOver();
                }
            }
        }

        //private void PlayGameOver_Click(object sender, EventArgs e)
        //{
        //    InitializeComponent();
        //    movimiento = true;
        //    genBal = true;
        //    Thread t = new Thread(mover);
        //    t.Start();
        //    Thread t4 = new Thread(balaEnemiga);
        //    t4.Start();
        //    Thread t5 = new Thread(moverVida);
        //    t5.Start();
        //    sonido.URL = Application.StartupPath + @"\SoundTrack\Level 1.mp3";
        //    pbGameOver.Visible = false;
        //    pbHome.Visible = false;
        //    pbPlay.Visible = false;
        //}

        //private void HomeGameOver_Click(object sender, EventArgs e)
        //{
        //    foreach (Control item in this.Controls)
        //    {
        //        item.Dispose();
        //    }
        //    sonido.controls.stop();
        //    Application.Restart();
        //}

        private void m_vidaAliada(Control c)
        {
            if (InvokeRequired)
            {
                d_vidaAliada dm = new d_vidaAliada(m_vidaAliada);
                Invoke(dm, c);
            }
            else
            {
                c.Visible = false;
                if (c.Name == "vida1")
                {
                    gameOver();
                }
            }
        }
        private void gameOver()
        {
            genBal = false;
            movimiento = false;
            usuario = lblUser.Text;
            foreach (Control item in this.Controls)
            {
                item.Tag = false;
            }
            this.Controls.Clear();
            sonido.URL = Application.StartupPath + @"\SoundTrack\Ending.mp3";
            OperacionesNaves on= new OperacionesNaves();
            on.Nombre = lblUser.Text;
            on.Puntuacion = puntuacion;
            on.Vidas = vidas;
            if (vida1.Visible==true)
            {
                if (on.AgregarGanador())
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (on.AgregarPerdedor())
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            DialogResult DR = MessageBox.Show("¿Desea reiniciar el juego?", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (DR == DialogResult.Retry)
            {
                InitializeComponent();
                lblUser.Text = usuario;
                i = 6;
                vidas = 6;
                puntuacion = 0;
                movimiento = true;
                genBal = true;
                Thread t = new Thread(mover);
                t.Start();
                Thread t4 = new Thread(balaEnemiga);
                t4.Start();
                Thread t5 = new Thread(moverVida);
                t5.Start();
                sonido.URL = Application.StartupPath + @"\SoundTrack\Level 1.mp3";
                //this.Refresh();
            }
            else
            {
                Application.Restart();
            }
            //pbPlay.Image = Properties.Resources.Play;
            //pbPlay.Size = new Size(273, 111);
            //pbPlay.SizeMode = PictureBoxSizeMode.CenterImage;
            //pbPlay.Location = new Point(177, 504);
            //pbPlay.Click += PlayGameOver_Click;
            //pbPlay.Visible = true;
            //this.Controls.Add(pbPlay);

            //pbHome.Image = Properties.Resources.Home;
            //pbHome.Size = new Size(200, 111);
            //pbHome.SizeMode = PictureBoxSizeMode.CenterImage;
            //pbHome.Location = new Point(456, 504);
            //pbHome.Click += HomeGameOver_Click;
            //pbHome.Visible = true;
            //this.Controls.Add(pbHome);

            //pbGameOver.Image = Properties.Resources.GameOver;
            //pbGameOver.SizeMode = PictureBoxSizeMode.AutoSize;
            //pbGameOver.Location=new Point(173, 133);
            //pbGameOver.Visible = true;
            //this.Controls.Add(pbGameOver);
            
        }

    }

}
