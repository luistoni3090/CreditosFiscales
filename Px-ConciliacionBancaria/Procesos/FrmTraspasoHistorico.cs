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



namespace PX_ConciliacionBancaria
{
    public partial class FrmTraspasoHistorico : FormaGenBar
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


        public FrmTraspasoHistorico()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Traspaso al Histórico";

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
            await cargaComboMoneda();
            txtAnio.Text = DateTime.Now.Year.ToString();
            dw_1 = new DataGridView();
            // Agregar controles a la ventana
            Controls.Add(dw_1);

        }

        private void comboMes_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        private async Task cargaComboMoneda()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "1",
                Text = "ENERO"
            };
            var item2 = new ComboBoxItem
            {
                Value = "2",
                Text = "FEBRERO"
            };
            var item3 = new ComboBoxItem
            {
                Value = "3",
                Text = "MARZO"
            };
            var item4 = new ComboBoxItem
            {
                Value = "4",
                Text = "ABRIL"
            };
            var item5 = new ComboBoxItem
            {
                Value = "5",
                Text = "MAYO"
            };
            var item6 = new ComboBoxItem
            {
                Value = "6",
                Text = "JUNIO"
            };
            var item7 = new ComboBoxItem
            {
                Value = "7",
                Text = "JULIO"
            };
            var item8 = new ComboBoxItem
            {
                Value = "8",
                Text = "AGOSTO"
            };
            var item9 = new ComboBoxItem
            {
                Value = "9",
                Text = "SEPTIEMBRE"
            };
            var item10 = new ComboBoxItem
            {
                Value = "10",
                Text = "OCTUBRE"
            };
            var item11 = new ComboBoxItem
            {
                Value = "11",
                Text = "NOVIEMBRE"
            };
            var item12 = new ComboBoxItem
            {
                Value = "12",
                Text = "DICIEMBRE"
            };
            comboList.Add(item1);
            comboList.Add(item2);
            comboList.Add(item3);
            comboList.Add(item4);
            comboList.Add(item5);
            comboList.Add(item6);
            comboList.Add(item7);
            comboList.Add(item8);
            comboList.Add(item9);
            comboList.Add(item10);
            comboList.Add(item11);
            comboList.Add(item12);

            // Asignar la lista al ComboBox
            comboMes.DataSource = comboList;
            comboMes.DisplayMember = "Text";
            comboMes.ValueMember = "Value";
            comboMes.SelectedIndex = -1;

        }

        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;


            if (string.IsNullOrWhiteSpace(txtAnio.Text))
                sErr += "Ingrese el mes ";

            if (string.IsNullOrWhiteSpace(comboCuenta.Text))
                sErr += "Seleccione la cuenta. ";

            if (string.IsNullOrWhiteSpace(comboMes.Text))
                sErr += "Seleccione el mes. ";


            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                string anio = txtAnio.Text;
                string IdBanco = comboBanco.SelectedValue.ToString();
                string IdCuenta = comboCuenta.SelectedValue.ToString();
                string IdMes = comboMes.SelectedValue.ToString();

            }

            return bExito;
        }


       
        private async void detcbAutorizar(object sender, EventArgs e)
        {
            bool bExito = await detValida();

            if (!bExito)
               return;

            await _Main.Status($"¿Deseas Continuar?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas Continuar?", "Depurar", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string Ii_Bco = comboBanco.SelectedValue?.ToString();
            string Ii_Cta = comboCuenta.SelectedValue?.ToString();
            string mes = comboMes.SelectedValue?.ToString();
            string anio = txtAnio.Text;

            int resultado = await UeProcesarAsync(Ii_Bco, Ii_Cta);

        }


        public async Task<int> UeProcesarAsync(string banco, string cuenta)
        {
            DateTime fecBco, fecIni;
            DateTime? tmpFechaAut = null, tmpFechaDep = null;
            int secConcilia = 0;

            DateTime fecha = Idt_Fecha.Date;

            // Obtener fecha bancaria inicial

            oReq.Tipo = 0;
            oReq.Query = $@"
            SELECT ADD_MONTHS(TO_DATE('{fecha}', 'YYYY-MM-DD'), -DGI.PARAMETRO_CB.VIGENCIABCO) AS FEC_BCO
            FROM DGI.PARAMETRO_CB";

            var oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Data.Tables.Count < 1 || oResProcesa.Data.Tables[0].Rows.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "Error al obtener la fecha bancaria inicial.", "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            fecBco = Convert.ToDateTime(oResProcesa.Data.Tables[0].Rows[0]["FEC_BCO"]);
            //fecIni = FProFechaIniMes(fecBco);

            // Consultar conciliación
            oReq.Tipo = 0;
            oReq.Query = $@"
                SELECT 
            DGI.CONCILIACION.FECHA_AUT AS FECHA_AUT, 
            DGI.CONCILIACION.FECHA_DEP AS FECHA_DEP,
            DGI.CONCILIACION.CONCILIACION AS SEC_CONCILIA
        FROM DGI.CONCILIACION
        WHERE 
            DGI.CONCILIACION.BANCO = {banco} AND
            DGI.CONCILIACION.CUENTA = {cuenta} AND
            TO_DATE(DGI.CONCILIACION.FECHA, 'YYYY-MM-DD') = {fecha}";

            oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Data.Tables.Count < 1 || oResProcesa.Data.Tables[0].Rows.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "No se encontraron registros de conciliación.", "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            var row = oResProcesa.Data.Tables[0].Rows[0];
            tmpFechaAut = row["FECHA_AUT"] != DBNull.Value ? Convert.ToDateTime(row["FECHA_AUT"]) : (DateTime?)null;
            tmpFechaDep = row["FECHA_DEP"] != DBNull.Value ? Convert.ToDateTime(row["FECHA_DEP"]) : (DateTime?)null;
            secConcilia = Convert.ToInt32(row["SEC_CONCILIA"]);

            // Validar las fechas y continuar con el procesamiento
            if (tmpFechaAut.HasValue && !tmpFechaDep.HasValue)
            {
                //int idbDepuraBco = await DwDepuraBcoRetrieve(banco, cuenta, secConcilia);
                //int idbDepuraAux = await DwDepuraAuxRetrieve(banco, cuenta, secConcilia);
                //int idbDepuraMatchManual = await DwDepuraAuxVsBcoRetrieve(banco, cuenta, secConcilia);
                //int idbDepuraMatchBco = await DepuraBcoVsBcoRetrieve(banco, cuenta, secConcilia);
                //int idbDepuraMatchAux = await DwDepuraAuxVsAuxRetrieve(banco, cuenta, secConcilia);

                // Habilitar botón de depuración y actualizar barra de progreso
                //cbDepurar.Enabled = true;
                int barraProgreso = 20;
                //oleBarraProgresiva.Object.Value = barraProgreso;
            }
            else
            {
                MessageBoxMX.ShowDialog(null, "La cuenta no ha sido autorizada o ya ha sido depurada.", "Error", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            MessageBoxMX.ShowDialog(null, "Proceso Autorizado con éxito.", "Aviso", (int)StatusColorsTypes.Success, false);
            return 1;
            
        }

      

        public static class AplFund
        {
            public static string UsuarioActual { get; set; } = "APACHECO";
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
