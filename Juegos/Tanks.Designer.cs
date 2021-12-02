namespace Juegos
{
    partial class Tanks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtClientePort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClienteIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Conectar = new System.Windows.Forms.Button();
            this.Iniciar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.btnJugar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPA = new System.Windows.Forms.Label();
            this.lblPE = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblOponent = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Servidor";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(37, 30);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(100, 20);
            this.txtServerIP.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(46, 54);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(100, 20);
            this.txtServerPort.TabIndex = 8;
            // 
            // txtClientePort
            // 
            this.txtClientePort.Location = new System.Drawing.Point(386, 54);
            this.txtClientePort.Name = "txtClientePort";
            this.txtClientePort.Size = new System.Drawing.Size(100, 20);
            this.txtClientePort.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "IP";
            // 
            // txtClienteIP
            // 
            this.txtClienteIP.Location = new System.Drawing.Point(377, 30);
            this.txtClienteIP.Name = "txtClienteIP";
            this.txtClienteIP.Size = new System.Drawing.Size(100, 20);
            this.txtClienteIP.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Cliente";
            // 
            // Conectar
            // 
            this.Conectar.Location = new System.Drawing.Point(518, 37);
            this.Conectar.Name = "Conectar";
            this.Conectar.Size = new System.Drawing.Size(75, 23);
            this.Conectar.TabIndex = 14;
            this.Conectar.Text = "Conectar";
            this.Conectar.UseVisualStyleBackColor = true;
            this.Conectar.Click += new System.EventHandler(this.Conectar_Click);
            // 
            // Iniciar
            // 
            this.Iniciar.Location = new System.Drawing.Point(197, 32);
            this.Iniciar.Name = "Iniciar";
            this.Iniciar.Size = new System.Drawing.Size(75, 23);
            this.Iniciar.TabIndex = 15;
            this.Iniciar.Text = "Iniciar";
            this.Iniciar.UseVisualStyleBackColor = true;
            this.Iniciar.Click += new System.EventHandler(this.Iniciar_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // txtChat
            // 
            this.txtChat.Enabled = false;
            this.txtChat.Location = new System.Drawing.Point(6, 80);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(204, 241);
            this.txtChat.TabIndex = 16;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(6, 326);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(100, 20);
            this.txtSend.TabIndex = 17;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(135, 326);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 18;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // btnJugar
            // 
            this.btnJugar.Location = new System.Drawing.Point(216, 78);
            this.btnJugar.Name = "btnJugar";
            this.btnJugar.Size = new System.Drawing.Size(75, 23);
            this.btnJugar.TabIndex = 19;
            this.btnJugar.Text = "Jugar";
            this.btnJugar.UseVisualStyleBackColor = true;
            this.btnJugar.Click += new System.EventHandler(this.btnJugar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblOponent);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Send);
            this.panel1.Controls.Add(this.btnJugar);
            this.panel1.Controls.Add(this.txtSend);
            this.panel1.Controls.Add(this.txtServerIP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtChat);
            this.panel1.Controls.Add(this.txtServerPort);
            this.panel1.Controls.Add(this.Iniciar);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Conectar);
            this.panel1.Controls.Add(this.txtClienteIP);
            this.panel1.Controls.Add(this.txtClientePort);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 355);
            this.panel1.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(302, -16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 23;
            this.label7.Tag = "0";
            this.label7.Text = "150";
            this.label7.Visible = false;
            // 
            // lblPA
            // 
            this.lblPA.AutoSize = true;
            this.lblPA.Location = new System.Drawing.Point(3, 10);
            this.lblPA.Name = "lblPA";
            this.lblPA.Size = new System.Drawing.Size(79, 13);
            this.lblPA.TabIndex = 21;
            this.lblPA.Tag = "0";
            this.lblPA.Text = "Puntos a favor:";
            this.lblPA.Visible = false;
            // 
            // lblPE
            // 
            this.lblPE.AutoSize = true;
            this.lblPE.Location = new System.Drawing.Point(527, 9);
            this.lblPE.Name = "lblPE";
            this.lblPE.Size = new System.Drawing.Size(91, 13);
            this.lblPE.TabIndex = 22;
            this.lblPE.Tag = "0";
            this.lblPE.Text = "Puntos en contra:";
            this.lblPE.Visible = false;
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Monoton", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(259, 28);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(57, 32);
            this.lblTiempo.TabIndex = 24;
            this.lblTiempo.Tag = "0";
            this.lblTiempo.Text = "150";
            this.lblTiempo.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(241, -4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 32);
            this.label8.TabIndex = 25;
            this.label8.Tag = "0";
            this.label8.Text = "Tiempo";
            this.label8.Visible = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.lblUser.Location = new System.Drawing.Point(551, 333);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 13);
            this.lblUser.TabIndex = 24;
            this.lblUser.Text = "Usuario";
            // 
            // lblOponent
            // 
            this.lblOponent.AutoSize = true;
            this.lblOponent.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.lblOponent.Location = new System.Drawing.Point(550, 308);
            this.lblOponent.Name = "lblOponent";
            this.lblOponent.Size = new System.Drawing.Size(43, 13);
            this.lblOponent.TabIndex = 25;
            this.lblOponent.Text = "Usuario";
            // 
            // Tanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(618, 410);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblTiempo);
            this.Controls.Add(this.lblPE);
            this.Controls.Add(this.lblPA);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tanks";
            this.Text = "Tanks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tanks_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tanks_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtClientePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtClienteIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Conectar;
        private System.Windows.Forms.Button Iniciar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Button btnJugar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPA;
        private System.Windows.Forms.Label lblPE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label lblUser;
        public System.Windows.Forms.Label lblOponent;
    }
}