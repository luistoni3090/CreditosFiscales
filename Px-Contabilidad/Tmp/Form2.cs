using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Contabilidad.Catalogos
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;

            InicioForma();

        }

        private void InicioForma()
        {

            this.Text = string.Empty; 
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.MaximizedBounds =  xMain.  ;

            //this.ClientSize = new System.Drawing.Size(1329, 812);
            this.MinimumSize = new System.Drawing.Size(800, 450);

            //this.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = FormWindowState.Maximized;

            //this.BackColor = System.Drawing.Color.FromArgb(106, 28, 50) ;


        }

        private void panTittle_MouseDown(object sender, MouseEventArgs e)
        {
            Utiles.Win32.User.ReleaseCapture();
            Utiles.Win32.User.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnXCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXMax_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void btnXMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
