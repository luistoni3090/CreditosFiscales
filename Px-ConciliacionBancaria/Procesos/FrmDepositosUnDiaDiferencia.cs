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
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;
using System.Diagnostics.Eventing.Reader;
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using Px_ConciliacionBancaria.Utiles.Win32;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;



namespace PX_ConciliacionBancaria
{
    public partial class FrmDepositosUnDiaDiferencia : FormaGenBar
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


        public FrmDepositosUnDiaDiferencia()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Conciliación Automática Por Diferencia de Fecha";

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

            await _Main.Status($"¿Deseas hacer la Conciliación Automática con diferencia de Fecha?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas hacer la Conciliación Automática con diferencia de Fecha?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string IdBanco = comboBanco.SelectedValue.ToString();
            string IdCuenta = comboCuenta.SelectedValue.ToString();


            var resultado = await ue_conciliada(IdBanco, IdCuenta);

            if (resultado == 1)

                resultado = await ue_cargada(IdBanco, IdCuenta);

            if (resultado == 1)
            {
                resultado = await ue_vigencia();

                if (resultado == 1)
                {
                    resultado = await ue_movtosvencidos(IdBanco, IdCuenta);

                    if (resultado == 1)
                    {
                        resultado = await ue_retrievemovtos();

                        if (resultado == 1)
                        {
                            resultado = await UeMovtosconciliadosAsync(IdBanco, IdCuenta);

                            if (resultado == 1)

                             MessageBoxMX.ShowDialog(null, "Proceso Autorizado con éxito", "Aviso", (int)StatusColorsTypes.Success, false);

                        }
                    }
                    

                }

                

            }

        }


        public async Task<int> ue_conciliada( string ii_banco, string ii_cuenta)
        {
            string Ld_fecha = "1998-12-31";
            DateTime Ldt_fechacon = DateTime.MinValue;

            oReq.Tipo = 0;
            oReq.Query = $@" SELECT FECHA_CON
            FROM DGI.CONCILIACION
            WHERE BANCO = '{ii_banco}'
              AND CUENTA = '{ii_cuenta}'
              AND TRUNC(FECHA) = TO_DATE('{Ld_fecha}', 'YYYY-MM-DD')";

            

            var oResProcesa = await WSServicio.Servicio(oReq);

            if (Object.ReferenceEquals(null, oResProcesa.Data.Tables[0]) || oResProcesa.Data.Tables.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "Al buscar FECHA_CON:", "Error", (int)StatusColorsTypes.Warning, false);

                return 0;
            }

            if (oResProcesa.Data.Tables[0].Rows.Count > 0)
            {
                Ldt_fechacon = Convert.ToDateTime(oResProcesa.Data.Tables[0].Rows[0]["FECHA_CON"]);
            }

