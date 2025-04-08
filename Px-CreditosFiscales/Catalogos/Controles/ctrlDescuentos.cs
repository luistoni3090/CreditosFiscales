using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_CreditosFiscales.Utiles.Generales;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;

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
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Globalization;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

namespace Px_CreditosFiscales.Catalogos.Controles
{
    public partial class ctrlDescuentos : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eDESCUENTOS _Reg = new eDESCUENTOS();
        eINCISO _Reg2 = new eINCISO();

        #region Constuctores
        public ctrlDescuentos()
        {
            InitializeComponent();
        }

        public ctrlDescuentos(xMain oMain, string CONSEC, string valida, string DESCRIPCION, string TIPO)
        {

            InitializeComponent();

            _Main = oMain;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.CONSEC = CONSEC;
            _Reg.DESCR = DESCRIPCION;
            txtValida.Text = valida;
            txtMaxId.Text = CONSEC;
            detActualiza(CONSEC,DESCRIPCION, TIPO);
        }


        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eDESCUENTOS();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task cargaComboTipoCredito()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT TIPO_CREDITO, DESCR FROM TIPO_CREDITO";
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
                            Value = row["TIPO_CREDITO"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboTipoCredito.DataSource = comboList;
                    comboTipoCredito.DisplayMember = "Text";
                    comboTipoCredito.ValueMember = "Value";
                    comboTipoCredito.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }



