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

using Px_ConciliacionBancaria.Catalogos;
using Px_ConciliacionBancaria.Properties;
using Px_ConciliacionBancaria.Utiles.Theme;
using Px_ConciliacionBancaria.Utiles.Generales;
using Px_ConciliacionBancaria.Catalogos.Controles;

using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;
using PX_ConciliacionBancaria;

namespace Px_ConciliacionBancaria
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
            Generales._AppState.Base = "conciliacionbancaria";
            Generales._AppState.EndPoint = ConfigurationManager.AppSettings["EndPoint"].ToString();


            Generales._AppState.Empresa = 1;
            Generales._AppState.Ejercicio = DateTime.Now.Year;



            await InicioForma();
            await conActualiza();


        }

        private async Task InicioForma()
        {
            await Task.Delay(0);

            this.Text = "GBC | Conciliación bancaria";
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

                case "Bancos":
                    var oFrmC03 = await AbrirFormulario<FrmBancos>(); oFrmC03._Main = this; break;

                case "Cuentas Bancarias":
                    var oFrmC04 = await AbrirFormulario<FrmCuentasBancarias>(); oFrmC04._Main = this; break;

                case "Usuarios del Sistema":
                    var oFrmC05 = await AbrirFormulario<FrmUsuariosSistema>(); oFrmC05._Main = this; break;

                case "Reglas de Validación":
                    var oFrmC07 = await AbrirFormulario<FrmReglasValidacion>(); oFrmC07._Main = this; break;

                case "Parámetros del Sistema":
                    var oFrmC08 = await AbrirFormulario<FrmParametrosSistema>(); oFrmC08._Main = this; break;

                case "Mantenimiento Acceso-Rol":
                    var oFrmC09 = await AbrirFormulario<FrmAccesoRol>(); oFrmC09._Main = this; break;


                    // PROCESOS


                case "Automática":
                    var oFrmP01 = await AbrirFormulario<FrmConciliacionAutomatica>(); oFrmP01._Main = this; break;

                case "Registro de Ajustes":
                    var oFrmP02 = await AbrirFormulario<FrmRegistroDeAjustes>(); oFrmP02._Main = this; break;

                case "Traspaso al Histórico":
                    var oFrmP03 = await AbrirFormulario<FrmTraspasoHistorico>(); oFrmP03._Main = this; break;

                case "Desconciliación":
                    var oFrmP04 = await AbrirFormulario<FrmDesconciliacionAutomatica>(); oFrmP04._Main = this; break;

                case "Contracuenta para Ajustes":
                    var oFrmP05 = await AbrirFormulario<FrmCapturaContracuentasAjustes>(); oFrmP05._Main = this; break;

                case "Conciliación a 5 Dígitos":
                    var oFrmP06 = await AbrirFormulario<FrmConciliacionA5Digitos>(); oFrmP06._Main = this; break;

                case "Conciliación a 5 Dígitos por Diferencia de Importe":
                    var oFrmP07 = await AbrirFormulario<FrmConciliacionA5DigitosDiferenciaImporte>(); oFrmP07._Main = this; break;

                case "Conciliación a 4 Dígitos":
                    var oFrmP08 = await AbrirFormulario<FrmConciliacionA4Digitos>(); oFrmP08._Main = this; break;

                case "Conciliación a 4 Dígitos por Diferencia de Importe":
                    var oFrmP09 = await AbrirFormulario<FrmConciliacionA4DigitosDiferenciaImporte>(); oFrmP09._Main = this; break;

                case "Conciliación a 3 Dígitos":
                    var oFrmP10 = await AbrirFormulario<FrmConciliacionA3Digitos>(); oFrmP10._Main = this; break;

                case "Conciliación a 3 Dígitos por Diferencia de Importe":
                    var oFrmP11 = await AbrirFormulario<FrmConciliacionA3DigitosDiferenciaImporte>(); oFrmP11._Main = this; break;

                case "Conciliación a 6 Dígitos":
                    var oFrmP12 = await AbrirFormulario<FrmConciliacionA6Digitos>(); oFrmP12._Main = this; break;

                case "Conciliación a 6 Dígitos por Diferencia de Importe":
                    var oFrmP13 = await AbrirFormulario<FrmConciliacionA6DigitosDiferenciaImporte>(); oFrmP13._Main = this; break;


                case "Depositos Con un Día de Diferencia":
                    var oFrmP14 = await AbrirFormulario<FrmDepositosUnDiaDiferencia>(); oFrmP14._Main = this; break;


                case "Conciliación Automática con mayor tolerancia":
                    var oFrmP15 = await AbrirFormulario<FrmConciliacionAutomaticaMayorTolerancia>(); oFrmP15._Main = this; break;


                case "Banco vs Banco":
                    var oFrmP16 = await AbrirFormulario<FrmConciliacionManualBncovsBanco>(); oFrmP16._Main = this; break;

                case "Auxiliar vs Banco a N M":
                    var oFrmP17 = await AbrirFormulario<FrmConciliacionManualAuxiliarvsBancoNaM>(); oFrmP17._Main = this; break;


                case "Auxiliar vs Auxiliar":
                    var oFrmP18 = await AbrirFormulario<FrmConciliacionManualAuxiliarvsAuxiliar>(); oFrmP18._Main = this; break;

                case "Auxiliar vs Banco Por Movimiento Distinto":
                var oFrmP19 = await AbrirFormulario<FrmConciliacionManualAuxiliarvsBancoMovimientoDistinto>(); oFrmP19._Main = this; break;


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
                List<TreeNode> oNodo3 = new List<TreeNode>();

                ControlHelper.FreezeControl(this, true);
                this.uiTreeViewEx1.Nodes.Add("", "  CONCILIACIÓN BANCARIA", 0);



                oNodo0 = new TreeNode("  Archivo", 1, 1);
                oNodo0.Nodes.Add("", "  Configurar conexión", 9);
                oNodo0.Nodes.Add("", "  Configurar impresora", 10);
                this.uiTreeViewEx1.Nodes.Add(oNodo0);


                oNodo1 = new List<TreeNode>();
                oNodo2 = new List<TreeNode>();
                oNodo3 = new List<TreeNode>();


                oNodo2.Add(new TreeNode("Automática", 13, 13));

                oNodo3.Add(new TreeNode(" Banco vs Banco", 13, 13));
                oNodo3.Add(new TreeNode(" Auxiliar vs Banco a N M", 13, 13));
                oNodo3.Add(new TreeNode(" Auxiliar vs Auxiliar", 13, 13));
                //oNodo3.Add(new TreeNode(" Auxiliar vs Banco", 13, 13));
                oNodo3.Add(new TreeNode(" Auxiliar vs Banco Por Movimiento Distinto", 13, 13));
                oNodo2.Add(new TreeNode("  Manual", 11, 11, oNodo3.ToArray()));



                oNodo1.Add(new TreeNode(" Conciliación", 11, 11, oNodo2.ToArray()));

                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Carga", 13, 13));
                oNodo2.Add(new TreeNode(" Descarga", 13, 13));
                oNodo2.Add(new TreeNode(" Temporal", 13, 13));
                oNodo2.Add(new TreeNode(" Traspaso de Movimientos", 13, 13));
                oNodo1.Add(new TreeNode("  Movimientos del banco", 11, 11, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode("  Registro de Ajustes", 11, 11));
                oNodo1.Add(new TreeNode("  Traspaso al Histórico", 11, 11));


                oNodo2 = new List<TreeNode>();
                oNodo2.Add(new TreeNode(" Conciliación a 5 Dígitos", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 5 Dígitos por Diferencia de Importe", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 4 Dígitos", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 4 Dígitos por Diferencia de Importe", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 3 Dígitos", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 3 Dígitos por Diferencia de Importe", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 6 Dígitos", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación a 6 Dígitos por Diferencia de Importe", 13, 13));
                oNodo2.Add(new TreeNode(" Depositos Con un Día de Diferencia", 13, 13));
                oNodo2.Add(new TreeNode(" Conciliación Automática con mayor tolerancia", 13, 13));
                oNodo1.Add(new TreeNode("  Procesos especiales", 11, 11, oNodo2.ToArray()));

                oNodo1.Add(new TreeNode("  Desconciliación", 11, 11));
                oNodo1.Add(new TreeNode("  Contracuenta para Ajustes", 11, 11));

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
                oNodo1.Add(new TreeNode(" Bancos", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Cuentas Bancarias", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Usuarios del Sistema", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Mantenimiento Acceso-Rol", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Reglas de Validación", 37, 37, oNodo2.ToArray()));
                oNodo1.Add(new TreeNode(" Parámetros del Sistema", 37, 37, oNodo2.ToArray()));


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
