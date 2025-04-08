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
    public partial class FrmDesconciliacionAutomatica : FormaGenBar
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


        public FrmDesconciliacionAutomatica()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Desconciliación Automática";

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


        private async void detcbDesconciliar(object sender, EventArgs e)
        {
            bool bExito = await detValida();

            if (!bExito)
               return;

            await _Main.Status($"¿Deseas Continuar?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas Continuar?", "Desconciliar", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string Ii_Bco = comboBanco.SelectedValue?.ToString();
            string Ii_Cta = comboCuenta.SelectedValue?.ToString();
            string mes = comboMes.SelectedValue?.ToString();
            string anio = txtAnio.Text;
            int resultado1 = await UeDesconciliar(Ii_Bco, Ii_Cta, DateTime.Now);
            if (resultado1 == 1)
            {

                int resultado2 = await UeDatosDescon(Ii_Bco, Ii_Cta, DateTime.Now);

                if (resultado2 == 1)
                {
                    int resultado3 = await UeDatosDescon(Ii_Bco, Ii_Cta, DateTime.Now);

                    if (resultado3 == 1)
                    {
                        int resultado4 = await UeDatosDescon(Ii_Bco, Ii_Cta, DateTime.Now);

                        if (resultado4 == 1)
                        {
                            int resultado5 = await UeAceptaDescon(Ii_Bco, Ii_Cta);

                            if (resultado5 == 1)
                            {
                                int resultado6 = await UeDepurada(Ii_Bco, Ii_Cta, 1);

                                if (resultado6 == 1)
                                {
                                    int resultado7 = await UeRegistrada(Ii_Bco, Ii_Cta, 1);

                                    if (resultado7 == 1)
                                    {
                                        
                                    }
                                }

                            }



                            

                        }
                    }

                    

                    
                }


               

                


            }

        }


        public async Task<int> UeDesconciliar(string iiBanco, string iiCuenta, DateTime idtFecha)
        {
            DateTime? ldtFechacon = null;
            DateTime ldFecha = idtFecha.Date;

            oReq.Tipo = 0;
            oReq.Query = $@"
            SELECT FECHA_CON
            FROM DGI.CONCILIACION
            WHERE BANCO = {iiBanco} 
              AND CUENTA = {iiCuenta} 
              AND FECHA = TO_DATE('{ldFecha:yyyy-MM-dd}', 'yyyy-MM-dd')";

            var oResProcesa = await WSServicio.Servicio(oReq);

            if (oResProcesa.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error al buscar FECHA_CON: " + oResProcesa.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oResProcesa.Data.Tables.Count > 0 && oResProcesa.Data.Tables[0].Rows.Count > 0)
            {
                var row = oResProcesa.Data.Tables[0].Rows[0];
                ldtFechacon = row["FECHA_CON"] != DBNull.Value ? Convert.ToDateTime(row["FECHA_CON"]) : (DateTime?)null;
            }

            if (ldtFechacon == null && oResProcesa.Err != 100)
            {
                MessageBoxMX.ShowDialog(null, "La cuenta no está conciliada!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }

        public async Task<int> UeDatosDescon(string iiBanco, string iiCuenta, DateTime idtFecha)
        {
            int liCont = 0;
            double ldConciliacion = 0;
            DateTime idInicial = FProFechaIniMes(idtFecha.Date);
            DateTime idFinal = idtFecha.Date;

            // Consulta para obtener la conciliación
            var oReqConciliacion = new eRequest
            {
                Tipo = 0,
                Query = $@"
                SELECT CONCILIACION
                FROM DGI.CONCILIACION
                WHERE BANCO = {iiBanco} 
                  AND CUENTA = {iiCuenta} 
                  AND FECHA = TO_DATE('{idtFecha:yyyy-MM-dd}', 'yyyy-MM-dd')
            "
            };

            var oResConciliacion = await WSServicio.Servicio(oReqConciliacion);
            if (oResConciliacion.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error en conciliación: " + oResConciliacion.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oResConciliacion.Data.Tables.Count > 0 && oResConciliacion.Data.Tables[0].Rows.Count > 0)
            {
                ldConciliacion = Convert.ToDouble(oResConciliacion.Data.Tables[0].Rows[0]["CONCILIACION"]);
            }

            // Arreglo para almacenar cargas
            List<long> arrCarga = new List<long>();

            // Cursor para obtener cargas
            var oReqCargas = new eRequest
            {
                Tipo = 0,
                Query = $@"
                SELECT CARGA
                FROM DGI.CARGABCO
                WHERE PERIODO = TO_DATE('{idtFecha:yyyy-MM-dd}', 'yyyy-MM-dd')
                  AND BANCO = {iiBanco} 
                  AND CUENTA = {iiCuenta}
            "
            };

            var oResCargas = await WSServicio.Servicio(oReqCargas);
            if (oResCargas.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error en cargas: " + oResCargas.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oResCargas.Data.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in oResCargas.Data.Tables[0].Rows)
                {
                    arrCarga.Add(Convert.ToInt64(row["CARGA"]));
                    liCont++;
                }
            }

            // Llamados a procedimientos almacenados
            if (!await EjecutarProcedimiento("p_des_con", iiBanco, iiCuenta, ldConciliacion)) return 0;
            if (!await EjecutarProcedimiento("p_des_con_movbco", iiBanco, iiCuenta, ldConciliacion)) return 0;
            if (!await EjecutarProcedimiento("p_des_con_matchbco", iiBanco, iiCuenta)) return 0;
            if (!await EjecutarProcedimiento("p_des_con_matchaux", iiBanco, iiCuenta)) return 0;

            // Iterar sobre arrCarga y ejecutar el procedimiento para cada carga
            foreach (var carga in arrCarga)
            {
                if (!await EjecutarProcedimientoMov("p_des_con_mov", iiBanco, iiCuenta, idInicial, idFinal, carga, ldConciliacion))
                    return 0;
            }

            return 1;
        }

        private DateTime FProFechaIniMes(DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }

        private async Task<bool> EjecutarProcedimientoMov(string procedimiento, string banco, string cuenta, DateTime idInicial, DateTime idFinal, long carga, double conciliacion)
        {
            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"
                CALL {procedimiento}(
                    {banco}, 
                    {cuenta}, 
                    TO_DATE('{idInicial:yyyy-MM-dd HH:mm:ss}', 'yyyy-MM-dd HH24:mi:ss'),
                    TO_DATE('{idFinal:yyyy-MM-dd HH:mm:ss}', 'yyyy-MM-dd HH24:mi:ss'),
                    {carga}, 
                    {conciliacion}
                )
            "
            };

            var oRes = await WSServicio.Servicio(oReq);
            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, $"{procedimiento.ToUpper()} Error: " + oRes.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return false;
            }

            return true;
        }

        private async Task<bool> EjecutarProcedimiento(string procedimiento, string banco, string cuenta, double? conciliacion = null)
        {
            var oReq = new eRequest
            {
                Tipo = 1,
                Query = conciliacion.HasValue
                    ? $@"CALL {procedimiento}({banco}, {cuenta}, {conciliacion.Value})"
                    : $@"CALL {procedimiento}({banco}, {cuenta})"
            };

            var oRes = await WSServicio.Servicio(oReq);
            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, $"{procedimiento.ToUpper()} Error: " + oRes.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return false;
            }

            return true;
        }


        public async Task<int> UeAceptaDescon(string iiBanco, string iiCuenta)
        {
            // Llamada al procedimiento almacenado p_des_con_match
            var oReq = new eRequest
            {
                Tipo = 1,
                Query = $@"CALL P_DES_CON_MATCH({iiBanco}, {iiCuenta})"
            };

            var oRes = await WSServicio.Servicio(oReq);
            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error en P_DES_CON_MATCH: " + oRes.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            return 1;
        }

        public async Task<int> UeDepurada(string iiBanco, string iiCuenta, double ilConciliacion)
        {
            DateTime? ldtFechaDep = null;

            // Consulta para obtener la fecha de depuración
            var oReq = new eRequest
            {
                Tipo = 0,
                Query = $@"
                SELECT FECHA_DEP
                FROM DGI.CONCILIACION
                WHERE BANCO = {iiBanco} 
                  AND CUENTA = {iiCuenta} 
                  AND CONCILIACION = {ilConciliacion}
            "
            };

            var oRes = await WSServicio.Servicio(oReq);
            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error al buscar FECHA_DEP: " + oRes.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                ldtFechaDep = Convert.ToDateTime(oRes.Data.Tables[0].Rows[0]["FECHA_DEP"]);
            }

            if (ldtFechaDep.HasValue)
            {
                MessageBoxMX.ShowDialog(null, "Esta cuenta ya fue depurada!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
        }

        public async Task<int> UeRegistrada(string iiBanco, string iiCuenta, double ilConciliacion)
        {
            DateTime? ldtFechaAut = null;

            // Consulta para obtener la fecha de autorización
            var oReq = new eRequest
            {
                Tipo = 0,
                Query = $@"
                SELECT FECHA_AUT
                FROM DGI.CONCILIACION
                WHERE BANCO = {iiBanco} 
                  AND CUENTA = {iiCuenta} 
                  AND CONCILIACION = {ilConciliacion}
            "
            };

            var oRes = await WSServicio.Servicio(oReq);
            if (oRes.Err != 0)
            {
                MessageBoxMX.ShowDialog(null, "Error al buscar FECHA_AUT: " + oRes.Message, "Información", (int)StatusColorsTypes.Danger, false);
                return 0;
            }

            if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
            {
                ldtFechaAut = Convert.ToDateTime(oRes.Data.Tables[0].Rows[0]["FECHA_AUT"]);
            }

            if (ldtFechaAut.HasValue)
            {
                MessageBoxMX.ShowDialog(null, "Imposible Desconciliar Ya que se han Registrado Ajustes para esta cuenta!", "Aviso", (int)StatusColorsTypes.Warning, false);
                return 0;
            }

            return 1;
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
