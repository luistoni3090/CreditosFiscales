using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using Px_ConciliacionBancaria.Utiles.Generales;
using Px_ConciliacionBancaria.Catalogos.Controles;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

namespace Px_ConciliacionBancaria.Catalogos
{
    public partial class FrmUsuariosSistema : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eUSUARIOSISTEMA> _Con = new List<eUSUARIOSISTEMA>();
        eUSUARIOSISTEMA _Reg = new eUSUARIOSISTEMA();
        List<eUSUARIOSISTEMA> _ConFiltra = new List<eUSUARIOSISTEMA>();

        #region Contructor
        public FrmUsuariosSistema()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Catálogo de usuarios";

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            uiTabControlExt1.SelectedIndexChanged += uiTabControlExt1_SelectedIndexChanged;

            // Menu
            btnmnuNuevo.Click += btnmnuNuevo_Click;
            btnmnuEdita.Click += btnmnuEdita_Click;
            btnmnuActualizaDet.Click += btnmnuActualizaDet_Click;
            btnmnuGuarda.Click += btnmnuGuarda_Click;
            btnmnuBorra.Click += btnmnuBorra_Click;

            btnmnuActualiza.Click += btnmnuActualiza_Click;
            btnmnuFiltros.Click += btnmnuFiltros_Click;
            btnmnuConsulta.Click += btnmnuConsulta_Click;

            btnmnuImprime.Click += btnmnuImprime_Click;
            btnmnuAyuda.Click += btnmnuAyuda_Click;

            txtBusca.TextChanged += txtBusca_TextChanged;

            await InicioForma();
            await conActualiza();

