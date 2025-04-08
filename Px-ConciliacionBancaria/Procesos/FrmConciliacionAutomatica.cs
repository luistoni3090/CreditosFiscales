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
    public partial class FrmConciliacionAutomatica : FormaGenBar
    {
        private string is_docname, is_named, is_archivoini, is_formato, is_nom_pol;
        private DateTime id_fecha_poliza;
        private DataTable dwPolizaDataTable;
        DataTable dwPolizaGastoDataTable = new DataTable();

        private int ii_filenum, ii_renglones, ii_origen;
        private decimal id_cifra;

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmConciliacionAutomatica()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Conciliación Automática";

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

            //tabPage1.Text = "Polizas";

            //panFiltros.Visible = false;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{_Titulo}", (int)MensajeTipo.Info);

            await cargaComboBanco();
            await cargaComboMoneda();
            txtAnio.Text = DateTime.Now.Year.ToString();


        }

        private async void detConciliar(object sender, EventArgs e)
        {
            bool bExito = await detValida();

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas hacer la Conciliación Automática?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas hacer la Conciliación Automática?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            MessageBoxMX.ShowDialog(null, "Proceso Autorizado con éxito", "Aviso", (int)StatusColorsTypes.Success, false);
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


        public async Task CrearTablaConciliacionAutomaticaAsync(int ii_banco, int ii_cuenta, DateTime id_vigenciaaux, DateTime Idt_Fecha, DateTime id_vigenciabco)
        {
            // Formato de fecha requerido por Oracle
            string fechaVigenciaAuxStr = id_vigenciaaux.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");
            string fechaVigenciaBcoStr = id_vigenciabco.ToString("dd/MM/yyyy");

            // Construcción de la consulta SQL
            var oReq = new eRequest();
            oReq.Tipo = 1; // Tipo 1 para ejecución de consulta

            oReq.Query = $@"
        CREATE TABLE is_tabla_cargos_ref AS 
        SELECT 
            DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
            DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
            DECODE(A.C1, NULL, 0, A.C1) AS C1,
            DECODE(B.C2, NULL, 0, B.C2) AS C2,
            DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
            DECODE(A.CONCEPTO, NULL, DECODE(B.CONCEPTO, NULL, A.CONCEPTO, B.CONCEPTO), A.CONCEPTO) AS CONCEPTO,
            (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
            A.MOVAUX,
            B.MOVBCO
        FROM 
            (SELECT BANCO, CUENTA, TIPO, CONCEPTO, IMPORTE, COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX
             FROM DGI.MOVAUX
             WHERE BANCO = {ii_banco} 
               AND CUENTA = {ii_cuenta} 
               AND STATUS IS NULL
               AND TIPO IN ('1', '5')
               AND CONCEPTO IS NOT NULL
               AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
               AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
             GROUP BY BANCO, CUENTA, TIPO, CONCEPTO, IMPORTE) A
        FULL OUTER JOIN 
            (SELECT BANCO, CUENTA, TIPO, CONCEPTO, IMPORTE, COUNT(*) AS C2, MAX(MOVBCO) AS MOVBCO
             FROM DGI.MOVBANCO
             WHERE BANCO = {ii_banco} 
               AND CUENTA = {ii_cuenta}
               AND STATUS IS NULL
               AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
               AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
               AND (CANCEL <> 'C' OR CANCEL IS NULL)
               AND TIPO = '1'
             GROUP BY BANCO, CUENTA, TIPO, CONCEPTO, IMPORTE) B
        ON A.BANCO = B.BANCO
       AND A.CUENTA = B.CUENTA
       AND A.IMPORTE = B.IMPORTE
       AND A.CONCEPTO = B.CONCEPTO
    ";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                System.Diagnostics.Debug.WriteLine("ERROR AL CREAR LA TABLA: " + oRes.Message);
            }
        }

        public async Task CrearTablaCargosAsync(int ii_banco, int ii_cuenta, DateTime id_vigenciaaux, DateTime Idt_Fecha, DateTime id_vigenciabco)
        {
            // Formato de fechas para Oracle
            string fechaVigenciaAuxStr = id_vigenciaaux.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");
            string fechaVigenciaBcoStr = id_vigenciabco.ToString("dd/MM/yyyy");

            // Construcción de la consulta SQL
            var oReq = new eRequest();
            oReq.Tipo = 1; // Tipo 1 para ejecución de consulta

        oReq.Query = $@"
        CREATE TABLE is_tabla_cargos AS 
        SELECT 
            DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
            DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
            DECODE(A.C1, NULL, 0, A.C1) AS C1,
            DECODE(B.C2, NULL, 0, B.C2) AS C2,
            DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
            DECODE(A.FECHA, NULL, DECODE(B.FECHA, NULL, A.FECHA, B.FECHA), A.FECHA) AS FECHA,
            (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
            A.MOVAUX,
            B.MOVBCO
        FROM 
            (SELECT 
                BANCO,
                CUENTA,
                DECODE(TIPO, '1', '1', '5', '1') AS TIPON,
                FECHA,
                IMPORTE,
                COUNT(*) AS C1,
                MAX(MOVAUX) AS MOVAUX,
                MAX(TIPO) AS TIPO
             FROM DGI.MOVAUX
             WHERE 
                BANCO = {ii_banco}
                AND CUENTA = {ii_cuenta}
                AND STATUS IS NULL
                AND TIPO IN ('1', '5')
                AND CONCEPTO IS NOT NULL
                AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
             GROUP BY 
                BANCO, CUENTA, DECODE(TIPO, '1', '1', '5', '1'), FECHA, IMPORTE) A
        FULL OUTER JOIN 
            (SELECT 
                BANCO,
                CUENTA,
                TIPO,
                FECHA,
                IMPORTE,
                COUNT(*) AS C2,
                MAX(MOVBCO) AS MOVBCO
             FROM DGI.MOVBANCO
             WHERE 
                BANCO = {ii_banco}
                AND CUENTA = {ii_cuenta}
                AND STATUS IS NULL
                AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
                AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                AND (CANCEL <> 'C' OR CANCEL IS NULL)
                AND TIPO = '1'
             GROUP BY 
                BANCO, CUENTA, TIPO, FECHA, IMPORTE) B
        ON 
            A.BANCO(+) = B.BANCO
            AND A.CUENTA(+) = B.CUENTA
            AND A.IMPORTE(+) = B.IMPORTE
            AND A.FECHA(+) = B.FECHA
        WHERE 
            B.BANCO = {ii_banco}
            AND B.CUENTA = {ii_cuenta}
    ";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                System.Diagnostics.Debug.WriteLine("ERROR AL CREAR LA TABLA CARGOS: " + oRes.Message);
            }
        }


        public async Task CrearTablaAbonosAsync(int ii_banco, int ii_cuenta, DateTime id_vigenciaaux, DateTime Idt_Fecha, DateTime id_vigenciabco)
        {
            string fechaVigenciaAuxStr = id_vigenciaaux.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");
            string fechaVigenciaBcoStr = id_vigenciabco.ToString("dd/MM/yyyy");

            var oReq = new eRequest();
            oReq.Tipo = 1;

            oReq.Query = $@"
        CREATE TABLE is_tabla_abonos AS 
        SELECT 
            DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
            DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
            DECODE(A.C1, NULL, 0, A.C1) AS C1,
            DECODE(B.C2, NULL, 0, B.C2) AS C2,
            DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
            DECODE(A.REFERENCIA, NULL, DECODE(B.REFERENCIA, NULL, A.REFERENCIA, B.REFERENCIA), A.REFERENCIA) AS REFERENCIA,
            (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
            A.MOVAUX,
            B.MOVBCO
        FROM (
            SELECT 
                BANCO, CUENTA, TIPO AS TIPON, TO_CHAR(REFERENCIA) AS REFERENCIA, IMPORTE, 
                COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX, MAX(TIPO) AS TIPO
            FROM DGI.MOVAUX
            WHERE 
                BANCO = {ii_banco} AND CUENTA = {ii_cuenta} AND STATUS IS NULL AND 
                TIPO IN ('2', '6') AND 
                FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY') AND 
                FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
            GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
        ) A,
        (
            SELECT 
                BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                COUNT(*) AS C2, MAX(MOVBCO) AS MOVBCO
            FROM DGI.MOVBANCO
            WHERE 
                BANCO = {ii_banco} AND CUENTA = {ii_cuenta} AND STATUS IS NULL AND 
                FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY') AND 
                FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY') AND 
                (CANCEL <> 'C' OR CANCEL IS NULL) AND TIPO = '2'
            GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
        ) B
        WHERE 
            A.BANCO(+) = B.BANCO AND 
            A.CUENTA(+) = B.CUENTA AND 
            A.IMPORTE(+) = B.IMPORTE AND 
            A.REFERENCIA(+) = B.REFERENCIA AND 
            A.BANCO = {ii_banco} AND 
            A.CUENTA = {ii_cuenta}
        UNION
        SELECT 
            DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
            DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
            DECODE(A.C1, NULL, 0, A.C1) AS C1,
            DECODE(B.C2, NULL, 0, B.C2) AS C2,
            DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
            DECODE(A.REFERENCIA, NULL, DECODE(B.REFERENCIA, NULL, A.REFERENCIA, B.REFERENCIA), A.REFERENCIA) AS REFERENCIA,
            (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
            A.MOVAUX,
            B.MOVBCO
        FROM (
            SELECT 
                BANCO, CUENTA, TIPO AS TIPON, TO_CHAR(REFERENCIA) AS REFERENCIA, IMPORTE, 
                COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX, MAX(TIPO) AS TIPO
            FROM DGI.MOVAUX
            WHERE 
                BANCO = {ii_banco} AND CUENTA = {ii_cuenta} AND STATUS IS NULL AND 
                TIPO IN ('2', '6') AND 
                FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY') AND 
                FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
            GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
        ) A,
        (
            SELECT 
                BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                COUNT(*) AS C2, MAX(MOVBCO) AS MOVBCO
            FROM DGI.MOVBANCO
            WHERE 
                BANCO = {ii_banco} AND CUENTA = {ii_cuenta} AND STATUS IS NULL AND 
                FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY') AND 
                FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY') AND 
                (CANCEL <> 'C' OR CANCEL IS NULL) AND TIPO = '2'
            GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
        ) B
        WHERE 
            A.BANCO = {ii_banco} AND 
            A.CUENTA = {ii_cuenta} AND 
            A.BANCO = B.BANCO(+) AND 
            A.CUENTA = B.CUENTA(+) AND 
            A.IMPORTE = B.IMPORTE(+) AND 
            A.REFERENCIA = B.REFERENCIA(+)
    ";

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                Console.WriteLine($"ERROR AL CREAR LA TABLA ABONOS: {oRes.Message}");
            }
        }


        public async Task CrearTablaCargosAuxAsync(int ii_Banco, int ii_Cuenta, DateTime id_vigenciaaux, DateTime Idt_Fecha)
        {
            string fechaVigenciaAuxStr = id_vigenciaaux.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");

            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"
            CREATE TABLE is_tabla_cargos_aux AS
            SELECT 
                DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
                DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
                DECODE(A.FECHA, NULL, DECODE(B.FECHA, NULL, A.FECHA, B.FECHA), A.FECHA) AS FECHA,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVAUX AS MOVAUX1,
                B.MOVAUX AS MOVAUX2
            FROM (
                SELECT BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                       COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '1' 
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE
            ) A,
            (
                SELECT BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                       COUNT(*) AS C2, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '3'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE
            ) B
            WHERE A.BANCO(+) = B.BANCO 
              AND A.CUENTA(+) = B.CUENTA 
              AND A.IMPORTE(+) = B.IMPORTE 
              AND A.FECHA(+) = B.FECHA
              AND B.BANCO = {ii_Banco} 
              AND B.CUENTA = {ii_Cuenta}
            UNION
            SELECT 
                DECODE(B.BANCO, NULL, DECODE(A.BANCO, NULL, B.BANCO, A.BANCO), B.BANCO) AS BANCO,
                DECODE(B.CUENTA, NULL, DECODE(A.CUENTA, NULL, B.CUENTA, A.CUENTA), B.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(B.IMPORTE, NULL, DECODE(A.IMPORTE, NULL, B.IMPORTE, A.IMPORTE), B.IMPORTE) AS IMPORTE,
                DECODE(A.FECHA, NULL, DECODE(B.FECHA, NULL, A.FECHA, B.FECHA), A.FECHA) AS FECHA,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVAUX AS MOVAUX1,
                B.MOVAUX AS MOVAUX2
            FROM (
                SELECT BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                       COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '1'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE
            ) A,
            (
                SELECT BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                       COUNT(*) AS C2, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '3'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE
            ) B
            WHERE A.BANCO = {ii_Banco}
              AND A.CUENTA = {ii_Cuenta}
              AND A.BANCO = B.BANCO(+) 
              AND A.CUENTA = B.CUENTA(+) 
              AND A.IMPORTE = B.IMPORTE(+) 
              AND A.FECHA = B.FECHA(+)
        "
            };

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                Console.WriteLine($"ERROR AL CREAR LA TABLA CARGOS AUX: {oRes.Message}");
            }
        }

        public async Task CrearTablaAbonosAuxAsync(int ii_Banco, int ii_Cuenta, DateTime id_vigenciaaux, DateTime Idt_Fecha)
        {
            string fechaVigenciaAuxStr = id_vigenciaaux.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");

            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"
            CREATE TABLE is_tabla_abonos_aux AS
            SELECT 
                DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
                DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(A.REFERENCIA, NULL, DECODE(B.REFERENCIA, NULL, A.REFERENCIA, B.REFERENCIA), A.REFERENCIA) AS REFERENCIA,
                DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVAUX AS MOVAUX1,
                B.MOVAUX AS MOVAUX2
            FROM (
                SELECT BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                       COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '2'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
            ) A,
            (
                SELECT BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                       COUNT(*) AS C2, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '4'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
            ) B
            WHERE B.BANCO = {ii_Banco}
              AND B.CUENTA = {ii_Cuenta}
              AND A.BANCO(+) = B.BANCO
              AND A.CUENTA(+) = B.CUENTA
              AND A.IMPORTE(+) = B.IMPORTE
              AND A.REFERENCIA(+) = B.REFERENCIA
            UNION
            SELECT 
                DECODE(B.BANCO, NULL, DECODE(A.BANCO, NULL, B.BANCO, A.BANCO), B.BANCO) AS BANCO,
                DECODE(B.CUENTA, NULL, DECODE(A.CUENTA, NULL, B.CUENTA, A.CUENTA), B.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(A.REFERENCIA, NULL, DECODE(B.REFERENCIA, NULL, A.REFERENCIA, B.REFERENCIA), A.REFERENCIA) AS REFERENCIA,
                DECODE(B.IMPORTE, NULL, DECODE(A.IMPORTE, NULL, B.IMPORTE, A.IMPORTE), B.IMPORTE) AS IMPORTE,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVAUX AS MOVAUX1,
                B.MOVAUX AS MOVAUX2
            FROM (
                SELECT BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                       COUNT(*) AS C1, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '2'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
            ) A,
            (
                SELECT BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE, 
                       COUNT(*) AS C2, MAX(MOVAUX) AS MOVAUX
                FROM DGI.MOVAUX
                WHERE BANCO = {ii_Banco} AND CUENTA = {ii_Cuenta} 
                  AND STATUS IS NULL AND TIPO = '4'
                  AND FECHA >= TO_DATE('{fechaVigenciaAuxStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, REFERENCIA, IMPORTE
            ) B
            WHERE A.BANCO = {ii_Banco}
              AND A.CUENTA = {ii_Cuenta}
              AND A.BANCO = B.BANCO(+)
              AND A.CUENTA = B.CUENTA(+)
              AND A.IMPORTE = B.IMPORTE(+)
              AND A.REFERENCIA = B.REFERENCIA(+)
        "
            };

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                Console.WriteLine($"ERROR AL CREAR LA TABLA ABONOS AUX: {oRes.Message}");
            }
        }


        public async Task CrearTablaCargosBcoAsync(int ii_Banco, int ii_Cuenta, DateTime id_vigenciabco, DateTime Idt_Fecha)
        {
            string fechaVigenciaBcoStr = id_vigenciabco.ToString("dd/MM/yyyy");
            string fechaStr = Idt_Fecha.ToString("dd/MM/yyyy");

            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"
            CREATE TABLE is_tabla_cargos_bco AS
            SELECT 
                DECODE(A.BANCO, NULL, DECODE(B.BANCO, NULL, A.BANCO, B.BANCO), A.BANCO) AS BANCO,
                DECODE(A.CUENTA, NULL, DECODE(B.CUENTA, NULL, A.CUENTA, B.CUENTA), A.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(A.IMPORTE, NULL, DECODE(B.IMPORTE, NULL, A.IMPORTE, B.IMPORTE), A.IMPORTE) AS IMPORTE,
                A.TIPO AS TIPO1,
                B.TIPO AS TIPO2,
                A.CANCEL AS CANCEL1,
                B.CANCEL AS CANCEL2,
                DECODE(A.FECHA, NULL, DECODE(B.FECHA, NULL, A.FECHA, B.FECHA), A.FECHA) AS FECHA,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVBCO AS MOVBCO1,
                B.MOVBCO AS MOVBCO2
            FROM (
                SELECT 
                    BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                    COUNT(*) AS C1, MAX(MOVBCO) AS MOVBCO, CANCEL
                FROM DGI.MOVBANCO
                WHERE TIPO = '1' 
                  AND (CANCEL <> 'C' OR CANCEL IS NULL)
                  AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                  AND STATUS IS NULL
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE, CANCEL
            ) A,
            (
                SELECT 
                    BANCO, CUENTA, TIPO, IMPORTE, MAX(FECHA) AS FECHA, COUNT(*) AS C2, 
                    MAX(MOVBCO) AS MOVBCO, CANCEL
                FROM DGI.MOVBANCO
                WHERE TIPO = '2' 
                  AND CANCEL = 'C'
                  AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                  AND STATUS IS NULL
                GROUP BY BANCO, CUENTA, TIPO, IMPORTE, CANCEL
            ) B
            WHERE B.BANCO = {ii_Banco}
              AND B.CUENTA = {ii_Cuenta}
              AND A.BANCO(+) = B.BANCO
              AND A.CUENTA(+) = B.CUENTA
              AND A.IMPORTE(+) = B.IMPORTE
            UNION
            SELECT 
                DECODE(B.BANCO, NULL, DECODE(A.BANCO, NULL, B.BANCO, A.BANCO), B.BANCO) AS BANCO,
                DECODE(B.CUENTA, NULL, DECODE(A.CUENTA, NULL, B.CUENTA, A.CUENTA), B.CUENTA) AS CUENTA,
                DECODE(A.C1, NULL, 0, A.C1) AS C1,
                DECODE(B.C2, NULL, 0, B.C2) AS C2,
                DECODE(B.IMPORTE, NULL, DECODE(A.IMPORTE, NULL, B.IMPORTE, A.IMPORTE), B.IMPORTE) AS IMPORTE,
                A.TIPO AS TIPO1,
                B.TIPO AS TIPO2,
                A.CANCEL AS CANCEL1,
                B.CANCEL AS CANCEL2,
                DECODE(A.FECHA, NULL, DECODE(B.FECHA, NULL, A.FECHA, B.FECHA), A.FECHA) AS FECHA,
                (DECODE(A.C1, NULL, 0, A.C1) - DECODE(B.C2, NULL, 0, B.C2)) AS DIF,
                A.MOVBCO AS MOVBCO1,
                B.MOVBCO AS MOVBCO2
            FROM (
                SELECT 
                    BANCO, CUENTA, TIPO, FECHA, IMPORTE, 
                    COUNT(*) AS C1, MAX(MOVBCO) AS MOVBCO, CANCEL
                FROM DGI.MOVBANCO
                WHERE TIPO = '1'
                  AND (CANCEL <> 'C' OR CANCEL IS NULL)
                  AND STATUS IS NULL
                  AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, FECHA, IMPORTE, CANCEL
            ) A,
            (
                SELECT 
                    BANCO, CUENTA, TIPO, IMPORTE, MAX(FECHA) AS FECHA, COUNT(*) AS C2,
                    MAX(MOVBCO) AS MOVBCO, CANCEL
                FROM DGI.MOVBANCO
                WHERE TIPO = '2'
                  AND CANCEL = 'C'
                  AND STATUS IS NULL
                  AND FECHA >= TO_DATE('{fechaVigenciaBcoStr}', 'DD/MM/YYYY')
                  AND FECHA <= TO_DATE('{fechaStr}', 'DD/MM/YYYY')
                GROUP BY BANCO, CUENTA, TIPO, IMPORTE, CANCEL
            ) B
            WHERE A.BANCO = {ii_Banco}
              AND A.CUENTA = {ii_Cuenta}
              AND A.BANCO = B.BANCO(+)
              AND A.CUENTA = B.CUENTA(+)
              AND A.IMPORTE = B.IMPORTE(+)
        "
            };

            var oRes = await WSServicio.Servicio(oReq);

            if (oRes.Err != 0)
            {
                Console.WriteLine($"ERROR AL CREAR LA TABLA CARGOS BCO: {oRes.Message}");
            }
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
