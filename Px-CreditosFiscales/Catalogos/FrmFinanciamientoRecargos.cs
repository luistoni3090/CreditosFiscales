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
    public partial class FrmFinanciamientoRecargos : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        DataGridView oGrid2 = new DataGridView();
        List<eFINANC_ESTATAL> _Con = new List<eFINANC_ESTATAL>();
        List<eFINANC_ESTATAL> _ConFiltra = new List<eFINANC_ESTATAL>();
        List<eFINANC_FEDERAL> _Con2 = new List<eFINANC_FEDERAL>();
        List<eFINANC_FEDERAL> _ConFiltra2 = new List<eFINANC_FEDERAL>();
        eFINANC_ESTATAL _Reg3 = new eFINANC_ESTATAL();
        eFINANC_FEDERAL _Reg4 = new eFINANC_FEDERAL();

        #region Contructor
        public FrmFinanciamientoRecargos()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Financiamiento y Recargos";

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
            await conActualiza2();


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

            await GridFinanciamientoEstatal();
            await GridFinanciamientoFederal();
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
            string TIPO = comboTipoCredito.SelectedValue.ToString();

            if (TIPO == "E")
            {

                if (oGrid.Rows.Count > 0)
                {
                    string ANIO = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                    string MES = oGrid.SelectedRows[0].Cells[1].Value.ToString();
                    var existe = await conValidaSiExisteRegistro(ANIO, MES, TIPO);
                    if (existe)
                    {
                        panFiltros.Visible = false;
                        await detEdita(ANIO, MES, TIPO, "Editar");
                    }
                    else
                    {
                        await _Main.Status($"Registro no disponible", (int)MensajeTipo.Error);
                    }
                }
            }
            else if (TIPO == "F")
            {

                if (oGrid2.Rows.Count > 0)
                {
                    string ANIO = oGrid2.SelectedRows[0].Cells[0].Value.ToString();
                    string MES = oGrid2.SelectedRows[0].Cells[1].Value.ToString();
                    var existe = await conValidaSiExisteRegistro(ANIO, MES, TIPO);
                    if (existe)
                    {
                        panFiltros.Visible = false;
                        await detEdita(ANIO, MES, TIPO, "Editar");
                    }
                    else
                    {
                        await _Main.Status($"Registro no disponible", (int)MensajeTipo.Error);
                    }
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
            string TIPO = comboTipoCredito.SelectedValue.ToString();
            if(TIPO == "E")
            await conActualiza();
            else if (TIPO == "F")
            await conActualiza2();
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

        #region "Grid Financiamiento Estatal"
        private async Task GridFinanciamientoEstatal()
        {

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Año", Width = 80,  DataPropertyName = "ANIO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Mes", Width = 80,  DataPropertyName = "MES" },
                new DataGridViewTextBoxColumn() { HeaderText = "Financiamiento", Width = 200,  DataPropertyName = "IND_FINANC"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Recargos", Width = 150,  DataPropertyName = "IND_RECAR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Fecha Publicación", Width = 200,  DataPropertyName = "FEC_PUBLICA_DOF"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Recargo Diario", Width = 200,  DataPropertyName = "IND_RECAR_DIARIO"  }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid.MultiSelect = false;
            oGrid.ReadOnly = true;
            oGrid.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid);

            await Task.Delay(0);
        }
        #endregion

        #region "Grid Financiamiento Federal"
        private async Task GridFinanciamientoFederal()
        {

            oGrid2.Dock = DockStyle.Fill;
            oGrid2.BackgroundColor = Color.White;
            oGrid2.BorderStyle = BorderStyle.None;

            oGrid2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Año", Width = 80,  DataPropertyName = "ANIO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Mes", Width = 80,  DataPropertyName = "MES" },
                new DataGridViewTextBoxColumn() { HeaderText = "Financiamiento", Width = 200,  DataPropertyName = "IND_FINANC"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Recargos", Width = 150,  DataPropertyName = "IND_RECAR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Publicado", Width = 200,  DataPropertyName = "FEC_PUBLICA_DOF"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Financ. Min", Width = 200,  DataPropertyName = "IND_FED_MINIMO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Recarg. Min", Width = 200,  DataPropertyName = "IND_RECAR_MINIMO"  }
            });
            oGrid2.AutoGenerateColumns = false;
            oGrid2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid2.MultiSelect = false;
            oGrid2.ReadOnly = true;
            oGrid2.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid2);

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

            await detCreaTab("0","0","0", valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlFinanciamientoRecargos oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlFinanciamientoRecargos oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlFinanciamientoRecargos oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string ANIO, string MES, string TIPO, string valida)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{ANIO}{MES}{TIPO}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{ANIO}{MES}{TIPO}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{ANIO}{MES}{TIPO}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"Nuevo";
            else
            tabPage.Text = $"               {ANIO}-{MES}";
            ctrlFinanciamientoRecargos oCtrl = new ctrlFinanciamientoRecargos(_Main, ANIO, MES, TIPO, valida);
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
            await conActualizaBDGridFinanciamientoEstatal();
            oGrid.DataSource = _ConFiltra;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Grid Financiamiento Estatal. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualiza2()
        {

            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            await conMenu();

            oGrid2.DataSource = null;
            await conActualizaBDGridFinanciamientoFederal();
            oGrid2.DataSource = _ConFiltra2;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Grid Financiamiento Federal. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task cargaComboTipoCredito()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "E",
                Text = "ESTATAL"
            };
            var item2 = new ComboBoxItem
            {
                Value = "F",
                Text = "FEDERAL"
            };
            
            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboTipoCredito.DataSource = comboList;
            comboTipoCredito.DisplayMember = "Text";
            comboTipoCredito.ValueMember = "Value";
            comboTipoCredito.SelectedIndex = 0;
        }

        private async void cbTipoCredito_SelectedValueChanged(object sender, EventArgs e)
        {
            
            string TIPO = comboTipoCredito.SelectedValue.ToString();

            if (TIPO == "E")
            {
                
                oGrid.Visible = true;
                oGrid2.Visible = false;
                await conActualiza();

            }
            else if(TIPO == "F")
            {
                
                oGrid.Visible = false;
                oGrid2.Visible = true;
                await conActualiza2();

            }


        }


        private async Task conActualizaBDGridFinanciamientoEstatal()
        {
                
            try
            {                

                    oReq.Query = @"SELECT 
	                                ANIO,
	                                MES,
	                                IND_FINANC,
	                                IND_RECAR,
	                                TO_CHAR(FEC_PUBLICA_DOF, 'DD/MM/YYYY') AS FEC_PUBLICA_DOF,
	                                IND_RECAR_DIARIO,
                                    (
                                        CASE WHEN length(ANIO) > 0 THEN CAST(ANIO AS VARCHAR(10)) ELSE '' END ||
					                    CASE WHEN length(MES) > 0 THEN CAST(MES AS VARCHAR(10)) ELSE '' END ||
					                    CASE WHEN length(IND_FINANC) > 0 THEN CAST(IND_FINANC AS VARCHAR(10)) ELSE '' END ||
					                    CASE WHEN length(IND_RECAR) > 0 THEN CAST(IND_RECAR AS VARCHAR(10)) ELSE '' END ||
                                        CASE WHEN FEC_PUBLICA_DOF IS NOT NULL THEN TO_CHAR(FEC_PUBLICA_DOF, 'DD/MM/YYYY') ELSE '' END ||
                                        CASE WHEN length(IND_RECAR_DIARIO) > 0 THEN CAST(IND_RECAR_DIARIO AS VARCHAR(10)) ELSE '' END
                                    ) AS ALLCOLUMNS
                                FROM INDICE_FINAN_EST ORDER BY ANIO, MES";
                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Con = oRes.Data.Tables[0].AListaDe<eFINANC_ESTATAL>();

                    _ConFiltra = _Con;                

            }
            catch (Exception ex)
            { }

        }

        private async Task conActualizaBDGridFinanciamientoFederal()
        {

            try
            {

                oReq.Query = @"SELECT  ANIO, 
		                                MES,
		                                IND_FINANC,
		                                IND_RECAR,
                                        TO_CHAR(FEC_PUBLICA_DOF, 'DD/MM/YYYY') AS FEC_PUBLICA_DOF,
		                                IND_FED_MINIMO,
		                                IND_RECAR_MINIMO,
                                        (
                                            CASE WHEN length(ANIO) > 0 THEN CAST(ANIO AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(MES) > 0 THEN CAST(MES AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(IND_FINANC) > 0 THEN CAST(IND_FINANC AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(IND_RECAR) > 0 THEN CAST(IND_RECAR AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN FEC_PUBLICA_DOF IS NOT NULL THEN TO_CHAR(FEC_PUBLICA_DOF, 'DD/MM/YYYY') ELSE '' END ||
                                            CASE WHEN length(IND_FED_MINIMO) > 0 THEN CAST(IND_FED_MINIMO AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(IND_RECAR_MINIMO) > 0 THEN CAST(IND_RECAR_MINIMO AS VARCHAR(10)) ELSE '' END
                                        ) AS ALLCOLUMNS
                                FROM INDICE_FINAN_FED
                                ORDER BY ANIO , MES ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con2 = oRes.Data.Tables[0].AListaDe<eFINANC_FEDERAL>();

                _ConFiltra2 = _Con2;

            }
            catch (Exception ex)
            { }

        }

        #endregion


        #region "Abrir tab Editar Registro"
        private async Task detEdita(string ANIO, string MES, string TIPO,string valida)
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            await detCreaTab(ANIO, MES, TIPO, valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion


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
        private async Task<bool> conValidaSiExisteRegistro(string ANIO, string MES, string TIPO)
        {
            var existe = false;

            try
            {

                if (TIPO == "E")
                {

                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT 
	                                ANIO,
	                                MES,
	                                IND_FINANC,
	                                IND_RECAR,
	                                FEC_PUBLICA_DOF,
	                                IND_RECAR_DIARIO
                                FROM INDICE_FINAN_EST
                                WHERE ANIO = " + ANIO + " AND MES = " + MES + " ";
                    var oRes = await WSServicio.Servicio(oReq);

                    if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                    {
                        if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                            _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eFINANC_ESTATAL>();
                        existe = true;
                    }

                }
                else if (TIPO == "F")
                {
                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT  ANIO, 
		                                    MES,
		                                    IND_FINANC,
		                                    IND_RECAR,
		                                    FEC_PUBLICA_DOF,
		                                    IND_FED_MINIMO,
		                                    IND_RECAR_MINIMO
                                    FROM INDICE_FINAN_FED
                                WHERE ANIO = " + ANIO + " AND MES = " + MES + " ";
                    var oRes = await WSServicio.Servicio(oReq);

                    if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                    {
                        if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                            _Reg4 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eFINANC_FEDERAL>();
                        existe = true;
                    }

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

            string TIPO = comboTipoCredito.SelectedValue.ToString();


            if (TIPO == "E")
            {
                _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con : _Con.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
                oGrid.DataSource = _ConFiltra;
            }
            else if (TIPO == "F")
            {
                _ConFiltra2 = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con2 : _Con2.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
                oGrid2.DataSource = _ConFiltra2;
            }



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
