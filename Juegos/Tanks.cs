using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using dll_videojuegos;

namespace Juegos
{
    public partial class Tanks : Form
    {
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        int tiempo = 150;
        public PictureBox player1 = new PictureBox();
        public PictureBox player2= new PictureBox();
        public BackgroundWorker bwGeneral;
        public bool preparando=true;
        public string recieve;
        public string TextSend;
        public char Press,PressRecieve;
        public bool jugando = false,p1=false,p2=false,aliado,inicio=true;
        public bool up = false, down = true, left = false, right = false;
        public bool up2 = false, down2 = true, left2 = false, right2 = false;
        public delegate void d_mover(Control c, int x, int y);
        public int disparosdadosj1 = 0, disparosdadosj2 = 0, disparosrealizadosj1 = 0, disparosrealizadosj2 = 0;
        public int victoriasj1 = 0, victoriasj2 = 0;
        
        public Tanks()
        {
            InitializeComponent();
            jugando = false;
            p1 = false;
            p2=false;
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach(IPAddress address in localIP)
            {
                if (address.AddressFamily==AddressFamily.InterNetwork)
                {
                    txtServerIP.Text = address.ToString();
                }
            }
        }
        private void Iniciar_Click(object sender, EventArgs e)
        {
            p1 = true;
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(txtServerPort.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.WorkerSupportsCancellation = true;
        }

        private void Tanks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Restart();
        }

