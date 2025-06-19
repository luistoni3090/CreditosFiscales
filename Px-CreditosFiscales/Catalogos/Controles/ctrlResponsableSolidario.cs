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
    public partial class ctrlResponsableSolidario : UserControl
    { 
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eRESPONSABLESOLIDARIO _Reg = new eRESPONSABLESOLIDARIO();

        #region Constuctores
        public ctrlResponsableSolidario()
        {
            InitializeComponent();
            Inicio();
        }
        public ctrlResponsableSolidario(xMain oMain, string TIPO, string LEY, string valida)
        {

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.TIPO = TIPO;
            _Reg.LEY = LEY;
            txtValida.Text = valida;
            cargaComboLey();
            comboLey.Enabled = false;
            if (LEY == "F") 
                comboLey.SelectedValue = "F";
            else 
                comboLey.SelectedValue = "E";
            txtTipoOrigen.Text = _Reg.TIPO;
            txtLeyOrigen.Text = _Reg.LEY;
            detActualiza(TIPO, LEY);
        }

        private async Task Inicio()
        {


        }
        #endregion


        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eRESPONSABLESOLIDARIO();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string TIPO, string LEY)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            txtLeyOrigen.Visible = false;
            txtTipoOrigen.Visible = false;
            await detNuevo();
            await detBusca(LEY, TIPO);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {TIPO}-{LEY} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca(_Reg.LEY, _Reg.TIPO);
        }


        private async Task cargaComboLey()
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
            comboLey.DataSource = comboList;
            comboLey.DisplayMember = "Text";
            comboLey.ValueMember = "Value";
            comboLey.SelectedIndex = 0;
        }

        private async Task detBusca(string LEY, string TIPO)
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
                                        TIPO, DESCRIPCION, FRACCION, DOCTO, Ley FROM TIPO_RESPONSABLE 
                                    WHERE LEY = :LEY AND TIPO = :TIPO";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "LEY", Valor = LEY},
                    new eParametro(){ Tipo = DbType.Int16, Nombre = "TIPO", Valor = TIPO}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eRESPONSABLESOLIDARIO>();

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
            await _Main.Status($"¿Deseas borrar el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null,  $"¿Deseas borrar el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}??", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            string LeyOrigen = txtLeyOrigen.Text.Trim();
            string TipoOrigen = txtTipoOrigen.Text.Trim();


            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE TIPO_RESPONSABLE
                                WHERE LEY = '{_Reg.LEY}' AND TIPO = {_Reg.TIPO} ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = "00";
                    await limpiaCampos();
                    txtTipoOrigen.Text = comboLey.SelectedValue.ToString();
                    txtLeyOrigen.Text = "0";
                    txtTipo.Enabled = true;
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

                bool existe = await conValidaSiExisteRegistro(_Reg.TIPO, _Reg.LEY);

                if (existe)
                {

                    await _Main.Status($"El TIPO {_Reg.TIPO} Y la Ley {_Reg.LEY} ya estan en uso", (int)MensajeTipo.Error);
                    return;
                }
            }
                

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Tipo {_Reg.TIPO} y Ley {_Reg.LEY}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO TIPO_RESPONSABLE
                                    (
                                        TIPO,
                                        LEY,
                                        DESCRIPCION,
                                        FRACCION,
                                        DOCTO
                                        )
                                VALUES
                                        (
                                        {_Reg.TIPO},
                                        '{_Reg.LEY}',
                                        '{_Reg.DESCRIPCION}',
                                        '{_Reg.FRACCION}',
                                        {_Reg.DOCTO}
                                        )
                                ";
            }
            else
            {
                string LeyOrigen = comboLey.SelectedValue.ToString();
                string TipoOrigen = txtTipoOrigen.Text.Trim();

                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE TIPO_RESPONSABLE
                                    SET 
                                        TIPO = {_Reg.TIPO},
                                        LEY = '{_Reg.LEY}',
                                        DESCRIPCION = '{_Reg.DESCRIPCION}',
                                        FRACCION = '{_Reg.FRACCION}',
                                        DOCTO = {_Reg.DOCTO}
                                WHERE TIPO = {_Reg.TIPO} AND LEY = '{_Reg.LEY}' ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    tabPage.Text = "               " + _Reg.TIPO + "-" + _Reg.LEY;
                    tabPage.Name = $"{_Reg.TIPO}{_Reg.LEY}";

                    txtValida.Text = "Editar";
                    txtTipo.Enabled = false;
                    txtLeyOrigen.Text = _Reg.LEY;
                    txtTipoOrigen.Text = _Reg.TIPO;

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

            if (string.IsNullOrWhiteSpace(txtTipo.Text) || !decimal.TryParse(txtTipo.Text, out _))
            {
                sErr += "Debes de ingresar el Tipo, solo se permiten números. ";
                errorProvider1.SetError(txtTipo, "Debes de ingresar el Tipo, solo se permiten números. ");
            }


            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                sErr += "Debes de ingresar la descripción. ";
                errorProvider1.SetError(txtDescripcion, "Debes de ingresar la descripción. ");
            }

            if (string.IsNullOrWhiteSpace(txtFraccion.Text))
            {
                sErr += "Debes de ingresar la Fracción. ";
                errorProvider1.SetError(txtFraccion, "Debes de ingresar la descripción. ");
            }

            if (string.IsNullOrWhiteSpace(txtDocto.Text) || !decimal.TryParse(txtDocto.Text, out _))
            {
                sErr += "Debes de ingresar el Docto, solo se permiten números. ";
                errorProvider1.SetError(txtDocto, "Debes de ingresar el Docto, solo se permiten números. ");
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
                    _Reg.LEY = comboLey.SelectedValue.ToString();
                    _Reg.TIPO = txtTipo.Text.Trim();
                    _Reg.DESCRIPCION = txtDescripcion.Text.Trim();
                    _Reg.FRACCION = txtFraccion.Text.Trim();
                    _Reg.DOCTO = txtDocto.Text.Trim();
                    txtTipoOrigen.Text = _Reg.TIPO;
                    txtLeyOrigen.Text = _Reg.LEY;
                }
                else {
                    _Reg.TIPO = txtTipo.Text.Trim();
                    _Reg.LEY = comboLey.SelectedValue.ToString();
                    _Reg.DESCRIPCION = txtDescripcion.Text.Trim();
                    _Reg.FRACCION = txtFraccion.Text.Trim();
                    _Reg.DOCTO = txtDocto.Text.Trim();
                }                

            }

            return bExito;
        }

        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string TIPO, string LEY)
        {
            var existe = false;

            try
            {
                if (txtValida.Text == "Nuevo")
                {

                    oReq.Tipo = 0;
                    oReq.Query = $@"SELECT * FROM TIPO_RESPONSABLE WHERE TIPO = {TIPO} AND LEY = '{LEY}' ";
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

            txtTipo.Text = _Reg.TIPO;
            txtDescripcion.Text = _Reg.DESCRIPCION;
            txtFraccion.Text = _Reg.FRACCION;
            txtDocto.Text = _Reg.DOCTO;
            txtLeyOrigen.Text = _Reg.LEY;
            txtTipoOrigen.Text = _Reg.TIPO;

            if (txtValida.Text == "Editar")
                txtTipo.Enabled = false;

        }
        #endregion

        private async Task limpiaCampos()
        {
            
            if (txtValida.Text == "Nuevo")
            {
                txtTipo.Text = "";
                txtDescripcion.Text = "";
                txtFraccion.Text = "";
                txtDocto.Text = "";

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

        private void txtFracción_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
