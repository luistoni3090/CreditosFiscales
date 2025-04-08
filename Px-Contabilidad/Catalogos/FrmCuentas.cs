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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Px_Contabilidad.Catalogos
{
    public partial class FrmCuentas : FormaGen
    {

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        List<eCUENTA> _Con = new List<eCUENTA>();
        List<eCUENTA> _ConFiltra = new List<eCUENTA>();
        List<eCLASE> _ConClase = new List<eCLASE>();
        List<eTIPO_CUENTA> _ConTipo = new List<eTIPO_CUENTA>();
        List<eGRUPO> _ConGrupo = new List<eGRUPO>();
        List<eEJERCICIO> _ConEjercicio = new List<eEJERCICIO>();

        public FrmCuentas()
        {
            InitializeComponent();
            Inicio();
        }

        #region Eventos iniciales

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            lblTitulo.Text = "Cuentas contables";

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            txtBusca.TextChanged += txtBusca_TextChanged;

            await InicioForma();
            await conActualizaCatalogos();
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

            tabPage1.Left = 100;
            tabPage1.Text = "Estructura";
            tabPage2.Text = "Listado";
            tabPage3.Text = "Cuentas";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            //panBusca.Height = 0;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            await CombosInicia();
            await ArbolInicia();
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
        private async Task conActualiza(bool bAct = false)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid.DataSource = null;

            if (Generales._AppState._Cuentas != null && !bAct)
            {
                _Con = Generales._AppState._Cuentas;
            }
            else
            {
                await conActualizaDB();
            }

            _ConFiltra = _Con;
            oGrid.DataSource = _ConFiltra;




            // Armo el arbolito
            await ArbolTLVCrea();
            await ArbolCrea();

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
                                    CVE_CUENTA,
                                    DESCR,
                                    GRUPO,
                                    NIVEL,
                                    MONEDA,
                                    MOVTO_MANUAL,
                                    CUENTA_PADRE,
                                    ACTIVA,
                                    TRANSITORIA,
                                    AFECT_PAT,
                                    TERMINAL,
                                    NATURALEZA,
                                    CENTRO_COSTOS,
                                    FECHA,
                                    NIVEL1,
                                    NIVEL2,
                                    NIVEL3,
                                    NIVEL4,
                                    NIVEL5,
                                    NIVEL6,
                                    EJERCICIO,

                                    CASE WHEN length(NIVEL1) > 0 THEN NIVEL1 END ||
                                    CASE WHEN length(NIVEL2) > 0 THEN '.' || NIVEL2 END ||
                                    CASE WHEN length(NIVEL3) > 0 THEN '.' || NIVEL3 END ||
                                    CASE WHEN length(NIVEL4) > 0 THEN '.' || NIVEL4 END ||
                                    CASE WHEN length(NIVEL5) > 0 THEN '.' || NIVEL5 END ||
                                    CASE WHEN length(NIVEL6) > 0 THEN '.' || NIVEL6 END AS CUENTACONTABLE,

                                    CASE WHEN length(NIVEL1) > 0 THEN NIVEL1 END || 
                                    CASE WHEN length(NIVEL2) > 0 THEN '.' || NIVEL2 END || 
                                    CASE WHEN length(NIVEL3) > 0 THEN '.' || NIVEL3 END || 
                                    CASE WHEN length(NIVEL4) > 0 THEN '.' || NIVEL4 END || 
                                    CASE WHEN length(NIVEL5) > 0 THEN '.' || NIVEL5 END || 
                                    CASE WHEN length(NIVEL6) > 0 THEN '.' || NIVEL6 END 
                                    || ' ' ||
                                    DESCR AS ALLCOLUMNS

                                    --NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6 AS CUENTACONTABLE

                                FROM CUENTA
                                WHERE EMPRESA = {1} 
                                AND EJERCICIO = {cmbEjercicio.SelectedValue}
                                ORDER BY CUENTACONTABLE
                            ";


                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eCUENTA>();
            }
            catch (Exception ex)
            { }

        }

        private async Task conActualizaCatalogos()
        {
            try
            {
                oReq.Query = $@"SELECT * FROM CLASE ORDER BY DESCR ";
                var oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    if (oRes.Data.Tables[0] != null)
                        _ConClase = oRes.Data.Tables[0].AListaDe<eCLASE>();

                oReq.Query = $@"
                                SELECT
                                    TIPO_CUENTA,
                                    DESCR,
                                    CLASE,
                                    ORDEN_REP,
                                    TIPO_CTA_ORFIS
                                 FROM TIPO_CUENTA ORDER BY DESCR ";
                oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    if (oRes.Data.Tables[0] != null)
                        _ConTipo = oRes.Data.Tables[0].AListaDe<eTIPO_CUENTA>();

                oReq.Query = $@"SELECT * FROM GRUPO ORDER BY DESCR";
                oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    if (oRes.Data.Tables[0] != null)
                        _ConGrupo = oRes.Data.Tables[0].AListaDe<eGRUPO>();

                await ActualizaDBCombos();

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
            tabPage.Text = $"Reporte {ID.ToString("N0")}";
            tabPage.UseVisualStyleBackColor = true;

            ctrlReportes oCtrl = new ctrlReportes(_Main, ID);
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
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "ID", Width = 60,  DataPropertyName = "CVE_CUENTA", Visible=false },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N1", Width = 30,  DataPropertyName = "NIVEL1" },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N2", Width = 30,  DataPropertyName = "NIVEL2" },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N3", Width = 30,  DataPropertyName = "NIVEL3" },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N4", Width = 30,  DataPropertyName = "NIVEL4" },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N5", Width = 30,  DataPropertyName = "NIVEL5" },
                //new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "N6", Width = 30,  DataPropertyName = "NIVEL6" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Cuenta", Width = 120,  DataPropertyName = "CUENTACONTABLE" },
                new DataGridViewTextBoxColumn() { ReadOnly = true, HeaderText = "Nombre", Width = 650,  DataPropertyName = "DESCR" },
            });
            oGrid.AutoGenerateColumns = false;
            oGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            oGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            oGrid.CellClick += oGrid_CellClick;

            panCuentasLista.Controls.Add(oGrid);


            await Task.Delay(0);
        }

        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int oVal = 0;
            var oValX = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            //var xReg = _Con.Where(x => x.CA_CUENTA == oValX.ToString());
            //foreach (var x in xReg)
            //    oVal = (int)x.CVE_CUENTA;

            //int iEmpresa = 1;
            //int iEjercicio = int.Parse(cmbEjercicio.SelectedValue.ToString());

            await detEdita(int.Parse(oValX));

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



                //List<KeyValuePair<string, string>> lstCom = new List<KeyValuePair<string, string>>();
                //foreach (var xItem in _ConEjercicio)
                //{
                //    lstCom.Add(new KeyValuePair<string, string>(xItem.EJERCICIO.ToString(), xItem.DESCR));
                //}

                //uiCombox1.Source = lstCom;

            }
            catch (Exception ex)
            { }

        }
        private async Task CombosInicia()
        {
            await Task.Delay(0);

            cmbEjercicio.FlatStyle = FlatStyle.Flat;
            cmbEjercicio.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEjercicio.DisplayMember = "DESCR";
            cmbEjercicio.ValueMember = "EJERCICIO";
            cmbEjercicio.DataSource = _ConEjercicio;

            cmbEjercicio.SelectedIndexChanged += cmbEjercicios_SelectedIndexChanged;


            //uiCombox1.Source = _ConEjercicio;


        }

        private async void cmbEjercicios_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            await conActualiza(true);
        }
        #endregion


        #region Arbolito
        private async Task ArbolInicia()
        {
            ColumnHeader oCH = new ColumnHeader();
            oCH.Width = 400;
            oCH.Text = "Cuenta";
            treeListCuentas.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 500;
            oCH.Text = "Nombre";
            treeListCuentas.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 60;
            oCH.Text = "Nivel";
            oCH.TextAlign = HorizontalAlignment.Right;
            treeListCuentas.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 60;
            oCH.Text = "Naturaleza";
            treeListCuentas.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 60;
            oCH.Text = "Activa";
            treeListCuentas.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 60;
            oCH.Text = "Transitoria";
            treeListCuentas.Columns.Add(oCH);

            treeListCuentas.MultiSelect = false;
            treeListCuentas.BorderStyle = BorderStyle.None;
            treeListCuentas.PlusMinusLineColor = Color.FromArgb(224, 224, 224);
            treeListCuentas.ForeColor = Color.FromArgb(61, 61, 61);
            //treeListView1.ShowPlusMinus = false;
            treeListCuentas.CollapseAll();

        }
        private async Task ArbolTLVCrea()
        {
            await Task.Delay(0);

            treeListCuentas.Items.Clear();
            List<TreeListViewItem> oHijos = new List<TreeListViewItem>();
            TreeListViewItem oNodo = new TreeListViewItem();

            foreach (var oClase in _ConClase)
            {
                oHijos = await ArbolTLVCreaTipo(oClase.CLASE.Trim());

                oNodo = new TreeListViewItem();
                oNodo.Tag = oClase.CLASE.Trim();
                oNodo.Text = oClase.DESCR.Trim();
                oNodo.ImageIndex = 1;
                oNodo.SubItems.Add("CLASE DE CUENTA");

                if (oHijos.Count > 0)
                    foreach (var oHijo in oHijos)
                        oNodo.Items.Add(oHijo);

                treeListCuentas.Items.Add(oNodo);

            }
        }
        private async Task<List<TreeListViewItem>> ArbolTLVCreaTipo(string sClase)
        {
            await Task.Delay(0);

            List<TreeListViewItem> oNodos = new List<TreeListViewItem>();
            List<TreeListViewItem> oHijos = new List<TreeListViewItem>();
            TreeListViewItem oNodo = new TreeListViewItem();

            var xLista = _ConTipo.Where(x => x.CLASE.Trim() == sClase.Trim()).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oTipo in xLista)
                {
                    oHijos = await ArbolTLVCreaGrupo(oTipo.TIPO_CUENTA.Trim());

                    oNodo = new TreeListViewItem();
                    oNodo.Tag = oTipo.TIPO_CUENTA.Trim();
                    oNodo.Text = oTipo.DESCR.Trim();
                    oNodo.ImageIndex = 2;
                    oNodo.SubItems.Add("TIPO DE CUENTA");

                    if (oHijos.Count > 0)
                        foreach (var oHijo in oHijos)
                            oNodo.Items.Add(oHijo);

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }

        private async Task<List<TreeListViewItem>> ArbolTLVCreaGrupo(string sTipo)
        {
            await Task.Delay(0);

            List<TreeListViewItem> oNodos = new List<TreeListViewItem>();
            List<TreeListViewItem> oHijos = new List<TreeListViewItem>();
            TreeListViewItem oNodo = new TreeListViewItem();

            var xLista = _ConGrupo.Where(x => x.TIPO_CUENTA.Trim() == sTipo.Trim()).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oGrupo in xLista)
                {
                    oHijos = await ArbolTLVCreaCuentas(1, oGrupo.GRUPO, 0);

                    oNodo = new TreeListViewItem();
                    oNodo.Tag = oGrupo.GRUPO.ToString();
                    oNodo.Text = oGrupo.DESCR.Trim();
                    oNodo.ImageIndex = 3;
                    oNodo.SubItems.Add("GRUPO DE CUENTA");

                    if (oHijos.Count > 0)
                        foreach (var oHijo in oHijos)
                            oNodo.Items.Add(oHijo);

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }

        private async Task<List<TreeListViewItem>> ArbolTLVCreaCuentas(decimal Nivel, decimal sGrupo, decimal dPadre)
        {
            await Task.Delay(0);

            List<TreeListViewItem> oNodos = new List<TreeListViewItem>();
            List<TreeListViewItem> oHijos = new List<TreeListViewItem>();
            TreeListViewItem oNodo = new TreeListViewItem();

            var xLista = _ConFiltra.Where(x => x.GRUPO == sGrupo && x.NIVEL == Nivel && x.CUENTA_PADRE == dPadre).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oCuenta in xLista)
                {
                    if (Nivel <= 5)
                        oHijos = await ArbolTLVCreaCuentas(Nivel + 1, sGrupo, oCuenta.CVE_CUENTA);

                    oNodo = new TreeListViewItem();
                    oNodo.Tag = oCuenta.CVE_CUENTA.ToString();
                    oNodo.Text = $"{oCuenta.CUENTACONTABLE}";
                    oNodo.ImageIndex = (int)Nivel + 4;
                    oNodo.SubItems.Add($"{oCuenta.DESCR.Trim()}");
                    oNodo.SubItems.Add(oCuenta.NIVEL.ToString());
                    oNodo.SubItems.Add($"{oCuenta.NATURALEZA}");
                    oNodo.SubItems.Add($"{oCuenta.ACTIVA}");
                    oNodo.SubItems.Add(oCuenta.TRANSITORIA);

                    if (oHijos.Count > 0)
                        foreach (var oHijo in oHijos)
                            oNodo.Items.Add(oHijo);

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }


        // Árbol tradicional
        private async Task ArbolCrea()
        {

            await Task.Delay(0);

            treeCuentas.Nodes.Clear();
            List<TreeNode> oHijos = new List<TreeNode>();
            TreeNode oNodo = new TreeNode();

            foreach (var oClase in _ConClase)
            {
                oHijos = await ArbolCreaTipo(oClase.CLASE.Trim());

                oNodo = new TreeNode();
                oNodo.Tag = oClase.CLASE.Trim();
                oNodo.Text = oClase.DESCR.Trim();
                oNodo.SelectedImageIndex = 0;
                oNodo.ImageIndex = 1;

                if (oHijos.Count > 0)
                {
                    foreach (var oHijo in oHijos)
                    {
                        oNodo.Nodes.Add(oHijo);
                    }
                }

                treeCuentas.Nodes.Add(oNodo);
            }
        }

        private async Task<List<TreeNode>> ArbolCreaTipo(string sClase)
        {
            await Task.Delay(0);

            List<TreeNode> oNodos = new List<TreeNode>();
            List<TreeNode> oHijos = new List<TreeNode>();
            TreeNode oNodo = new TreeNode();

            var xLista = _ConTipo.Where(x => x.CLASE.Trim() == sClase.Trim()).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oTipo in xLista)
                {
                    oHijos = await ArbolCreaGrupo(oTipo.TIPO_CUENTA.Trim());

                    oNodo = new TreeNode();
                    oNodo.Tag = oTipo.TIPO_CUENTA.Trim();
                    oNodo.Text = oTipo.DESCR.Trim();
                    oNodo.SelectedImageIndex = 0;
                    oNodo.ImageIndex = 2;

                    if (oHijos.Count > 0)
                    {
                        foreach (var oHijo in oHijos)
                        {
                            oNodo.Nodes.Add(oHijo);
                        }
                    }

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }

        private async Task<List<TreeNode>> ArbolCreaGrupo(string sTipo)
        {
            await Task.Delay(0);

            List<TreeNode> oNodos = new List<TreeNode>();
            List<TreeNode> oHijos = new List<TreeNode>();
            TreeNode oNodo = new TreeNode();

            var xLista = _ConGrupo.Where(x => x.TIPO_CUENTA.Trim() == sTipo.Trim()).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oTipo in xLista)
                {
                    oHijos = await ArbolCreaCuentas(1, oTipo.GRUPO, 0);

                    oNodo = new TreeNode();
                    oNodo.Tag = oTipo.TIPO_CUENTA.Trim();
                    oNodo.Text = oTipo.DESCR.Trim();
                    oNodo.SelectedImageIndex = 0;
                    oNodo.ImageIndex = 3;

                    if (oHijos.Count > 0)
                    {
                        foreach (var oHijo in oHijos)
                        {
                            oNodo.Nodes.Add(oHijo);
                        }
                    }

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }

        private async Task<List<TreeNode>> ArbolCreaCuentas(decimal Nivel, decimal sGrupo, decimal dPadre)
        {
            await Task.Delay(0);

            List<TreeNode> oNodos = new List<TreeNode>();
            List<TreeNode> oHijos = new List<TreeNode>();
            TreeNode oNodo = new TreeNode();

            var xLista = _ConFiltra.Where(x => x.GRUPO == sGrupo && x.NIVEL == Nivel && x.CUENTA_PADRE == dPadre).ToList();
            if (xLista.Count != 0)
            {
                foreach (var oCuenta in xLista)
                {
                    if (Nivel <= 5)
                        oHijos = await ArbolCreaCuentas(Nivel + 1, sGrupo, oCuenta.CVE_CUENTA);

                    oNodo = new TreeNode();
                    oNodo.Tag = oCuenta.CVE_CUENTA.ToString();
                    oNodo.Text = $"{oCuenta.CUENTACONTABLE} \t {oCuenta.DESCR.Trim()}";
                    oNodo.SelectedImageIndex = 0;
                    oNodo.ImageIndex = (int)Nivel + 4;

                    if (oHijos.Count > 0)
                    {
                        foreach (var oHijo in oHijos)
                        {
                            oNodo.Nodes.Add(oHijo);
                        }
                    }

                    oNodos.Add(oNodo);
                }
            }

            return oNodos;
        }



        #endregion


        #region Buscar
        private async void txtBusca_TextChanged(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            _ConFiltra = string.IsNullOrWhiteSpace(txtBusca.Text) ? _Con : _Con.Where(x => x.ALLCOLUMNS.ToLower().Contains(txtBusca.Text.ToLower())).ToList();

            oGrid.DataSource = _ConFiltra;

            await ArbolTLVCrea();
            await ArbolCrea();

        }


        #endregion

        bool collapse = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (collapse) treeListCuentas.CollapseAll();
            else treeListCuentas.ExpandAll();
            collapse = !collapse;
        }
    }
}
