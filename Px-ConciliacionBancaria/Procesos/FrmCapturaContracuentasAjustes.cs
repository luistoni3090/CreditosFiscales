using Px_ConciliacionBancaria.Utiles.Formas;
using Px_Utiles.Models.Api;
using Px_Utiles.Servicio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;

using Px_ConciliacionBancaria.Utiles.Generales;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;



namespace PX_ConciliacionBancaria
{
    public partial class FrmCapturaContracuentasAjustes : FormaGenBar
    {

        eRequest oReq = new eRequest();

        // Variables globales
        private int Ii_Bco, Ii_Cta;
        private DateTime Idt_Fecha, idt_sysdate;
        private long Il_Conciliacion, il_lote;
        private string Is_Moneda, Is_TipoReg, Is_tipo_o, is_fecha;
        private bool Ib_Complete;
        private decimal Id_Abonos, Id_Cargos, Id_Tc;


        // Controles de la ventana
        private DataGridView dw_1;
        private Button cb_Autorizar;
        private Button cb_Salir;
        private DataGridView dw_Ajustes;
        private DataGridView dw_SinAjustes;
        private AxHost ole_Autoriza;


        public FrmCapturaContracuentasAjustes()
        {
            InitializeComponent();
            Start();
        }


        private async Task Start()
        {
            _Titulo = "Captura de Contracuentas por Ajustes";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();

        }

        private void comboBanco_SelectedValueChanged(object sender, EventArgs e)
        {
            string IdBanco = comboBanco.SelectedValue.ToString();

            var algo = "";
            cargaComboCuenta(IdBanco);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task StartForm()
        {

            eRequest oReq = new eRequest();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{_Titulo}", (int)MensajeTipo.Info);

            await cargaComboBanco();
            ConfigurarDataGrid();
            Controls.Add(dw_1);

        }

        #region "Carga Combo Banco"
        private async Task cargaComboBanco()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT BANCO IDBanco, LPAD(BANCO, 2, '0') BANCO, DESCR FROM BANCO ORDER BY BANCO";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDBanco"].ToString(),
                            Text = "(" + row["BANCO"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboBanco.DataSource = comboList;
                    comboBanco.DisplayMember = "Text";
                    comboBanco.ValueMember = "Value";

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion


        #region "Carga Combo Cuenta"
        private async Task cargaComboCuenta(string IdBanco)
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT CUENTA IDCUENTA, LPAD(CUENTA, 3, '0') CUENTA, DESCR FROM CUENTA WHERE BANCO = " + IdBanco + " ORDER BY CUENTA asc";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDCUENTA"].ToString(),
                            Text = "(" + row["CUENTA"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCuenta.DataSource = comboList;
                    comboCuenta.DisplayMember = "Text";
                    comboCuenta.ValueMember = "Value";
                    comboCuenta.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion


        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;


            if (string.IsNullOrWhiteSpace(comboCuenta.Text))
                sErr += "Seleccione la cuenta. ";


            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                string IdBanco = comboBanco.SelectedValue.ToString();
                string IdCuenta = comboCuenta.SelectedValue.ToString();

            }

            return bExito;
        }


        private void ConfigurarDataGrid()
        {
            // Crear un DataTable y definir sus columnas
            DataTable table = new DataTable();
            table.Columns.Add("Banco", typeof(string));
            table.Columns.Add("Cuenta", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Ref", typeof(string));
            table.Columns.Add("Fecha Reg", typeof(DateTime));
            table.Columns.Add("Importe", typeof(decimal));
            table.Columns.Add("Complemento", typeof(string));
            table.Columns.Add("Contracuenta", typeof(string));


            // Asignar el DataTable como fuente de datos al DataGridView existente
            dataGridView2.DataSource = table;

        }


        private async void detcbDesconciliar(object sender, EventArgs e)
        {
            bool bExito = await detValida();

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas Continuar?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas Continuar?", "Aviso", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            CbActualizar_Click();



        }


        private void comboCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Ii_Bco = comboBanco.SelectedValue?.ToString();
            string Ii_Cta = comboCuenta.SelectedValue?.ToString();

            int selectedIndex = comboCuenta.SelectedIndex;

            // Verificar si hay un elemento seleccionado
            if (selectedIndex > 0)
            {
                filtrarInformacionAsync(Ii_Bco, Ii_Cta);
            }
          

            
        }


        public async Task filtrarInformacionAsync(string iiBanco, string iiCuenta)
        {
            int liCant = 0;

            try
            {

                // Inicializar el ComboBox de contracuentas
                oReq.Tipo = 0; // Tipo 1 para ejecución de consulta
                oReq.Query = $@"
                    SELECT DISTINCT CTACONTA 
                FROM DGI.CONTRACUENTAS 
                WHERE BANCO = '{iiBanco}' 
                  AND CUENTA = '{iiCuenta}'";


                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Err != 0 || oRes.Data.Tables.Count < 1 || oRes.Data.Tables[0].Rows.Count == 0)
                {

                    MessageBoxMX.ShowDialog(null, $"No existen contracuentas para el banco y cuenta seleccionados.", "Aviso", (int)StatusColorsTypes.Warning);

                    cbActualizar.Enabled = false;

                    return;
                }

                // Poblar ComboBox con los datos obtenidos
                var comboData = oRes.Data.Tables[0];
                comboCuenta.Items.Clear();
                foreach (DataRow row in comboData.Rows)
                {
                    comboCuenta.Items.Add(row["CTACONTA"].ToString());
                }

                cbActualizar.Enabled = true;

                // Filtrar DataGridView por el banco y la cuenta seleccionada
                oReq.Query = $@"
                    SELECT * 
                    FROM DGI.AJUSTES_CONTRACUENTA 
                    WHERE BANCO = '{iiBanco}' 
                      AND CUENTA = '{iiCuenta}'";

                oRes = await WSServicio.Servicio(oReq);

                if (oRes.Err != 0 || oRes.Data.Tables.Count < 1 || oRes.Data.Tables[0].Rows.Count == 0)
                {

                    MessageBoxMX.ShowDialog(null, $"No existe información para mostrar.", "Aviso", (int)StatusColorsTypes.Warning, true);

                    dataGridView2.DataSource = "";
                    cbActualizar.Enabled = false;
                    return;
                }

                // Poblar DataGridView con los datos obtenidos
                dataGridView2.DataSource = oRes.Data.Tables[0];
                cbActualizar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBoxMX.ShowDialog(null, $"Error al filtrar información:", "Error", (int)StatusColorsTypes.Warning, true);
                return;

            }
            
        }


        private void CbActualizar_Click()
        {

            MessageBoxMX.ShowDialog(null, $"La modificación ha sido exitosa", "Aviso", (int)StatusColorsTypes.Success, true);


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


    }
}
