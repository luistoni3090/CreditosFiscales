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
    public partial class ctrlGastosEjecMultas : UserControl
    { 
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eMULTA _Reg = new eMULTA();

        #region Constuctores
        public ctrlGastosEjecMultas()
        {
            InitializeComponent();
            Inicio();
        }
        public ctrlGastosEjecMultas(xMain oMain, string Inferior, string Superior, string Clave, string Tipo, string valida)
        {

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.INFERIOR = Inferior;
            _Reg.SUPERIOR = Superior;
            _Reg.CLAVE = Clave;
            _Reg.TIPO = Tipo;
            txtValida.Text = valida;
            txtLimiteInferiorOrigen.Text = _Reg.INFERIOR;
            txtLimiteSuperiorOrigen.Text = _Reg.SUPERIOR;
            detActualiza(Inferior, Superior,Clave, Tipo);
        }

        private async Task Inicio()
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

            // Asignar la lista al ComboBox
            comboTipo.DataSource = comboList;
            comboTipo.DisplayMember = "Text";
            comboTipo.ValueMember = "Value";


        }

        private async Task cargaComboEquivalente()
        {

            var comboList = new List<ComboBoxItem>();

            var item0 = new ComboBoxItem
            {
                Value = "0",
                Text = ""
            };
            var item1 = new ComboBoxItem
            {
                Value = "P",
                Text = "PESOS"
            };
            var item2 = new ComboBoxItem
            {
                Value = "C",
                Text = "POR CIENTO"
            };
            var item3 = new ComboBoxItem
            {
                Value = "S",
                Text = "SALARIO MINIMO"
            };
            comboList.Add(item0);
            comboList.Add(item1);
            comboList.Add(item2);
            comboList.Add(item3);

            // Asignar la lista al ComboBox
            comboEquivalente.DataSource = comboList;
            comboEquivalente.DisplayMember = "Text";
            comboEquivalente.ValueMember = "Value";
            comboEquivalente.SelectedIndex = 0;

        }

        private async Task cargaComboClave(string Tipo)
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT CLAVE, TIPO, DESCR FROM CPTO WHERE TIPO = '"+Tipo+"' ";
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
                            Value = row["CLAVE"].ToString(),
                            Text = "( "+row["CLAVE"].ToString()+" ) " + "( " + row["TIPO"].ToString() + " ) " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboClave.DataSource = comboList;
                    comboClave.DisplayMember = "Text";
                    comboClave.ValueMember = "Value";

                    if(txtValida.Text == "Editar")
                    comboClave.SelectedValue = _Reg.CLAVE;

                }

            }
            catch (Exception ex)
            { }
        }

        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eMULTA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string Inferior, string Superior, string Clave, string Tipo)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            txtLimiteInferiorOrigen.Visible = false;
            txtLimiteSuperiorOrigen.Visible = false;
            await detNuevo();
            await detBusca(Inferior, Superior, Clave , Tipo);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {Inferior}-{Superior} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca(_Reg.INFERIOR, _Reg.SUPERIOR, _Reg.CLAVE, _Reg.TIPO);
        }


        private async Task detBusca(string Inferior, string Superior, string Clave , string Tipo)
        {

            

            if (txtValida.Text == "Nuevo")
            {
                await cargaComboTipo();
                await cargaComboEquivalente();
                await limpiaCampos();
                return;
            }
            else
            {
                try
                {
                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT INFERIOR, SUPERIOR, CLAVE, TIPO, IMPORTE, EQUIVALENTE FROM MULTA
                                    WHERE INFERIOR = :INFERIOR  AND SUPERIOR = :SUPERIOR AND CLAVE = :CLAVE AND TIPO = :TIPO ";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "INFERIOR", Valor = Inferior},
                    new eParametro(){ Tipo = DbType.String, Nombre = "SUPERIOR", Valor = Superior},
                    new eParametro(){ Tipo = DbType.String, Nombre = "CLAVE", Valor = Clave},
                    new eParametro(){ Tipo = DbType.String, Nombre = "TIPO", Valor = Tipo}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eMULTA>();

                    txtValida.Text = "Editar";
                    comboTipo.Enabled = false;
                    comboClave.Enabled = false;
                    await cargaComboTipo();
                    await cargaComboEquivalente();
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
            await _Main.Status($"¿Deseas borrar el registro {_Reg.INFERIOR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.INFERIOR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.INFERIOR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.INFERIOR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            string InferiorOrigen = txtLimiteInferiorOrigen.Text.Trim();
            string SuperiorOrigen = txtLimiteSuperiorOrigen.Text.Trim();


            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE MULTA
                                WHERE INFERIOR = {InferiorOrigen} AND SUPERIOR = {SuperiorOrigen} AND CLAVE = {_Reg.CLAVE} AND TIPO = '{_Reg.TIPO}'
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = "0000";
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

            if (txtValida.Text == "Nuevo") {

                bool existe = await conValidaSiExisteRegistro(_Reg.INFERIOR, _Reg.SUPERIOR, _Reg.CLAVE, _Reg.TIPO);

                if (existe)
                {

                    await _Main.Status($"El Límite Inferior {_Reg.INFERIOR}, el Límite Superior {_Reg.SUPERIOR}, La Clave {_Reg.CLAVE} y el Tipo {_Reg.TIPO} ya estan en uso", (int)MensajeTipo.Error);
                    return;
                }

            }

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Límite Inferior {_Reg.INFERIOR} y Límite Superior {_Reg.SUPERIOR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Límite Inferior {_Reg.INFERIOR} y Límite Superior {_Reg.SUPERIOR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO MULTA
                                    (
                                        INFERIOR,
                                        SUPERIOR,
                                        CLAVE,
                                        TIPO,
                                        IMPORTE,
                                        EQUIVALENTE
                                        )
                                VALUES
                                        (
                                        {_Reg.INFERIOR},
                                        {_Reg.SUPERIOR},
                                        {_Reg.CLAVE},
                                        '{_Reg.TIPO}',
                                        {_Reg.IMPORTE},
                                        '{_Reg.EQUIVALENTE}'
                                        )
                                ";
            }
            else
            {
                string InferiorOrigen = txtLimiteInferiorOrigen.Text.Trim();
                string SuperiorOrigen = txtLimiteSuperiorOrigen.Text.Trim();

                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE MULTA
                                    SET 
                                        INFERIOR = {_Reg.INFERIOR},
                                        SUPERIOR = {_Reg.SUPERIOR},
                                        CLAVE = {_Reg.CLAVE},
                                        TIPO = '{_Reg.TIPO}',
                                        IMPORTE = {_Reg.IMPORTE},
                                        EQUIVALENTE = '{_Reg.EQUIVALENTE}'
                                WHERE INFERIOR = {InferiorOrigen} AND SUPERIOR = {SuperiorOrigen} AND CLAVE = {_Reg.CLAVE} AND TIPO = '{_Reg.TIPO}'
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    tabPage.Text = "               " + _Reg.INFERIOR + "-" + _Reg.SUPERIOR + "-" + _Reg.CLAVE + "-" + _Reg.TIPO;
                    tabPage.Name = $"{_Reg.INFERIOR}{_Reg.SUPERIOR}{_Reg.CLAVE}{_Reg.TIPO}";

                    txtValida.Text = "Editar";
                    comboTipo.Enabled = false;
                    comboClave.Enabled = false;

                    txtLimiteInferiorOrigen.Text = _Reg.INFERIOR;
                    txtLimiteSuperiorOrigen.Text = _Reg.SUPERIOR;

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

            if (string.IsNullOrWhiteSpace(comboTipo.Text))
                sErr += "Debes seleccionar el Tipo. ";
            errorProvider1.SetError(comboTipo, "Debes seleccionar el Tipo.");

            if (string.IsNullOrWhiteSpace(comboClave.Text))
                sErr += "Debes seleccionar la Clave. ";
            errorProvider1.SetError(comboClave, "Debes de ingresar la Clave.");

            if (string.IsNullOrWhiteSpace(txtLimiteInferior.Text) || !decimal.TryParse(txtLimiteInferior.Text, out _))
                sErr += "Debes de ingresar el Límite Inferior, solo se permiten números ";
            errorProvider1.SetError(txtLimiteInferior, "Debes de ingresar el Límite Inferior, solo se permiten números");

            if (string.IsNullOrWhiteSpace(txtLimiteSuperior.Text) || !decimal.TryParse(txtLimiteSuperior.Text, out _))
            {
                sErr += "Debes de ingresar el Límite Superior, solo se permiten números ";
                errorProvider1.SetError(txtLimiteSuperior, "Debes de ingresar el Límite Superior, solo se permiten números");
            }
            else {
                /*
                if (decimal.TryParse(txtLimiteInferior.Text, out _)) {
                    
                    if (decimal.Parse(txtLimiteInferior.Text) >= decimal.Parse(txtLimiteSuperior.Text))
                    {
                        sErr += "El Límite Inferior no puede ser mayor o igual al Límite Superior";
                        errorProvider1.SetError(txtLimiteInferior, "El Límite Inferior no puede ser mayor o igual al Límite Superior");
                    }
                }*/
                  
            }

           

            if (!string.IsNullOrWhiteSpace(txtImporte.Text))
            {
                    if (!Regex.IsMatch(txtImporte.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$"))
                    {
                        sErr += "Para el Valor, solo se permiten números. ";
                        errorProvider1.SetError(txtImporte, "Para el Valor, solo se permiten números");
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
                    _Reg.INFERIOR = txtLimiteInferior.Text.Trim();
                    _Reg.SUPERIOR = txtLimiteSuperior.Text.Trim();
                    txtLimiteInferiorOrigen.Text = _Reg.INFERIOR;
                    txtLimiteSuperiorOrigen.Text = _Reg.SUPERIOR;
                }
                else {
                    _Reg.INFERIOR = txtLimiteInferior.Text.Trim();
                    _Reg.SUPERIOR = txtLimiteSuperior.Text.Trim();
                }                

                _Reg.CLAVE = comboClave.SelectedValue.ToString();
                _Reg.TIPO = comboTipo.SelectedValue.ToString();
                _Reg.IMPORTE = txtImporte.Text.Trim() != "" ? txtImporte.Text.Trim() : "null";
                _Reg.EQUIVALENTE = comboEquivalente.SelectedValue.ToString() != "0" ? comboEquivalente.SelectedValue.ToString() : null;

            }

            return bExito;
        }

        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string Inferior, string Superior, string Clave, string Tipo)
        {
            var existe = false;

            try
            {

                oReq.Tipo = 0;
                /*  DESCOMENTAR ESTO PARA PODER VALIDAR LOS LIMITES INFERIOR Y SUPERIOR POR RANGOS
                    
                    oReq.Query = $@"SELECT * FROM MULTA WHERE 
                              (
                                CLAVE = {Clave} AND TIPO = '{Tipo}'
                                    AND({Inferior} BETWEEN INFERIOR AND SUPERIOR OR {Superior} BETWEEN INFERIOR AND SUPERIOR)
                               )";*/

                oReq.Query = $@"SELECT * FROM MULTA WHERE 
                              (
                                CLAVE = {Clave} AND TIPO = '{Tipo}'
                                AND INFERIOR = {Inferior} AND SUPERIOR = {Superior}
                               )";
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
            txtLimiteInferiorOrigen.Text = _Reg.INFERIOR;
            txtLimiteSuperiorOrigen.Text = _Reg.SUPERIOR;
            txtLimiteInferior.Text = _Reg.INFERIOR;
            txtLimiteSuperior.Text = _Reg.SUPERIOR;
            comboTipo.SelectedValue = _Reg.TIPO;
            txtImporte.Text = _Reg.IMPORTE;
            comboEquivalente.SelectedValue = _Reg.EQUIVALENTE == "" ? "0" : _Reg.EQUIVALENTE;
        }

        #endregion

        private async Task limpiaCampos()
        {
            if (txtValida.Text == "Nuevo")
            {
                txtLimiteInferiorOrigen.Text = "";
                txtLimiteSuperiorOrigen.Text = "";
                txtLimiteInferior.Text = "";
                txtLimiteSuperior.Text = "";
                comboClave.Enabled = true;
                comboTipo.Enabled = true;
                txtImporte.Text = "";
                comboEquivalente.SelectedIndex = 0;



            }
            else { 
            
            }
            

        }


        private async void ChangeTipoValue(object sender, EventArgs e)
        {
            string Tipo = comboTipo.SelectedValue.ToString();
            if (Tipo == "F" || Tipo == "E") {
                comboClave.DataSource = null;
                comboClave.Items.Clear();

                await cargaComboClave(Tipo);

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