            if (!IsDBNull(Ldt_fechacon) && oResProcesa.Err != 100)
            {
                MessageBoxMX.ShowDialog(null, "La cuenta ya está conciliada", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }


        public async Task<int> ue_cargada(string ii_banco, string ii_cuenta)
        {
            Id_SaldoIniAux = 0;
            Id_SaldoFinAux = 0;
            Id_SaldoIniBco = 0;
            Id_SaldoFinBco = 0;

            DateTime Ld_fecha = Idt_Fecha.Date;

            oReq.Tipo = 0;
            oReq.Query = $@"
            SELECT SALDOINIAUX, SALDOFINAUX, SALDOINIBCO, SALDOFINBCO
            FROM DGI.SALDO
            WHERE BANCO = '{ii_banco}'
              AND CUENTA = '{ii_cuenta}'
              AND TO_DATE(FECHA) = '{Ld_fecha}'";

            var oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Err == 100)
            {
                MessageBoxMX.ShowDialog(null, "La cuenta no ha sido cargada!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }
            else if (oResProcesa.Err == -1)
            {
                MessageBoxMX.ShowDialog(null, "Al buscar SALDOS:", "Error", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            if (Object.ReferenceEquals(null, oResProcesa.Data.Tables[0]) || oResProcesa.Data.Tables.Count < 1)
            {
                MessageBoxMX.ShowDialog(null, "No se encontraron datos en la tabla SALDO.", "Error", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oResProcesa.Data.Tables[0].Rows.Count > 0)
            {
                var row = oResProcesa.Data.Tables[0].Rows[0];
                Id_SaldoIniAux = Convert.ToDecimal(row["SALDOINIAUX"]);
                Id_SaldoFinAux = Convert.ToDecimal(row["SALDOFINAUX"]);
                Id_SaldoIniBco = Convert.ToDecimal(row["SALDOINIBCO"]);
                Id_SaldoFinBco = Convert.ToDecimal(row["SALDOFINBCO"]);
            }

            if (IsDBNull(Id_SaldoFinAux))
            {
                MessageBoxMX.ShowDialog(null, "No se han cargado los movimientos del auxiliar contable!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            if (IsDBNull(Id_SaldoFinBco))
            {
                MessageBoxMX.ShowDialog(null, "No se ha cargado el estado de cuenta del banco!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }


        public async Task<int> ue_vigencia()
        {
            int li_res = 0;
            DateTime ld_fecha = DateTime.Parse(Idt_Fecha.ToString());
            DateTime ldt_fechaAux = Idt_Fecha.Date, ldt_fechaBco = Idt_Fecha.Date;
            decimal ld_tolerancia = 0, ld_toleranciaDlls = 0;
            decimal? LdTolerancia = null;
            decimal? LdToleranciaDlls = null;

            // Consulta a la base de datos
            oReq.Tipo = 0;
            oReq.Query = $@"
        SELECT 
            ADD_MONTHS({ld_fecha}, -VIGENCIAAUX) AS FECHA_AUX,
            ADD_MONTHS({ld_fecha}, -VIGENCIABCO) AS FECHA_BCO,
            TOLERANCIA,
            TOLERANCIADLLS
        FROM 
            DGI.PARAMETRO_CB
    ";
            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                ldt_fechaAux = DateTime.Parse(oRes.Data.Tables[0].Rows[0]["FECHA_AUX"].ToString());
                ldt_fechaBco = DateTime.Parse(oRes.Data.Tables[0].Rows[0]["FECHA_BCO"].ToString());
                ld_tolerancia = decimal.Parse(oRes.Data.Tables[0].Rows[0]["TOLERANCIA"].ToString());
                ld_toleranciaDlls = decimal.Parse(oRes.Data.Tables[0].Rows[0]["TOLERANCIADLLS"].ToString());
            }
            else
            {

                if (MessageBoxMX.ShowDialog(null, $"Aviso: No hay registrados parámetros de vigencia. ¿Desea continuar con el proceso??", "Aviso", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return 0;
                }

                
            }

            // Asignación de valores
            if (Is_Moneda == "P")
            {
                LdTolerancia = ld_tolerancia;
            }
            else
            {
                ld_tolerancia = ld_toleranciaDlls;
            }

            if (ld_tolerancia == null) ld_tolerancia = 0;

            // Asignación de fechas
            if (ldt_fechaAux == null) Id_VigenciaAux = FProFechaIniMes(ld_fecha);
            //else Id_VigenciaAux = FProFechaIniMes(Date(ldt_fechaAux));

            if (ldt_fechaBco == null) Id_VigenciaBco = FProFechaIniMes(ld_fecha);
            //else Id_VigenciaBco = FProFechaIniMes(Date(ldt_fechaBco));

            return 1;
        }


        public async Task<int> ue_movtosvencidos(string Ii_banco, string Ii_cuenta)
        {
            DateTime ldt_FechaMin;
            DateTime ld_fechaMin;
            long ll_Banco, ll_Cuenta, ll_Con;
            String ls_VigenciaAux, ls_VigenciaBco;

            // Movimientos del Auxiliar
            ls_VigenciaAux = Id_VigenciaAux.ToString("dd-MM-yyyy");

            // Consulta a la base de datos
                oReq.Tipo = 0;
                oReq.Query = $@"
                SELECT 
                    BANCO,
                    CUENTA,
                    CONCILIACION,
                    MIN(FECHA) AS FECHA_MIN
                FROM DGI.MOVAUX
                WHERE 
                    BANCO = {Ii_banco} AND 
                    CUENTA = {Ii_cuenta} AND 
                    CONCILIACION IS NULL
                GROUP BY BANCO, CUENTA, CONCILIACION
            ";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                ldt_FechaMin = DateTime.Parse(oRes.Data.Tables[0].Rows[0]["FECHA_MIN"].ToString());
                //ld_fechaMin = Date(ldt_FechaMin);

                if (ldt_FechaMin < Id_VigenciaAux)
                {
                    MessageBoxMX.ShowDialog(null, $@"Existen movimientos rezagados del auxiliar contable con fecha menor al límite de vigencia! Vigencia =  {ls_VigenciaAux} ", "Aviso", (int)StatusColorsTypes.Question, true);
                   
                        return 0;
                }
            }

            // Movimientos del Banco
            ls_VigenciaBco = Id_VigenciaBco.ToString("dd-MM-yyyy");

            // Consulta a la base de datos
            oReq.Tipo = 0;
            oReq.Query = @"
                SELECT 
                    MIN(DGI.MOVBANCO.FECHA) AS FECHA_MIN
                FROM
                    DGI.MOVBANCO
                WHERE
                    (DGI.MOVBANCO.BANCO = :Ii_banco) AND
                    (DGI.MOVBANCO.CUENTA = :Ii_cuenta) AND
                    (DGI.MOVBANCO.CONCILIACION IS NULL)";

            oRes = await WSServicio.Servicio(oReq);

            if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                ldt_FechaMin = DateTime.Parse(oRes.Data.Tables[0].Rows[0]["FECHA_MIN"].ToString());
                //ld_fechaMin = Date(ldt_FechaMin);

                if (ldt_FechaMin < Id_VigenciaBco)
                {
                    MessageBoxMX.ShowDialog(null, $@"Existen movimientos rezagados del banco con fecha menor al límite de vigencia! Vigencia = {ls_VigenciaBco} ", "Aviso", (int)StatusColorsTypes.Warning, false);
                    return 0;
                }
            }

            return 1;
        }


        public async Task<int> ue_retrievemovtos()
        {
            DateTime ld_FechaIni, ld_FechaFin;
            Decimal  ld_Resultado, ld_SaldoConAux, ld_SaldoConBco;

            ld_FechaFin = Idt_Fecha.Date;
            ld_FechaIni = FProFechaIniMes(ld_FechaFin);

            ld_SaldoConAux = await WfSaldo('A');
            ld_Resultado = Id_SaldoFinAux - (Id_SaldoIniAux + ld_SaldoConAux);

            if (ld_Resultado != 0)
            {
                MessageBoxMX.ShowDialog(null, "El saldo obtenido de la suma de cargos y abonos mas el saldo inicial del auxiliar contable es diferente al saldo registrado por el modulo de bancos", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            ld_SaldoConBco = await WfSaldo('B');
            ld_Resultado = Id_SaldoFinBco - (Id_SaldoIniBco + ld_SaldoConBco);

            if (ld_Resultado != 0)
            {
                MessageBoxMX.ShowDialog(null, "El saldo obtenido de la suma de cargos y abonos mas el saldo inicial del banco es diferente al saldo registrado en la carga de movimientos", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            // Llamada al procedimiento almacenado
            var oReq = new eRequest();
            oReq.Tipo = 1;
            oReq.Query = $@"
            EXECUTE DGI.P_ACTUALIZAMOVTOS 
                @BANCO = {Ii_Banco}, 
                @CUENTA = {Ii_Cuenta}, 
                @FECHA_INI = TO_DATE('{ld_FechaIni:yyyy-MM-dd}', 'YYYY-MM-DD'), 
                @FECHA_FIN = TO_DATE('{ld_FechaFin:yyyy-MM-dd}', 'YYYY-MM-DD')";


            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, oRes.Message, "Error", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }


        public async Task<int> UeMovtosconciliadosAsync(string Ii_banco, string Ii_cuenta)
        {
             DateTime IdtFecha;
            DateTime IdVigenciaBco = new DateTime(2024, 03, 01);
             DateTime IdVigenciaAux = new DateTime(2024, 04, 01);
            decimal IdTolerancia = 10;
            decimal IlConciliacion = 0;
            string UsuarioActual = "ADMIN";

        DateTime LdFechaFin = Idt_Fecha.Date;
            DateTime LdFechaIniBco = FProFechaIniMes(IdVigenciaBco);
            DateTime LdFechaIniAux = FProFechaIniMes(IdVigenciaAux);

            // 1. Ejecutar P_FECDIF
            var oReq1 = new eRequest();
            oReq1.Tipo = 1;
            oReq1.Query = $@"
            EXECUTE DGI.P_FECDIF 
                @BANCO = {Ii_banco}, 
                @CUENTA = {Ii_cuenta}, 
                @CONCILIACION = {IlConciliacion}";

            var oRes1 = await WSServicio.Servicio(oReq1);

            if (oRes1.Err != 0)
            {
                MessageBoxMX.ShowDialog(null,
                    $"ERROR EN P_FECDIF: {oRes1.Message}",
                    "Error",
                    (int)StatusColorsTypes.Danger,
                    false);
                return 0;
            }

            // 2. Ejecutar P_FEC_DIFIMPTE si el checkbox está activado
            if (checkConciliaSimilitud.Checked)
            {
                var oReq2 = new eRequest();
                oReq2.Tipo = 1;
                oReq2.Query = $@"
                EXECUTE DGI.P_FEC_DIFIMPTE 
                    @BANCO = {Ii_banco},
                    @CUENTA = {Ii_cuenta},
                    @FECHA_INI_AUX = TO_DATE('{LdFechaIniAux:yyyy-MM-dd}', 'YYYY-MM-DD'),
                    @FECHA_INI_BCO = TO_DATE('{LdFechaIniBco:yyyy-MM-dd}', 'YYYY-MM-DD'),
                    @FECHA_FIN = TO_DATE('{LdFechaFin:yyyy-MM-dd}', 'YYYY-MM-DD'),
                    @TOLERANCIA = {IdTolerancia},
                    @CONCILIACION = {IlConciliacion},
                    @USUARIO = '{UsuarioActual}'";

                var oRes2 = await WSServicio.Servicio(oReq2);

                if (oRes2.Err != 0)
                {
                    MessageBoxMX.ShowDialog(null,
                        $"ERROR EN P_FEC_DIFIMPTE: {oRes2.Message}",
                        "Error",
                        (int)StatusColorsTypes.Danger,
                        false);
                    return 0;
                }
            }

            return 1;
        }


        private async Task<decimal> WfSaldo(char tipo)
        {
            string tabla = (tipo == 'A') ? "MOVAUX" : "MOVBANCO";

            DateTime ld_FechaIni = DateTime.Today; ;

            ld_FechaIni = FProFechaIniMes(ld_FechaIni);
            DateTime Ld_FechaFin = DateTime.Today;

            oReq.Tipo = 0;
            oReq.Query = $@"
            SELECT NVL(SUM(CARGOS - ABONOS), 0) AS SALDO
            FROM DGI.{tabla}
            WHERE 
                BANCO = {Ii_Banco} AND 
                CUENTA = {Ii_Cuenta} AND 
                FECHA BETWEEN 
                    TO_DATE('{ld_FechaIni:yyyy-MM-dd}', 'YYYY-MM-DD') AND 
                    TO_DATE('{Ld_FechaFin:yyyy-MM-dd}', 'YYYY-MM-DD')";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0 || oRes.Data.Tables[0].Rows.Count == 0)
                return 0;

            return Convert.ToDecimal(oRes.Data.Tables[0].Rows[0]["SALDO"]);
        }


        private bool IsDBNull(object value)
        {
            return value == null || value == DBNull.Value;
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
