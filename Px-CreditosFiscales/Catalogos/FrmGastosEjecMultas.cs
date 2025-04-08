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
    public partial class FrmGastosEjecMultas : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();
        DataGridView oGrid2 = new DataGridView();
        List<eCPTO> _Con = new List<eCPTO>();
        List<eMULTA> _Con2 = new List<eMULTA>();
        List<eMULTA> _ConFiltra = new List<eMULTA>();
        eMULTA _Reg3 = new eMULTA();

        #region Contructor
        public FrmGastosEjecMultas()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Gastos Ejec. y Multas";

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

            await cargaComboTipo();
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

            await GridCPTO();
            await GridMULTA();


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

            if (oGrid2.Rows.Count > 0)
            {

                string Inferior = oGrid2.SelectedRows[0].Cells[2].Value.ToString();
                string Superior = oGrid2.SelectedRows[0].Cells[3].Value.ToString();
                string Clave = oGrid2.SelectedRows[0].Cells[0].Value.ToString();
                string Tipo = oGrid2.SelectedRows[0].Cells[1].Value.ToString();
                var existe = await conValidaSiExisteRegistro(Inferior, Superior,Clave, Tipo);
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
                string TipoGarantia = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                await conActualiza2(TipoGarantia);
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

        private async Task cargaComboTipo()
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

            comboTipo.SelectedIndexChanged -= changeComboTipo;


            // Asignar la lista al ComboBox
            comboTipo.DataSource = comboList;
            comboTipo.DisplayMember = "Text";
            comboTipo.ValueMember = "Value";
            

        }

        #region Grid
        private async Task GridCPTO()
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
                new DataGridViewTextBoxColumn() { HeaderText = "Clave", Width = 80,  DataPropertyName = "CLAVE" },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo", Width = 80,  DataPropertyName = "TIPO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Descripción", Width = 500,  DataPropertyName = "DESCR"  }
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
        private async Task GridMULTA()
        {

            oGrid2.AutoGenerateColumns = false;
            oGrid2.Dock = DockStyle.Fill;
            oGrid2.BackgroundColor = Color.White;
            oGrid2.BorderStyle = BorderStyle.None;

            oGrid2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Clave", Width = 80,  DataPropertyName = "CLAVE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Tipo", Width = 80, DataPropertyName = "TIPO"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Límite Inferior", Width = 120,  DataPropertyName = "INFERIOR" },
                new DataGridViewTextBoxColumn() { HeaderText = "Límite Superior", Width = 120,  DataPropertyName = "SUPERIOR" },
                new DataGridViewTextBoxColumn() { HeaderText = "Valor", Width = 120,  DataPropertyName = "IMPORTE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "Equivalente", Width = 250,  DataPropertyName = "EQUIVALENTE"  }

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

            await detCreaTab("0","0","0","0", valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        private async Task detBorra()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGastosEjecMultas oCtrl in tabPage.Controls)
                await oCtrl.detBorra(tabPage);
        }
        private async Task detGuarda()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGastosEjecMultas oCtrl in tabPage.Controls)
            {
                await oCtrl.detGuarda(tabPage);
                
            }
        }
        private async Task detActualiza()
        {
            TabPage tabPage = uiTabControlExt1.SelectedTab;
            foreach (ctrlGastosEjecMultas oCtrl in tabPage.Controls)
                await oCtrl.detActualiza();
        }
        private async Task detCreaTab(string Inferior, string Superior, string Clave, string Tipo, string valida)
        {
            await Task.Delay(0);

            TabPage tabPage = new TabPage();

            foreach (TabPage xtabPage in uiTabControlExt1.TabPages)
            {
                if (xtabPage.Name == $"{Inferior}{Superior}{Clave}{Tipo}")
                {
                    await _Main.Status("ya esta abierto", (int)MensajeTipo.Warning);
                    return;
                }

            }

            tabPage.Location = new System.Drawing.Point(4, 104);
            tabPage.Name = $"{Inferior}{Superior}{Clave}{Tipo}";
            tabPage.Size = new System.Drawing.Size(565, 313);
            tabPage.TabIndex = uiTabControlExt1.TabCount + 1;
            tabPage.Tag = $"{Inferior}{Superior}{Clave}{Tipo}";
            tabPage.UseVisualStyleBackColor = true;
            if (valida =="Nuevo")
            tabPage.Text = $"Nuevo";
            else
            tabPage.Text = "               " + Inferior + "-"+ Superior + "-"+ Clave + "-" + Tipo;

            ctrlGastosEjecMultas oCtrl = new ctrlGastosEjecMultas(_Main, Inferior, Superior, Clave, Tipo, valida);
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
            await conActualizaDBGridCPTO();
            oGrid.DataSource = _Con;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Gastos Ejec. y Multas. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDBGridCPTO()
        {

            string TIPO = comboTipo.SelectedValue.ToString();

            try
            {
                oReq.Query = @"SELECT * FROM CPTO WHERE TIPO = '"+TIPO+"' ORDER BY CLAVE";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con = oRes.Data.Tables[0].AListaDe<eCPTO>();

            }
            catch (Exception ex)
            { }

        }

        #endregion

        #region "Consulta2"
        private async Task conActualiza2(string CLAVE)
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            oGrid2.DataSource = null;
            await conActualizaDBGridMULTA(CLAVE);
            oGrid2.DataSource = _Con2;

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Gastos Ejec. y Multas: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        private async Task conActualizaDBGridMULTA(string CLAVE)
        {

            string TIPO = comboTipo.SelectedValue.ToString();


            try
            {
                oReq.Query = @"SELECT 
                                INFERIOR, 
                                SUPERIOR, 
                                CLAVE, 
                                TIPO, 
                                IMPORTE,
                                CASE 
                                    WHEN EQUIVALENTE = 'P' THEN 'PESOS'
                                    WHEN EQUIVALENTE = 'C' THEN 'POR CIENTO'
                                    WHEN EQUIVALENTE = 'S' THEN 'SALARIO MINIMO'
                                    ELSE ''
                                END AS EQUIVALENTE,
                                 (
                                    CASE WHEN length(INFERIOR) > 0 THEN CAST(INFERIOR AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(SUPERIOR) > 0 THEN CAST(SUPERIOR AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(CLAVE) > 0 THEN CAST(CLAVE AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(TIPO) > 0 THEN TIPO ELSE '' END || 
                                    CASE WHEN length(IMPORTE) > 0 THEN CAST(IMPORTE AS VARCHAR(10)) ELSE '' END ||
                                    CASE WHEN length(EQUIVALENTE) > 0 THEN EQUIVALENTE ELSE '' END
                                ) AS ALLCOLUMNS
                                FROM MULTA 
                                WHERE CLAVE = " + CLAVE + " AND TIPO = '" + TIPO + "' ";

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Con2 = oRes.Data.Tables[0].AListaDe<eMULTA>();

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

            string Inferior = oGrid2.CurrentRow.Cells[2].Value.ToString();
            string Superior = oGrid2.CurrentRow.Cells[3].Value.ToString();
            string Clave = oGrid2.CurrentRow.Cells[0].Value.ToString();
            string Tipo = oGrid2.CurrentRow.Cells[1].Value.ToString();
            await detCreaTab(Inferior, Superior, Clave, Tipo, valida);

            TimeSpan ts = DateTime.Now - startTime;
            Cursor = Cursors.Default;
        }
        #endregion

        private  void oGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            oGrid.DataBindingComplete -= oGrid_DataBindingComplete;

            var cellEventArgs = new DataGridViewCellEventArgs(0, 0);
            oGrid_CellClick(sender, cellEventArgs);
                 
            comboTipo.SelectedIndexChanged += changeComboTipo;
                
        }

        #region "Grid"
        private async void oGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }

            string CLAVE = oGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
             await conActualiza2(CLAVE);

        }
        #endregion


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string Inferior, string Superior, string Clave, string Tipo)
        {
            var existe = false;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT * FROM MULTA WHERE CLAVE = "+ Clave + " AND tipo = '"+Tipo+"' AND " +
                              " INFERIOR = " + Inferior + " AND SUPERIOR = " + Superior + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0){ 
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg3 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eMULTA>();
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
        public class ComboBoxItem
        {
            public string Text { get; set; } // Texto que se muestra en el ComboBox
            public object Value { get; set; } // Valor asociado al item

            public override string ToString()
            {
                return Text; // Esto es lo que se muestra en el ComboBox
            }
        }


        private async void changeComboTipo(object sender, EventArgs e)
        {
            await conActualiza();

            if (oGrid.Rows.Count > 0)
            {
                string CLAVE = oGrid.SelectedRows[0].Cells[0].Value.ToString();
                await conActualiza2(CLAVE);
            }

        }
    }
}
