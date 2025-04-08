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
using System.Security.Cryptography;

namespace Px_ConciliacionBancaria.Catalogos
{
    public partial class FrmCuentasBancarias : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        DataGridView oGrid2 = new DataGridView();
        List<eCUENTASBANCARIAS> _Con = new List<eCUENTASBANCARIAS>();
        List<eCUENTASBANCARIAS2> _Con2 = new List<eCUENTASBANCARIAS2>();
        List<eTIPO> _Con3 = new List<eTIPO>();
        List<eCUENTASBANCARIAS2> _ConFiltra = new List<eCUENTASBANCARIAS2>();

        #region Contructor
        public FrmCuentasBancarias()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Cuentas Bancarias";

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
            await detNuevo("Nuevo");
        }

        private async void btnmnuEdita_Click(object sender, EventArgs e)
        {
           
            if (oGrid.Rows.Count > 0)
            {
                int BancoId = int.Parse(oGrid2.SelectedRows[0].Cells[0].Value.ToString());
                int CuentaId = int.Parse(oGrid2.SelectedRows[0].Cells[1].Value.ToString());
                var existe = await conValidaSiExisteRegistro(BancoId, CuentaId);
                if (existe)
                {
                    panFiltros.Visible = false;
                    await detEdita("Edita");
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
            var BANCOID = oGrid.CurrentRow.Cells[0].Value.ToString();
            await conActualiza2(int.Parse(BANCOID));
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
            var tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Clave", Width = 50,  DataPropertyName = "BANCO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 200,  DataPropertyName = "DESCR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Dirección", Width = 300,  DataPropertyName = "DIRECC"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Teléfono", Width = 100,  DataPropertyName = "TEL"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Ejecutivo", Width = 330,  DataPropertyName = "EJECUTIVO"  }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid.MultiSelect = false;
            oGrid.ReadOnly = true;
            oGrid.CellClick += oGrid_CellClick;

            tableLayoutPanel.Controls.Add(oGrid, 0, 0);

            panCatalogo.Controls.Add(tableLayoutPanel);

            oGrid.DataBindingComplete += oGrid_DataBindingComplete;


            await Task.Delay(0);
        }
        #endregion

        #region Grid2
        private async Task GridInicia2()
        {

            oGrid2.AutoGenerateColumns = false;
            oGrid2.Dock = DockStyle.Fill;
            oGrid2.BackgroundColor = Color.White;
            oGrid2.BorderStyle = BorderStyle.None;

            oGrid2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Banco", Width = 50,  DataPropertyName = "BANCO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Cuenta", Width = 100,  DataPropertyName = "CUENTA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Municipio", Width = 100,  DataPropertyName = "MPO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Delegación", Width = 100,  DataPropertyName = "DEL"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Cuenta Bancaria", Width = 130,  DataPropertyName = "DESCR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo", Width = 250,  DataPropertyName = "TIPO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Moneda", Width = 100,  DataPropertyName = "MONEDA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Cta. Contable", Width = 130,  DataPropertyName = "CTACONTABLE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Sub. Cuenta", Width = 100,  DataPropertyName = "SUBCTA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Subsubcuenta", Width = 100,  DataPropertyName = "SUBSUBCTA"  }
            });

            oGrid2.AutoGenerateColumns = false;
            oGrid2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid2.MultiSelect = false;
            oGrid2.ReadOnly = true;
            oGrid2.CellClick += oGrid2_CellClick;

            ((TableLayoutPanel)panCatalogo.Controls[0]).Controls.Add(oGrid2, 0, 1);

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
        private async Task detNuevo(string valida)
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            await detCreaTab(0,0,valida,"0");

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlCuentasBancarias oCtrl in tabPage.Controls)
                await oCtrl.detBorra();
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlCuentasBancarias oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlCuentasBancarias oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(int ID, int CUENTA, string valida, string CuentaDescr)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{CuentaDescr}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{CuentaDescr}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"CUENTA {CuentaDescr}";
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Tag = CUENTA.ToString();

            var BANCOID = int.Parse(oGrid.CurrentRow.Cells[0].Value.ToString());
            ctrlCuentasBancarias oCtrl = new ctrlCuentasBancarias(_Main, BANCOID, CUENTA, valida);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
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
            oGrid.DataSource = _Con;
            oGrid2.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Cuentas Bancarias. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB()
        {
            try
            {
                oReq.Query = "Select BANCO, DESCR, DIRECC, TEL, EJECUTIVO From Banco ORDER BY BANCO";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eCUENTASBANCARIAS>();

            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Consulta2"
        private async Task conActualiza2(Int32 ID)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid2.DataSource = null;
            await conActualizaDB2(ID);
            oGrid2.DataSource = _Con2;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Cuentas Bancarias. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB2(Int32 ID)
        {
            try
            {
                oReq.Query = @"SELECT CTA.BANCO, CTA.CUENTA, MU.DESCR AS MPO, DEL.DESCR DEL, CTA.DESCR, TCTA.DESCR TIPO, 
                    CASE WHEN CTA.MONEDA = 'P' THEN 'Pesos'
                    WHEN CTA.MONEDA = 'D' THEN 'Dólares' 
                    ELSE CTA.MONEDA END AS MONEDA, CTA.CTACONTABLE, CTA.SUBCTA, CTA.SUBSUBCTA,
                    (
                        CASE WHEN length(CTA.BANCO) > 0 THEN CAST(CTA.BANCO AS VARCHAR(10)) ELSE '' END ||
					    CASE WHEN length(CTA.CUENTA) > 0 THEN CAST(CTA.CUENTA AS VARCHAR(10)) ELSE '' END ||
					    CASE WHEN length(DEL.DESCR) > 0 THEN DEL.DESCR ELSE '' END || 
						CASE WHEN length(MU.DESCR) > 0 THEN MU.DESCR ELSE '' END || 
                        CASE WHEN length(CTA.DESCR) > 0 THEN CTA.DESCR ELSE '' END ||
                        CASE WHEN length(TCTA.DESCR) > 0 THEN TCTA.DESCR ELSE '' END ||
                        CASE WHEN length(CTA.CTACONTABLE) > 0 THEN CTA.CTACONTABLE ELSE '' END || 
                        CASE WHEN length(CTA.SUBCTA) > 0 THEN CTA.SUBCTA ELSE '' END ||
                        CASE WHEN length(CTA.SUBSUBCTA) > 0 THEN CTA.SUBSUBCTA ELSE '' END ||
                        CASE WHEN length(MONEDA) > 0 THEN MONEDA ELSE '' END
                    ) AS ALLCOLUMNS
                    From CUENTA CTA
                    LEFT JOIN MUNICIPIO MU ON (CTA.MPO = MU.MPO)
                    LEFT JOIN DELEGACION DEL ON (CTA.MPO = DEL.MPO)
                    LEFT JOIN TIPO_CUENTA TCTA ON (CTA.TIPO = TCTA.TIPO_CUENTA)
                    WHERE CTA.BANCO = " + ID +" ";

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con2 = oRes.Data.Tables[0].AListaDe<eCUENTASBANCARIAS2>();

                _ConFiltra = _Con2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        #endregion

        #region "Abrir tab Editar Registro"
        private async Task detEdita(string valida)
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            var BANCOID = oGrid2.CurrentRow.Cells[0].Value.ToString();
            var CuentaID = oGrid2.CurrentRow.Cells[1].Value.ToString();
            var CuentaDescr = oGrid2.CurrentRow.Cells[4].Value.ToString();
            await detCreaTab(int.Parse(BANCOID), int.Parse(CuentaID), valida, CuentaDescr);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion

        private async void oGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            oGrid.DataBindingComplete -= oGrid_DataBindingComplete;

            if (oGrid.Rows.Count > 0)
            {
                
                if (oGrid2.Rows.Count <= 0) {
                    GridInicia2();

                    var cellEventArgs = new DataGridViewCellEventArgs(0, 0);
                    oGrid_CellClick(sender, cellEventArgs);
                }

                
            }
        }

        #region "Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }

            var ID = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            await conActualiza2(int.Parse(ID));

        }
        #endregion

        #region "Seleciona row Grid2"
        private async void oGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int BancoId = int.Parse(oGrid2.Rows[e.RowIndex].Cells[0].Value.ToString());
            int CuentaId = int.Parse(oGrid2.Rows[e.RowIndex].Cells[1].Value.ToString());
            var existe = await conValidaSiExisteRegistro(BancoId, CuentaId);
            if (existe)
            {
                panFiltros.Visible = false;
                await detEdita("Edita");
            }

        }
        #endregion

        #region VAlida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(int BancoId, int CuentaId)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT BANCO, CUENTA
                                FROM CUENTA WHERE BANCO = " + BancoId + " AND CUENTA = " + CuentaId + " ";
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
        #endregion

        private async Task Buscar()
        {
            
            await Task.Delay(0);
            _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con2 : _Con2.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
            oGrid2.DataSource = _ConFiltra;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }

    }
}
