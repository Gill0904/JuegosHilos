﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juegos
{
    public partial class RockLee : UserControl
    {
        public bool vivo = false, atacando = false, movimiento = false;
        public string rango = "Corto";
        public RockLee()
        {
            InitializeComponent();
        }
        public void vida()
        {
            if (progressBar1.Value > 0)
            {
                progressBar1.Value -= 10;
            }
            else
            {
                vivo = false;
            }
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
