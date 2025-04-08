using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
//using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
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

namespace Px_CreditosFiscales.Catalogos.Controles
{
    public partial class ctrlGarantias : UserControl
    { 
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eDET_TIPO_GARANTIA _Reg = new eDET_TIPO_GARANTIA();

        #region Constuctores
        public ctrlGarantias()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlGarantias(xMain oMain, string TipoDetalle, string TipoGarantia, string valida, string Descripcion)
        {

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.TIPO_DETALLE = TipoDetalle;
            _Reg.TIPO_GARANTIA = TipoGarantia;
            _Reg.DESCR = Descripcion;
            txtValida.Text = valida;
            detActualiza(TipoDetalle, TipoGarantia);
        }

        private async Task Inicio()
        {


        }
        #endregion


        private async Task cargaComboTipoGarantia()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT TIPO_GARANTIA, DESCR FROM TIPO_GARANTIA ORDER BY TIPO_GARANTIA ";
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
                            Value = row["TIPO_GARANTIA"].ToString(),
                            Text = "("+ row["TIPO_GARANTIA"].ToString()+") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboTipoGarantia.DataSource = comboList;
                    comboTipoGarantia.DisplayMember = "Text";
                    comboTipoGarantia.ValueMember = "Value";
                    comboTipoGarantia.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }



        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eDET_TIPO_GARANTIA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string TipoDetalle, string TipoGarantia)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            await detNuevo();
            await detBusca(TipoDetalle, TipoGarantia);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {TipoDetalle}-{TipoGarantia}-{_Reg.DESCR.Trim()} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca(_Reg.TIPO_DETALLE, _Reg.TIPO_GARANTIA);
        }


        private async Task detBusca(string TipoDetalle, string TipoGarantia)
        {

            await cargaComboTipoGarantia();

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
                    oReq.Query = @"SELECT TIPO_DETALLE, TIPO_GARANTIA, DESCR FROM DET_TIPO_GARANTIA
                                    WHERE TIPO_DETALLE = :TipoDetalle  AND TIPO_GARANTIA = :TipoGarantia ";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "TipoDetalle", Valor = TipoDetalle},
                    new eParametro(){ Tipo = DbType.String, Nombre = "TipoGarantia", Valor = TipoGarantia}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eDET_TIPO_GARANTIA>();

                    txtValida.Text = "Editar";
                    txtTipoDetalle.Enabled = false;
                    comboTipoGarantia.Enabled = false;
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
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;
            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE DET_TIPO_GARANTIA
                                WHERE TIPO_DETALLE = {_Reg.TIPO_DETALLE} AND TIPO_GARANTIA = '{_Reg.TIPO_GARANTIA}'
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = "000";
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

                bool existe = await conValidaSiExisteRegistro();

                if (existe)
                {

                    await _Main.Status($"El Tipo Detalle {_Reg.TIPO_DETALLE} y el Tipo de Garantía {_Reg.TIPO_GARANTIA} ya estan en uso", (int)MensajeTipo.Error);
                    return;
                }

            }

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO DET_TIPO_GARANTIA
                                    (
                                        TIPO_DETALLE,
                                        TIPO_GARANTIA,
                                        DESCR
                                        )
                                VALUES
                                        (
                                        {_Reg.TIPO_DETALLE},
                                        '{_Reg.TIPO_GARANTIA}',
                                        '{_Reg.DESCR}'
                                        )
                                ";
            }
            else
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE DET_TIPO_GARANTIA
                                    SET 
                                        TIPO_DETALLE = {_Reg.TIPO_DETALLE},
                                        TIPO_GARANTIA = '{_Reg.TIPO_GARANTIA}',
                                        DESCR = '{_Reg.DESCR}'
                                WHERE TIPO_DETALLE = {_Reg.TIPO_DETALLE} AND TIPO_GARANTIA = '{_Reg.TIPO_GARANTIA}'
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    //tabPage.Name = $"{TipoDetalle}{TipoGarantia}{Descripcion.Trim()}";
                    //tabPage.Text = "               " + TipoDetalle + "-" + TipoGarantia + "-" + Descripcion.Trim();

                    tabPage.Text = "               " + _Reg.TIPO_DETALLE + "-" + _Reg.TIPO_GARANTIA + "-" + _Reg.DESCR.Trim();
                    tabPage.Name = $"{_Reg.TIPO_DETALLE}{_Reg.TIPO_GARANTIA}{_Reg.DESCR.Trim()}";

                    txtValida.Text = "Editar";
                    txtTipoDetalle.Enabled = false;
                    comboTipoGarantia.Enabled = false;

                    await _Main.Status($"Registro {_Reg.DESCR.Trim()} guardado correctamente", (int)MensajeTipo.Success);
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

            if (string.IsNullOrWhiteSpace(txtTipoDetalle.Text) || !decimal.TryParse(txtTipoDetalle.Text, out _))
                sErr += "Debes de ingresar el Tipo Detalle, solo se permiten números ";
            errorProvider1.SetError(txtTipoDetalle, "Debes de ingresar el Tipo Detalle, solo se permiten números");

            if (string.IsNullOrWhiteSpace(comboTipoGarantia.Text))
                sErr += "Debes seleccionar el Tipo de Garantía. ";
            errorProvider1.SetError(comboTipoGarantia, "Debes seleccionar el Tipo de Garantía.");

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                sErr += "Debes de ingresar la Descripción. ";
            errorProvider1.SetError(txtDescripcion, "Debes de ingresar la Descripción.");
            

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {
                
                _Reg.TIPO_DETALLE = txtTipoDetalle.Text.Trim();
                _Reg.TIPO_GARANTIA = comboTipoGarantia.SelectedValue.ToString();
                _Reg.DESCR = txtDescripcion.Text.Trim();

            }

            return bExito;
        }

        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro()
        {
            var existe = false;

            try
            {
                oReq.Query = @"SELECT TIPO_DETALLE, TIPO_GARANTIA, DESCR
                                FROM DET_TIPO_GARANTIA WHERE TIPO_DETALLE = " + _Reg.TIPO_DETALLE + " AND TIPO_GARANTIA = '" + _Reg.TIPO_GARANTIA + "' ";
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
            txtTipoDetalle.Text = _Reg.TIPO_DETALLE.ToString();
            comboTipoGarantia.SelectedValue = _Reg.TIPO_GARANTIA.ToString();
            txtDescripcion.Text = _Reg.DESCR;
        }

        #endregion

        private async Task limpiaCampos()
        {

            txtTipoDetalle.Enabled = true;
            comboTipoGarantia.Enabled = true;
            txtTipoDetalle.Text = "";
            comboTipoGarantia.SelectedIndex = 0;
            txtDescripcion.Text = "";

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
