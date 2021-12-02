using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Juegos
{
    public partial class Naruto : UserControl
    {
        public bool vivo = false, atacando = false, movimiento = false;
        public string rango="Corto";

        public Naruto()
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
       
    }
}
