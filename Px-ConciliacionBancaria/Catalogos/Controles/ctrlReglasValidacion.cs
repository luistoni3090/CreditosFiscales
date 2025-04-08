using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;

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
using System.Security.Cryptography;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using System.Collections;
using System.Text.RegularExpressions;
using Px_Utiles.Utiles.Cadenas;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Px_Controles.Controls.Tab;

namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    public partial class ctrlReglasValidacion : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eREGLASVALIDACION _Reg = new eREGLASVALIDACION();

        #region Constuctores
        public ctrlReglasValidacion()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlReglasValidacion(xMain oMain, Int32 IdBanco, Int32 IdCuenta, Int32 Orden)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.BANCO = IdBanco;
            _Reg.CUENTA = IdCuenta;
            _Reg.ORDEN = Orden;
            detActualiza(IdBanco, IdCuenta, Orden);
        }

        private async Task Inicio()
        {

        }
        #endregion

        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eREGLASVALIDACION();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(Int32 IdBanco, Int32 IdCuenta, Int32 Orden)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtEditarNuevo.Visible = false;
            comboBanco.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            await detNuevo();
            await detBusca(IdBanco,  IdCuenta, Orden);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {Orden} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            if(txtEditarNuevo.Text != "0")
            await detBusca((Int32)_Reg.BANCO, (Int32)_Reg.CUENTA, (Int32)_Reg.ORDEN);
        }

        #region "Cargar combos"
        private async Task CargarComboBanco()
        {

            try
            {
                oReq.Query = "SELECT BANCO IDBanco, LPAD(BANCO, 2, '0') BANCO, DESCR FROM BANCO ORDER BY BANCO";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    var bancoList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDBanco"].ToString(),
                            Text = "(" + row["BANCO"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        bancoList.Add(item);
                    }
                    comboBanco.DataSource = bancoList;
                    comboBanco.DisplayMember = "Text";
                    comboBanco.ValueMember = "Value";
                    comboBanco.SelectedIndex = -1;
                }

                comboBanco.SelectedIndexChanged += CbBanco_SelectedIndexChanged;

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Combo banco item selected"
        private async void CbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBanco.SelectedItem != null)
            {
                var bancoId = ((ComboBoxItem)comboBanco.SelectedItem).Value;

                var cuentaQuery = "SELECT BANCO, CUENTA AS IDCUENTA, LPAD(CUENTA, 3, '0') CUENTA, DESCR FROM CUENTA WHERE BANCO = " + bancoId + " ORDER BY BANCO, CUENTA";

                oReq.Query = cuentaQuery;
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables.Count > 0)
                {

                    var dataTable = oRes.Data.Tables[0];

                    var cuentaList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDCUENTA"].ToString(),
                            Text = "(" + row["CUENTA"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        cuentaList.Add(item);
                    }

                    comboCuenta.DataSource = null;
                    comboCuenta.Items.Clear();
                    comboCuenta.SelectedIndex = -1;
                    comboCuenta.DataSource = cuentaList;
                    comboCuenta.DisplayMember = "Text";
                    comboCuenta.ValueMember = "Value";
                    comboCuenta.SelectedIndex = -1;

                    if (txtEditarNuevo.Text != "0") {
                        comboCuenta.SelectedValue = _Reg.CUENTA.ToString();
                        comboBanco.Enabled = false;
                        comboCuenta.Enabled = false;
                        txtOrden.Enabled = false;
                    }

                }
            }
        }
        #endregion

        private async Task detBusca(Int32 IdBanco, Int32 IdCuenta, Int32 Orden)
        {
            txtEditarNuevo.Text = IdBanco.ToString();
            await CargarComboBanco();

            if (Orden == 0)
                return;

            try
            {

                oReq.Query = "SELECT BANCO, CUENTA, ORDEN, DESCR, VALIDA, QUERY, REGRESA_QUERY FROM REGLA WHERE BANCO = :BANCO AND CUENTA = :CUENTA AND ORDEN = :ORDEN";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "BANCO", Valor = IdBanco},
                    new eParametro(){ Tipo = DbType.String, Nombre = "CUENTA", Valor = IdCuenta},
                    new eParametro(){ Tipo = DbType.String, Nombre = "ORDEN", Valor = Orden}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eREGLASVALIDACION>();

                await detPresenta();

            }
            catch (Exception ex)
            {
                //await JS.Notify($"Error", $"{ex.Message}", Gen.MensajeToastTipo.error, 1);
            }

        }


        /// <summary>
        /// Guardo en la base de datos los datos capturados
        /// Insero o actualizo
        /// </summary>
        /// <returns></returns>
        public async Task detGuarda(TabPage tabPage)
        {
            bool bExito = await detValida();

            _Reg.Nuevo_Editar = txtEditarNuevo.Text;

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas {(_Reg.Nuevo_Editar == "0" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(_Reg.Nuevo_Editar == "0" ? "guardar" : "actualizar")} el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();
            if (_Reg.Nuevo_Editar == "0")
            {

                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO REGLA
                                    (
                                        BANCO,
                                        CUENTA,
                                        ORDEN,
                                        DESCR,
                                        VALIDA,
                                        QUERY,
                                        REGRESA_QUERY
                                        )
                                VALUES (
                                        '{_Reg.BANCO}',
                                        '{_Reg.CUENTA}',
                                        '{_Reg.ORDEN}',
                                        '{_Reg.DESCR}',
                                        '{_Reg.VALIDA}',
                                        '{_Reg.QUERY}',
                                        '{_Reg.REGRESA_QUERY}'
                                        )
                                ";
            }
            else
            {

                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE REGLA
                                    SET BANCO = '{_Reg.BANCO}',
                                        CUENTA = '{_Reg.CUENTA}',
                                        ORDEN = '{_Reg.ORDEN}',
                                        DESCR = '{_Reg.DESCR}',
                                        VALIDA = '{_Reg.VALIDA}',
                                        QUERY = '{_Reg.QUERY}',
                                        REGRESA_QUERY = '{_Reg.REGRESA_QUERY}'
                                WHERE BANCO = '{_Reg.BANCO}' AND CUENTA = '{_Reg.CUENTA}' AND ORDEN = '{_Reg.ORDEN}'
                             ";


            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Err == 0)
                {
                    tabPage.Text = $"Regla {_Reg.BANCO.ToString()}{_Reg.CUENTA.ToString()}{_Reg.ORDEN.ToString()}";
                    tabPage.Name = $"{_Reg.BANCO.ToString()}{_Reg.CUENTA.ToString()}{_Reg.ORDEN.ToString()}";
                    Console.WriteLine(oReq.Query);
                    await _Main.Status($"Registro {_Reg.DESCR} guardado correctamente", (int)MensajeTipo.Success);
                        if (txtEditarNuevo.Text == "0") {
                        txtEditarNuevo.Text = _Reg.BANCO.ToString();
                        comboBanco.Enabled = false;
                        comboCuenta.Enabled = false;
                        txtOrden.Enabled = false;
                    }
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

            if (string.IsNullOrWhiteSpace(comboBanco.Text))
                sErr += "Seleccione el banco. ";
                errorProvider1.SetError(comboBanco, "Seleccione el banco. ");

            if (string.IsNullOrWhiteSpace(comboCuenta.Text))
                sErr += "Seleccione la cuenta. ";
            errorProvider1.SetError(comboCuenta, "Seleccione la cuenta. ");

            if (!txtOrden.Text.IsDecimal() || txtOrden.Text.Length > 2)
                sErr += "Debes de ingresar el orden, solo se permiten números, maximo 2 dígitos. ";
            errorProvider1.SetError(txtOrden, "Debes de ingresar el orden, solo se permiten números, maximo 2 dígitos.");

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                sErr += "Ingrese la descripción. ";
            errorProvider1.SetError(txtDescripcion, "Ingrese la descripción.");

            if (string.IsNullOrWhiteSpace(txtValidacion.Text))
                sErr += "Ingrese la validación. ";
            errorProvider1.SetError(txtValidacion, "Ingrese la validación.");

            if (string.IsNullOrWhiteSpace(txtQuery.Text))
                sErr += "Ingrese el query. ";
            errorProvider1.SetError(txtQuery, "Ingrese el query.");

            if (string.IsNullOrWhiteSpace(txtTipoDatoRegresa.Text))
                sErr += "Ingrese el tipo de dato regresa. ";
            errorProvider1.SetError(txtTipoDatoRegresa, "Ingrese el tipo de dato regresa.");

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                _Reg.BANCO = int.Parse(comboBanco.SelectedValue.ToString());
                _Reg.CUENTA = int.Parse(comboCuenta.SelectedValue.ToString());
                _Reg.ORDEN = int.Parse(txtOrden.Text);
                _Reg.DESCR = txtDescripcion.Text;
                _Reg.VALIDA = txtValidacion.Text;
                _Reg.QUERY = txtQuery.Text;
                _Reg.REGRESA_QUERY = txtTipoDatoRegresa.Text;

            }

            return bExito;
        }


        /// <summary>
        /// Borro el registro en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task detBorra()
        {

            _Reg.Nuevo_Editar = txtEditarNuevo.Text;

            if (_Reg.Nuevo_Editar == "0") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE REGLA
                                WHERE BANCO = {_Reg.BANCO} AND CUENTA = {_Reg.CUENTA} AND ORDEN = {_Reg.ORDEN}
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Err == 0)
                {
                    await limpiarCampos();
                    await _Main.Status($"Registro eliminado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);
                }

            }

            Cursor = Cursors.Default;
        }

        private async Task limpiarCampos()
        {
            comboBanco.Enabled = true;
            comboCuenta.Enabled = true;
            txtOrden.Enabled = true;
            comboBanco.SelectedIndex = -1;
            comboCuenta.SelectedIndex = -1;
            comboCuenta.DataSource = null;
            comboCuenta.Items.Clear();
            txtOrden.Text = ""; ;
            txtDescripcion.Text = "";
            txtValidacion.Text = "";
            txtQuery.Text = "";
            txtTipoDatoRegresa.Text = "";
            txtEditarNuevo.Text = "0";
        }


        private async Task detPresenta()
        {

            comboBanco.SelectedValue = _Reg.BANCO.ToString();
            txtOrden.Text = _Reg.ORDEN.ToString(); ;
            txtDescripcion.Text = _Reg.DESCR;
            txtValidacion.Text = _Reg.VALIDA;
            txtQuery.Text = _Reg.QUERY;
            txtTipoDatoRegresa.Text = _Reg.REGRESA_QUERY;
        }

        #endregion


        private void rxrPosiciones_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
