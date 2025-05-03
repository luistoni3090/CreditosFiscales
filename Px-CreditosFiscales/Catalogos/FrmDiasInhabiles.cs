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

using Px_CreditosFiscales.Utiles.Formas;
using Px_CreditosFiscales.Utiles.Generales;
using Px_CreditosFiscales.Catalogos.Controles;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Px_CreditosFiscales.Catalogos
{
    public partial class FrmDiasInhabiles : FormaGen
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eDIASINHABILES> _Con = new List<eDIASINHABILES>();
        List<eDIASINHABILES> _ConFiltra = new List<eDIASINHABILES>();
        eDIASINHABILES _Reg3 = new eDIASINHABILES();

        #region Contructor
        public FrmDiasInhabiles()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Catálogo de Días Inhábiles";

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
            await conActualiza("TODOS");

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

            cargaComboAnio();
            comboAnio.Enabled = false;
            rbMostrarTodos.Checked = true;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await Grid1();


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
            return;
            panFiltros.Visible = false;
            await detNuevo("Nuevo");
        }

        private async void btnmnuEdita_Click(object sender, EventArgs e)
        {
            return;
            if (oGrid.Rows.Count > 0)
            {

                string LEY = oGrid.SelectedRows[0].Cells[4].Value.ToString();
                string TIPO = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                var existe = await conValidaSiExisteRegistro(LEY, TIPO);
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
            if (oGrid.Rows.Count > 0)
            {
                if (rbFiltroPorAnio.Checked)
                {
                    conActualiza("E");


                }
                else {
                    await conActualiza("F");
                }
                
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
        private async Task Grid1()
        {

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Año ", Width = 100,  DataPropertyName = "ANIO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Mes", Width = 100,  DataPropertyName = "MES"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Día", Width = 100,  DataPropertyName = "DIA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 500,  DataPropertyName = "DESCR"  }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid.MultiSelect = false;
            oGrid.ReadOnly = true;
            oGrid.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid);

            oGrid.DataBindingComplete += oGrid_DataBindingComplete;


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

            await detCreaTab("0","0", valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlResponsableSolidario oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlResponsableSolidario oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlResponsableSolidario oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string TIPO, string LEY, string valida)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{TIPO}{LEY}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{TIPO}{LEY}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{TIPO}{LEY}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"Nuevo";
            else
            tabPage.Text = "               " + TIPO + "-"+ LEY;

            if (rbFiltroPorAnio.Checked)
                LEY = "E";
            else
                LEY = "F";

            ctrlResponsableSolidario oCtrl = new ctrlResponsableSolidario(_Main, TIPO, LEY, valida);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
        }
        #endregion

        #region "Consulta"
        private async Task conActualiza(string VALOR)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            await conMenu();

            oGrid.DataSource = null;
            await conActualizaDBGrid1(VALOR);
            oGrid.DataSource = _Con;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Días Inhábiles. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDBGrid1(string VALOR)
        {

            try
            {

                if (VALOR == "ANIO")
                {
                    var ANIO = comboAnio.SelectedValue;


                    oReq.Query = @"SELECT ANIO, MES, DIA, DESCR,
                             (
                                CASE WHEN length(ANIO) > 0 THEN CAST(ANIO AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(MES) > 0 THEN CAST(MES AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(DIA) > 0 THEN CAST(DIA AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(DESCR) > 0 THEN CAST(DESCR AS VARCHAR(10)) ELSE '' END 
                            ) AS ALLCOLUMNS
                            FROM DIA_INHABIL WHERE ANIO = " + ANIO + " ";
                    var oRes = await WSServicio.Servicio(oReq);
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Con = oRes.Data.Tables[0].AListaDe<eDIASINHABILES>();
                }
                else {

                    oReq.Query = @"SELECT ANIO, MES, DIA, DESCR,
                             (
                                CASE WHEN length(ANIO) > 0 THEN CAST(ANIO AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(MES) > 0 THEN CAST(MES AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(DIA) > 0 THEN CAST(DIA AS VARCHAR(10)) ELSE '' END ||
                                CASE WHEN length(DESCR) > 0 THEN CAST(DESCR AS VARCHAR(10)) ELSE '' END 
                            ) AS ALLCOLUMNS
                            FROM DIA_INHABIL";
                    var oRes = await WSServicio.Servicio(oReq);
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Con = oRes.Data.Tables[0].AListaDe<eDIASINHABILES>();
                }


            }
            catch (Exception ex)
            { }

        }
        #endregion


        #region "Abrir tab Editar Registro"
        private async Task detEdita(string valida)
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            string TIPO = oGrid.CurrentRow.Cells[0].Value.ToString();
            string LEY = oGrid.CurrentRow.Cells[4].Value.ToString();
            await detCreaTab(TIPO, LEY, valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion

        private  void oGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            oGrid.DataBindingComplete -= oGrid_DataBindingComplete;

            var cellEventArgs = new DataGridViewCellEventArgs(0, 0);
            oGrid_CellClick(sender, cellEventArgs);
                
        }

        #region "Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0) {
                return;
            }

            string ANIO = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

        }
        #endregion


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string LEY, string TIPO)
        {
            var existe = false;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT TIPO, DESCRIPCION, FRACCION, DOCTO FROM TIPO_RESPONSABLE 
                                WHERE LEY = '" + LEY + "' AND TIPO = + '" + TIPO + "' ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0){ 
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eDIASINHABILES>();
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

        private async Task cargaComboAnio()
        {

            //var comboList = new List<ComboBoxItem>();


            var items = new List<object>
            {
                new { Value = "2007", Text = "2007" },
                new { Value = "2008", Text = "2008" },
                new { Value = "2009", Text = "2009" },
                new { Value = "2010", Text = "2010" },
                new { Value = "2011", Text = "2011" },
                new { Value = "2012", Text = "2012" },
                new { Value = "2013", Text = "2013" },
                new { Value = "2014", Text = "2014" },
                new { Value = "2015", Text = "2015" },
                new { Value = "2016", Text = "2016" },
                new { Value = "2017", Text = "2017" },
                new { Value = "2018", Text = "2018" },
                new { Value = "2019", Text = "2019" },
                new { Value = "2020", Text = "2020" },
                new { Value = "2021", Text = "2021" },
                new { Value = "2022", Text = "2022" },
                new { Value = "2023", Text = "2023" },
                new { Value = "2024", Text = "2024" },
                new { Value = "2025", Text = "2025" },
            };


            // Asignar la lista al ComboBox
            comboAnio.DataSource = items;
            comboAnio.DisplayMember = "Text";
            comboAnio.ValueMember = "Value";

            comboAnio.SelectedIndex = -1;


        }


        private void rbFiltrarAnioChanged(object sender, EventArgs e)
        {
            //

            string valor = comboAnio.SelectedItem?.ToString();

            if(valor == null)
                comboAnio.Enabled = true;

            if (rbMostrarTodos.Checked)
            {
                conActualiza("TODOS");

            }

            
        }

        private void rbMostrarTodosChanged(object sender, EventArgs e)
        {
            comboAnio.Enabled = false;
            comboAnio.SelectedIndex = -1;
        }

        private void comboAnioChanged(object sender, EventArgs e)
        {
            if (rbFiltroPorAnio.Checked)
            {
                conActualiza("ANIO");

            }
        }
    }
}
