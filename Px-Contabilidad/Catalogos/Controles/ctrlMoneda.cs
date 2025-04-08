/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 ctrlMoneda.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Control para el detalle de monedas

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;

using Px_Contabilidad.Utiles.Generales;
using static Px_Contabilidad.Utiles.Emun.Enumerados;

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
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using Px_Utiles.Utiles.Cadenas;

namespace Px_Contabilidad.Catalogos.Controles
{
    public partial class ctrlMoneda : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eMONEDA _Reg = new eMONEDA();

        #region Constuctores
        public ctrlMoneda()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlMoneda(xMain oMain, Int32 ID)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.MONEDA = ID;
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

            _Reg = new eMONEDA();

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
            await detBusca((Int32)_Reg.MONEDA);
        }


        /// <summary>
        /// Traigo el registro desde a base de datos
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task detBusca(Int32 ID)
        {

            if (ID == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = "SELECT * FROM bts.moneda Where MONEDA = :MONEDA";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "MONEDA", Valor = ID}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eMONEDA>();


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
        public async Task detBorra()
        {

            if (_Reg.MONEDA == 0) return;
            await _Main.Status($"¿Deseas borrar la moneda {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar la moneda {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar la moneda {_Reg.DESCR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar la moneda {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE MONEDA
                                WHERE MONEDA = {_Reg.MONEDA}
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Err == 0)
                {
                    await detNuevo();
                    await detPresenta();
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
        public async Task detGuarda()
        {
            bool bExito = await detValida();

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas {(_Reg.MONEDA == 0 ? "guardar" : "actualizar")} la moneda {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(_Reg.MONEDA == 0 ? "guardar" : "actualizar")} la moneda {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();
            if (_Reg.MONEDA == 0)
            {
                _Reg.MONEDA = await detID();
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO MONEDA
                                    (
                                        MONEDA,
                                        DESCR,
                                        TIPO_CAMBIO,
                                        SIMBOLO
                                        )
                                VALUES
                                        (
                                        {_Reg.MONEDA},
                                        '{_Reg.DESCR}',
                                        {_Reg.TIPO_CAMBIO},
                                        '{_Reg.SIMBOLO}'
                                        )
                                ";
            }
            else
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE MONEDA
                                    SET DESCR = '{_Reg.DESCR}',
                                        TIPO_CAMBIO = {_Reg.TIPO_CAMBIO},
                                        SIMBOLO = '{_Reg.SIMBOLO}'
                                WHERE MONEDA = {_Reg.MONEDA}
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Err == 0)
                {
                    await _Main.Status($"Registro {_Reg.DESCR} guardado correctamente", (int)MensajeTipo.Success);
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

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                sErr += "Debes de indicar el nombre de la moneda";

            if (!txtTC.Text.IsDecimal())
                sErr += "Debes de indicar el tipo de cambio, solo se permiten números";

            if (string.IsNullOrWhiteSpace(txtSimbolo.Text))
                sErr += "Debes de indicar el símbolo";

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {
                _Reg.DESCR = txtNombre.Text;
                _Reg.TIPO_CAMBIO = decimal.Parse(txtTC.Text);
                _Reg.SIMBOLO = txtSimbolo.Text;

            }

            return bExito;
        }

        /// <summary>
        /// Traigo el ultimo registro de la tabla por que no tiene PrimaryKey
        /// </summary>
        /// <returns></returns>
        private async Task<Int64> detID()
        {
            Int64 ID = 0;
            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT COUNT(MONEDA) FROM bts.moneda";
                oReq.Parametros = new List<eParametro>();
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    if (oRes.Data.Tables[0] != null)
                    {
                        foreach (DataRow oRow in oRes.Data.Tables[0].Rows)
                        {
                            ID = Int64.Parse(oRow[0].ToString());
                        }
                    }
            }
            catch (Exception ex)
            {
                await _Main.Status($"Error al buscar la serie: {ex.Message}", (int)MensajeTipo.Warning);
            }

            ID += 1;
            return ID;
        }

        /// <summary>
        /// Asigno los valores de la estructura a los controles de la forma
        /// </summary>
        /// <returns></returns>
        private async Task detPresenta()
        {
            txtNombre.Text = _Reg.DESCR;
            txtTC.Text = _Reg.TIPO_CAMBIO.ToString();
            txtSimbolo.Text = _Reg.SIMBOLO;
        }

        #endregion

    }
}
