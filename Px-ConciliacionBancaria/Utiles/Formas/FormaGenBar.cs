
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;

using Px_Utiles.Servicio;
using Px_Utiles.Utiles.DataTables;

using Px_Utiles.Models.Api;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

using Px_ConciliacionBancaria.Utiles.Formas;
using Px_ConciliacionBancaria.Utiles.Generales;
using Px_ConciliacionBancaria.Catalogos.Controles;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Px_ConciliacionBancaria.Utiles.Formas
{
    public partial class FormaGenBar : Form
    {
        public xMain _Main { get; set; }

        string _titulo = "";
        public string _Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;

                this.Text = _titulo;
                lblTitulo.Text = _titulo;

                //OnAgeChanged();  // Invoca la función local OnAgeChanged
            }
        }

        public FormaGenBar()
        {
            InitializeComponent();
            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = _Titulo;

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            await InicioForma();

            Cursor = Cursors.Default;
        }


        private async Task InicioForma()
        {

            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            if (_Main != null)
            {
                //await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            }

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
