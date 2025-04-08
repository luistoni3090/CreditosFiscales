/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 ctrlSecuencias.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Control para las secuencias de pólizas

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Contabilidad.Utiles.Generales;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;
using static Px_Contabilidad.Utiles.Emun.Enumerados;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Px_Contabilidad.Catalogos.Controles
{
    public partial class ctrlSecuencias : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eSECUENCIA _Reg = new eSECUENCIA();
        List<eSECUENCIA> _Con = new List<eSECUENCIA>();
        List<eEJERCICIO> _ConEjercicio = new List<eEJERCICIO>();
        List<eTIPO_POLIZA> _ConTipoPoliza = new List<eTIPO_POLIZA>();

        #region Constuctores
        public ctrlSecuencias()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlSecuencias(xMain oMain, int Empresa, int Ejercicio, string ID, List<eEJERCICIO> xEjercicio, List<eTIPO_POLIZA> xTipoPoliza)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            _ConEjercicio = xEjercicio;
            _ConTipoPoliza = xTipoPoliza;

            ComboIniciaEjercicio();
            ComboIniciaTipoPoliza();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.EMPRESA = Empresa;
            _Reg.EJERCICIO = Ejercicio;
            _Reg.TIPO_POLIZA = ID;

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

            _Reg = new eSECUENCIA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string ID)
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
            await detBusca(_Reg.TIPO_POLIZA);
        }

        private async Task detBusca(string ID)
        {

            if (ID.Length == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = $"SELECT * FROM bts.SECUENCIA WHERE EMPRESA = {_Reg.EMPRESA} AND EJERCICIO = {_Reg.EJERCICIO} AND TIPO_POLIZA = '{_Reg.TIPO_POLIZA}'";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eSECUENCIA>();

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

            cmbEjercicio.SelectedValue = _Reg.EJERCICIO;
            cmbTP.SelectedValue = _Reg.TIPO_POLIZA;

            txtNoSecuencia.Text = _Reg.SECUENCIA.ToString();

            await Task.Delay(0);
        }

        #endregion


        #region Combos


        private async Task ComboIniciaEjercicio()
        {
            await Task.Delay(0);

            cmbEjercicio.FlatStyle = FlatStyle.Flat;
            cmbEjercicio.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEjercicio.DisplayMember = "DESCR";
            cmbEjercicio.ValueMember = "EJERCICIO";
            cmbEjercicio.DataSource = _ConEjercicio;

        }

        private async Task ComboIniciaTipoPoliza()
        {
            await Task.Delay(0);

            cmbTP.FlatStyle = FlatStyle.Flat;
            cmbTP.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbTP.DisplayMember = "DESCR";
            cmbTP.ValueMember = "TIPO_POLIZA";
            cmbTP.DataSource = _ConTipoPoliza;

        }

        #endregion



    }
}
