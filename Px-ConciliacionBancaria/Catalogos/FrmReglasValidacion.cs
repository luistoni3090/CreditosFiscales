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
using Px_Controles.Controls.DataGridView;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Printing;

namespace Px_ConciliacionBancaria.Catalogos
{
    public partial class FrmReglasValidacion : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eREGLASVALIDACION> _Con = new List<eREGLASVALIDACION>();
        List<eREGLASVALIDACION> _ConFiltra = new List<eREGLASVALIDACION>();

        #region Contructor
        public FrmReglasValidacion()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Reglas de Validación";

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
            cbBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCuenta.DropDownStyle = ComboBoxStyle.DropDownList;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await CargarComboBanco();
            await GridInicia();
            await conMenu();

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
                int CuentaId = int.Parse(oGrid.SelectedRows[0].Cells[1].Value.ToString());
                int Orden = int.Parse(oGrid.SelectedRows[0].Cells[2].Value.ToString());
                var existe = await conValidaSiExisteRegistro(BancoId, CuentaId, Orden);
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
            if (oGrid.RowCount > 0)
            {
                var BANCOID = oGrid.CurrentRow.Cells[0].Value.ToString();
                var CuentaID = oGrid.CurrentRow.Cells[1].Value.ToString();
                await conActualiza(int.Parse(BANCOID), int.Parse(CuentaID));
            }
               
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
            oGrid.DataSource = null;
            oGrid.Rows.Clear();

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Banco", Width = 80,  DataPropertyName = "BANCO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Cuenta", Width = 80,  DataPropertyName = "CUENTA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Orden", Width = 80,  DataPropertyName = "ORDEN"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 200,  DataPropertyName = "DESCR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Validación", Width = 350,  DataPropertyName = "VALIDA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Query", Width = 350,  DataPropertyName = "QUERY"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo Dato regresa", Width = 150,  DataPropertyName = "REGRESA_QUERY"  }
,            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid.MultiSelect = false;
            oGrid.ReadOnly = true;
            oGrid.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid);

            tabPage1.Controls.Add(panCatalogo);

            oGrid.Location = new Point(0, 189);

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

            await detCreaTab(0, 0, 0);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlReglasValidacion oCtrl in tabPage.Controls)
                await oCtrl.detBorra();
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlReglasValidacion oCtrl in tabPage.Controls)
            {

                await oCtrl.detGuarda(tabPage);

            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlReglasValidacion oCtrl in tabPage.Controls)
               await oCtrl.detActualiza();
        }
        private async Task detCreaTab(int ID, int CUENTA, int orden)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{ID.ToString()}{CUENTA.ToString()}{orden.ToString()}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{ID.ToString()}{CUENTA.ToString()}{orden.ToString()}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Regla {ID.ToString()}{CUENTA.ToString()}{orden.ToString()}";
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Tag = CUENTA.ToString();

            ctrlReglasValidacion oCtrl = new ctrlReglasValidacion(_Main, ID, CUENTA, orden);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
        }
        #endregion

        #region "Consulta"
        private async Task conActualiza(Int32 bancoId, Int32 cuentaId)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid.DataSource = null;
            await conActualizaDB(bancoId,cuentaId);
            //oGrid.DataSource = _Con;
            oGrid.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Reglas de Validación. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB(Int32 bancoId, Int32 cuentaId)
        {
            try
            {
                oReq.Query = @"SELECT BANCO, CUENTA, ORDEN, DESCR, VALIDA, QUERY, REGRESA_QUERY, 
                                  (
                                    CASE WHEN length(BANCO) > 0 THEN CAST(BANCO AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(CUENTA) > 0 THEN CAST(CUENTA AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(ORDEN) > 0 THEN CAST(ORDEN AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(DESCR) > 0 THEN DESCR ELSE '' END ||
                                    CASE WHEN length(VALIDA) > 0 THEN VALIDA ELSE '' END ||
                                    CASE WHEN length(QUERY) > 0 THEN QUERY ELSE '' END ||
                                    CASE WHEN length(REGRESA_QUERY) > 0 THEN REGRESA_QUERY ELSE '' END
                                    ) AS ALLCOLUMNS
                                FROM REGLA WHERE BANCO = " + bancoId + " AND CUENTA = " + cuentaId + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eREGLASVALIDACION>();

                    _ConFiltra = _Con;
                
            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Cargar combo Banco"
        private async Task CargarComboBanco()
        {

            try
            {
                oReq.Query = "SELECT LPAD(BANCO, 2, '0') BANCO, DESCR FROM BANCO ORDER BY BANCO";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var bancoList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["BANCO"].ToString(),
                            Text = "("+row["BANCO"].ToString()+") "+row["DESCR"].ToString()
                        };
                        bancoList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    cbBanco.DataSource = bancoList;
                    cbBanco.DisplayMember = "Text";
                    cbBanco.ValueMember = "Value";

                    // Asegurarse de que no haya una selección inicial
                    cbBanco.SelectedIndex = -1;
                }

                cbBanco.SelectedIndexChanged += CbBanco_SelectedIndexChanged;
                
            }
            catch (Exception ex)
            { }

        }
        #endregion


        #region "Combo banco item selected"
        private async void CbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

            oGrid.AllowUserToAddRows = false;
            oGrid.DataSource = null;
            oGrid.Rows.Clear();

            if (cbBanco.SelectedItem != null)
            {
                var bancoId = ((ComboBoxItem)cbBanco.SelectedItem).Value;

                var cuentaQuery = "SELECT BANCO, LPAD(CUENTA, 3, '0') CUENTA, DESCR FROM CUENTA WHERE BANCO =" + bancoId + " ORDER BY BANCO, CUENTA";

                oReq.Query = cuentaQuery;
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {

                    var dataTable = oRes.Data.Tables[0];

                    var cuentaList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["CUENTA"].ToString(),
                            Text = "(" + row["CUENTA"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        cuentaList.Add(item);
                    }

                    cbCuenta.SelectedIndexChanged -= CbCuenta_SelectedIndexChanged;

                    cbCuenta.DataSource = null;
                    cbCuenta.Items.Clear();
                    cbCuenta.SelectedIndex = -1;
                    cbCuenta.DataSource = cuentaList;
                    cbCuenta.DisplayMember = "Text";
                    cbCuenta.ValueMember = "Value";
                    cbCuenta.SelectedIndex = -1;

                    cbCuenta.SelectedIndexChanged += CbCuenta_SelectedIndexChanged;

                }

            }
            
         }
        #endregion


        #region "cargar grid validacion"
        private async void CbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

            oGrid.DataSource = null;
            oGrid.Rows.Clear();

            if (cbCuenta.SelectedIndex != -1)
            {
                var cuentaId = int.Parse(((ComboBoxItem)cbCuenta.SelectedItem).Value);

                var bancoId = int.Parse(((ComboBoxItem)cbBanco.SelectedItem).Value);

                oGrid.DataSource = null;
                oGrid.Rows.Clear();
                await conActualiza(bancoId, cuentaId);

            }
          
        }
        #endregion

        #region "Abrir tab Editar Registro"
        private async Task detEdita()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            if (oGrid.Rows.Count > 0) {

                var algo = oGrid.CurrentRow.Cells[0].Value;

                var BANCOID = oGrid.CurrentRow.Cells[0].Value.ToString();
                var CuentaID = oGrid.CurrentRow.Cells[1].Value.ToString();
                var orden = oGrid.CurrentRow.Cells[2].Value.ToString();
                await detCreaTab(int.Parse(BANCOID), int.Parse(CuentaID), int.Parse(orden));

                TimeSpan ts = DateTime.Now - startTime;
                Cursor = Cursors.Default;
            }
               
        }
        #endregion

        #region "Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }
            int BancoId = int.Parse(oGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            int CuentaId = int.Parse(oGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            int Orden = int.Parse(oGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
            var existe = await conValidaSiExisteRegistro(BancoId, CuentaId, Orden);
            if (existe)
            {
                panFiltros.Visible = false;
                await detEdita();
            }

        }
        #endregion

        #region VAlida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(int BancoId, int CuentaId, int Orden)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT BANCO, CUENTA, ORDEN
                                FROM REGLA WHERE BANCO = " + BancoId + " AND CUENTA = " + CuentaId + " AND ORDEN = "+ Orden + " ";
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
            //await conActualiza();
        }
    }
}
