/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmOrganismoEjercicio.cs
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
using Px_Controles.Controls.Tab;
using Px_Utiles.Models.Sistemas.General;

namespace Px_Contabilidad.Catalogos
{
    public partial class FrmMomentosContables : FormaGen
    {
        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eMOMENTO_CONTABLE> _Con = new List<eMOMENTO_CONTABLE>();
        List<eTIPO_MOMENTO> _ConTipoMomento = new List<eTIPO_MOMENTO>();
        List<eLlaveValorString> _ComboSN = new List<eLlaveValorString>();


        #region Contructor
        public FrmMomentosContables()
        {
            InitializeComponent();
            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Momentos contables";

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
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await CombosInicia();
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
        #endregion

        #region Grid
        private async Task GridInicia()
        {

            oGrid.Dock = DockStyle.Fill;
            oGrid.BackgroundColor = Color.White;
            oGrid.BorderStyle = BorderStyle.None;

            oGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Momento", Width = 60,  DataPropertyName = "MOMENTO" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Descripción", Width = 200,  DataPropertyName = "DESCR"  },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Tipo póliza", Width = 100,  DataPropertyName = "TIPO_POLIZA" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Orden", Width = 60,  DataPropertyName = "ORDEN"  },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Genera Id tramite", Width = 100,  DataPropertyName = "GENERA_IDTRAMITE" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Naturaleza", Width = 100,  DataPropertyName = "NATURALEZA_MOMENTO"  },
                new DataGridViewComboBoxColumn() { ValueMember = "Llave", DisplayMember ="Valor", DataSource = _ComboSN, DataPropertyName = "OBLIGATORIO_DIARIO"  }
                //new DataGridViewTextBoxColumn() { HeaderText = "Obligatorio diario", Width = 100,  DataPropertyName = "OBLIGATORIO_DIARIO" }
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            oGrid.CellClick += oGrid_CellClick;

            panGen.Controls.Add(oGrid);
            //tabPage1.Controls.Add(oGrid);


            await Task.Delay(0);
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
                oReq.Query = $@"SELECT     
                                    TIPO_MOMENTO,
                                    MOMENTO,
                                    DESCR,
                                    TIPO_POLIZA,
                                    ORDEN,
                                    GENERA_IDTRAMITE,
                                    NATURALEZA_MOMENTO,
                                    OBLIGATORIO_DIARIO
                                FROM bts.MOMENTO_CONTABLE 
                                Where TIPO_MOMENTO = {cmbEjercicio.SelectedValue}";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eMOMENTO_CONTABLE>();
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

            //TabPage tabPage = new TabPage();

            //tabPage.Location = new System.Drawing.Point(4, 104);
            //tabPage.Name = $"tabDet{uiTabControlExt1.TabCount + 1}";
            //tabPage.Size = new System.Drawing.Size(565, 313);
            //tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            //tabPage.Text = $"Moneda {ID.ToString("N0")}";
            //tabPage.UseVisualStyleBackColor = true;

            //ctrlMoneda oCtrl = new ctrlMoneda(_Main, ID);
            //oCtrl.Dock = DockStyle.Fill;
            //tabPage.Controls.Add(oCtrl);

            //uiTabControlExt1.Controls.Add(tabPage);
            //uiTabControlExt1.SelectedIndex = uiTabControlExt1.TabCount - 1;


            TimeSpan ts = DateTime.Now - startTime;
            //await JS.Notify("Correcto", $"Usuario {_Reg.usu_Nombre} cargado correctamente. Tiempo: {ts}", Gen.MensajeToastTipo.info, 1);

            Cursor = Cursors.Default;
        }


        #endregion

        #region "Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var oVal = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            await detEdita(int.Parse(oVal));

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
                oReq.Query = "SELECT TIPO_MOMENTO, DESCR FROM TIPO_MOMENTO ORDER BY DESCR DESC";
                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _ConTipoMomento = oRes.Data.Tables[0].AListaDe<eTIPO_MOMENTO>();

                cmbEjercicio.DataSource = _ConTipoMomento;



                _ComboSN.Add(new eLlaveValorString { Llave = "N", Valor = "No" });
                _ComboSN.Add(new eLlaveValorString { Llave = "S", Valor = "Si" });


            }
            catch (Exception ex)
            { }

        }
        private async Task CombosInicia()
        {

            cmbEjercicio.FlatStyle = FlatStyle.Flat;
            cmbEjercicio.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEjercicio.DisplayMember = "DESCR";
            cmbEjercicio.ValueMember = "TIPO_MOMENTO";
            cmbEjercicio.DataSource = _ConTipoMomento;

            cmbEjercicio.SelectedIndexChanged += cmbEjercicios_SelectedIndexChanged;

        }

        private async void cmbEjercicios_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            await conActualiza();
        }
        #endregion

    }
}
