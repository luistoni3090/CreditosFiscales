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
    public partial class FrmRegistroDeAjustes : FormaGenBar
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


        public FrmRegistroDeAjustes()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Registro de Ajustes por Diferencia de Importes";

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
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas Continuar?", "Autorizar", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;


            string Ii_Bco = comboBanco.SelectedValue?.ToString();
            string Ii_Cta = comboCuenta.SelectedValue?.ToString();
            string mes = comboMes.SelectedValue?.ToString();
            string anio = txtAnio.Text;

            int resultado = await UeConciliadaAsync(Ii_Bco, Ii_Cta, mes, anio);
            if (resultado == 1) {
                await UeAutorizadaAsync(Ii_Bco, Ii_Cta, mes, anio);
                await UeImpteAjusteAsync();
                await UeGrabaAjuste();
                await UpdateConciliacionAsync(1);

            }
           

        }





        // Método ue_conciliada
        public async Task<int> UeConciliadaAsync(string Ii_Bco, string Ii_Cta, string mes, string anio)
        {
            DateTime Ldt_fechacon;
            DateTime Ld_fecha = Idt_Fecha.Date;

            // Configurar la consulta  


            oReq.Tipo = 0;
            oReq.Query = $@"
                SELECT FECHA_CON
                FROM DGI.CONCILIACION
                WHERE BANCO = '{Ii_Bco}' 
                  AND CUENTA = '{Ii_Cta}' 
                  AND FECHA = TO_DATE('{Ld_fecha:yyyy-MM-dd}', 'yyyy-MM-dd')";
            var oRes = await WSServicio.Servicio(oReq);

            // Ejecutar la consulta
            var oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Err == -1 || oResProcesa.Data.Tables.Count < 1 || oResProcesa.Data.Tables[0].Rows.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "Al buscar FECHA_CON: " + "No se encontraron registros", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            var row = oResProcesa.Data.Tables[0].Rows[0];
            if (row.IsNull("FECHA_CON"))
            {
                MessageBoxMX.ShowDialog(null, "La cuenta no ha sido conciliada!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            Ldt_fechacon = Convert.ToDateTime(row["FECHA_CON"]);
            return 1;
        }


        public async Task<int> UeAutorizadaAsync(string Ii_Bco, string Ii_Cta, string mes, string anio)
        {
            DateTime? Ldt_fechaaut;
            DateTime Ld_fecha = Idt_Fecha.Date;

            // Configurar la consulta
            var oReq = new eRequest
            {
                Tipo = 0,
                Query = $@"
                SELECT FECHA_AUT
                FROM DGI.CONCILIACION
                WHERE BANCO = '{Ii_Bco}' 
                  AND CUENTA = '{Ii_Cta}' 
                  AND FECHA = TO_DATE('{Ld_fecha:yyyy-MM-dd}', 'yyyy-MM-dd')
            "
            };

            // Ejecutar la consulta
            var oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Err == -1 || oResProcesa.Data.Tables.Count < 1 || oResProcesa.Data.Tables[0].Rows.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "Error al buscar FECHA_AUT: " + oResProcesa.Message, "Error", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            var row = oResProcesa.Data.Tables[0].Rows[0];
            Ldt_fechaaut = row.IsNull("FECHA_AUT") ? (DateTime?)null : Convert.ToDateTime(row["FECHA_AUT"]);

            if (Ldt_fechaaut.HasValue)
            {
                MessageBoxMX.ShowDialog(null, "La cuenta ya está autorizada!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }

        // Método ue_impteajuste
        public async Task<int> UeImpteAjusteAsync()
        {
            // Configurar llamada a procedimiento almacenado
            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"
                BEGIN
                    DGI.P_AJUSTES(
                        {Ii_Bco},
                        {Ii_Cta},
                        TO_DATE('{Idt_Fecha:yyyy-MM-dd}', 'yyyy-MM-dd'),
                        {Il_Conciliacion},
                        {Id_Cargos},
                        {Id_Abonos}
                    );
                END;
            "
            };

            // Ejecutar el procedimiento
            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error (P_AJUSTES): " + oRes.Message, "Error", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            return 1;
        }


        public async Task<int> UeGrabaAjuste()
        {
            decimal ldAbonosDLL = 0;
            decimal ldCargosDLL = 0;
            string lsBcoDescr = string.Empty, lsCtaDescr = string.Empty, lsCtaCon = string.Empty, lsCtaSub = string.Empty, lsCtaSubSubCta = string.Empty;
            string lsTipo = string.Empty, lsMes = string.Empty, lsFecha = string.Empty, isFecha = string.Empty;
            DateTime ldFecha = DateTime.MinValue, ldtFecha = DateTime.MinValue;
            int liAnio = 0;
             

        int currentRow = dw_1.CurrentCell.RowIndex;
            lsMes = dw_1.Rows[currentRow].Cells["mes"].Value?.ToString() ?? string.Empty;
            lsMes = lsMes.PadLeft(2, '0');
            liAnio = Convert.ToInt32(dw_1.Rows[currentRow].Cells["anio"].Value ?? 0);
            lsFecha = $"{lsMes}{liAnio}";
            isFecha = lsFecha;
            ldFecha = DateTime.ParseExact($"01/{lsMes}/{liAnio}", "dd/MM/yyyy", null);

            var oReq = new eRequest();
            oReq.Tipo = 0;
            oReq.Query = @"
                SELECT TO_DATE(SYSDATE) AS Fecha
                FROM SYS.DUAL
            ";

            var oResProcesa = await WSServicio.Servicio(oReq);
            if (oResProcesa.Data.Tables[0].Rows.Count > 0)
            {
                ldtFecha = Convert.ToDateTime(oResProcesa.Data.Tables[0].Rows[0]["Fecha"]);
            }

            if (Id_Abonos == 0 && Id_Cargos == 0)
            {
                // Obtener descripción del banco
                oReq.Query = $@"
            SELECT DESCR 
            FROM DGI.BANCO 
            WHERE BANCO = '{Ii_Bco}'
        ";
                oResProcesa = await WSServicio.Servicio(oReq);
                if (oResProcesa.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null, $"Error: {oResProcesa.Message}", "Información", (int)StatusColorsTypes.Danger, false);
                    return 0;
                }
                lsBcoDescr = oResProcesa.Data.Tables[0].Rows[0]["DESCR"].ToString();

                // Obtener información de la cuenta
                oReq.Query = $@"
            SELECT DESCR, CTACONTABLE, SUBCTA, SUBSUBCTA
            FROM DGI.CUENTA
            WHERE CUENTA = '{Ii_Cta}' AND BANCO = '{Ii_Bco}'
        ";
                oResProcesa = await WSServicio.Servicio(oReq);
                if (oResProcesa.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null, $"Error: {oResProcesa.Message}", "Información", (int)StatusColorsTypes.Danger, false);
                    return 0;
                }
                var row = oResProcesa.Data.Tables[0].Rows[0];
                lsCtaDescr = row["DESCR"].ToString();
                lsCtaCon = row["CTACONTABLE"].ToString();
                lsCtaSub = row["SUBCTA"].ToString();
                lsCtaSubSubCta = row["SUBSUBCTA"].ToString();

                MessageBoxMX.ShowDialog(null, $"No hubieron ajustes para la cuenta: {Ii_Bco}-{Ii_Cta}", "Aviso", (int)StatusColorsTypes.Info, false);
                return 1;
            }

            // Reset de variables
            ldAbonosDLL = 0;
            ldCargosDLL = 0;

            if (Is_Moneda == "D")
            {
                oReq.Query = @"
            EXEC SQLCA.P_TC(:Id_Tc)
        ";
                oResProcesa = await WSServicio.Servicio(oReq);
                if (oResProcesa.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null, $"Error (P_TC): {oResProcesa.Message}", "Información", (int)StatusColorsTypes.Danger, false);
                    return 0;
                }
                ldAbonosDLL = Math.Abs(Math.Round(Id_Abonos * Id_Tc, 2) - Id_Abonos);
                ldCargosDLL = Math.Abs(Math.Round(Id_Cargos * Id_Tc, 2) - Id_Cargos);
            }

            if (Id_Cargos != 0)
            {
                Is_TipoReg = "1";
                oReq.Query = $@"
            EXEC SQLCA.P_DEPOSITO('{Ii_Bco}', '{Ii_Cta}', '1', TO_DATE('{Idt_Fecha:yyyy-MM-dd}', 'yyyy-MM-dd'), {Id_Cargos}, {ldCargosDLL}, '{Is_TipoReg}', '{AplFund.UsuarioActual}')
        ";
                oResProcesa = await WSServicio.Servicio(oReq);
                if (oResProcesa.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null, $"Error (P_DEPOSITO (CARGOS)): {oResProcesa.Message}", "Información", (int)StatusColorsTypes.Danger, false);
                    return 0;
                }
                Is_tipo_o = Id_Cargos < 0 ? "3" : "1";
            }

            return 1;
        }

        public async Task<int> UpdateConciliacionAsync(int Il_Conciliacion)
        {
            var oReq = new eRequest
            {
                Tipo = 1, // Tipo 1 indica una consulta de modificación (UPDATE).
                Query = $@"
            UPDATE DGI.CONCILIACION
            SET FECHA_AUT = SYSDATE,
                APP_AUT = '{AplFund.UsuarioActual}'
            WHERE CONCILIACION = {Il_Conciliacion}
        "
            };

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                MessageBox.Show($"Update Error: {oRes.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Indica que hubo un error en el proceso.
            }

            return 1; // Indica que la actualización fue exitosa.
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