            Cursor = Cursors.Default;
        }

        private async Task InicioForma()
        {

            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            tabPage1.Text = "Consulta";

            panFiltros.Visible = false;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await GridInicia();

        }
        #endregion

        #region Eventos
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

        private async void uiTabControlExt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await conMenu();
        }
        private async void btnmnuNuevo_Click(object sender, EventArgs e)
        {
            panFiltros.Visible = false;
            await detNuevo();
        }

        private async void btnmnuEdita_Click(object sender, EventArgs e)
        {
            string ID = oGrid.SelectedRows[0].Cells[0].Value.ToString();
            var existe = await conValidaSiExisteRegistro(ID);
            if (existe)
            {
                panFiltros.Visible = false;
                await detEdita();
            }
        }
        private async void btnmnuGuarda_Click(object sender, EventArgs e)
        {
            await detGuarda();
        }
        private async void btnmnuActualizaDet_Click(object sender, EventArgs e)
        {
            await detActualiza();
        }
        private async void btnmnuBorra_Click(object sender, EventArgs e)
        {
            await detBorra();
        }
        private async void btnmnuActualiza_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }
        private void btnmnuFiltros_Click(object sender, EventArgs e)
        {
            panFiltros.Visible = !panFiltros.Visible;
        }
        private void btnmnuConsulta_Click(object sender, EventArgs e)
        {
            uiTabControlExt1.SelectedIndex = 0;
        }
        private void btnmnuImprime_Click(object sender, EventArgs e)
        {
        }
        private void btnmnuAyuda_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Grid
        private async Task GridInicia()
        {

            oGrid.AllowUserToAddRows = false;
            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Login", Width = 150,  DataPropertyName = "APP_LOGIN" },
                new DataGridViewTextBoxColumn() { HeaderText = "Apellido Paterno", Width = 150,  DataPropertyName = "AP_PATER"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Apellido Materno", Width = 150,  DataPropertyName = "AP_MATER"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Nombre", Width = 150,  DataPropertyName = "NOMBRE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Municipio", Width = 150,  DataPropertyName = "MUNICIPIO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Password", Width = 150,  DataPropertyName = "APP_PWD"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Rfc", Width = 150,  DataPropertyName = "RFC"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Usuario", Width = 150,  DataPropertyName = "SYSUSER"  }
,            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            oGrid.ReadOnly = true;

            oGrid.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid);


            await Task.Delay(0);
        }
        #endregion

        #region "Crear Menu"
        private async Task conMenu()
        {
            bool bConsulta = uiTabControlExt1.SelectedIndex == 0 ? true : false;

            btnmnuNuevo.Visible = true;
            btnmnuEdita.Visible = bConsulta;
            btnmnuActualizaDet.Visible = !bConsulta;
            btnmnuBorra.Visible = !bConsulta;
            btnmnuGuarda.Visible = !bConsulta;

            btnmnuActualiza.Visible = bConsulta;
            btnmnuFiltros.Visible = bConsulta;
            btnmnuConsulta.Visible = !bConsulta;

            btnmnuImprime.Visible = true;
            btnmnuAyuda.Visible = true;
        }
        #endregion


        #region "Agregar funcionalidad Botones del CRUD"
        private async Task detNuevo()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            await detCreaTab("0");

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlUsuariosSistema oCtrl in tabPage.Controls)
                await oCtrl.detBorra();
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlUsuariosSistema oCtrl in tabPage.Controls)
            {

                await oCtrl.detGuarda(tabPage);

            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlUsuariosSistema oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string ID)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{ID.ToString()}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{ID.ToString()}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Usuario {ID}";
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Tag = ID.ToString();

            ctrlUsuariosSistema oCtrl = new ctrlUsuariosSistema(_Main, ID);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
        }

        #endregion

        #region "Abrir tab Editar Registro"
        private async Task detEdita()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            var ID = oGrid.CurrentRow.Cells[0].Value.ToString();
            await detCreaTab(ID);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion

        #region "Consulta"
        private async Task conActualiza()
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            await conMenu();

            oGrid.DataSource = null;
            await conActualizaDB();
            //oGrid.DataSource = _Con;
            oGrid.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Usuarios. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB()
        {
            try
            {
                oReq.Query = @"SELECT APP_LOGIN, AP_PATER, AP_MATER, NOMBRE, MU.DESCR AS MUNICIPIO,
                             APP_PWD, RFC, SYSUSER,
                             (
                                CASE WHEN length(APP_LOGIN) > 0 THEN APP_LOGIN ELSE '' END ||
                                CASE WHEN length(AP_PATER) > 0 THEN AP_PATER ELSE '' END ||
                                CASE WHEN length(AP_PATER) > 0 THEN AP_PATER ELSE '' END ||
                                CASE WHEN length(MU.DESCR) > 0 THEN MU.DESCR ELSE '' END ||
                                CASE WHEN length(APP_PWD) > 0 THEN APP_PWD ELSE '' END ||
                                CASE WHEN length(RFC) > 0 THEN RFC ELSE '' END ||
                                CASE WHEN length(SYSUSER) > 0 THEN SYSUSER ELSE '' END
                              ) AS ALLCOLUMNS
                             FROM DGI_USR USR
                             JOIN MUNICIPIO MU ON (USR.MPO = MU.MPO) ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eUSUARIOSISTEMA>();

                _ConFiltra = _Con;
            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Cell Click Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }
            string ID = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            var existe = await conValidaSiExisteRegistro(ID);
            if (existe)
            {
                panFiltros.Visible = false;
                await detEdita();
            }

        }
        #endregion

        #region VAlida si esxiste registro
        private async Task<bool> conValidaSiExisteRegistro(string APP_LOGIN)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT APP_LOGIN, NOMBRE
                                FROM DGI_USR WHERE APP_LOGIN = '"+ APP_LOGIN + "' ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                    existe = true;

                return existe;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");
                return existe;
            }

        }
        #endregion


        #region Buscar
        private async void txtBusca_TextChanged(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {

            await Task.Delay(0);
            _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con : _Con.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
            oGrid.DataSource = _ConFiltra;

        }
        #endregion

        private async void button1_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }
    }
}
