using Px_Contabilidad.Properties;
using Px_Contabilidad.Utiles.Generales;
using Px_Controles.Controls.NavigationMenu;
using Px_Controles.Helpers;
using Px_Utiles.Models.Api;
using Px_Utiles.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Contabilidad.Catalogos
{
    public partial class Form1 : Form
    {

        eRequest oReq = new eRequest();

        public Form1()
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

            MenuCrea();
            Cuentas();
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



        private async void Cuentas()
        {
            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            Cursor = Cursors.WaitCursor;

            dataGridView1.DataSource = null;

            oReq.Query = "SELECT * FROM EJERCICIO";

            var oRes = await WSServicio.Servicio(oReq);

            lblRes.Text = oRes.Message;

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                dataGridView1.DataSource = oRes.Data.Tables[0];

            Cursor = Cursors.Default;

        }


        #region "Menu"
        private void MenuCrea()
        {

            //return;

            uiNavigationMenu1.Items = null;

            NavigationMenuItem oNMItem = new NavigationMenuItem();
            List<NavigationMenuItem> oNMItem0 = new List<NavigationMenuItem>();
            List<NavigationMenuItem> oNMItem1 = new List<NavigationMenuItem>();
            List<NavigationMenuItem> oNMItem2 = new List<NavigationMenuItem>();


            oNMItem1.Add(new NavigationMenuItem { Text = "Configurar conexión", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Configurar impresora", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Salir", Icon = Resources.mnuTmp });
            oNMItem = new NavigationMenuItem();
            oNMItem.Text = "Archivo";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem1.ToArray();
            oNMItem0.Add(oNMItem);

            oNMItem = new NavigationMenuItem();
            oNMItem1 = new List<NavigationMenuItem>();
            oNMItem2 = new List<NavigationMenuItem>();

            oNMItem2.Add(new NavigationMenuItem { Text = "Manuales [Captura]", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Automáticas [Carga]", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Autorización / Aplicación", Icon = Resources.mnuTmp });


            oNMItem2 = new List<NavigationMenuItem>();
            oNMItem2.Add(new NavigationMenuItem { Text = "Manuales [Captura]", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Automáticas [Carga]", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Autorización / Aplicación", Icon = Resources.mnuTmp });
            oNMItem = new NavigationMenuItem();
            oNMItem.Text = "Pre pólizas";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem2.ToArray();
            oNMItem1.Add(oNMItem);

            oNMItem1.Add(new NavigationMenuItem { Text = "Cambio de periodo / ejercicio", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Cierre de ejercicio", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Carga de presupuesto", Icon = Resources.mnuTmp });


            oNMItem2 = new List<NavigationMenuItem>();
            oNMItem2.Add(new NavigationMenuItem { Text = "Desaplicación", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Mantenimiento", Icon = Resources.mnuTmp });
            oNMItem = new NavigationMenuItem();
            oNMItem.Text = "Pólizas";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem2.ToArray();
            oNMItem1.Add(oNMItem);

            oNMItem2 = new List<NavigationMenuItem>();
            oNMItem2.Add(new NavigationMenuItem { Text = "Importar cuentas equivalentes", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Exportar saldos iniciales", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Carga saldos iniciales", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Generación de archivo de consolidación", Icon = Resources.mnuTmp });
            oNMItem2.Add(new NavigationMenuItem { Text = "Generación archivo SEVAC", Icon = Resources.mnuTmp });
            oNMItem = new NavigationMenuItem();
            oNMItem.Text = "Armonización";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem2.ToArray();
            oNMItem1.Add(oNMItem);

            oNMItem1.Add(new NavigationMenuItem { Text = "Aplicar momentos contables", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Consolidar información", Icon = Resources.mnuTmp });
            oNMItem1.Add(new NavigationMenuItem { Text = "Refrescar vistas", Icon = Resources.mnuTmp });
            oNMItem = new NavigationMenuItem();
            oNMItem.Text = "Procesos";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem1.ToArray();
            oNMItem0.Add(oNMItem);

            oNMItem = new NavigationMenuItem();
            oNMItem1 = new List<NavigationMenuItem>();
            oNMItem2 = new List<NavigationMenuItem>();
            oNMItem1.Add(new NavigationMenuItem { Text = "31" });
            oNMItem1.Add(new NavigationMenuItem { Text = "32" });
            oNMItem1.Add(new NavigationMenuItem { Text = "33" });
            oNMItem.Text = "Consulta";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem1.ToArray();
            oNMItem0.Add(oNMItem);

            oNMItem = new NavigationMenuItem();
            oNMItem.AnchorRight = true;
            oNMItem.Text = "Salir";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem0.Add(oNMItem);

            oNMItem = new NavigationMenuItem();
            oNMItem1 = new List<NavigationMenuItem>();
            oNMItem2 = new List<NavigationMenuItem>();
            oNMItem1.Add(new NavigationMenuItem { Text = "41" });
            oNMItem1.Add(new NavigationMenuItem { Text = "42" });
            oNMItem1.Add(new NavigationMenuItem { Text = "43" });
            oNMItem.Text = "Ayuda";
            oNMItem.Icon = Resources.mnuTmp;
            oNMItem.Items = oNMItem1.ToArray();
            oNMItem.AnchorRight = true;
            oNMItem0.Add(oNMItem);


            uiNavigationMenu1.Items = oNMItem0.ToArray();




        }
        #endregion





    }
}
