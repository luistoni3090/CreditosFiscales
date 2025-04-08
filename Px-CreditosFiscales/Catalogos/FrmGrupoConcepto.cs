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

namespace Px_CreditosFiscales.Catalogos
{
    public partial class FrmGrupoConcepto : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        DataGridView oGrid2 = new DataGridView();
        List<eGRUPO> _Con = new List<eGRUPO>();
        List<eINCISO> _Con2 = new List<eINCISO>();
        List<eINCISO> _ConFiltra = new List<eINCISO>();
        eValidar _Reg3 = new eValidar();

        #region Contructor
        public FrmGrupoConcepto()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Conceptos";

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

            await GridGrupo();

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
                int CLAVE = int.Parse(oGrid2.SelectedRows[0].Cells[0].Value.ToString());
                int GRUPO = int.Parse(oGrid2.SelectedRows[0].Cells[1].Value.ToString());
                var existe = await conValidaSiExisteRegistro(CLAVE, GRUPO);
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
            var GRUPO = oGrid.CurrentRow.Cells[0].Value.ToString();
            await conActualiza2(int.Parse(GRUPO));
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
        private async Task GridGrupo()
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
                new DataGridViewTextBoxColumn() { HeaderText = "Grupo", Width = 80,  DataPropertyName = "GRUPO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 450,  DataPropertyName = "DESCRIPCION"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo Impuesto", Width = 150,  DataPropertyName = "TIPO_IMPUESTO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Orden", Width = 80,  DataPropertyName = "ORDEN"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Concepto", Width = 80,  DataPropertyName = "CONCEPTO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Status", Width = 100,  DataPropertyName = "STATUS"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Grupo Descripción", Width = 450,  DataPropertyName = "GRUPO_DESCRIPCION"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Impuesto", Width = 200,  DataPropertyName = "IMPUESTO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "ISRT", Width = 200,  DataPropertyName = "ISRT"  }
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
        private async Task GridInciso()
        {

            oGrid2.AutoGenerateColumns = false;
            oGrid2.Dock = DockStyle.Fill;
            oGrid2.BackgroundColor = Color.White;
            oGrid2.BorderStyle = BorderStyle.None;

            oGrid2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Clave", Width = 80,  DataPropertyName = "CLAVE" },
                new DataGridViewTextBoxColumn() { HeaderText = "Grupo", Width = 80,  DataPropertyName = "GRUPO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Concepto", Width = 450,  DataPropertyName = "CONCEPTO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Impuesto", Width = 100,  DataPropertyName = "TIPO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Inciso", Width = 100,  DataPropertyName = "INCISO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Inciso 2012", Width = 100,  DataPropertyName = "INCISO_2012"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Orden", Width =80,  DataPropertyName = "ORDEN"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Capital", Width = 80,  DataPropertyName = "CAPITAL"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Identificador", Width = 130,  DataPropertyName = "IDENTIFICADOR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo Impuesto", Width = 100,  DataPropertyName = "TIPO_IMPTO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Grupo Equivalente", Width = 150,  DataPropertyName = "GRUPO_EQUIVALENTE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Inciso Equivalente", Width = 150,  DataPropertyName = "INCISO_EQUIVALENTE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "CCCP", Width = 100,  DataPropertyName = "CCCP"  },
                new DataGridViewTextBoxColumn() { HeaderText = "CCLP", Width = 100,  DataPropertyName = "CCLP"  },
                new DataGridViewTextBoxColumn() { HeaderText = "CING", Width = 100,  DataPropertyName = "CING"  },
                new DataGridViewTextBoxColumn() { HeaderText = "PPTO_EJER", Width = 100,  DataPropertyName = "PPTO_EJER"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Devengado", Width = 100,  DataPropertyName = "DEVENGADO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Obligacion", Width = 100,  DataPropertyName = "OBLIGACION"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Pasivo Diferido CP", Width = 150,  DataPropertyName = "PASDIF_CP"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Pasivo Diferido LP", Width = 150,  DataPropertyName = "PASDIF_LP"  },
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

            await detCreaTab(0,0,valida,"0");

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGrupoConcepto oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGrupoConcepto oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGrupoConcepto oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(int CLAVE, int GRUPO, string valida, string CONCEPTO)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{CLAVE}{GRUPO}{CONCEPTO.Trim()}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{CLAVE}{GRUPO}{CONCEPTO.Trim()}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{CLAVE}{GRUPO}{CONCEPTO}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"{CLAVE}-{GRUPO}-{CONCEPTO.Trim()}";
            else
            tabPage.Text = "               " + CLAVE+"-"+GRUPO +"-"+CONCEPTO.Trim();

            ctrlGrupoConcepto oCtrl = new ctrlGrupoConcepto(_Main, CLAVE, GRUPO, valida, CONCEPTO);
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
                oReq.Query = @"SELECT GI.GRUPO,
                                GI.DESCR AS DESCRIPCION,
                                TI.DESCR AS TIPO_IMPUESTO,
                                ORDEN,
                                CPTO_INGRESO AS CONCEPTO,
                                CASE WHEN GI.STATUS = 'H' THEN 'HISTORICO' WHEN GI.STATUS = 'A' THEN 'ACTIVO' ELSE GI.STATUS END AS STATUS,
                                (SELECT (GI1.GRUPO || '-' || GI1.DESCR) FROM GRUPO_IMPTO GI1 WHERE GI1.GRUPO = GI.GRUPO_EQ) AS GRUPO_DESCRIPCION,
                                CI.DESCR AS IMPUESTO,
                                CASE WHEN ISRT = 'N' THEN 'NO'WHEN ISRT = 'S' THEN 'SI' ELSE ISRT END AS ISRT 
                                FROM GRUPO_IMPTO GI 
                                LEFT JOIN TIPO_IMPTO TI ON (GI.TIPO_IMPTO = TI.TIPO_IMPTO)
                                LEFT JOIN CF_IMPUESTO CI ON (GI.IMPTO = CI.IMPTO) 
                                ORDER BY GI.GRUPO";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eGRUPO>();

            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Consulta2"
        private async Task conActualiza2(Int32 GRUPO)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid2.DataSource = null;
            await conActualizaDB2(GRUPO);
            oGrid2.DataSource = _Con2;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Conceptos. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB2(Int32 GRUPO)
        {
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

            var CLAVE = oGrid2.CurrentRow.Cells[0].Value.ToString();
            var GRUPO = oGrid2.CurrentRow.Cells[1].Value.ToString();
            //var CONCEPTO = oGrid2.CurrentRow.Cells[2].Value.ToString();
            await detCreaTab(int.Parse(CLAVE), int.Parse(GRUPO), valida, _Reg3.CONCEPTO);

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
                    GridInciso();

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

            var GRUPO = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            await conActualiza2(int.Parse(GRUPO));

        }
        #endregion


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(int CLAVE, int GRUPO)
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT CLAVE, GRUPO, DESCR AS CONCEPTO
                                FROM INCISO WHERE CLAVE = " + CLAVE + " AND GRUPO = " + GRUPO + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0){ 
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eValidar>();
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
            _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con2 : _Con2.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
            oGrid2.DataSource = _ConFiltra;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }

    }
}
