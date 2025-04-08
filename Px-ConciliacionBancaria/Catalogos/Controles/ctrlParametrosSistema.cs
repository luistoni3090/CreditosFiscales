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

namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    public partial class ctrlParametrosSistema : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        ePARAMETROSISTEMA _Reg = new ePARAMETROSISTEMA();

        #region Constuctores
        public ctrlParametrosSistema()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlParametrosSistema(xMain oMain, string ID, string valida)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            txtValida.Text = valida;
            _Reg.GOBIERNO = ID;
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

            _Reg = new ePARAMETROSISTEMA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Llammo el registro desde la base de datos
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private async Task detActualiza(string ID)
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
            await detBusca(_Reg.GOBIERNO);
        }

        private async Task detBusca(string ID)
        {

            if (txtValida.Text == "Nuevo")
                return;

            try
            {

                oReq.Query = "SELECT GOBIERNO, SECRETARIA, TOLERANCIA, TOLERANCIADLLS, VIGENCIAAUX, VIGENCIABCO, FECHACARGA, ANIOS_CONCILIAR FROM PARAMETRO_CB Where GOBIERNO = :GOBIERNO";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "GOBIERNO", Valor = ID}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0)
                {
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<ePARAMETROSISTEMA>();

                    txtGobierno.Enabled = false;
                    await detPresenta();
                }                    


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

            _Reg.VALIDA = txtValida.Text;


            if (_Reg.VALIDA == "Nuevo") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg.GOBIERNO}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.GOBIERNO}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.GOBIERNO}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.GOBIERNO}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE PARAMETRO_CB
                                WHERE GOBIERNO = '{_Reg.GOBIERNO}'
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    await detNuevo();
                    await detPresenta();
                    txtValida.Text = "Nuevo";
                    txtGobierno.Enabled = true;
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

            _Reg.VALIDA = txtValida.Text;

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas {(_Reg.VALIDA == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.GOBIERNO}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(_Reg.VALIDA == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg.GOBIERNO}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();
            if (_Reg.VALIDA == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO PARAMETRO_CB
                                    (
                                        GOBIERNO,
                                        SECRETARIA,
                                        TOLERANCIA,
                                        TOLERANCIADLLS,
                                        VIGENCIAAUX,
                                        VIGENCIABCO,
                                        FECHACARGA,
                                        ANIOS_CONCILIAR
                                        )
                                VALUES
                                       (
                                        '{_Reg.GOBIERNO}',
                                        '{_Reg.SECRETARIA}',
                                        '{_Reg.TOLERANCIA}',
                                        '{_Reg.TOLERANCIADLLS}',
                                        '{_Reg.VIGENCIAAUX}',
                                        '{_Reg.VIGENCIABCO}',
                                        '{_Reg.FECHACARGA}',
                                        '{_Reg.ANIOS_CONCILIAR}'
                                        )
                                ";
            }
            else
            {
                
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE PARAMETRO_CB
                                    SET GOBIERNO = '{_Reg.GOBIERNO}',
                                        SECRETARIA = '{_Reg.SECRETARIA}',
                                        TOLERANCIA = '{_Reg.TOLERANCIA}',
                                        TOLERANCIADLLS = '{_Reg.TOLERANCIADLLS}',
                                        VIGENCIAAUX = '{_Reg.VIGENCIAAUX}',
                                        VIGENCIABCO = '{_Reg.VIGENCIABCO}',
                                        FECHACARGA = '{_Reg.FECHACARGA}',
                                        ANIOS_CONCILIAR = '{_Reg.ANIOS_CONCILIAR}'
                                WHERE GOBIERNO = '{_Reg.GOBIERNO}'
                             "; 
                
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    tabPage.Text = $"Parámetro {_Reg.GOBIERNO}";
                    tabPage.Name = _Reg.GOBIERNO;
                    Console.WriteLine(oReq.Query);
                    await _Main.Status($"Registro {_Reg.GOBIERNO} guardado correctamente", (int)MensajeTipo.Success);

                    if (txtValida.Text == "Nuevo") {
                        txtValida.Text = "Editar";
                    }
                    txtGobierno.Enabled = false;

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

            
            if (string.IsNullOrWhiteSpace(txtGobierno.Text))
                sErr += "Ingrese la descripciom para el GOBIERNO. ";
                errorProvider1.SetError(txtGobierno, "Ingrese la descripciom para el GOBIERNO.");

            if (string.IsNullOrWhiteSpace(txtSecretaria.Text))
                sErr += "Ingrese la secretaria. ";
            errorProvider1.SetError(txtSecretaria, "Ingrese la secretaria.");

            if (!txtTolerancia.Text.IsDecimal())
                sErr += "Ingrese la tolerancia, solo se permiten números. ";
            errorProvider1.SetError(txtTolerancia, "Ingrese la tolerancia, solo se permiten números");

            if (!txtTolerancia.Text.IsDecimal())
                sErr += "Ingrese la tolerancia, solo se permiten números. ";
            errorProvider1.SetError(txtTolerancia, "Ingrese la tolerancia, solo se permiten números");

            if (!txtToleranciaDolares.Text.IsDecimal())
                sErr += "Ingrese la tolerancia en dólares, solo se permiten números. ";
            errorProvider1.SetError(txtToleranciaDolares, "Ingrese la tolerancia, solo se permiten números");

            if (!txtVigenciaAuxiliar.Text.IsDecimal())
                sErr += "Ingrese la vigencia auxiliar, solo se permiten números. ";
            errorProvider1.SetError(txtVigenciaAuxiliar, "Ingrese la vigencia auxiliar, solo se permiten números");

            if (!txtVigenciaBanco.Text.IsDecimal())
                sErr += "Ingrese la vigencia banco, solo se permiten números. ";
            errorProvider1.SetError(txtVigenciaBanco, "Ingrese la vigencia banco, solo se permiten números");

            if (string.IsNullOrWhiteSpace(txtFechaCarga.Text))
                sErr += "Ingrese la fecha carga.";
            errorProvider1.SetError(txtFechaCarga, "Ingrese la fecha carga.");

            if (!txtAniosConciliar.Text.IsDecimal())
                sErr += "Ingrese los años conciliar, solo se permiten números. ";
            errorProvider1.SetError(txtAniosConciliar, "Ingrese los años conciliar, solo se permiten números");


            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                _Reg.GOBIERNO = txtGobierno.Text;
                _Reg.SECRETARIA = txtSecretaria.Text;
                _Reg.TOLERANCIA = int.Parse(txtTolerancia.Text);
                _Reg.TOLERANCIADLLS = int.Parse(txtToleranciaDolares.Text);
                _Reg.VIGENCIAAUX = int.Parse(txtVigenciaAuxiliar.Text);
                _Reg.VIGENCIABCO = int.Parse(txtVigenciaBanco.Text);
                _Reg.FECHACARGA = txtFechaCarga.Text;
                _Reg.ANIOS_CONCILIAR = int.Parse(txtAniosConciliar.Text);

            }

            return bExito;
        }

        /// <summary>
        /// Asigno los valores de la estructura a los controles de la forma
        /// </summary>
        /// <returns></returns>
        private async Task detPresenta()
        {
            txtGobierno.Text = _Reg.GOBIERNO;
            txtSecretaria.Text = _Reg.SECRETARIA;
            txtTolerancia.Text = _Reg.TOLERANCIA.ToString();
            txtToleranciaDolares.Text = _Reg.TOLERANCIADLLS.ToString();
            txtVigenciaAuxiliar.Text = _Reg.VIGENCIAAUX.ToString();
            txtVigenciaBanco.Text = _Reg.VIGENCIABCO.ToString();
            txtFechaCarga.Text = _Reg.FECHACARGA;
            txtAniosConciliar.Text = _Reg.ANIOS_CONCILIAR.ToString();
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
