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

using Px_CreditosFiscales.Utiles.Generales;
using Px_CreditosFiscales.Catalogos.Controles;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using System.Security.Cryptography;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

namespace Px_CreditosFiscales.Catalogos
{
    public partial class FrmDescuentos : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        DataGridView oGrid2 = new DataGridView();
        List<eDESCUENTOS> _Con = new List<eDESCUENTOS>();
        List<eINCISO> _Con2 = new List<eINCISO>();
        List<eDESCUENTOS> _ConFiltra = new List<eDESCUENTOS>();
        eDESCUENTOS _Reg3 = new eDESCUENTOS();

        #region Contructor
        public FrmDescuentos()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Descuentos";

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

            await GridDescuentos();
            await cargaComboTipoCredito();

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
                string CONSEC = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                string TIPO_CREDITO = comboTipoCredito.SelectedValue.ToString();
                var existe = await conValidaSiExisteRegistro(CONSEC, TIPO_CREDITO);
                if (existe)
                {
                    panFiltros.Visible = false;
                    await detEdita("Editar");
                }
                else {
                    await _Main.Status($"Registro no disponible", (int)MensajeTipo.Error);
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

        #region Grid
        private async Task GridDescuentos()
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
                new DataGridViewTextBoxColumn() { HeaderText = "Consec", Width = 80,  DataPropertyName = "CONSEC" },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 450,  DataPropertyName = "DESCR" },
                new DataGridViewTextBoxColumn() { HeaderText = "Vigencia Inicial", Width = 150,  DataPropertyName = "VIGENCIA_INI"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Vigencia Final", Width = 150,  DataPropertyName = "VIGENCIA_FIN"  }
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
        private async Task GridDescuentoAnt()
        {

            oGrid2.AutoGenerateColumns = false;
            oGrid2.Dock = DockStyle.Fill;
            oGrid2.BackgroundColor = Color.White;
            oGrid2.BorderStyle = BorderStyle.None;

            oGrid2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Periodo Inicial", Width = 150,  DataPropertyName = "PERIODO_INI" },
                new DataGridViewTextBoxColumn() { HeaderText = "Periodo Final", Width = 150,  DataPropertyName = "PERIODO_FIN" },
                new DataGridViewTextBoxColumn() { HeaderText = "%", Width = 150,  DataPropertyName = "POR_CIENTO"  },
            });

            oGrid2.AutoGenerateColumns = false;
            oGrid2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid2.MultiSelect = false;
            oGrid2.ReadOnly = true;

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

