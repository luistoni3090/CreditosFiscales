/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmOrigenPolizas.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Forma de mantenimiento de origen de pólizas

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
    public partial class FrmOrigenPolizas : FormaGen
    {

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eORIGEN> _Con = new List<eORIGEN>();

        public FrmOrigenPolizas()
        {
            InitializeComponent();
            Inicio();
        }

        #region Eventos iniciales

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Mantenimiento de origen de pólizas";

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            await InicioForma();
            //await ActualizaDBCombos();
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

        #endregion



        #region "Consulta"
        private async Task conActualiza()
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

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
                oReq.Query = $"SELECT * FROM bts.ORIGEN  ";
                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eORIGEN>();
            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Detalle"
        private async Task detEdita(Int32 ID)
        {
            await Task.Delay(0);

            DateTime startTime = DateTime.Now;

            Cursor = Cursors.AppStarting;

            TabPage tabPage = new TabPage();

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"tabDet{uiTabControlExt1.TabCount + 1}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Nivel {ID.ToString("N0")}";
            tabPage.UseVisualStyleBackColor = true;

            ctrlOrigenPolizas oCtrl = new ctrlOrigenPolizas(_Main, ID);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);
            uiTabControlExt1.Controls.Add(tabPage);
            uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;


            TimeSpan ts = DateTime.Now - startTime;
            //await JS.Notify("Correcto", $"Usuario {_Reg.usu_Nombre} cargado correctamente. Tiempo: {ts}", Gen.MensajeToastTipo.info, 1);

            Cursor = Cursors.Default;
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
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Clave", Width = 100,  DataPropertyName = "ORIGEN" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Descripción", Width = 500,  DataPropertyName = "DESCR"  }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            oGrid.CellClick += oGrid_CellClick;

            tabPage1.Controls.Add(oGrid);


            await Task.Delay(0);
        }

        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var oVal = int.Parse(oGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            //int iEmpresa = 1;
            //int iEjercicio = int.Parse(oGrid.Rows[e.RowIndex].Cells[3].Value.ToString());

            await detEdita(oVal);

            //switch (e.ColumnIndex)
            //{
            //    case 0:
            //        break;
            //}

        }

        #endregion


    }
}
