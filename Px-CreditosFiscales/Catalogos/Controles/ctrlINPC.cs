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
using System.Text.RegularExpressions;

namespace Px_CreditosFiscales.Catalogos.Controles
{
    public partial class ctrlINPC : UserControl
    { 
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eINPC _Reg = new eINPC();

        #region Constuctores
        public ctrlINPC()
        {
            InitializeComponent();
            Inicio();
        }
        public ctrlINPC(xMain oMain, string ANIO, string MES, string valida)
        {

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.ANIO = ANIO;
            _Reg.MES = MES;
            txtValida.Text = valida;
            txtAnioOrigen.Text = _Reg.ANIO;
            txtMesOrigen.Text = _Reg.MES;
            detActualiza(ANIO, MES);
        }

        private async Task Inicio()
        {


        }
        #endregion


        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eINPC();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string ANIO, string MES)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            txtAnioOrigen.Visible = false;
            txtMesOrigen.Visible = false;
            txtFechaPublicacion.ValueChanged += new EventHandler(dateTimePicker1_CheckedChanged);
            await detNuevo();
            await detBusca(ANIO, MES);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {ANIO}-{MES} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca(_Reg.ANIO, _Reg.MES);
        }


        private async Task detBusca(string ANIO, string MES)
        {

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
                    oReq.Query = @"SELECT
                                        ANIO,
                                        MES,
                                        FACTOR,
                                        TO_CHAR(FECHA_PUBLICACION, 'YYYY-MM-DD') AS FECHA_PUBLICACION
                                    FROM INPC
                                    WHERE ANIO = :ANIO  AND MES = :MES ";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "ANIO", Valor = ANIO},
                    new eParametro(){ Tipo = DbType.String, Nombre = "MES", Valor = MES}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eINPC>();

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
            await _Main.Status($"¿Deseas borrar el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}??", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            string AniOrigen = txtAnioOrigen.Text.Trim();
            string MesOrigen = txtMesOrigen.Text.Trim();


            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE INPC
                                WHERE ANIO = {AniOrigen} AND MES = {MesOrigen} ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = "00";
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

                bool existe = await conValidaSiExisteRegistro(_Reg.ANIO, _Reg.MES);

                if (existe)
                {

                    await _Main.Status($"El AÑO {_Reg.ANIO}, el MES {_Reg.MES} ya estan en uso", (int)MensajeTipo.Error);
                    return;
                }

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con AÑO {_Reg.ANIO} y MES {_Reg.MES}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con AÑO {_Reg.ANIO} y MES {_Reg.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO INPC
                                    (
                                        ANIO,
                                        MES,
                                        FACTOR,
                                        FECHA_PUBLICACION
                                        )
                                VALUES
                                        (
                                        {_Reg.ANIO},
                                        {_Reg.MES},
                                        {_Reg.FACTOR},
                                        TO_DATE('{_Reg.FECHA_PUBLICACION}','YYYY-MM-DD')
                                        )
                                ";
            }
            else
            {
                string AniOrigen = txtAnioOrigen.Text.Trim();
                string MesOrigen = txtMesOrigen.Text.Trim();

                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE INPC
                                    SET 
                                        ANIO = {_Reg.ANIO},
                                        MES = {_Reg.MES},
                                        FACTOR = {_Reg.FACTOR},
                                        FECHA_PUBLICACION = TO_DATE('{_Reg.FECHA_PUBLICACION}', 'YYYY-MM-DD')
                                WHERE ANIO = {AniOrigen} AND MES = {MesOrigen} ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    tabPage.Text = "               " + _Reg.ANIO + "-" + _Reg.MES;
                    tabPage.Name = $"{_Reg.ANIO}{_Reg.MES}";

                    txtValida.Text = "Editar";

                    txtAnioOrigen.Text = _Reg.ANIO;
                    txtMesOrigen.Text = _Reg.MES;

                    await _Main.Status($"Registro guardado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);
                }
            }
            Cursor = Cursors.Default;
        }



        /// <summary>
        /// Valido que los campos estén ingresados
        /// </summary>
        /// <returns></returns>
        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtAnio.Text) || !decimal.TryParse(txtAnio.Text, out _))
            {
                sErr += "Debes de ingresar Año, solo se permiten números ";
                errorProvider1.SetError(txtAnio, "Debes de ingresar el Año, solo se permiten números");
            }


            if (string.IsNullOrWhiteSpace(txtMes.Text) || !decimal.TryParse(txtMes.Text, out _))
            {
                sErr += "Debes de ingresar el Mes, solo se permiten números ";
                errorProvider1.SetError(txtMes, "Debes de ingresar el Mes, solo se permiten números");
            }
            else {
                decimal valor = decimal.Parse(txtMes.Text);
                if (valor < 1 || valor > 12)
                {
                    sErr += "Mes, el valor debe estar entre 1 y 12. ";
                    errorProvider1.SetError(txtMes, "Importe, el valor debe estar entre 1 y 12");
                }
            }
            

            if (!string.IsNullOrWhiteSpace(txtFactor.Text))
            {
                    if (!Regex.IsMatch(txtFactor.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$"))
                    {
                        sErr += "Para el Factor, solo se permiten números. ";
                        errorProvider1.SetError(txtFactor, "Para el Factor, solo se permiten números");
                    }
                    else
                    {
                        /*
                        decimal valor = decimal.Parse(txtImporte.Text);
                        if (valor < 0 || valor > 9999999999)
                        {
                            sErr += "Importe, el valor debe estar entre 0 y 9999999999. ";
                            errorProvider1.SetError(txtImporte, "Importe, el valor debe estar entre 0 y 9999999999");
                        }*/
                    }
            }

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                if (txtValida.Text == "Nuevo")
                {
                    _Reg.ANIO = txtAnio.Text.Trim();
                    _Reg.MES = txtMes.Text.Trim();
                    txtAnioOrigen.Text = _Reg.ANIO;
                    txtMesOrigen.Text = _Reg.MES;
                }
                else {
                    _Reg.ANIO = txtAnio.Text.Trim();
                    _Reg.MES = txtMes.Text.Trim();
                }                

                _Reg.FACTOR = txtFactor.Text.Trim() != "" ? txtFactor.Text.Trim() : "null";
                if (txtFechaPublicacion.Text.Trim() != "")
                {
                    DateTime fechaConvertida = DateTime.ParseExact(txtFechaPublicacion.Text, "dd-MM-yyyy", null);
                    _Reg.FECHA_PUBLICACION = fechaConvertida.ToString("yyyy-MM-dd");
                }
                else
                {
                    _Reg.FECHA_PUBLICACION = null;
                }

            }

            return bExito;
        }

        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string ANIO, string MES)
        {
            var existe = false;

            try
            {
                if (txtValida.Text == "Nuevo")
                {

                    oReq.Tipo = 0;
                    oReq.Query = $@"SELECT * FROM INPC WHERE ANIO = {ANIO} AND MES = {MES}";
                }
                else if (txtValida.Text == "Editar")
                {
                    oReq.Tipo = 0;
                    oReq.Query = $@"SELECT * FROM INPC WHERE ( ANIO = {ANIO} AND MES = {MES} ) AND  ( ANIO <> {txtAnioOrigen.Text} OR MES <> {txtMesOrigen.Text} )";
                }
                
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                {
                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
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


        private async Task detPresenta()
        {

            txtAnio.Text = _Reg.ANIO;
            txtMes.Text = _Reg.MES;
            txtFactor.Text = _Reg.FACTOR;
            txtAnioOrigen.Text = _Reg.ANIO;
            txtMesOrigen.Text = _Reg.MES;

            if (_Reg.FECHA_PUBLICACION == "")
            {
                txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
                txtFechaPublicacion.CustomFormat = " ";
                txtFechaPublicacion.ShowCheckBox = true;
                txtFechaPublicacion.Checked = false;
            }
            else
            {


                txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
                DateTime fecha = DateTime.ParseExact(_Reg.FECHA_PUBLICACION, "yyyy-MM-dd", null);
                txtFechaPublicacion.Value = fecha;
                txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
                txtFechaPublicacion.ShowCheckBox = true;
                txtFechaPublicacion.Checked = true;
            }
            
        }
        #endregion

       


        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
            
        //        txtFechaPublicacion.CustomFormat = "dd-MM-yyyy"; // o el formato que desees
            
        //}

        private void dateTimePicker1_CheckedChanged(object sender, EventArgs e)
        {

            if (txtFechaPublicacion.Checked)
            {

                txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
            }
            else
            {
                txtFechaPublicacion.CustomFormat = " ";
            }

        }


        private async Task limpiaCampos()
        {
            
            if (txtValida.Text == "Nuevo")
            {
                txtAnio.Text = "";
                txtMes.Text = "";
                txtFactor.Text = "";

                txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
                txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
                txtFechaPublicacion.ShowCheckBox = true;
                txtFechaPublicacion.Checked = true;
                txtFechaPublicacion.Value = DateTime.Now;

            }
            else { 
            
            }


        }


        private async void ChangeTipoValue(object sender, EventArgs e)
        {
            /*
            string Tipo = comboTipo.SelectedValue.ToString();
            if (Tipo == "F" || Tipo == "E") {
                comboClave.DataSource = null;
                comboClave.Items.Clear();

                await cargaComboClave(Tipo);

            }*/
                
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
