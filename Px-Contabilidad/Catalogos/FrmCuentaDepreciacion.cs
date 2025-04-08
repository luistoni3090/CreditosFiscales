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

namespace Px_Contabilidad.Catalogos.Controles
{
    public partial class FrmCuentaDepreciacion : FormaGen
    {
        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eCUENTA_DEPRECIACION> _Con = new List<eCUENTA_DEPRECIACION>();
        List<eEJERCICIO> _ConEjercicio = new List<eEJERCICIO>();
        List<eCUENTA> _ConCuentas = new List<eCUENTA>();

        public FrmCuentaDepreciacion()
        {
            InitializeComponent();
            Inicio();
        }

        #region Eventos iniciales

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Catálogo cuentas depreciación";

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            await InicioForma();
            await ActualizaDBCombos();
            await conActualiza();

            Cursor = Cursors.Default;
        }

        private async Task InicioForma()
        {

            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2);

            this.BackColor = Color.White;

            tabPage1.Text = "Consulta";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await CombosInicia();
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
                oReq.Query = $@"
                                SELECT 
                                    CA.NIVEL1 || '.' || CA.NIVEL2 || '.' || CA.NIVEL3 || '.' || CA.NIVEL4 || '.' || CA.NIVEL5 || '.' || CA.NIVEL6 AS CA_CUENTA,
                                    CA.CVE_CUENTA AS CA_CVE_CUENTA, CA.DESCR AS CA_DESCR, CA.NIVEL1 AS CA_NIVEL1, CA.NIVEL2 AS CA_NIVEL2, CA.NIVEL3 AS CA_NIVEL3, CA.NIVEL4 AS CA_NIVEL4, CA.NIVEL5 AS CA_NIVEL5, CA.NIVEL6 AS CA_NIVEL6,
                                    CC.NIVEL1 || '.' || CC.NIVEL2 || '.' || CC.NIVEL3 || '.' || CC.NIVEL4 || '.' || CC.NIVEL5 || '.' || CC.NIVEL6 AS CC_CUENTA,
                                    CC.CVE_CUENTA AS CC_CVE_CUENTA, CC.DESCR AS CC_DESCR, CC.NIVEL1 AS CC_NIVEL1, CC.NIVEL2 AS CC_NIVEL2, CC.NIVEL3 AS CC_NIVEL3, CC.NIVEL4 AS CC_NIVEL4, CC.NIVEL5 AS CC_NIVEL5, CC.NIVEL6 AS CC_NIVEL6,
                                    CD.NIVEL1 || '.' || CD.NIVEL2 || '.' || CD.NIVEL3 || '.' || CD.NIVEL4 || '.' || CD.NIVEL5 || '.' || CD.NIVEL6 AS CD_CUENTA,
                                    CD.CVE_CUENTA AS CD_CVE_CUENTA, CD.DESCR AS CD_DESCR, CD.NIVEL1 AS CD_NIVEL1, CD.NIVEL2 AS CD_NIVEL2, CD.NIVEL3 AS CD_NIVEL3, CD.NIVEL4 AS CD_NIVEL4, CD.NIVEL5 AS CD_NIVEL5, CD.NIVEL6 AS CD_NIVEL6,

