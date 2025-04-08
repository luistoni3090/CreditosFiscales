/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmCuentaDepreciacion.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Forma catálogo de monedas

using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;

using Px_Utiles.Servicio;
using Px_Utiles.Utiles.DataTables;

using Px_Utiles.Models.Api;
using Px_Utiles.Models.Sistemas.Contabilidad;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;

using Px_Contabilidad.Utiles.Formas;
using Px_Contabilidad.Utiles.Generales;
using Px_Contabilidad.Catalogos.Controles;
using static Px_Contabilidad.Utiles.Emun.Enumerados;

namespace Px_Contabilidad.Catalogos
{
    public partial class FrmFirmas : FormaGen
    {
        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eFIRMA> _Con = new List<eFIRMA>();

        public FrmFirmas()
        {
            InitializeComponent();
            Inicio();
        }

        #region Eventos iniciales

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Catálogo Firmas";

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

            //await CombosInicia();
            await GridInicia();
        }

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

        #region Menu
        private async void uiTabControlExt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await conMenu();
        }
        private async void btnmnuNuevo_Click(object sender, EventArgs e)
        {
            await detNuevo();
        }

        private async void btnmnuEdita_Click(object sender, EventArgs e)
        {
            await detEdita();
        }
        private void btnmnuGuarda_Click(object sender, EventArgs e)
        {
        }
        private void btnmnuActualizaDet_Click(object sender, EventArgs e)
        {
        }
        private void btnmnuBorra_Click(object sender, EventArgs e)
        {
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
        #endregion



        #region "Consulta"
        private async Task conMenu()
        {
            bool bConsulta = uiTabControlExt1.SelectedIndex == 0 ? true : false;

            btnmnuNuevo.Visible = true;
            btnmnuEdita.Visible = bConsulta;
            btnmnuActualizaDet.Visible = !bConsulta;
            btnmnuGuarda.Visible = !bConsulta;
            btnmnuBorra.Visible = bConsulta;

            btnmnuActualiza.Visible = bConsulta;
            btnmnuFiltros.Visible = bConsulta;
            btnmnuConsulta.Visible = !bConsulta;

            btnmnuImprime.Visible = true;
            btnmnuAyuda.Visible = true;
        }
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

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de monedas. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDB()
        {
            try
            {
                oReq.Query = $@"
                                SELECT 
                                    EMPRESA,
                                    FIRMA,
                                    NOMBRE,
                                    PUESTO,
                                    CURP,
                                    RFC

                                FROM BTS.FIRMA
                                WHERE EMPRESA = {1}
                                ORDER BY NOMBRE
                               ";


                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eFIRMA>();
            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Detalle"
        private async Task detNuevo()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            await detCreaTab(0);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }

        private async Task detEdita()
        {
            DateTime startTime = DateTime.Now;
            Cursor = Cursors.AppStarting;

            var ID = oGrid.CurrentRow.Cells[0].Value.ToString();
            await detCreaTab(int.Parse(ID));

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }


        private async Task detCreaTab(int ID)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"tabDet{uiTabControlExt1.TabCount + 1}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Firma {ID.ToString("N0")}";
            tabPage.UseVisualStyleBackColor = true;

            ctrlFirmas oCtrl = new ctrlFirmas(_Main, ID);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            uiTabControlExt1.Controls.Add(tabPage);

            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;
        }
        #endregion

        #region "Grid"
        private async Task GridInicia()
        {

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Firma", Width = 150,  DataPropertyName = "FIRMA" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Nombre", Width = 250,  DataPropertyName = "NOMBRE" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Puesto", Width = 250,  DataPropertyName = "PUESTO" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "CURP", Width = 250,  DataPropertyName = "CURP" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "RFC", Width = 250,  DataPropertyName = "RFC" }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            oGrid.CellClick += oGrid_CellClick;

            panCatalogo.Controls.Add(oGrid);

            await Task.Delay(0);
        }

        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            await detEdita();
            ////int oVal = 0;
            //var oValX = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            ////var xReg = _Con.Where(x => x.CA_CUENTA == oValX.ToString());
            ////foreach (var x in xReg)
            ////    oVal = (int)x.CVE_CUENTA;

            //int iEmpresa = 1;
            ////int iEjercicio = int.Parse(cmbEjercicio.SelectedValue.ToString());

            //await detEdita(iEmpresa, int.Parse(oValX));

            ////switch (e.ColumnIndex)
            ////{
            ////    case 0:
            ////        break;
            ////}

        }

        #endregion



    }
}
