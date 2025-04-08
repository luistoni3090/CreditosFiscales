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

using Px_InversionPublica.Utiles.Generales;
using static Px_InversionPublica.Utiles.Emun.Enumerados;

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

namespace Px_InversionPublica.Catalogos.Controles
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
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eMONEDA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


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

        private async Task detActualiza()
        {
            await detBusca((Int32)_Reg.MONEDA);
        }

        private async Task detBusca(Int32 ID)
        {

            if (ID == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = "SELECT moneda, descr FROM bts.moneda Where MONEDA = :MONEDA";
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



        private async Task detGuarda()
        {


            await Task.Delay(0);
        }

        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;

            //if (string.IsNullOrWhiteSpace(_Reg.usu_Nombre))
            //    sErr += "<li>debes de indicar el nombre del proyecto</li>";

            //if (_ConDatPar.Count == 0)
            //    sErr += "<li>Debes indicar la cantidad</li>";

            //if (sErr.Length > 0)
            //{
            //    bExito = false;
            //    await JS.Notify($"Error de validación", $"{sErr}", Utiles.T40.Enumerados.Gen.MensajeToastTipo.warning, 1);
            //}

            return bExito;

        }



        private async Task detPresenta()
        {

            txtNombre.Text = _Reg.DESCR;
            rxrPosiciones.Text = _Reg.SIMBOLO;
            txtEjercicio.Text = _Reg.SIMBOLO;
        }

        #endregion

    }
}