            await detCreaTab("0",valida,"0","0");

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlDescuentos oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlDescuentos oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlDescuentos oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string CONSEC, string valida, string DESCRIPCION, string TIPO)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{CONSEC}{TIPO}{DESCRIPCION.Trim()}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{CONSEC}{TIPO}{DESCRIPCION.Trim()}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{CONSEC}{TIPO}{DESCRIPCION.Trim()}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"Nuevo";
            else
            tabPage.Text = $"               {DESCRIPCION.Trim()}";
            ctrlDescuentos oCtrl = new ctrlDescuentos(_Main, CONSEC, valida, DESCRIPCION, TIPO);
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
            
            await conActualizaBDGridDescuentos();
            oGrid.DataSource = _ConFiltra;
            //oGrid2.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Grid Descuentos. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task cargaComboTipoCredito()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT TIPO_CREDITO, DESCR FROM TIPO_CREDITO";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["TIPO_CREDITO"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboTipoCredito.DataSource = comboList;
                    comboTipoCredito.DisplayMember = "Text";
                    comboTipoCredito.ValueMember = "Value";

                    comboTipoCredito.SelectedIndexChanged += new EventHandler(cbTipoCredito_SelectedIndexChanged);

                    comboTipoCredito.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async void cbTipoCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            await conActualiza();

        }


        private async Task conActualizaBDGridDescuentos()
        {
                
            string TIPO_CREDITO = comboTipoCredito.SelectedValue.ToString();

            try
            {
                oReq.Query = @"SELECT   CONSEC,
                                        DESCR, 
                                        TO_CHAR(VIGENCIA_INI, 'DD/MM/YYYY') AS VIGENCIA_INI,
                                        TO_CHAR(VIGENCIA_FIN, 'DD/MM/YYYY')  AS VIGENCIA_FIN,
                                        (
                                            CASE WHEN length(CONSEC) > 0 THEN CAST(CONSEC AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(DESCR) > 0 THEN DESCR ELSE '' END
                                        ) AS ALLCOLUMNS
                                        FROM TIPO_DESCTO
                                        WHERE TIPO  = '" + TIPO_CREDITO + "' ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eDESCUENTOS>();

                _ConFiltra = _Con;

            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Consulta2"
        private async Task conActualiza2(Int32 CONSEC)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid2.DataSource = null;
            await conActualizaDB2(CONSEC);
            oGrid2.DataSource = _Con2;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Descuenntos. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB2(Int32 GRUPO)
        {
            /*
            try
            {
                oReq.Query = @"SELECT CLAVE,
                                GRUPO,
                                INC.DESCR AS CONCEPTO, 
                                CASE 
	                                WHEN TIPO = 'I' THEN 'IMPUESTOS'
	                                WHEN TIPO = 'A' THEN 'ACCESORIOS'
	                                ELSE TIPO
	                                END AS TIPO,
                                INC.INCISO, 
                                INC_ARMONIZ AS INCISO_2012,
                                INC.ORDEN, 
                                CASE 
	                                WHEN CAPITAL = 'S' THEN 'SI'
	                                WHEN CAPITAL = 'N' THEN 'NO'
	                                ELSE CAPITAL
	                                END AS CAPITAL,
                                IDN.DESCR AS IDENTIFICADOR,
                                CASE 
	                                WHEN TIPO_IMPTO = 'R' THEN 'RETENIDO'
	                                WHEN TIPO_IMPTO = 'P' THEN 'PROPIO'
	                                ELSE TIPO_IMPTO
	                                END AS TIPO_IMPTO,
                                    GRUPO_EQ AS GRUPO_EQUIVALENTE,
                                    INCISO_EQ AS INCISO_EQUIVALENTE,
                                    CCCP,
                                    CCLP,
                                    CING,
                                    PPTO_EJER,
                                    DEVENGADO,
                                    OB.DESCR AS OBLIGACION,
                                    PASDIF_CP,
                                    PASDIF_LP,
                                              (
                                                    CASE WHEN length(CLAVE) > 0 THEN CAST(CLAVE AS VARCHAR(10)) ELSE '' END ||
					                                CASE WHEN length(GRUPO) > 0 THEN CAST(GRUPO AS VARCHAR(10)) ELSE '' END ||
					                                CASE WHEN length(INC.DESCR) > 0 THEN INC.DESCR ELSE '' END || 
						                            CASE WHEN length(TIPO) > 0 THEN TIPO ELSE '' END ||
						                            CASE WHEN length(INC.INCISO) > 0 THEN INC.INCISO ELSE '' END ||
						                            CASE WHEN length(INC_ARMONIZ) > 0 THEN INC_ARMONIZ ELSE '' END ||
						                            CASE WHEN length( INC.ORDEN) > 0 THEN CAST( INC.ORDEN AS VARCHAR(10)) ELSE '' END ||
						                            CASE WHEN length(CAPITAL) > 0 THEN CAPITAL ELSE '' END ||
						                            CASE WHEN length(IDN.DESCR) > 0 THEN IDN.DESCR ELSE '' END ||
						                            CASE WHEN length(TIPO_IMPTO) > 0 THEN TIPO_IMPTO ELSE '' END ||
						                            CASE WHEN length(GRUPO_EQ) > 0 THEN CAST(GRUPO_EQ AS VARCHAR(10)) ELSE '' END ||
						                            CASE WHEN length(INCISO_EQ) > 0 THEN INCISO_EQ ELSE '' END ||
						                            CASE WHEN length(CCCP) > 0 THEN CCCP ELSE '' END ||
						                            CASE WHEN length(CCLP) > 0 THEN CCLP ELSE '' END ||
						                            CASE WHEN length(CING) > 0 THEN CING ELSE '' END ||
						                            CASE WHEN length(PPTO_EJER) > 0 THEN PPTO_EJER ELSE '' END ||
						                            CASE WHEN length(DEVENGADO) > 0 THEN DEVENGADO ELSE '' END ||
						                            CASE WHEN length(OB.DESCR) > 0 THEN OB.DESCR ELSE '' END ||
						                            CASE WHEN length(PASDIF_CP) > 0 THEN DEVENGADO ELSE '' END ||
                                                    CASE WHEN length(PASDIF_LP) > 0 THEN PASDIF_LP ELSE '' END
                                                ) AS ALLCOLUMNS
                                    FROM INCISO INC
                                    LEFT JOIN IDEN IDN ON (INC.IDEN = IDN.IDEN AND INC.IDEN = IDN.IDEN2)
                                    LEFT JOIN OBLIGACION OB ON (INC.OBLIGACION = OB.OBLIGACION)
                                    WHERE GRUPO = " + GRUPO + " ORDER BY CLAVE ";

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con2 = oRes.Data.Tables[0].AListaDe<eINCISO>();

                //_ConFiltra = _Con2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }*/

        }

        #endregion

        #region "Abrir tab Editar Registro"
        private async Task detEdita(string valida)
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            string CONSEC = oGrid.CurrentRow.Cells[0].Value.ToString();
            string DESCRIPCION = oGrid.CurrentRow.Cells[1].Value.ToString();
            //string TIPO = comboTipoCredito.SelectedValue.ToString();
            await detCreaTab(CONSEC, valida, _Reg3.DESCR, _Reg3.TIPO);

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
                    GridDescuentoAnt();

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

            //var GRUPO = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            //await conActualiza2(int.Parse(GRUPO));

        }
        #endregion


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string CONSEC, string TIPO_CREDITO)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT CONSEC, TIPO, DESCR
                                FROM TIPO_DESCTO WHERE CONSEC = " + CONSEC + " AND TIPO = '" + TIPO_CREDITO + "' ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0){ 
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eDESCUENTOS>();
                    existe = true;
                }

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
            _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con : _Con.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
            oGrid.DataSource = _ConFiltra;

        }

        public class ComboBoxItem
        {
            public string Text { get; set; } // Texto que se muestra en el ComboBox
            public object Value { get; set; } // Valor asociado al item

            public override string ToString()
            {
                return Text; // Esto es lo que se muestra en el ComboBox
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }
    }
}
