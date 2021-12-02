using dll_videojuegos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Juegos
{


    public partial class Cartas : Form
    {
        #region Variable
        public bool game = true, lost = false;
        public int torresRestantes = 0, torresEliminadas = 0, cartas = 0;
        WindowsMediaPlayer sonido;
        #endregion
        #region Delegados
        public delegate void d_mover(Control c, int x, int y);
        public delegate void d_atack(Control c);
        public delegate void d_moverEnemigo(Control c, int x, int y);
        public delegate void d_atackEnemigo(Control c);
        #endregion
        public Cartas()
        {
            InitializeComponent();
            String[] ninjas = new String[5];
            ninjas[0] = "Itachi";
            ninjas[1] = "Naruto";
            ninjas[2] = "Sasuke";
            ninjas[3] = "Kakashi";
            ninjas[4] = "RockLee";
            Byte[] torres = new Byte[3];
            torres[0] = 0;
            torres[1] = 1;
            torres[2] = 2;
            foreach (var item in ninjas)
            {
                listBox1.Items.Add(item.ToString());
            }
            this.panel1.AllowDrop = true;
            try
            {
                if (sonido == null)
                {
                    sonido = new WindowsMediaPlayer();
                    sonido.URL = Application.StartupPath + @"\SoundTrack\07_The_Rising_Fighting_Spirit.mp3";
                    sonido.controls.play();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }

        }

        delegate int testDel();
        public void gameOver()
        {
            if (TorreE1.IsDisposed)
            {
                torresEliminadas++;
            }
            if (TorreE3.IsDisposed)
            {
                torresEliminadas++;
            }
            if (TorreE2.IsDisposed)
            {
                torresEliminadas++;
            }
            if (TorreA1.IsDisposed)
            {
                torresRestantes++;
            }
            if (TorreA3.IsDisposed)
            {
                torresRestantes++;
            }
            if (TorreA2.IsDisposed)
            {
                torresRestantes++;
            }
            OperacionesCartas oc = new OperacionesCartas();
            oc.Nombre = labelUser.Text;
            oc.TorresRestantes = torresRestantes;
            oc.TorresEliminadas = torresEliminadas;
            foreach (Control item in this.Controls)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Invoke(new Action(() => item.Tag=false));
                    item.Dispose();
                }));
            }

            if (lost)
            {
                MessageBox.Show("Has perdido", "Game Over");
                if (oc.AgregarGanador())
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
                MessageBox.Show("Has ganado", "Game Over");
                if (oc.AgregarPerdedor())
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Partida agregada exitosamente al historial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            sonido.close();
            MessageBox.Show("Gracias por jugar");
            Application.Exit();
        }
        #region DragDrop
        private void Cartas_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox list = sender as ListBox;
            listBox1.DoDragDrop(list.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (vidaA2.Value == 0)
            {
                lost = true;
                gameOver();
            }
            else if (vidaE2.Value == 0)
            {
                gameOver();
            } else
            {
                cartas++;
                string texto = (string)e.Data.GetData(DataFormats.Text).ToString();
                switch (texto)
                {
                    case "Naruto":
                        Naruto naruto = new Naruto();
                        naruto.Tag = true;
                        naruto.vivo = true;
                        naruto.movimiento = true;
                        naruto.Location = new Point(e.X - this.Location.X - naruto.Width, e.Y - this.Location.Y - naruto.Height);
                        panel1.Controls.Add(naruto);
                        Thread thread = new Thread(mover);
                        thread.Start(naruto);
                        break;
                    case "Itachi":
                        listBox1.Items.Remove("Itachi");
                        Itachi itachi = new Itachi();
                        itachi.Tag = true;
                        itachi.vivo = true;
                        itachi.movimiento = true;
                        itachi.Location = new Point(e.X - this.Location.X - itachi.Width, e.Y - this.Location.Y - itachi.Height);
                        panel1.Controls.Add(itachi);
                        Thread thread2 = new Thread(mover);
                        thread2.Start(itachi);
                        break;
                    case "Choji":
                        choji choji = new choji();
                        choji.Tag = true;
                        choji.vivo = true;
                        choji.movimiento = true;
                        choji.Location = new Point(e.X - this.Location.X - choji.Width, e.Y - this.Location.Y - choji.Height);
                        panel1.Controls.Add(choji);
                        Thread thread3 = new Thread(mover);
                        thread3.Start(choji);
                        break;
                    case "Sasuke":
                        Sasuke sasuke = new Sasuke();
                        sasuke.Tag = true;
                        sasuke.vivo = true;
                        sasuke.movimiento = true;
                        sasuke.Location = new Point(e.X - this.Location.X - sasuke.Width, e.Y - this.Location.Y - sasuke.Height);
                        panel1.Controls.Add(sasuke);
                        Thread thread4 = new Thread(mover);
                        thread4.Start(sasuke);
                        break;
                    case "RockLee":
                        RockLee rockLee = new RockLee();
                        rockLee.Tag = true;
                        rockLee.vivo = true;
                        rockLee.movimiento = true;
                        rockLee.Location = new Point(e.X - this.Location.X - rockLee.Width, e.Y - this.Location.Y - rockLee.Height);
                        panel1.Controls.Add(rockLee);
                        Thread thread5 = new Thread(mover);
                        thread5.Start(rockLee);
                        break;
                    case "Kakashi":
                        Kakashi kakashi = new Kakashi();
                        kakashi.Tag = true;
                        kakashi.vivo = true;
                        kakashi.movimiento = true;
                        kakashi.Location = new Point(e.X - this.Location.X - kakashi.Width, e.Y - this.Location.Y - kakashi.Height);
                        panel1.Controls.Add(kakashi);
                        Thread thread6 = new Thread(mover);
                        thread6.Start(kakashi);
                        break;
                }
                Random rnd = new Random();
                int i = rnd.Next(0, 4);
                int t = rnd.Next(0, 2);

                Itachi itachiE = new Itachi();
                Naruto narutoE = new Naruto();
                Sasuke sasukeE = new Sasuke();
                Kakashi kakashiE = new Kakashi();
                RockLee rockLeeE = new RockLee();
                switch (t)
                {
                    case 0:
                        itachiE.Location = new Point(TorreE1.Location.X + itachiE.Width / 2, TorreE1.Location.Y - itachiE.Height / 2);
                        narutoE.Location = new Point(TorreE1.Location.X + narutoE.Width / 2, TorreE1.Location.Y - narutoE.Height / 2);
                        sasukeE.Location = new Point(TorreE1.Location.X + sasukeE.Width / 2, TorreE1.Location.Y - sasukeE.Height / 2);
                        kakashiE.Location = new Point(TorreE1.Location.X + kakashiE.Width / 2, TorreE1.Location.Y - kakashiE.Height / 2);
                        rockLeeE.Location = new Point(TorreE1.Location.X + rockLeeE.Width / 2, TorreE1.Location.Y - rockLeeE.Height / 2);
                        break;
                    case 1:
                        itachiE.Location = new Point(TorreE2.Location.X + itachiE.Width / 2, TorreE2.Location.Y - itachiE.Height / 2);
                        narutoE.Location = new Point(TorreE2.Location.X + narutoE.Width / 2, TorreE2.Location.Y - narutoE.Height / 2);
                        sasukeE.Location = new Point(TorreE2.Location.X + sasukeE.Width / 2, TorreE2.Location.Y - sasukeE.Height / 2);
                        kakashiE.Location = new Point(TorreE2.Location.X + kakashiE.Width / 2, TorreE2.Location.Y - kakashiE.Height / 2);
                        rockLeeE.Location = new Point(TorreE2.Location.X + rockLeeE.Width / 2, TorreE2.Location.Y - rockLeeE.Height / 2);
                        break;
                    case 2:
                        itachiE.Location = new Point(TorreE3.Location.X + itachiE.Width / 2, TorreE3.Location.Y - itachiE.Height / 2);
                        narutoE.Location = new Point(TorreE3.Location.X + narutoE.Width / 2, TorreE3.Location.Y - narutoE.Height / 2);
                        sasukeE.Location = new Point(TorreE3.Location.X + sasukeE.Width / 2, TorreE3.Location.Y - sasukeE.Height / 2);
                        kakashiE.Location = new Point(TorreE3.Location.X + kakashiE.Width / 2, TorreE3.Location.Y - kakashiE.Height / 2);
                        rockLeeE.Location = new Point(TorreE3.Location.X + rockLeeE.Width / 2, TorreE3.Location.Y - rockLeeE.Height / 2);
                        break;
                }
                switch (i)
                {
                    case 0:
                        itachiE.Tag = true;
                        itachiE.vivo = true;
                        itachiE.movimiento = true;
                        itachiE.pictureBox1.Image = Properties.Resources.Itachi_walkI;
                        panel1.Controls.Add(itachiE);
                        Thread thread2 = new Thread(moverEnemigo);
                        thread2.Start(itachiE);
                        break;
                    case 1:
                        narutoE.Tag = true;
                        narutoE.vivo = true;
                        narutoE.movimiento = true;
                        narutoE.pictureBox1.Image = Properties.Resources.naruto_walk_I;
                        panel1.Controls.Add(narutoE);
                        Thread thread = new Thread(moverEnemigo);
                        thread.Start(narutoE);
                        break;
                    case 2:
                        sasukeE.Tag = true;
                        sasukeE.vivo = true;
                        sasukeE.movimiento = true;
                        sasukeE.pictureBox1.Image = Properties.Resources.sasuke_run_I;
                        panel1.Controls.Add(sasukeE);
                        Thread thread4 = new Thread(moverEnemigo);
                        thread4.Start(sasukeE);
                        break;
                    case 3:
                        kakashiE.Tag = true;
                        kakashiE.vivo = true;
                        kakashiE.movimiento = true;
                        kakashiE.pictureBox1.Image = Properties.Resources.kakashi_run_I;
                        panel1.Controls.Add(kakashiE);
                        Thread thread6 = new Thread(moverEnemigo);
                        thread6.Start(kakashiE);
                        break;
                    case 4:
                        rockLeeE.Tag = true;
                        rockLeeE.vivo = true;
                        rockLeeE.movimiento = true;
                        rockLeeE.pictureBox1.Image = Properties.Resources.rocklee_walk_I;
                        panel1.Controls.Add(rockLeeE);
                        Thread thread5 = new Thread(moverEnemigo);
                        thread5.Start(rockLeeE);
                        break;
                }
            }
        }
        #endregion

        #region MovimientoAliado
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
                    Control con = c as Control;
                    con.Location = new Point(x, y);
                }
            }
            catch { Application.ExitThread(); }
        }
        public void mover(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                if (control.GetType().ToString() == "Juegos.Itachi")
                {
                    Itachi itachi = control as Itachi;
                    while (itachi.movimiento)
                    {
                        if (itachi.Bounds.IntersectsWith(panelRLE1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreE1.Location.Y);
                            }));
                            ataque(itachi, TorreE1, vidaE1, panelRLE1);
                        }
                        if (itachi.Bounds.IntersectsWith(panelRLE2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreE2.Location.Y);
                            }));
                            ataque(itachi, TorreE2, vidaE2, panelRLE2);
                        }
                        if (itachi.Bounds.IntersectsWith(panelRLE3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreE3.Location.Y);
                            }));
                            ataque(itachi, TorreE3, vidaE3, panelRLE3);
                        }
                        if (itachi.Location.X + 20 > this.Width)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.movimiento = false;
                                itachi.Dispose();
                            }));
                        }
                        else
                        {
                            x += 10;
                            m_mover(control, x, y);
                        }
                        Thread.Sleep(500);
                    }
                    if (!itachi.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            itachi.movimiento = false;
                            itachi.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Naruto")
                {
                    Naruto naruto = control as Naruto;
                    while (naruto.movimiento)
                    {
                        if (naruto.Bounds.IntersectsWith(TorreE1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreE1.Location.Y);
                            }));
                            ataque(naruto, TorreE1, vidaE1, panelRLE1);
                        }
                        if (naruto.Bounds.IntersectsWith(TorreE2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreE2.Location.Y);
                            }));
                            ataque(naruto, TorreE2, vidaE2, panelRLE2);
                        }
                        if (naruto.Bounds.IntersectsWith(TorreE3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreE3.Location.Y);
                            }));
                            ataque(naruto, TorreE3, vidaE3, panelRLE3);
                        }
                        if (naruto.Location.X + 20 > this.Width)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.movimiento = false;
                                naruto.Dispose();
                            }));
                        }
                        else
                        {
                            x += 15;
                            m_mover(control, x, y);
                        }
                        Thread.Sleep(400);
                    }
                    if (!naruto.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            naruto.movimiento = false;
                            naruto.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.choji")
                {
                    choji choji = control as choji;
                    while (choji.movimiento)
                    {
                        x += 5;
                        m_mover(control, x, y);
                        Thread.Sleep(500);
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Sasuke")
                {
                    Sasuke sasuke = control as Sasuke;
                    while (sasuke.movimiento)
                    {
                        if (sasuke.Bounds.IntersectsWith(panelRLE1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreE1.Location.Y);
                            }));
                            ataque(sasuke, TorreE1, vidaE1, panelRLE1);
                        }
                        if (sasuke.Bounds.IntersectsWith(panelRLE2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreE2.Location.Y);
                            }));
                            ataque(sasuke, TorreE2, vidaE2, panelRLE2);
                        }
                        if (sasuke.Bounds.IntersectsWith(panelRLE3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreE3.Location.Y);
                            }));
                            ataque(sasuke, TorreE3, vidaE3, panelRLE3);
                        }
                        if (sasuke.Location.X + 20 > this.Width)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.movimiento = false;
                                sasuke.Dispose();
                            }));
                        }
                        else
                        {
                            x += 5;
                            m_mover(control, x, y);
                        }
                        Thread.Sleep(150);
                    }
                    if (!sasuke.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            sasuke.movimiento = false;
                            sasuke.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.RockLee")
                {
                    RockLee rockLee = control as RockLee;
                    while (rockLee.movimiento)
                    {
                        if (rockLee.Bounds.IntersectsWith(TorreE1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreE1.Location.Y);
                            }));
                            ataque(rockLee, TorreE1, vidaE1, panelRLE1);
                        }
                        if (rockLee.Bounds.IntersectsWith(TorreE2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreE2.Location.Y);
                            }));
                            ataque(rockLee, TorreE2, vidaE2, panelRLE2);
                        }
                        if (rockLee.Bounds.IntersectsWith(TorreE3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreE3.Location.Y);
                            }));
                            ataque(rockLee, TorreE3, vidaE3, panelRLE3);
                        }
                        if (rockLee.Location.X + 20 > this.Width)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.movimiento = false;
                                rockLee.Dispose();
                            }));
                        }
                        else
                        {
                            x += 10;
                            m_mover(control, x, y);
                        }
                        Thread.Sleep(320);
                    }
                    if (!rockLee.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            rockLee.movimiento = false;
                            rockLee.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Kakashi")
                {
                    Kakashi kakashi = control as Kakashi;
                    while (kakashi.movimiento)
                    {
                        if (kakashi.Bounds.IntersectsWith(panelRLE1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreE1.Location.Y);
                            }));
                            ataque(kakashi, TorreE1, vidaE1, panelRLE1);
                        }
                        if (kakashi.Bounds.IntersectsWith(panelRLE2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreE2.Location.Y);
                            }));
                            ataque(kakashi, TorreE2, vidaE2, panelRLE2);
                        }
                        if (kakashi.Bounds.IntersectsWith(panelRLE3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreE3.Location.Y);
                            }));
                            ataque(kakashi, TorreE3, vidaE3, panelRLE3);
                        }
                        if (kakashi.Location.X + 20 > this.Width)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.movimiento = false;
                                kakashi.Dispose();
                            }));
                        }
                        else
                        {
                            x += 15;
                            m_mover(control, x, y);
                        }
                        Thread.Sleep(400);
                    }
                    if (!kakashi.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            kakashi.movimiento = false;
                            kakashi.Dispose();
                        }));
                    }
                }
            }
            catch
            {
                Application.ExitThread();
            }
        }
        #endregion

        #region MovimientoEnemigo
        private void m_moverEnemigo(Control c, int x, int y)
        {
            try
            {
                if (InvokeRequired)
                {
                    d_moverEnemigo dm = new d_moverEnemigo(m_moverEnemigo);
                    object[] parametros = new object[] { c, x, y };
                    Invoke(dm, parametros);
                }
                else
                {
                    Control con = c as Control;
                    con.Location = new Point(x, y);
                    if (!(bool)con.Tag)
                    {
                        con.Invoke(new MethodInvoker(delegate ()
                        {
                            con.Dispose();
                        }));
                    }
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void moverEnemigo(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                if (control.GetType().ToString() == "Juegos.Itachi")
                {
                    Itachi itachi = control as Itachi;
                    while (itachi.movimiento)
                    {
                        if (itachi.Bounds.IntersectsWith(panelRLA1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreA1.Location.Y);
                            }));
                            ataqueEnemigo(itachi, TorreA1, vidaA1, panelRLA1);
                        }
                        if (itachi.Bounds.IntersectsWith(panelRLA2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreA2.Location.Y);
                            }));
                            ataqueEnemigo(itachi, TorreA2, vidaE2, panelRLA2);
                        }
                        if (itachi.Bounds.IntersectsWith(panelRLA3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.Location = new Point(itachi.Location.X, TorreA3.Location.Y);
                            }));
                            ataqueEnemigo(itachi, TorreA3, vidaA3, panelRLA3);
                        }

                        if (itachi.Location.X + 20 < 0)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                itachi.movimiento = false;
                                itachi.Dispose();
                            }));
                        }
                        else
                        {
                            x -= 10;
                            m_moverEnemigo(control, x, y);
                        }
                        Thread.Sleep(500);
                    }
                    if (!itachi.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            itachi.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Naruto")
                {
                    Naruto naruto = control as Naruto;
                    while (naruto.movimiento)
                    {
                        if (naruto.Bounds.IntersectsWith(TorreA1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreA1.Location.Y);
                            }));
                            ataqueEnemigo(naruto, TorreA1, vidaA1, panelRLA1);
                        }
                        if (naruto.Bounds.IntersectsWith(TorreA2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreA2.Location.Y);
                            }));
                            ataqueEnemigo(naruto, TorreA2, vidaA2, panelRLA2);
                        }
                        if (naruto.Bounds.IntersectsWith(TorreA3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.Location = new Point(naruto.Location.X, TorreA3.Location.Y);
                            }));
                            ataqueEnemigo(naruto, TorreA3, vidaA3, panelRLA3);
                        }

                        if (naruto.Location.X + 20 < 0)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                naruto.movimiento = false;
                                naruto.Dispose();
                            }));
                        }
                        else
                        {
                            x -= 15;
                            m_moverEnemigo(control, x, y);
                        }
                        Thread.Sleep(400);
                    }
                    if (!naruto.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            naruto.movimiento = false;
                            naruto.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.choji")
                {
                    choji choji = control as choji;
                    while (choji.movimiento)
                    {
                        x += 5;
                        m_mover(control, x, y);
                        Thread.Sleep(500);
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Sasuke")
                {
                    Sasuke sasuke = control as Sasuke;
                    while (sasuke.movimiento)
                    {
                        if (sasuke.Bounds.IntersectsWith(panelRLA1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreA1.Location.Y);
                            }));
                            ataqueEnemigo(sasuke, TorreA1, vidaA1, panelRLA1);
                        }
                        if (sasuke.Bounds.IntersectsWith(panelRLA2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreA2.Location.Y);
                            }));
                            ataqueEnemigo(sasuke, TorreA2, vidaA2, panelRLA2);
                        }
                        if (sasuke.Bounds.IntersectsWith(panelRLA3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.Location = new Point(sasuke.Location.X, TorreA3.Location.Y);
                            }));
                            ataqueEnemigo(sasuke, TorreA3, vidaA3, panelRLA3);
                        }
                        if (sasuke.Location.X + 20 < 0)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                sasuke.movimiento = false;
                                sasuke.Dispose();
                            }));
                        }
                        else
                        {
                            x -= 5;
                            m_moverEnemigo(control, x, y);
                        }
                        Thread.Sleep(150);
                    }
                    if (!sasuke.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            sasuke.movimiento = false;
                            sasuke.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.RockLee")
                {
                    RockLee rockLee = control as RockLee;
                    while (rockLee.movimiento)
                    {
                        if (rockLee.Bounds.IntersectsWith(TorreA1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreA1.Location.Y);
                            }));
                            ataqueEnemigo(rockLee, TorreA1, vidaA1, panelRLA1);
                        }
                        if (rockLee.Bounds.IntersectsWith(TorreA2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreA2.Location.Y);
                            }));
                            ataqueEnemigo(rockLee, TorreA2, vidaA2, panelRLA2);
                        }
                        if (rockLee.Bounds.IntersectsWith(TorreA3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.Location = new Point(rockLee.Location.X, TorreA3.Location.Y);
                            }));
                            ataqueEnemigo(rockLee, TorreA3, vidaA3, panelRLA3);
                        }
                        if (rockLee.Location.X + 20 < 0)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                rockLee.movimiento = false;
                                rockLee.Dispose();
                            }));
                        }
                        else
                        {
                            x -= 10;
                            m_moverEnemigo(control, x, y);
                        }
                        Thread.Sleep(320);
                    }
                    if (!rockLee.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            rockLee.movimiento = false;
                            rockLee.Dispose();
                        }));
                    }
                }
                else if (control.GetType().ToString() == "Juegos.Kakashi")
                {
                    Kakashi kakashi = control as Kakashi;
                    while (kakashi.movimiento)
                    {
                        if (kakashi.Bounds.IntersectsWith(panelRLA1.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreA1.Location.Y);
                            }));
                            ataqueEnemigo(kakashi, TorreA1, vidaA1, panelRLA1);
                        }
                        if (kakashi.Bounds.IntersectsWith(panelRLA2.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreA2.Location.Y);
                            }));
                            ataqueEnemigo(kakashi, TorreA2, vidaA2, panelRLA2);
                        }
                        if (kakashi.Bounds.IntersectsWith(panelRLA3.Bounds))
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.Location = new Point(kakashi.Location.X, TorreA3.Location.Y);
                            }));
                            ataqueEnemigo(kakashi, TorreA3, vidaA3, panelRLA3);
                        }
                        if (kakashi.Location.X + 20 < 0)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                kakashi.movimiento = false;
                                kakashi.Dispose();
                            }));
                        }
                        else
                        {
                            x -= 15;
                            m_moverEnemigo(control, x, y);
                        }
                        Thread.Sleep(400);
                    }
                    if (!kakashi.vivo)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            kakashi.movimiento = false;
                            kakashi.Dispose();
                        }));
                    }
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        #endregion

        #region AtacksAliados
        public void atack(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                while ((bool)control.Tag)
                {
                    x += 4;
                    m_mover(control, x, y);
                    Thread.Sleep(400);
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void atackE(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                while ((bool)control.Tag)
                {
                    x -= 5;
                    m_mover(control, x, y);
                    Thread.Sleep(100);
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void ataque(Object per, PictureBox torre, ProgressBar vida, Panel panel)
        {
            try
            {
                if (per.GetType().ToString() == "Juegos.Itachi")
                {
                    Itachi itachi = per as Itachi;
                    while (itachi.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackE);
                        thread.Start(picture);
                        //Ataque del dios itachi
                        itachi.atacando = true;
                        itachi.movimiento = false;
                        itachi.pictureBox1.Image = Properties.Resources.Itachi_PrepAtackD;
                        PictureBox pictureBox = new PictureBox
                        {
                            Tag = true,
                            Image = Properties.Resources.Amaterasu,
                            Size = new Size(40, 40),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        pictureBox.Location = new Point(itachi.Location.X + pictureBox.Width, itachi.Location.Y + pictureBox.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(pictureBox);
                        }));
                        Thread t1 = new Thread(atack);
                        t1.Start(pictureBox);
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            itachi.Invoke(new MethodInvoker(delegate ()
                            {
                                if (itachi.progressBar1.Value > 10)
                                {
                                    itachi.vida();
                                }
                                else
                                {
                                    itachi.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(6000);
                        if (pictureBox.Bounds.IntersectsWith(torre.Bounds))
                        {
                            pictureBox.Invoke(new MethodInvoker(delegate ()
                            {
                                pictureBox.Tag = false;
                                pictureBox.Dispose();
                            }));

                            vida.Invoke(new MethodInvoker(delegate ()
                            {
                                if (vida.Value > 50)
                                {
                                    vida.Value -= 50;
                                }
                                else
                                {
                                    vida.Value = 0;
                                }
                                if (vida.Value == 0)
                                {
                                    torre.Dispose();
                                    vida.Visible = false;
                                    panel.Dispose();
                                    itachi.movimiento = true;
                                    itachi.vivo = false;
                                    itachi.pictureBox1.Image = Properties.Resources.Itachi_walkD;
                                    Thread t2 = new Thread(mover);
                                    t2.Start(itachi);
                                }
                            }));
                        }
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Naruto")
                {
                    Naruto naruto = per as Naruto;
                    while (naruto.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackE);
                        thread.Start(picture);
                        //Ataque del narudios
                        naruto.atacando = true;
                        naruto.movimiento = false;
                        naruto.pictureBox1.Image = Properties.Resources.NarutoAtack;
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            naruto.Invoke(new MethodInvoker(delegate ()
                            {
                                if (naruto.progressBar1.Value > 10)
                                {
                                    naruto.vida();
                                }
                                else
                                {
                                    naruto.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(2000);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 10)
                            {
                                vida.Value -= 10;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                naruto.movimiento = true;
                                naruto.vivo = false;
                                naruto.pictureBox1.Image = Properties.Resources.naruto_walk_D;
                                Thread t2 = new Thread(mover);
                                t2.Start(naruto);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Sasuke")
                {
                    Sasuke sasuke = per as Sasuke;
                    while (sasuke.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackE);
                        thread.Start(picture);
                        //Ataque del narudios
                        sasuke.atacando = true;
                        sasuke.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            sasuke.Size = new Size(140, 80);
                            sasuke.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            sasuke.pictureBox1.Image = Properties.Resources.sasuke_attack_D;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            sasuke.Invoke(new MethodInvoker(delegate ()
                            {
                                if (sasuke.progressBar1.Value > 10)
                                {
                                    sasuke.vida();
                                }
                                else
                                {
                                    sasuke.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(2000);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 35)
                            {
                                vida.Value -= 35;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                sasuke.movimiento = true;
                                sasuke.vivo = false;
                                sasuke.pictureBox1.Image = Properties.Resources.sasuke_run_D;
                                Thread t2 = new Thread(mover);
                                t2.Start(sasuke);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.RockLee")
                {
                    RockLee rockLee = per as RockLee;
                    while (rockLee.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackE);
                        thread.Start(picture);
                        //Ataque del narudios
                        rockLee.atacando = true;
                        rockLee.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            rockLee.pictureBox1.Image = Properties.Resources.rocklee_attack_D;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            rockLee.Invoke(new MethodInvoker(delegate ()
                            {
                                if (rockLee.progressBar1.Value > 10)
                                {
                                    rockLee.vida();
                                }
                                else
                                {
                                    rockLee.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(1500);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 25)
                            {
                                vida.Value -= 25;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                rockLee.movimiento = true;
                                rockLee.vivo = false;
                                rockLee.pictureBox1.Image = Properties.Resources.rocklee_walk_D;
                                Thread t2 = new Thread(mover);
                                t2.Start(rockLee);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Kakashi")
                {
                    Kakashi kakashi = per as Kakashi;
                    while (kakashi.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackE);
                        thread.Start(picture);
                        //Ataque del narudios
                        kakashi.atacando = true;
                        kakashi.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            kakashi.Size = new Size(150, 100);
                            kakashi.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            kakashi.pictureBox1.Image = Properties.Resources.kakashi_attack_D;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            kakashi.Invoke(new MethodInvoker(delegate ()
                            {
                                if (kakashi.progressBar1.Value > 10)
                                {
                                    kakashi.vida();
                                }
                                else
                                {
                                    kakashi.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(1500);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 25)
                            {
                                vida.Value -= 25;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                kakashi.movimiento = true;
                                kakashi.vivo = false;
                                kakashi.pictureBox1.Image = Properties.Resources.kakashi_run_D;
                                Thread t2 = new Thread(mover);
                                t2.Start(kakashi);
                            }
                        }));
                    }
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        #endregion

        #region AtacksEnemigos
        public void atackEnemigo(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                while ((bool)control.Tag)
                {
                    x -= 4;
                    m_moverEnemigo(control, x, y);
                    Thread.Sleep(400);
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void atackEEnemigo(Object o)
        {
            try
            {
                Control control = o as Control;
                int x = control.Location.X;
                int y = control.Location.Y;
                while ((bool)control.Tag)
                {
                    x += 5;
                    m_moverEnemigo(control, x, y);
                    Thread.Sleep(100);
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void ataqueEnemigo(Object per, PictureBox torre, ProgressBar vida, Panel panel)
        {
            try
            {
                if (per.GetType().ToString() == "Juegos.Itachi")
                {
                    Itachi itachi = per as Itachi;
                    while (itachi.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width + 100, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackEEnemigo);
                        thread.Start(picture);
                        //Ataque del dios itachi
                        itachi.atacando = true;
                        itachi.movimiento = false;
                        itachi.pictureBox1.Image = Properties.Resources.Itachi_PrepAtackI;
                        PictureBox pictureBox = new PictureBox
                        {
                            Tag = true,
                            Image = Properties.Resources.Amaterasu,
                            Size = new Size(40, 40),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        pictureBox.Location = new Point(itachi.Location.X - pictureBox.Width, itachi.Location.Y + pictureBox.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(pictureBox);
                        }));
                        Thread t1 = new Thread(atackEnemigo);
                        t1.Start(pictureBox);
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            itachi.Invoke(new MethodInvoker(delegate ()
                            {
                                if (itachi.progressBar1.Value > 10)
                                {
                                    itachi.vida();
                                }
                                else
                                {
                                    itachi.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(6000);
                        if (pictureBox.Bounds.IntersectsWith(torre.Bounds))
                        {
                            pictureBox.Invoke(new MethodInvoker(delegate ()
                            {
                                pictureBox.Tag = false;
                                pictureBox.Dispose();
                            }));

                            vida.Invoke(new MethodInvoker(delegate ()
                            {
                                if (vida.Value > 50)
                                {
                                    vida.Value -= 50;
                                }
                                else
                                {
                                    vida.Value = 0;
                                }
                                if (vida.Value == 0)
                                {
                                    torre.Dispose();
                                    vida.Visible = false;
                                    panel.Dispose();
                                    itachi.movimiento = true;
                                    itachi.vivo = false;
                                    itachi.pictureBox1.Image = Properties.Resources.Itachi_walkI;
                                    Thread t2 = new Thread(moverEnemigo);
                                    t2.Start(itachi);
                                }
                            }));
                        }
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Naruto")
                {
                    Naruto naruto = per as Naruto;
                    while (naruto.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width + 100, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackEEnemigo);
                        thread.Start(picture);
                        //Ataque del narudios
                        naruto.atacando = true;
                        naruto.movimiento = false;
                        naruto.pictureBox1.Image = Properties.Resources.NarutoAtack;
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            naruto.Invoke(new MethodInvoker(delegate ()
                            {
                                if (naruto.progressBar1.Value > 10)
                                {
                                    naruto.vida();
                                }
                                else
                                {
                                    naruto.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(2000);

                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 10)
                            {
                                vida.Value -= 10;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                naruto.movimiento = true;
                                naruto.vivo = false;
                                naruto.pictureBox1.Image = Properties.Resources.naruto_walk_I;
                                Thread t2 = new Thread(moverEnemigo);
                                t2.Start(naruto);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Sasuke")
                {
                    Sasuke sasuke = per as Sasuke;
                    while (sasuke.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width + 100, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackEEnemigo);
                        thread.Start(picture);
                        //Ataque del narudios
                        sasuke.atacando = true;
                        sasuke.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            sasuke.Size = new Size(140, 80);
                            sasuke.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            sasuke.pictureBox1.Image = Properties.Resources.sasuke_attack_I;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            sasuke.Invoke(new MethodInvoker(delegate ()
                            {
                                if (sasuke.progressBar1.Value > 10)
                                {
                                    sasuke.vida();
                                }
                                else
                                {
                                    sasuke.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(2000);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 35)
                            {
                                vida.Value -= 35;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                sasuke.movimiento = true;
                                sasuke.vivo = false;
                                sasuke.pictureBox1.Image = Properties.Resources.sasuke_run_I;
                                Thread t2 = new Thread(moverEnemigo);
                                t2.Start(sasuke);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.RockLee")
                {
                    RockLee rockLee = per as RockLee;
                    while (rockLee.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width + 100, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackEEnemigo);
                        thread.Start(picture);
                        //Ataque del narudios
                        rockLee.atacando = true;
                        rockLee.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            rockLee.pictureBox1.Image = Properties.Resources.rocklee_attack_I;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            rockLee.Invoke(new MethodInvoker(delegate ()
                            {
                                if (rockLee.progressBar1.Value > 10)
                                {
                                    rockLee.vida();
                                }
                                else
                                {
                                    rockLee.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(1500);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 25)
                            {
                                vida.Value -= 25;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Dispose();
                                rockLee.movimiento = true;
                                rockLee.vivo = false;
                                rockLee.pictureBox1.Image = Properties.Resources.rocklee_walk_I;
                                Thread t2 = new Thread(moverEnemigo);
                                t2.Start(rockLee);
                            }
                        }));
                    }
                }
                else if (per.GetType().ToString() == "Juegos.Kakashi")
                {
                    Kakashi kakashi = per as Kakashi;
                    while (kakashi.vivo)
                    {
                        //Ataque de la torre
                        PictureBox picture = new PictureBox
                        {
                            BackColor = Color.Red,
                            Size = new Size(20, 10),
                            Tag = true
                        };
                        picture.Location = new Point(torre.Location.X + picture.Width + 100, torre.Location.Y + picture.Height);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            panel1.Controls.Add(picture);
                        }));
                        Thread thread = new Thread(atackEEnemigo);
                        thread.Start(picture);
                        //Ataque del narudios
                        kakashi.atacando = true;
                        kakashi.movimiento = false;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            kakashi.Size = new Size(150, 100);
                            kakashi.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            kakashi.pictureBox1.Image = Properties.Resources.kakashi_attack_I;
                        }));
                        Thread.Sleep(2000);
                        if (picture.Bounds.IntersectsWith(panel.Bounds))
                        {
                            kakashi.Invoke(new MethodInvoker(delegate ()
                            {
                                if (kakashi.progressBar1.Value > 10)
                                {
                                    kakashi.vida();
                                }
                                else
                                {
                                    kakashi.vivo = false;
                                }
                            }));
                            picture.Invoke(new MethodInvoker(delegate ()
                            {
                                picture.Tag = false;
                                picture.Dispose();
                            }));
                        }
                        Thread.Sleep(1500);
                        vida.Invoke(new MethodInvoker(delegate ()
                        {
                            if (vida.Value > 25)
                            {
                                vida.Value -= 25;
                            }
                            else
                            {
                                vida.Value = 0;
                            }
                            if (vida.Value == 0)
                            {
                                torre.Enabled = false;
                                torre.Dispose();
                                vida.Visible = false;
                                panel.Enabled = false;
                                panel.Dispose();
                                kakashi.movimiento = true;
                                kakashi.vivo = false;
                                kakashi.pictureBox1.Image = Properties.Resources.kakashi_run_I;
                                Thread t2 = new Thread(moverEnemigo);
                                t2.Start(kakashi);
                            }
                        }));
                    }
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        #endregion

        private void Cartas_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Invoke(new Action(() => item.Tag = false));
                    item.Dispose();
                }));
            }
            sonido.close();
            Application.Exit();
        }

        private void TorreE1_Click(object sender, EventArgs e)
        {

        }
    }
}
