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



namespace PX_ConciliacionBancaria
{
    public partial class FrmConciliacionA5DigitosDiferenciaImporte : FormaGenBar
    {
        private bool ib_complete = true, ib_coment = false;
        private int ii_interval, Ii_Banco, Ii_Cuenta;
        private string Is_Moneda, Is_Coment;
        private DateTime Idt_Fecha;
        private DateTime Id_VigenciaAux, Id_VigenciaBco;
        private decimal Id_SaldoIniAux, Id_SaldoIniBco, Id_SaldoFinAux, Id_SaldoFinBco;
        private decimal Id_SaldoCon, Id_AbonoAux, Id_AbonoBco, Id_CargoAux, Id_CargoBco, Id_Tolerancia, Id_Ajuste;
        private long Il_Inicio, Il_Conciliacion, Il_TotAbonoAux, Il_TotAbonoBco, Il_TotCargoAux, Il_TotCargoBco, Il_TotAjuste, Il_RezagoAux, Il_RezagoBco;

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmConciliacionA5DigitosDiferenciaImporte()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Conciliación Automática Por Similitud de Referencia";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            //await LoadMainGrid();

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


        #region "Carga Combo Moneda"
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
        #endregion

        private async void detConciliar(object sender, EventArgs e)
        {
            bool bExito = await detValida();

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas hacer la Conciliación por Diferencia de Importe 5 Dígitos?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas hacer la Conciliación por Diferencia de Importe 5 Dígitos?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string Ls_user = "UsuarioActual"; // Reemplazar por lógica para obtener el usuario actual

            var resultado = await UeMovtosConciliados(Ls_user);

            if (resultado == 1) {

                 resultado = await UeVigencia();

                if (resultado == 1)
                {
                    string IdBanco = comboBanco.SelectedValue.ToString();
                    string IdCuenta = comboCuenta.SelectedValue.ToString();


                   string  resultado1 = await UeTipoCtaAsync(IdBanco, IdCuenta);

                   // MessageBoxMX.ShowDialog(null, "Proceso Autorizado", "Aviso", (int)StatusColorsTypes.Success, false);


                }


            }



            }


        public async Task<int> UeMovtosConciliados(string usuarioActual)
        {
            DateTime Ld_FechaIniBco = FProFechaIniMes(Id_VigenciaBco);
            DateTime Ld_FechaIniAux = FProFechaIniMes(Id_VigenciaAux);
            DateTime Ld_FechaFin = Idt_Fecha;

            if (checkConciliaSimilitud.Checked)
            {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                    BEGIN
                        DGI.P_CON_DIF_SREF(
                            {Ii_Banco},
                            {Ii_Cuenta},
                            TO_DATE('{Ld_FechaIniAux:yyyy-MM-dd HH:mm:ss}', 'yyyy-MM-dd HH24:MI:SS'),
                            TO_DATE('{Ld_FechaIniBco:yyyy-MM-dd HH:mm:ss}', 'yyyy-MM-dd HH24:MI:SS'),
                            TO_DATE('{Ld_FechaFin:yyyy-MM-dd HH:mm:ss}', 'yyyy-MM-dd HH24:MI:SS'),
                            '{usuarioActual}',
                            {Il_Conciliacion},
                            {Id_Tolerancia}
                        );
                    END;
                ";

                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null, "Error en P_CON_DIF_SREF: " + oRes.Message, "Error", 2, false);
                    return 0;
                }
            }

            return 1;
            
        }


        public async Task<int> UeVigencia()
        {
            int li_Res;
            DateTime ld_Fecha = Idt_Fecha.Date;
            DateTime? ldt_FechaAux = null;
            DateTime? ldt_FechaBco = null;
            decimal? ld_Tolerancia = null;
            decimal? ld_ToleranciaDlls = null;

            // Consulta para obtener los parámetros de vigencia

            oReq.Tipo = 0;
                oReq.Query = $@"
            SELECT 
                ADD_MONTHS(TO_DATE('{Idt_Fecha:yyyy-MM-dd}', 'yyyy-MM-dd'), -DGI.PARAMETRO_CB.VIGENCIAAUX),
                        ADD_MONTHS(TO_DATE('{Idt_Fecha:yyyy-MM-dd}', 'yyyy-MM-dd'), -DGI.PARAMETRO_CB.VIGENCIABCO),
                DGI.PARAMETRO_CB.TOLERANCIA,
                DGI.PARAMETRO_CB.TOLERANCIADLLS
                FROM DGI.PARAMETRO_CB";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err == 0 && oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                var row = oRes.Data.Tables[0].Rows[0];
                ldt_FechaAux = row.Field<DateTime?>(0);
                ldt_FechaBco = row.Field<DateTime?>(1);
                ld_Tolerancia = row.Field<decimal?>(2);
                ld_ToleranciaDlls = row.Field<decimal?>(3);
            }
            else
            {
                // Si ocurre un error o no hay datos
                MessageBoxMX.ShowDialog(null, $"Error al obtener los parámetros: {oRes.Message}", "Error", (int)StatusColorsTypes.Danger, false);
                ldt_FechaAux = ldt_FechaAux ?? FProFechaIniMes(ld_Fecha);
                ldt_FechaBco = ldt_FechaBco ?? FProFechaIniMes(ld_Fecha);

                await _Main.Status($"No se encontraron parámetros de vigencia. ¿Desea continuar sin considerar vigencias ni tolerancia?", (int)MensajeTipo.Question);
                if (MessageBoxMX.ShowDialog(null, $"No se encontraron parámetros de vigencia. ¿Desea continuar sin considerar vigencias ni tolerancia?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                    return 0;

                /*li_Res = MessageBoxMX.ShowDialog(null,
                    "No se encontraron parámetros de vigencia. ¿Desea continuar sin considerar vigencias ni tolerancia?",
                    "Aviso",
                    (int)MessageBoxButtons.YesNo, false);*/
                else {
                    //if (li_Res == (int)DialogResult.No)
                    //{
                    //    return 0;
                    //}
                    return 1;
                }
                
            }

            // Asignar valores basados en Is_Moneda
            Id_Tolerancia = (Is_Moneda == "P" ? ld_Tolerancia : ld_ToleranciaDlls) ?? 0;

            // Validar fechas y asignar valores de vigencia
            Id_VigenciaAux = FProFechaIniMes(ldt_FechaAux ?? ld_Fecha);
            Id_VigenciaBco = FProFechaIniMes(ldt_FechaBco ?? ld_Fecha);

            return 1;
        }


        public async Task<string> UeTipoCtaAsync(string iiBanco, string iiCuenta)
        {
            string lsTipo = string.Empty;


            oReq.Tipo = 0; // Tipo de consulta: SELECT
            oReq.Query = $@"
            SELECT 
                TIPO
            FROM 
                DGI.CUENTA
            WHERE 
                DGI.CUENTA.BANCO = '{iiBanco}' AND
                DGI.CUENTA.CUENTA = '{iiCuenta}' ";
           

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes != null && oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                lsTipo = oRes.Data.Tables[0].Rows[0]["TIPO"]?.ToString() ?? string.Empty;
            }

            return lsTipo;
        }




        /// <summary>
        /// Calcula el inicio del mes de una fecha dada.
        /// </summary>
        private DateTime FProFechaIniMes(DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }




        #region "Validar datos"
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
        #endregion


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
