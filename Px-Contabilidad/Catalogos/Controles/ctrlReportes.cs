/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 ctrlReportes.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Control para el detalle de reportes y firmas

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
using System.Security.Cryptography;

namespace Px_Contabilidad.Catalogos.Controles
{
    public partial class ctrlReportes : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eREPORTE _Reg = new eREPORTE();

        List<eFIRMA_REPORTE> _ConLista = new List<eFIRMA_REPORTE>();

        #region Constuctores
        public ctrlReportes()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlReportes(xMain oMain, int ID)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();


            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.REPORTE = ID;

            detActualiza(ID);

        }

        private async Task Inicio()
        {

            await GridInicia();
        }
        #endregion


        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eREPORTE();

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
            await detBusca((Int32)_Reg.REPORTE);
        }

        private async Task detBusca(Int32 ID)
        {

            if (ID == 0)
                return;

            try
            {

                //_SpinnerTitulo = $"Cargando detalle de usuario ID {ID}, espere por favor . . .";
                //_Reg.usu_ID = ID;

                oReq.Query = $"SELECT * FROM bts.REPORTE WHERE REPORTE = {_Reg.REPORTE} ";
                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eREPORTE>();


                oReq.Query = $@"
                                SELECT
                                    FIRMA_REPORTE.EMPRESA,
                                    FIRMA_REPORTE.REPORTE,
                                    FIRMA_REPORTE.FIRMA,
                                    FIRMA_REPORTE.ORDEN_REP,
                                    FIRMA.NOMBRE
                                FROM FIRMA_REPORTE
                                INNER JOIN FIRMA ON FIRMA.FIRMA = FIRMA_REPORTE.FIRMA AND FIRMA.EMPRESA = {1}
                                WHERE FIRMA_REPORTE.EMPRESA = {1}
                                AND FIRMA_REPORTE.REPORTE = {_Reg.REPORTE}
                                ORDER BY FIRMA_REPORTE.ORDEN_REP
                                ";
                oRes = await WSServicio.Servicio(oReq);
                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _ConLista = oRes.Data.Tables[0].AListaDe<eFIRMA_REPORTE>();


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

            txtDescripcion.Text = _Reg.DESCR;
            txtEncabezado1.Text = _Reg.ENCABE1;
            txtEncabezado2.Text = _Reg.ENCABE2;
            txtEncabezado3.Text = _Reg.ENCABE3;

            dataGridView1.DataSource = _ConLista;

            await Task.Delay(0);
        }

        #endregion



        #region Grid
        private async Task GridInicia()
        {

            //dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { HeaderText = "Nombre", Width = 250,  DataPropertyName = "NOMBRE" },
                new DataGridViewTextBoxColumn() { HeaderText = "Ordden", Width = 100,  DataPropertyName = "ORDEN_REP" }
            });
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //dataGridView1.CellClick += oGrid_CellClick;


            await Task.Delay(0);
        }

        #endregion


    }
}