                                CUENTA_DEPRECIACION.*
                                FROM BTS.CUENTA_DEPRECIACION
                                INNER JOIN CUENTA CA ON CA.CVE_CUENTA = CUENTA_DEPRECIACION.CVE_CUENTA AND CA.EMPRESA = {1} AND CA.EJERCICIO = {cmbEjercicio.SelectedValue}
                                INNER JOIN CUENTA CC ON CC.CVE_CUENTA = CUENTA_DEPRECIACION.CVE_CUENTA_CONTRA AND CC.EMPRESA = {1} AND CC.EJERCICIO = {cmbEjercicio.SelectedValue}
                                INNER JOIN CUENTA CD ON CD.CVE_CUENTA = CUENTA_DEPRECIACION.CVE_CUENTA_DEPRE AND CD.EMPRESA = {1} AND CD.EJERCICIO = {cmbEjercicio.SelectedValue}
                                WHERE CUENTA_DEPRECIACION.EMPRESA = {1}
                                AND CUENTA_DEPRECIACION.EJERCICIO = {cmbEjercicio.SelectedValue}                
                               ";


                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eCUENTA_DEPRECIACION>();
            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Detalle"
        private async Task detEdita(int Empresa, int Ejercicio, Int32 ID)
        {
            await Task.Delay(0);

            DateTime startTime = DateTime.Now;

            Cursor = Cursors.AppStarting;

            TabPage tabPage = new TabPage();

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"tabDet{uiTabControlExt1.TabCount + 1}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Text = $"Cuenta Depreciación {ID.ToString("N0")}";
            tabPage.UseVisualStyleBackColor = true;

            ctrlCuentaDepreciacion oCtrl = new ctrlCuentaDepreciacion(_Main, Empresa, Ejercicio, ID, _ConCuentas, _ConEjercicio);
            oCtrl.Dock = DockStyle.Fill;
            tabPage.Controls.Add(oCtrl);

            //oCtrl._Cuentas = _ConCuentas;

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
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Cuenta Activo", Width = 150,  DataPropertyName = "CA_CUENTA" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Cuenta Activo Nombre", Width = 250,  DataPropertyName = "CA_DESCR" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Contra Cuenta", Width = 150,  DataPropertyName = "CC_CUENTA" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Contra Cuenta Nombre", Width = 250,  DataPropertyName = "CC_DESCR" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Cuenta Depreciación", Width = 150,  DataPropertyName = "CD_CUENTA" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Cuenta Depreciación Nombre", Width = 250,  DataPropertyName = "CD_DESCR" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Años Vida", Width = 200,  DataPropertyName = "ANIOS_VIDA"  },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "% Depreciación", Width = 200,  DataPropertyName = "PORC_DEPRECIA"  }
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
            int oVal = 0;
            var oValX = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            var xReg = _Con.Where(x => x.CA_CUENTA == oValX.ToString());
            foreach (var x in xReg)
                oVal = (int)x.CVE_CUENTA;

            int iEmpresa = 1;
            int iEjercicio = int.Parse(cmbEjercicio.SelectedValue.ToString());

            await detEdita(iEmpresa, iEjercicio, oVal);

            //switch (e.ColumnIndex)
            //{
            //    case 0:
            //        break;
            //}

        }

        #endregion


        #region Combos

        private async Task ActualizaDBCombos()
        {
            try
            {
                oReq.Query = "SELECT * FROM EJERCICIO  ORDER BY EJERCICIO DESC";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.Int32, Nombre = "EMPRESA", Valor = 1}
                };
                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _ConEjercicio = oRes.Data.Tables[0].AListaDe<eEJERCICIO>();


                cmbEjercicio.DataSource = _ConEjercicio;


                await ActualizaCuentas();
            }
            catch (Exception ex)
            { }

        }
        private async Task ActualizaCuentas()
        {
            try
            {
                oReq.Query = $"SELECT * FROM BTS.CUENTA WHERE EMPRESA = {1} AND EJERCICIO = {cmbEjercicio.SelectedValue}";
                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _ConCuentas = oRes.Data.Tables[0].AListaDe<eCUENTA>();

            }
            catch (Exception ex)
            { }

        }
        private async Task CombosInicia()
        {

            cmbEjercicio.FlatStyle = FlatStyle.Flat;
            cmbEjercicio.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEjercicio.DisplayMember = "DESCR";
            cmbEjercicio.ValueMember = "EJERCICIO";
            cmbEjercicio.DataSource = _ConEjercicio;

            cmbEjercicio.SelectedIndexChanged += cmbEjercicios_SelectedIndexChanged;

        }

        private async void cmbEjercicios_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            await conActualiza();
        }
        #endregion


    }
}
