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
using Px_Utiles.Models.Sistemas.ConciliacionBancaria;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

namespace Px_ConciliacionBancaria.Catalogos
{
    public partial class FrmBancos : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eBANCO> _Con = new List<eBANCO>();
        List<eBANCO> _ConFiltra = new List<eBANCO>();

        #region Contructor
        public FrmBancos()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Bancos";

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
            if (oGrid.Rows.Count > 0)
            {
                int BancoId = int.Parse(oGrid.SelectedRows[0].Cells[0].Value.ToString());
                var existe = await conValidaSiExisteRegistro(BancoId);
                if (existe)
                {
                    panFiltros.Visible = false;
                    await detEdita();
                }
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

        #region "Crear el Grid"
        private async Task GridInicia()
        {

            oGrid.AllowUserToAddRows = false;
            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Clave", Width = 60,  DataPropertyName = "BANCO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 230,  DataPropertyName = "DESCR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Dirección", Width = 330,  DataPropertyName = "DIRECC"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Teléfono", Width = 100,  DataPropertyName = "TEL"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Ejecutivo", Width = 330,  DataPropertyName = "EJECUTIVO"  }
            });
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

            await detCreaTab(0,"0");

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlBanco oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlBanco oCtrl in tabPage.Controls) {

                await oCtrl.detGuarda(tabPage);

            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlBanco oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(int ID, string DescrBanco)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{ID.ToString()}{DescrBanco.ToString()}")
                {
                    //uiTabControlExt1.SelectedIndex = xtabPage.;
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{ID.ToString()}{DescrBanco.ToString()}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Banco {ID.ToString()}-{DescrBanco.ToString()}";
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Tag = ID.ToString();

            ctrlBanco oCtrl = new ctrlBanco(_Main, ID);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
        }
        #endregion


        #region "Generando Consulta"
        private async Task conActualiza()
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            await conMenu();

            oGrid.DataSource = null;
            await conActualizaDB();
            oGrid.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de bancos. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }
        #endregion


        #region "BD carga datos grid Bancos"
        private async Task conActualizaDB()
        {
            try
            {
                oReq.Query = @"Select BANCO, DESCR, DIRECC, TEL, EJECUTIVO,
                               (
                                CASE WHEN length(BANCO) > 0 THEN CAST(BANCO AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(DESCR) > 0 THEN DESCR ELSE '' END ||
                                CASE WHEN length(DIRECC) > 0 THEN DIRECC ELSE '' END ||
                                CASE WHEN length(TEL) > 0 THEN CAST(TEL AS VARCHAR(15)) ELSE '' END ||
                                CASE WHEN length(EJECUTIVO) > 0 THEN EJECUTIVO ELSE '' END
                                   ) AS ALLCOLUMNS
                                From Banco";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eBANCO>();

                _ConFiltra = _Con;
            }
            catch (Exception ex)
            { }

        }
        #endregion


        #region "Abrir tab Editar Registro"
        private async Task detEdita()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            var ID = oGrid.CurrentRow.Cells[0].Value.ToString();
            var DescrBanco = (oGrid.CurrentRow.Cells[1].Value != null) ? oGrid.CurrentRow.Cells[1].Value.ToString() : "";
            await detCreaTab(int.Parse(ID),DescrBanco);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion

        #region "Eventos del Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }
            int BancoId = int.Parse(oGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            var existe = await conValidaSiExisteRegistro(BancoId);
            if (existe)
            {
                panFiltros.Visible = false;
                await detEdita();
            }

        }
        #endregion

        #region VAlida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(int BancoId)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT BANCO, DESCR
                                FROM BANCO WHERE BANCO = " + BancoId + " ";
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
