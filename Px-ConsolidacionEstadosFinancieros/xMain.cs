/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 xMain.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma contenedor de toda la app

using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;

using Px_Controles.Colors;
using Px_Controles.Helpers;
using Px_Controles.Forms.Msg;
using Px_Controles.Controls.NavigationMenu;

using Px_ConsolidacionEstadosFinancieros.Catalogos;
using Px_ConsolidacionEstadosFinancieros.Properties;
using Px_ConsolidacionEstadosFinancieros.Utiles.Theme;
using Px_ConsolidacionEstadosFinancieros.Utiles.Generales;
using Px_ConsolidacionEstadosFinancieros.Catalogos.Controles;

using static Px_ConsolidacionEstadosFinancieros.Utiles.Emun.Enumerados;

namespace Px_ConsolidacionEstadosFinancieros
{
    public partial class xMain : Form
    {
        eRequest oReq = new eRequest();
        Timer TimerStatus = new Timer();

        #region Constructores
        public xMain()
        {
            InitializeComponent();

            Inicio();
        }


        private async Task Inicio()
        {

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            btnTMMenu.Click += btnTMMenu_Click;

            panXTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            uiTreeViewEx1.AfterSelect += uiTreeViewEx1_AfterSelect;

            TimerStatus.Tick += TimerStatus_Tick;
            TimerStatus.Interval = 2000;
            TimerStatus.Enabled = true;

            // Parámetros Generales
            Generales._AppState.Base = "contabilidad";
            Generales._AppState.EndPoint = ConfigurationManager.AppSettings["EndPoint"].ToString();


            Generales._AppState.Empresa = 1;
            Generales._AppState.Ejercicio = DateTime.Now.Year;



            await InicioForma();
            await conActualiza();


        }

        private async Task InicioForma()
        {
            await Task.Delay(0);

            this.Text = "GBC | Consolidación de estados financieros";
            lblTitulo.Text = this.Text;

            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ControlBox = false;
            //this.FormBorderStyle = FormBorderStyle.None;

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            this.ClientSize = new System.Drawing.Size(1329, 812);
            this.MinimumSize = new System.Drawing.Size(950, 650);

            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            this.FormClosing += xMain_FormClosing;

            lblVer.Text = string.Format("Version: {0}", Application.ProductVersion);

            ArbolCrea();
            MenuCrea();

            await Status($"{this.Text}  .:: Power by PANXEA | Wero MX ::.", (int)MensajeTipo.Info);
        }

        #endregion

