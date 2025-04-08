using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;

using Px_ConciliacionBancaria.Utiles.Generales;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;

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
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using Px_Utiles.Utiles.Cadenas;
using Px_Controles.Controls.Tab;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;

namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    public partial class ctrlBanco : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eBANCO _Reg = new eBANCO();

        #region Constuctores
        public ctrlBanco()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlBanco(xMain oMain, Int32 ID)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            txtValida.Text = ID.ToString();
            _Reg.BANCO = ID;
            detActualiza(ID);
        }

        private async Task Inicio()
        {


        }
        #endregion

        #region "Detalle"
        /// <summary>
        /// Reseteo las instancias para nuevo registro
        /// </summary>
        /// <returns></returns>
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eBANCO();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Llammo el registro desde la base de datos
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task detActualiza(Int32 ID)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;
            txtValida.Visible = false;
            await detNuevo();
            await detBusca(ID);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {ID} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Llamo la función para traer el registro
        /// </summary>
        /// <returns></returns>
        public async Task detActualiza()
        {
            await detBusca((Int32)_Reg.BANCO);
        }

        private async Task detBusca(Int32 ID)
        {

            if (ID == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = "Select BANCO, DESCR, DIRECC, TEL, EJECUTIVO From Banco Where BANCO = :BANCO";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "BANCO", Valor = ID}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eBANCO>();


                await detPresenta();


            }
            catch (Exception ex)
            {
                //await JS.Notify($"Error", $"{ex.Message}", Gen.MensajeToastTipo.error, 1);
            }

        }

        /// <summary>
        /// Borro el registro en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task detBorra(TabPage tabPage)
        {

            if (txtValida.Text == "0") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE BANCO
                                WHERE BANCO = {_Reg.BANCO}
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    await detNuevo();
                    await detPresenta();
                    txtTelefono.Text = "";
                    txtValida.Text = "0";
                    tabPage.Name = $"00";
                    tabPage.Text = $"Banco 0-0";
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

            await _Main.Status($"¿Deseas {(txtValida.Text == "0" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "0" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();
            if (txtValida.Text == "0")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO BANCO
                                    (
                                        BANCO,
                                        DESCR,
                                        DIRECC,
                                        TEL,
                                        EJECUTIVO
                                        )
                                VALUES
                                       (
                                        (SELECT NVL(MAX(BANCO), 0) + 1 FROM BANCO),
                                        '{_Reg.DESCR}',
                                        '{_Reg.DIRECC}',
                                        '{_Reg.TEL}',
                                        '{_Reg.EJECUTIVO}'
                                        )
                                ";
            }
            else
            {

                _Reg.BANCO = int.Parse(txtValida.Text);

                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE BANCO
                                    SET DESCR = '{_Reg.DESCR}',
                                        DIRECC = '{_Reg.DIRECC}',
                                        TEL = '{_Reg.TEL}',
                                        EJECUTIVO = '{_Reg.EJECUTIVO}'
                                WHERE BANCO = '{_Reg.BANCO}'
                             "; 
                
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    if (txtValida.Text == "0")
                    {
                        oReq.Tipo = 0;
                        oReq.Query = $@"SELECT MAX(BANCO) AS BancoID FROM BANCO";
                        var oRes2 = await WSServicio.Servicio(oReq);

                        if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                        {
                            int maxId = Convert.ToInt32(oRes2.Data.Tables[0].Rows[0][0]);
                            txtValida.Text = (maxId.ToString());

                        }
                    }

                    tabPage.Name = $"{txtValida.Text}{_Reg.DESCR}";
                    tabPage.Text = $"Banco {txtValida.Text}-{_Reg.DESCR}";

                    Console.WriteLine(oReq.Query);
                    await _Main.Status($"Registro {_Reg.DESCR} guardado correctamente", (int)MensajeTipo.Success);

                    //if (_Reg.BANCO == 0) {
                    //    await limpiarInputs();
                    //}

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

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                sErr += "Ingrese la descripción. ";
                errorProvider1.SetError(txtDescripcion, "Ingrese la descripción.");

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                sErr += "Ingrese la dirección. ";
            errorProvider1.SetError(txtDireccion, "Ingrese la dirección.");

            if (!txtTelefono.Text.IsDecimal())
                sErr += "Ingrese el teléfono, solo se permiten números. ";
            errorProvider1.SetError(txtTelefono, "Ingrese el teléfono, solo se permiten números.");

            if (string.IsNullOrWhiteSpace(txtEjecutivo.Text))
                sErr += "Ingrese el nombre del ejecutivo.";
            errorProvider1.SetError(txtEjecutivo, "Ingrese el nombre del ejecutivo.");

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {
                _Reg.DESCR = txtDescripcion.Text;
                _Reg.DIRECC = txtDireccion.Text;
                _Reg.TEL = decimal.Parse(txtTelefono.Text);
                _Reg.EJECUTIVO = txtEjecutivo.Text;

            }

            return bExito;
        }

        /// <summary>
        /// Asigno los valores de la estructura a los controles de la forma
        /// </summary>
        /// <returns></returns>
        private async Task detPresenta()
        {

            txtDescripcion.Text = _Reg.DESCR;
            txtDireccion.Text = _Reg.DIRECC;
            txtTelefono.Text = _Reg.TEL.ToString();
            txtEjecutivo.Text = _Reg.EJECUTIVO;
        }
        #endregion

        /*
        private async Task limpiarInputs()
        {

            txtDescripcion.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEjecutivo.Text = "";
        }*/


        private void rxrPosiciones_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
