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
using Px_Utiles.Models.Sistemas.Contabilidad;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;

using Px_CreditosFiscales.Utiles.Formas;
using Px_CreditosFiscales.Utiles.Generales;
using Px_CreditosFiscales.Catalogos.Controles;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Px_CreditosFiscales.Utiles.Formas
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

            xbtnXCerrar.Click += btnXCerrar_Click;
            xbtnXMax.Click += btnXMax_Click;
            xbtnXMin.Click += btnXMin_Click;

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

    /*
    public partial class FormaGenBar : Form
    {
        public xMain _Main { get; set; }


        private bool _minShow = true;
        private bool _maxShow = true;
        public bool _MinShow
        {
            get { return _minShow; }
            set
            {
                _minShow = value;
                xbtnXMin.Visible = _minShow;
            }
        }

        public bool _MaxShow
        {
            get { return _maxShow; }
            set
            {
                _maxShow = value;
                xbtnXMax.Visible = _maxShow;
            }
        }



        string _titulo = "";
        public string _Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;

                //this.Text = _titulo;
                lblTitulo.Text = _titulo;

                //OnAgeChanged();  // Invoca la función local OnAgeChanged
            }
        }

        int imgIdx = 0;
        public int _ImgIdx
        {
            get { return imgIdx; }
            set
            {
                imgIdx = value;

                while (_Main == null)
                    Task.Delay(100);

                picIco.Image = _Main._ImageMenu.Images[value].MejorarImagen(32,32);
                //picIco.Image = new Bitmap(_Main._ImageMenu.Images[value]);
                //uiImageHD1.Image = new Bitmap(_Main._ImageMenu.Images[value]);

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

            xbtnXCerrar.Click += btnXCerrar_Click;
            xbtnXMax.Click += btnXMax_Click;
            xbtnXMin.Click += btnXMin_Click;

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
            //this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

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

    */
}
