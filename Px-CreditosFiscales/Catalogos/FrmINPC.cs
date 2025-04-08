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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

namespace Px_CreditosFiscales.Catalogos
{
    public partial class FrmINPC : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eINPC> _Con = new List<eINPC>();
        List<eINPC> _ConFiltra = new List<eINPC>();
        eINPC _Reg3 = new eINPC();

        #region Contructor
        public FrmINPC()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Catálogo de INP";

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

            await GridINPC();


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

                string ANIO = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                string MES = oGrid.SelectedRows[0].Cells[1].Value.ToString();
                var existe = await conValidaSiExisteRegistro(ANIO, MES);
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
                await conActualiza();
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
        private async Task GridINPC()
        {

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Año ", Width = 100,  DataPropertyName = "ANIO" },
                new DataGridViewTextBoxColumn() { HeaderText = "Mes", Width = 100,  DataPropertyName = "MES"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Factor", Width = 100,  DataPropertyName = "FACTOR"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Fecha Publicaciión", Width = 100,  DataPropertyName = "FECHA_PUBLICACION"  }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oGrid.MultiSelect = false;
            oGrid.ReadOnly = true;
            oGrid.CellClick += oGrid_CellClick;

            //tableLayoutPanel.Controls.Add(oGrid, 0, 0);

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
            foreach (ctrlINPC oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlINPC oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlINPC oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string ANIO, string MES, string valida)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{ANIO}{MES}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{ANIO}{MES}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{ANIO}{MES}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"Nuevo";
            else
            tabPage.Text = "               " + ANIO + "-"+ MES;

            ctrlINPC oCtrl = new ctrlINPC(_Main, ANIO, MES, valida);
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
            await conActualizaDBGridINPC();
            oGrid.DataSource = _Con;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Gastos Ejec. y Multas. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDBGridINPC()
        {

            try
            {
                oReq.Query = @"SELECT ANIO,
                                        MES,
                                        FACTOR,
                                        TO_CHAR(FECHA_PUBLICACION, 'DD/MM/YYYY') AS FECHA_PUBLICACION,
                                        (
                                            CASE WHEN length(ANIO) > 0 THEN CAST(ANIO AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(MES) > 0 THEN CAST(MES AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN length(FACTOR) > 0 THEN CAST(FACTOR AS VARCHAR(10)) ELSE '' END ||
                                            CASE WHEN FECHA_PUBLICACION IS NOT NULL THEN TO_CHAR(FECHA_PUBLICACION, 'DD/MM/YYYY') ELSE '' END
                                        ) AS ALLCOLUMNS
                                        FROM INPC
                                        ORDER BY ANIO DESC, MES ASC ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eINPC>();

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

            string ANIO = oGrid.CurrentRow.Cells[0].Value.ToString();
            string MES = oGrid.CurrentRow.Cells[1].Value.ToString();
            await detCreaTab(ANIO, MES, valida);

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
            if (e.RowIndex < 0) {
                return;
            }

            string ANIO = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

        }
        #endregion


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string ANIO, string MES)
        {
            var existe = false;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT * FROM INPC WHERE ANIO = " + ANIO + " AND MES = "+ MES + "  ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0){ 
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eINPC>();
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
        

    }
}