        #region Eventos
        private void xMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Status("¿Deseas salir de contabilidad del GBC?", (int)MensajeTipo.Question);
            e.Cancel = MessageBoxMX.ShowDialog(null, "¿Deseas salir de contabilidad del GBC?", lblTitulo.Text, (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.OK ? false : true;

        }


        private void panTittle_MouseDown(object sender, MouseEventArgs e)
        {
            Utiles.Win32.User.ReleaseCapture();
            Utiles.Win32.User.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnXCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //Application.Exit();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnXMax_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void btnXMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnTMMenu_Click(object sender, EventArgs e)
        {
            panTMI.Width = panTMI.Width == 320 ? 10 : 320;
            uiTreeViewEx1.Visible = panTMI.Width == 320 ? true : false;
        }
        #endregion

        #region Abir formas hjas
        private async Task<T> AbrirFormulario<T>() where T : Form, new()
        {
            Panel xContent = panContent;
            Form xFrm = xContent.Controls.OfType<T>().FirstOrDefault();
            if (xFrm != null)
            {
                //Si la instancia esta minimizada la dejamos en su estado normal
                if (xFrm.WindowState == FormWindowState.Minimized)
                    xFrm.WindowState = FormWindowState.Normal;

                //Si la instancia existe la pongo en primer plano
                xFrm.BringToFront();
                return (T)xFrm;
            }

            //Se abre el form
            xFrm = new T();
            await Task.Delay(500);
            xFrm.TopLevel = false;
            panContent.Controls.Add(xFrm);
            panContent.Tag = xFrm;
            xFrm.Location = new System.Drawing.Point((xContent.Width / 2) - (xFrm.Width / 2), (xContent.Height / 2) - (xFrm.Height / 2));
            xFrm.BringToFront();
            xFrm.Show();

            return (T)xFrm;
        }
        #endregion

        #region "Menu Arbol"
        private async void uiTreeViewEx1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string strName = e.Node.Text.Trim();
            await Status($"Iniciando {strName}. . .", (int)MensajeTipo.Info);

            switch (strName)
            {
                case "Salir": this.Close(); break;


                // Menú Catálogos 
                case "Monedas":
                    var oFrmC02 = await AbrirFormulario<FrmMonedas>(); oFrmC02._Main = this; break;

            }

        }
        private void ArbolCrea()
        {
            try
            {
                uiTreeViewEx1.ImageList = imgMenu;

                TreeNode oNodo0 = new TreeNode();
                List<TreeNode> oNodo1 = new List<TreeNode>();
                List<TreeNode> oNodo2 = new List<TreeNode>();

                ControlHelper.FreezeControl(this, true);
                this.uiTreeViewEx1.Nodes.Add("", "  CONTABILIDAD", 0);



                oNodo0 = new TreeNode("  Archivo", 1, 1);
                oNodo0.Nodes.Add("", "  Configurar conexión", 9);
                oNodo0.Nodes.Add("", "  Configurar impresora", 10);
                this.uiTreeViewEx1.Nodes.Add(oNodo0);


                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode("Manuales [Captura]", 12, 12));
                oNodo2.Add(new TreeNode("Automáticas [Carga]", 13, 13));
                oNodo2.Add(new TreeNode("Autorización / Aplicación", 14, 14));
                oNodo1.Add(new TreeNode("  Pre pólizas", 11, 11, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode("  Cambio de periodo / ejercicio", 15, 15));
                oNodo1.Add(new TreeNode("  Cierre de ejercicio", 16, 16));
                oNodo1.Add(new TreeNode("  Carga de presupuesto", 17, 17));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Desaplicación", 19, 19));
                oNodo2.Add(new TreeNode(" Mantenimiento", 20, 20));
                oNodo1.Add(new TreeNode("  Pólizas", 18, 18, oNodo2.ToArray()));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Importar cuentas equivalentes", 22, 22));
                oNodo2.Add(new TreeNode(" Exportar saldos iniciales", 23, 23));
                oNodo2.Add(new TreeNode(" Carga saldos iniciales", 24, 24));
                oNodo2.Add(new TreeNode(" Generación de archivo de consolidación", 25, 25));
                oNodo2.Add(new TreeNode(" Generación archivo SEVAC", 26, 26));
                oNodo1.Add(new TreeNode("  Armonización", 21, 21, oNodo2.ToArray()));


                oNodo1.Add(new TreeNode("  Aplicar momentos contables", 27, 27));
                oNodo1.Add(new TreeNode("  Consolidar información", 28, 28));
                oNodo1.Add(new TreeNode("  Refrescar vistas", 29, 29));

                oNodo0 = new TreeNode("  Procesos", 2, 2, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);


                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo1.Add(new TreeNode("  Pólizas", 31, 31));
                oNodo1.Add(new TreeNode("  Cuentas", 32, 32));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Pólizas pendientes por aplicar", 33, 33));
                oNodo2.Add(new TreeNode(" Pólizas aplicadas", 34, 34));
                oNodo2.Add(new TreeNode(" Relación folio SIP-MX", 35, 35));
                oNodo1.Add(new TreeNode("  Momentos contables", 30, 30, oNodo2.ToArray()));

                oNodo0 = new TreeNode("  Consultas", 3, 3, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);



                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Estado de contabilidad (Balance)", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de afectación patrimonial", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de origen y aplicación de recursos", 36, 36));
                oNodo2.Add(new TreeNode(" Balanza de comprobación", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de resultados", 36, 36));
                oNodo2.Add(new TreeNode(" Comparativo de ingresos y egresos", 36, 36));
                oNodo2.Add(new TreeNode(" Balance comparativo", 36, 36));
                oNodo1.Add(new TreeNode(" Estados financieros", 4, 4, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode(" Analítico por auxiliar", 36, 36));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Diarios por cuenta", 36, 36));
                oNodo2.Add(new TreeNode(" Por cuenta", 36, 36));
                oNodo1.Add(new TreeNode(" Saldos", 4, 4, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode(" Prepólizas", 36, 36));
                oNodo1.Add(new TreeNode(" Pólizas", 36, 36));
                oNodo1.Add(new TreeNode(" Archivo TXT ORFIS", 36, 36));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Estado analítico del activo", 36, 36));
                oNodo2.Add(new TreeNode(" Estado analítico de la deuda y otros pasivos", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de actividades", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de situación financiera", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de flujos de efectivo", 36, 36));
                oNodo2.Add(new TreeNode(" Libro diario", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de cambios en situación financiera", 36, 36));
                oNodo2.Add(new TreeNode(" Estado de variación de hacienda pública", 36, 36));
                oNodo2.Add(new TreeNode(" Libro mayor", 36, 36));
                oNodo1.Add(new TreeNode(" Libro de balance", 4, 4, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode(" Comprobantes", 36, 36));
                oNodo1.Add(new TreeNode(" Transferencia de pólizas bancarias", 36, 36));
                oNodo1.Add(new TreeNode(" Analítico por auxiliar fuente financiamiento", 36, 36));

                oNodo0 = new TreeNode("  Reportes", 4, 4, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);



                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo1.Add(new TreeNode(" Organismo / Ejercicio", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Monedas", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Estructura de cuentas", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Cuentas", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Firmas", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Reportes", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Tipos de póliza", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Secuencias", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Origen de pólizas", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Sistema-Cuenta", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Cuentas especiales", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Momentos contables", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Cuentas depreciación", 37, 37, oNodo2.ToArray()));

                oNodo0 = new TreeNode("  Catálogos", 5, 5, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);


                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Privilegios", 38, 38));
                oNodo2.Add(new TreeNode(" Cambio de clave de seguridad", 38, 38));
                oNodo1.Add(new TreeNode(" Seguridad", 38, 38, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode(" Cargas de catálogo de cuentas", 38, 38));
                oNodo1.Add(new TreeNode(" Carga de pólizas", 38, 38));
                oNodo1.Add(new TreeNode(" Prueba de reportes", 38, 38));
                oNodo1.Add(new TreeNode(" Arranque de ejercicio", 38, 38));

                oNodo0 = new TreeNode("  Configurar", 6, 6, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);


                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo1.Add(new TreeNode(" Ayuda en línea", 39, 39));
                oNodo1.Add(new TreeNode(" Acerca de", 40, 40));

                oNodo0 = new TreeNode("  Ayuda", 7, 7, oNodo1.ToArray());
                this.uiTreeViewEx1.Nodes.Add(oNodo0);



                this.uiTreeViewEx1.Nodes.Add("", "  Salir", 8);

            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }


        }



        #endregion

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

        #region StatusBar

        public async Task Status(string sStatus, int xTipo = 0)
        {
            await Task.Delay(0);

            TimerStatus.Interval = 5000;
            switch (xTipo)
            {
                case (int)MensajeTipo.General:
                    lblTStatus.ForeColor = Color.FromArgb(106, 47, 66);
                    break;
                case (int)MensajeTipo.Info:
                    lblTStatus.ForeColor = Color.CornflowerBlue;
                    break;
                case (int)MensajeTipo.Success:
                    lblTStatus.ForeColor = Color.Green;
                    break;
                case (int)MensajeTipo.Warning:
                    lblTStatus.ForeColor = Color.Gold;
                    TimerStatus.Interval = 6000;
                    break;
                case (int)MensajeTipo.Error:
                    lblTStatus.ForeColor = Color.IndianRed;
                    TimerStatus.Interval = 10000;
                    break;
            }

            picStatus.Image = imgStatus.Images[xTipo];

            lblTStatus.Text = sStatus;
            lblTStatus.Refresh();

            TimerStatus.Start();


            this.Refresh();

        }
        private void TimerStatus_Tick(object sender, EventArgs e)
        {
            Status(this.Text);
            //Task.Run(async ()=> { await Status("GBC | Contabilidad"); } );
            TimerStatus.Stop();
        }
        #endregion

        #region "Consulta"
        private async Task conActualiza()
        {

            Cursor = Cursors.WaitCursor;
            //oGrid.DataSource = null;
            await conActualizaDB();
            //oGrid.DataSource = oDT;
            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB()
        {
            try
            {
                oReq.Base = Generales._AppState.Base;
                oReq.EndPoint = Generales._AppState.EndPoint;

                oReq.Query = "SELECT * FROM EMPRESA";
                var oRes = await WSServicio.Servicio(oReq);

                //if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                //    oDT = oRes.Data.Tables[0];
            }
            catch (Exception ex)
            { }

        }

        #endregion


    }
}