        private void Conectar_Click(object sender, EventArgs e)
        {
            p2 = true;
            client = new TcpClient();
            IPEndPoint IPEnd = new IPEndPoint(IPAddress.Parse(txtClienteIP.Text),int.Parse(txtClientePort.Text));
            try
            {
                client.Connect(IPEnd);
                if (client.Connected)
                {
                    txtChat.AppendText("Conectado al servidor\n");
                    STR = new StreamReader(client.GetStream());
                    STW = new StreamWriter(client.GetStream());
                    STW.AutoFlush = true;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker2.WorkerSupportsCancellation=true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)

        {
            while (client.Connected)
            {
                try
                {
                    if (preparando)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            player1.Image = Properties.Resources.tanque1D;
                            player1.Location = new Point(50, 150);
                            player1.Size = new Size(60, 100);
                            player1.SizeMode = PictureBoxSizeMode.Zoom;
                            player1.Visible = false;
                            player1.Tag = false;
                            this.Controls.Add(player1);

                            player2.Image = Properties.Resources.tanque2D;
                            player2.Location = new Point(250, 150);
                            player2.Size = new Size(60, 100);
                            player2.SizeMode = PictureBoxSizeMode.Zoom;
                            player2.Visible = false;
                            player2.Tag = false;
                            this.Controls.Add(player2);
                            lblPA.Text = "Puntos a favor:0";
                            lblPE.Text = "Puntos en contra=0";
                            lblTiempo.Text = "150";
                            lblPA.Tag = 0;
                            lblPE.Tag = 0;

                        }));
                        preparando = false;
                    }
                    
                    if (jugando)
                    {
                        PressRecieve = (char)STR.Read();
                        if (p1)
                        {
                            if (PressRecieve == (char)Keys.A)
                            {
                                    this.player2.Invoke(new MethodInvoker(delegate ()
                                    {
                                        player2.Image = Properties.Resources.tanque2L;
                                        player2.Size = new Size(100, 60);
                                        left2 = true;
                                        right2 = false;
                                        up2 = false;
                                        down2 = false;
                                        player2.Location = new Point(player2.Location.X-10, player2.Location.Y);
                                    }));
                            }
                            if (PressRecieve == (char)Keys.D)
                            {
                                    this.player2.Invoke(new MethodInvoker(delegate ()
                                    {
                                        player2.Image = Properties.Resources.tanque2R;
                                        player2.Size = new Size(100, 60);
                                        down2 = false;
                                        left2 = false;
                                        up2 = false;
                                        right2 = true;
                                        player2.Location = new Point(player2.Location.X+10 , player2.Location.Y);
                                    }));
                            }
                            if (PressRecieve == (char)Keys.W)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2U;
                                    player2.Size = new Size(60, 100);
                                    up2 = true;
                                    down2 = false;
                                    left2 = false;
                                    right2 = false;
                                    player2.Location = new Point(player2.Location.X, player2.Location.Y - 10);
                                }));
                            }
                            if (PressRecieve == (char)Keys.S)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2D;
                                    player2.Size = new Size(60, 100);
                                    up2 = false;
                                    down2 = true;
                                    left2 = false;
                                    right2 = false;
                                    player2.Location = new Point(player2.Location.X, player2.Location.Y + 10);
                                }));
                            }
                            if (PressRecieve == (char)Keys.O)
                            {
                                disparosrealizadosj2++;
                                PictureBox pb = new PictureBox();
                                pb.BackColor = Color.Transparent;
                                pb.Image = Properties.Resources.BalaTanqueE;
                                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                                pb.Size = new Size(15, 15);
                                pb.Tag = true;
                                int x = player2.Location.X + (player2.Width / 2);
                                int y = player2.Location.Y + (player2.Height/2);
                                pb.Location = new Point(x, y);
                                pb.Visible = true;
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    this.Controls.Add(pb);
                                    //BackgroundWorker bw = new BackgroundWorker();
                                    //bw.DoWork += bwGeneral_DoWork;
                                    //bw.RunWorkerAsync(pb);
                                }));
                                Thread t2 = new Thread(moverBala);
                                t2.Start(pb);

                            }

                        }
                        if (p2)
                        {
                            if (PressRecieve == (char)Keys.A)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1L;
                                    player1.Size = new Size(100, 60);
                                    left = true;
                                    right = false;
                                    up = false;
                                    down = false;
                                    player1.Location = new Point(player1.Location.X - 10, player1.Location.Y);
                                }));
                            }
                            if (PressRecieve == (char)Keys.D)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1R;
                                    player1.Size = new Size(100, 60);
                                    down = false;
                                    left = false;
                                    up = false;
                                    right = true;
                                    player1.Location = new Point(player1.Location.X + 10, player1.Location.Y);
                                }));
                            }
                            if (PressRecieve == (char)Keys.W)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1U;
                                    player1.Size = new Size(60, 100);
                                    up = true;
                                    down = false;
                                    left = false;
                                    right = false;
                                    player1.Location = new Point(player1.Location.X, player1.Location.Y - 10);
                                }));
                            }
                            if (PressRecieve == (char)Keys.S)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1D;
                                    player1.Size = new Size(60, 100);
                                    up = false;
                                    down = true;
                                    left = false;
                                    right = false;
                                    player1.Location = new Point(player1.Location.X, player1.Location.Y + 10);
                                }));
                            }
                            if (PressRecieve == (char)Keys.O)
                            {
                                PictureBox pb = new PictureBox();
                                pb.BackColor = Color.Transparent;
                                pb.Image = Properties.Resources.BalaTanqueE;
                                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                                pb.Size = new Size(15, 15);
                                pb.Tag = true;
                                int x = player1.Location.X + (player1.Width / 2);
                                int y = player1.Location.Y + (player1.Height/2);
                                pb.Location = new Point(x, y);
                                pb.Visible = true;
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    this.Controls.Add(pb);
                                    //BackgroundWorker bw = new BackgroundWorker();
                                    //bw.DoWork += bwGeneral_DoWork;
                                    //bw.RunWorkerAsync(pb);
                                }));
                                Thread t2 = new Thread(moverBala);
                                t2.Start(pb);
                            }
                        }
                        aliado = false;
                    }
                    else
                    {
                        recieve = STR.ReadLine();
                        if (inicio)
                        {
                            if (p1)
                            {
                                lblOponent.Text = recieve;
                                inicio = false;
                            }
                            if (p2)
                            {
                                STW.WriteLine (""+ lblUser.Text);
                                inicio = false;
                            }
                        }
                        
                        if (recieve.Equals("VAMOS"))
                        {
                            player2.Tag = true;
                        }
                        else if(recieve!="")
                        {
                            this.txtChat.Invoke(new MethodInvoker(delegate ()
                            {
                                txtChat.AppendText("El:" + recieve + "\n");
                            }));
                        }
                        if (bool.Parse(player1.Tag.ToString()) && bool.Parse(player2.Tag.ToString()))
                        {
                            jugando = true;
                            //Thread T = new Thread(time);
                            //T.Start();
                            BackgroundWorker bw = new BackgroundWorker();
                            bw.DoWork += time;
                            bw.RunWorkerAsync();
                        }
                        recieve = "";
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message.ToString());
                }
            }
        }
        public void time(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (tiempo > 0)
                {
                    tiempo = int.Parse(lblTiempo.Text.ToString());
                    tiempo--;
                    lblTiempo.Invoke(new MethodInvoker(delegate ()
                    {
                        lblTiempo.Text = "" + tiempo;
                    }));
                    Thread.Sleep(1000);
                }
                //this.Invoke(new MethodInvoker(delegate ()
                //{
                //    this.Enabled = false;
                //}));
            }
            catch
            {
                if (p1)
                {
                    OperacionesTanques ot = new OperacionesTanques();
                    ot.Jugador1 = lblUser.Text;
                    ot.Jugador2 = lblOponent.Text;
                    ot.DisparosDadosJ1 = disparosdadosj1;
                    ot.DisparosDadosJ2 = disparosdadosj2;
                    ot.DisparosRealizadosJ1 = disparosrealizadosj1;
                    ot.DisparosRealizadosJ2 = disparosrealizadosj2;
                    ot.VictoriasJ1 = victoriasj1;
                    ot.VictoriasJ2 = victoriasj2;
                    if (ot.AgregarDisparos() && ot.AgregarPartidas())
                    {
                        MessageBox.Show("Partida agregada exitosamente al historial", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Partida agregada exitosamente al historial", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show("Apenas empezaba lo bueno :v");
            }
        }

        public void gameOver()
        {
            int pa = int.Parse(lblPA.Tag.ToString());
            int pe = int.Parse(lblPE.Tag.ToString());
            player1.Tag = false;
            player2.Tag = false;
            DialogResult DR;
            if (pa==pe)
            {
                 DR = MessageBox.Show("Empate" +
                    " \n ¿Desea reiniciar el juego?", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
            else if (pa<pe)
            {
                victoriasj2++;
                 DR = MessageBox.Show("Has perdido" +
                   " \n ¿Desea reiniciar el juego?", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
            else
            {
                victoriasj1++;
                 DR = MessageBox.Show("Has ganado" +
                  " \n ¿Desea reiniciar el juego?", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
            if (DR==DialogResult.Retry)
            {
                preparando = true;
                player1.Tag = true;
                STW.WriteLine("VAMOS");
            }
            else
            {
                if (p1)
                {
                    OperacionesTanques ot = new OperacionesTanques();
                    ot.Jugador1 = lblUser.Text;
                    ot.Jugador2 = lblOponent.Text;
                    ot.DisparosDadosJ1 = disparosdadosj1;
                    ot.DisparosDadosJ2 = disparosdadosj2;
                    ot.DisparosRealizadosJ1 = disparosrealizadosj1;
                    ot.DisparosRealizadosJ2 = disparosrealizadosj2;
                    ot.VictoriasJ1 = victoriasj1;
                    ot.VictoriasJ2 = victoriasj2;
                    if (ot.AgregarDisparos() && ot.AgregarPartidas())
                    {
                        MessageBox.Show("Info", "Partida agregada exitosamente al historial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "No se pudo agregar la partida al historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Application.Restart();
            }
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (client.Connected)
                {
                    if (jugando)
                    {
                        STW.WriteLine(Press);
                        if (p2)
                        {
                            if (Press == (char)Keys.A)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2L;
                                    player2.Size = new Size(100, 60);
                                    left2 = true;
                                    right2 = false;
                                    up2 = false;
                                    down2 = false;
                                    player2.Location = new Point(player2.Location.X - 10, player2.Location.Y);
                                }));
                            }
                            if (Press == (char)Keys.D)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2R;
                                    player2.Size = new Size(100, 60);
                                    down2 = false;
                                    left2 = false;
                                    up2 = false;
                                    right2 = true;
                                    player2.Location = new Point(player2.Location.X + 10, player2.Location.Y);
                                }));
                            }
                            if (Press == (char)Keys.W)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2U;
                                    player2.Size = new Size(60, 100);
                                    up2 = true;
                                    down2 = false;
                                    left2 = false;
                                    right2 = false;
                                    player2.Location = new Point(player2.Location.X, player2.Location.Y - 10);
                                }));
                            }
                            if (Press == (char)Keys.S)
                            {
                                this.player2.Invoke(new MethodInvoker(delegate ()
                                {
                                    player2.Image = Properties.Resources.tanque2D;
                                    player2.Size = new Size(60, 100);
                                    up2 = false;
                                    down2 = true;
                                    left2 = false;
                                    right2 = false;
                                    player2.Location = new Point(player2.Location.X, player2.Location.Y + 10);
                                }));
                            }
                            if (Press == (char)Keys.O)
                            {
                                PictureBox pb = new PictureBox();
                                pb.BackColor = Color.Transparent;
                                pb.Image = Properties.Resources.TanqueBalaA;
                                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                                pb.Size = new Size(15, 15);
                                pb.Tag = true;
                                int x = player2.Location.X + (player2.Width / 2);
                                int y = player2.Location.Y + (player2.Height / 2);
                                pb.Location = new Point(x, y);
                                pb.Visible = true;
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    this.Controls.Add(pb);
                                //BackgroundWorker bw = new BackgroundWorker();
                                //bw.DoWork += bwGeneral_DoWork;
                                //bw.RunWorkerAsync(pb);
                            }));
                                Thread t2 = new Thread(moverBala);
                                t2.Start(pb);

                            }

                        }
                        if (p1)
                        {
                            if (Press == (char)Keys.A)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1L;
                                    player1.Size = new Size(100, 60);
                                    left = true;
                                    right = false;
                                    up = false;
                                    down = false;
                                    player1.Location = new Point(player1.Location.X - 10, player1.Location.Y);
                                }));
                            }
                            if (Press == (char)Keys.D)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1R;
                                    player1.Size = new Size(100, 60);
                                    down = false;
                                    left = false;
                                    up = false;
                                    right = true;
                                    player1.Location = new Point(player1.Location.X + 10, player1.Location.Y);
                                }));
                            }
                            if (Press == (char)Keys.W)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1U;
                                    player1.Size = new Size(60, 100);
                                    up = true;
                                    down = false;
                                    left = false;
                                    right = false;
                                    player1.Location = new Point(player1.Location.X, player1.Location.Y - 10);
                                }));
                            }
                            if (Press == (char)Keys.S)
                            {
                                this.player1.Invoke(new MethodInvoker(delegate ()
                                {
                                    player1.Image = Properties.Resources.tanque1D;
                                    player1.Size = new Size(60, 100);
                                    up = false;
                                    down = true;
                                    left = false;
                                    right = false;
                                    player1.Location = new Point(player1.Location.X, player1.Location.Y + 10);
                                }));
                            }
                            if (Press == (char)Keys.O)
                            {
                                disparosrealizadosj1++;
                                PictureBox pb = new PictureBox();
                                pb.BackColor = Color.Transparent;
                                pb.Image = Properties.Resources.TanqueBalaA;
                                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                                pb.Size = new Size(15, 15);
                                pb.Tag = true;
                                int x = player1.Location.X + (player1.Width / 2);
                                int y = player1.Location.Y + (player1.Height / 2);
                                pb.Location = new Point(x, y);
                                pb.Visible = true;
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    this.Controls.Add(pb);
                                //BackgroundWorker bw = new BackgroundWorker();
                                //bw.DoWork += bwGeneral_DoWork;
                                //bw.RunWorkerAsync(pb);
                            }));
                                Thread t2 = new Thread(moverBala);
                                t2.Start(pb);
                            }
                        }
                        aliado = true;
                    }
                    else
                    {
                        STW.WriteLine(TextSend);
                        this.txtChat.Invoke(new MethodInvoker(delegate ()
                        {
                            txtChat.AppendText("Yo:" + TextSend + "\n");
                        }));
                    }
                }
                else
                {
                    MessageBox.Show("Sending failed");
                }
                backgroundWorker2.CancelAsync();
            }catch(Exception ex)
            {
                MessageBox.Show("Error: No se encuentra conectado\n" +ex);
            }
        }

        public void moverBala(Object pb)
        {
            Control c = pb as Control;
            int x = c.Location.X;
            int y = c.Location.Y;
            bool tipBal=aliado;
            int xA = 0;
            int yA = 0;

            if (p1)
            {
                if (tipBal)
                {
                    if (left)
                    {
                        x -= 50;
                        xA = -5;
                    }
                    else if (right)
                    {
                        x += 50;
                        xA = 5;
                    }
                    else if (up)
                    {
                        y -= 50;
                        yA = -5;
                    }
                    else if (down)
                    {
                        y += 50;
                        yA = 5;
                    }
                }
                else
                {
                    if (left2)
                    {
                        x -= 50;
                        xA = -5;
                    }
                    else if (right2)
                    {
                        x += 50;
                        xA = 5;
                    }
                    else if (up2)
                    {
                        y -= 50;
                        yA = -5;
                    }
                    else if (down2)
                    {
                        y += 50;
                        yA = 5;
                    }
                }
            }
            else
            {
                if (!tipBal)
                {
                    if (left)
                    {
                        x -= 50;
                        xA = -5;
                    }
                    else if (right)
                    {
                        x += 50;
                        xA = 5;
                    }
                    else if (up)
                    {
                        y -= 50;
                        yA = -5;
                    }
                    else if (down)
                    {
                        y += 50;
                        yA = 5;
                    }
                }
                else
                {
                    if (left2)
                    {
                        x -= 50;
                        xA = -5;
                    }
                    else if (right2)
                    {
                        x += 50;
                        xA = 5;
                    }
                    else if (up2)
                    {
                        y -= 50;
                        yA = -5;
                    }
                    else if (down2)
                    {
                        y += 50;
                        yA = 5;
                    }
                }
            }
            while ((bool)c.Tag)
            {
                y += yA;
                x += xA;
                int pa = int.Parse( lblPA.Tag.ToString());
                int pe = int.Parse(lblPE.Tag.ToString());
                if (p1)
                {
                    if (tipBal)
                    {
                        if (c.Bounds.IntersectsWith(player2.Bounds))
                        {
                            pa++;
                            c.Tag = false;
                            this.lblPA.Invoke(new MethodInvoker(delegate ()
                            {
                                lblPA.Text = "Puntos a favor:" + pa;
                            }));
                            lblPA.Tag = "" + pa;
                        }
                    }
                    else
                    {
                        if (c.Bounds.IntersectsWith(player1.Bounds))
                        {
                            pe++;
                            this.lblPE.Invoke(new MethodInvoker(delegate ()
                            {
                                lblPE.Text = "Puntos en contra:" + pe;
                            }));
                            lblPE.Tag = "" + pe;
                            c.Tag = false;
                        }
                    }
                }
                if (p2)
                {
                    if (tipBal)
                    {
                        if (c.Bounds.IntersectsWith(player1.Bounds))
                        {
                            pa++;
                            this.lblPA.Invoke(new MethodInvoker(delegate ()
                            {
                                lblPA.Text = "Puntos a favor:" + pa;
                            }));
                            lblPA.Tag = "" + pa;
                            c.Tag = false;
                        }
                    }
                    else
                    {
                        if (c.Bounds.IntersectsWith(player2.Bounds))
                        {
                            pe++;
                            this.lblPE.Invoke(new MethodInvoker(delegate ()
                            {
                                lblPE.Text = "Puntos en contra:" + pe;
                            }));
                            lblPE.Tag = "" + pe;
                            c.Tag = false;
                        }
                    }
                }
                if (c.Location.Y < -20)
                {
                    c.Tag = false;
                }
                else if (c.Location.Y < -20)
                {
                    c.Tag = false;
                }
                else if (c.Location.X > this.Width + 20)
                {
                    c.Tag = false;
                }
                else if (c.Location.X < -20)
                {
                    c.Tag = false;
                }
                else if (c.Location.Y > this.Height + 20)
                {
                    c.Tag = false;
                }
                m_mover(c, x, y);
                Thread.Sleep(20);
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
                        if (!(bool)c.Tag)
                        {
                            c.Dispose();
                        }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex.Message.ToString());
            }
        }
        private void Send_Click(object sender, EventArgs e)
        {
            if (txtSend.Text!="")
            {
                TextSend = txtSend.Text;
                backgroundWorker2.RunWorkerAsync();
                txtSend.Text = "";
            }
        }
        private void btnJugar_Click(object sender, EventArgs e)
        {
            STW.WriteLine("VAMOS");
            this.ControlBox = false;
            player1.Tag = true;
            player1.Visible = true;
            player2.Visible = true;
            panel1.Visible = false;
            panel1.Enabled = false;
            lblPA.Visible = true;
            lblPE.Visible = true;
            lblTiempo.Visible = true;
            label8.Visible = true;
            btnJugar.Visible = false;
            this.WindowState = FormWindowState.Maximized;
            //txtChat.Location = new Point(this.Width-txtChat.Width-20,this.Height-txtChat.Height-100);
            //txtSend.Location = new Point(this.Width - txtChat.Width - 20, this.Height - txtSend.Height - 50);
            //Send.Location = new Point(this.Width - Send.Width - 20, this.Height - Send.Height - 50);
            Focus();
        }
        private void Tanks_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (tiempo == 0)
            {
                gameOver();
                Thread.Sleep(20);
            }
            else
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    Application.Exit();
                }
                if (jugando)
                {
                    if (!backgroundWorker2.IsBusy)
                    {
                        Press = e.KeyChar;
                        backgroundWorker2.RunWorkerAsync();
                    }
                }
            }
        }
    }
}
