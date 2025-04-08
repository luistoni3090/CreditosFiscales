/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 ctrlOrganismoEjercicio.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma contenedor de toda la app

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Contabilidad.Utiles.Generales;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;
using static Px_Contabilidad.Utiles.Emun.Enumerados;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Contabilidad.Catalogos
{
    public partial class ctrlOrganismoEjercicio : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eEMPRESA _Reg = new eEMPRESA();
        List<eEJERCICIO> _ConEjercicios = new List<eEJERCICIO>();


        #region Constuctores
        public ctrlOrganismoEjercicio()
        {
            InitializeComponent();

            Inicio();
        }

        public ctrlOrganismoEjercicio(xMain oMain, Int32 ID)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.EMPRESA = ID;
            detActualiza(ID);

        }

        private async Task Inicio()
        {


            await GridInicia();
        }
        #endregion

        #region Controles
        private async Task GridInicia()
        {

            dgvEjercicios.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "EJERCICIO", Width = 100,  DataPropertyName = "EJERCICIO" },
                new DataGridViewTextBoxColumn() { HeaderText = "DESCR", Width = 500,  DataPropertyName = "NOMBRE"  },
                new DataGridViewTextBoxColumn() { HeaderText = "STATUS", Width = 100,  DataPropertyName = "STATUS" },
                new DataGridViewTextBoxColumn() { HeaderText = "EMPRESA", Width = 500,  DataPropertyName = "EMPRESA"  },
                new DataGridViewTextBoxColumn() { HeaderText = "ACTIVO", Width = 100,  DataPropertyName = "ACTIVO" },
            });

            dgvEjercicios.AutoGenerateColumns = false;
            dgvEjercicios.BackgroundColor = Color.White;
        }
        #endregion


        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eEMPRESA();

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
            await detBusca(_Reg.EMPRESA);
        }

        private async Task detBusca(Int32 ID)
        {

            if (ID == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = "SELECT * FROM EMPRESA ";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "EMPRESA ", Valor = ID}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eEMPRESA>();






                await detPresenta();
                await conEjercicios();


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



        #region "Ejercicios"
        private async Task conEjercicios()
        {
            oReq.Query = "SELECT EJERCICIO, DESCR, STATUS, EMPRESA, ACTIVO FROM BTS.EJERCICIO ";
            var oRes = await WSServicio.Servicio(oReq);

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                _ConEjercicios = oRes.Data.Tables[0].AListaDe<eEJERCICIO>();

            dgvEjercicios.DataSource = _ConEjercicios;


        }
        #endregion


        private async Task detPresenta()
        {

            txtNombre.Text = _Reg.NOMBRE;
            txtDireccion.Text = _Reg.DIRECCION;
            txtMunicipio.Text = _Reg.MUNICIPIO;
            txtEstado.Text = _Reg.ESTADO;
            txtTelefono.Text = _Reg.TELEFONO;
            txtCP.Text = _Reg.CP.ToString();
            txtRFC.Text = _Reg.RFC;
            txtRepresentante.Text = _Reg.REP_LEGAL;
            txtCURP.Text = _Reg.CURP;

        }

        #endregion


    }
}