        private async Task detActualiza(string CONSEC, string DESCRIPCION, string TIPO)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            txtMaxId.Visible = false;
            await detNuevo();
            await detBusca(CONSEC, TIPO);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {CONSEC}-{DESCRIPCION.Trim()} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca(_Reg.CONSEC, _Reg.TIPO);
        }


        private async Task detBusca(string CONSEC, string TIPO)
        {

            await cargaComboTipoCredito();

            if (txtValida.Text == "Nuevo")
            {

                await limpiaCampos();
                return;
            }
            else
            {
                try
                {
                    oReq.Tipo = 0;
                    oReq.Query = @" SELECT 
                                        CONSEC,
                                        TIPO,
                                        DESCR,
                                        TO_CHAR(VIGENCIA_INI, 'YYYY-MM-DD') AS VIGENCIA_INI,
                                        TO_CHAR(VIGENCIA_FIN, 'YYYY-MM-DD') AS VIGENCIA_FIN
                                    FROM TIPO_DESCTO
                                    WHERE CONSEC = :CONSEC AND TIPO = :TIPO";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "CONSEC", Valor = CONSEC},
                    new eParametro(){ Tipo = DbType.String, Nombre = "TIPO", Valor = TIPO}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eDESCUENTOS>();

                    txtValida.Text = "Editar";
                    await detPresenta();

                }
                catch (Exception ex)
                {
                    //await JS.Notify($"Error", $"{ex.Message}", Gen.MensajeToastTipo.error, 1);
                }
            }


        }

        /// <summary>
        /// Borro el registro en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task detBorra(TabPage tabPage)
        {

            if (txtValida.Text == "Nuevo") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            _Reg.CONSEC = txtMaxId.Text;

            Cursor = Cursors.AppStarting;
            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE TIPO_DESCTO
                                WHERE CONSEC = {_Reg.CONSEC} AND TIPO = '{_Reg.TIPO}'";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    txtMaxId.Text = "0";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = $"000";
                    await limpiaCampos();
                    await _Main.Status($"Registro eliminado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);
                }

            }

            Cursor = Cursors.Default;
        }


        /// <summary>
        /// Guardo en la base de datos los datos capturados
        /// Insero o actualizo
        /// </summary>
        /// <returns></returns>
        public async Task detGuarda(TabPage tabPage)
        {
            bool bExito = await detValida();

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO TIPO_DESCTO
                                    (
                                        CONSEC,
                                        TIPO,
                                        DESCR,
                                        VIGENCIA_INI,
                                        VIGENCIA_FIN
                                        )
                                VALUES
                                        ((SELECT NVL(MAX(CONSEC), 0) + 1 FROM DESCUENTO),
                                        '{_Reg.TIPO}',
                                        '{_Reg.DESCR}',
                                        TO_DATE('{_Reg.VIGENCIA_INI}','YYYY-MM-DD'),
                                        TO_DATE('{_Reg.VIGENCIA_FIN}','YYYY-MM-DD')
                                        )
                                ";
            }
            else
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE TIPO_DESCTO
                                    SET 
                                        TIPO = '{_Reg.TIPO}',
                                        DESCR = '{_Reg.DESCR}',
                                        VIGENCIA_INI = TO_DATE('{_Reg.VIGENCIA_INI}','YYYY-MM-DD'),
                                        VIGENCIA_FIN = TO_DATE('{_Reg.VIGENCIA_FIN}','YYYY-MM-DD')
                                WHERE CONSEC = {_Reg.CONSEC}
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    
                    tabPage.Text = "               "+_Reg.DESCR.Trim();
                    tabPage.Name = $"{_Reg.CONSEC}{_Reg.TIPO}{_Reg.DESCR.Trim()}";

                    if (txtValida.Text == "Nuevo")
                    {
                        oReq.Tipo = 0;
                        oReq.Query = $@"SELECT MAX(CONSEC) AS CONSEC FROM TIPO_DESCTO";
                        var oRes2 = await WSServicio.Servicio(oReq);

                        if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                        {
                            int maxId = Convert.ToInt32(oRes2.Data.Tables[0].Rows[0][0]);
                            txtMaxId.Text = (maxId.ToString());
                            _Reg.CONSEC = (maxId.ToString());
                            tabPage.Name = $"{txtMaxId.Text}{_Reg.TIPO}{_Reg.DESCR.Trim()}";
                        }
                    }

                    txtValida.Text = "Editar";

                    await _Main.Status($"Registro {_Reg.DESCR} guardado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);
                }
            }
            Cursor = Cursors.Default;
        }


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro()
        {
            var existe = false;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT CONSEC, TIPO, DESCR
                                FROM TIPO_DESCTO WHERE CONSEC = " + _Reg.CONSEC + " AND TIPO = '" + _Reg.TIPO + "' ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                    existe = true;

                return existe;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");
                return existe;
            }

        }
        #endregion


        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                sErr += "Debes de ingresar la Descripción. ";
            errorProvider1.SetError(txtDescripcion, "Debes de ingresar la Descripción");


            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                _Reg.CONSEC = txtMaxId.Text;
                _Reg.TIPO = comboTipoCredito.SelectedValue.ToString();
                _Reg.DESCR = txtDescripcion.Text.Trim();
                DateTime vigenciaInicial = txtVigenciaInicial.Value;
                DateTime vigenciaFinal = txtVigenciaFinal.Value;
                _Reg.VIGENCIA_INI = vigenciaInicial.ToString("yyyy-MM-dd");
                _Reg.VIGENCIA_FIN = vigenciaFinal.ToString("yyyy-MM-dd");


            }

            return bExito;
        }


        private async Task detPresenta()
        {
            comboTipoCredito.SelectedValue = _Reg.TIPO.ToString();
            txtDescripcion.Text = _Reg.DESCR.Trim();
            try
            {
                DateTime fechaInicialConvertida = DateTime.ParseExact(_Reg.VIGENCIA_INI, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                txtVigenciaInicial.Value = fechaInicialConvertida;
                DateTime fechaFinalConvertida = DateTime.ParseExact(_Reg.VIGENCIA_FIN, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                txtVigenciaFinal.Value = fechaFinalConvertida;
            }
            catch (FormatException)
            {
                
            }

        }

        #endregion

        private async Task limpiaCampos()
        {
            DateTime vigenciaInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime vigenciaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            txtVigenciaInicial.Value = vigenciaInicial;
            txtVigenciaFinal.Value = vigenciaFinal;
            txtDescripcion.Text = "";
            comboTipoCredito.SelectedIndex = 0;

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

        private void rxrPosiciones_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
