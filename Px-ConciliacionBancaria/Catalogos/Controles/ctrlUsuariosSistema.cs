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
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;

namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    public partial class ctrlUsuariosSistema : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eUSUARIOSISTEMA _Reg = new eUSUARIOSISTEMA();

        #region Constuctores
        public ctrlUsuariosSistema()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlUsuariosSistema(xMain oMain, string ID)
        {
            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.APP_LOGIN = ID;
            _Reg.Nuevo_Editar = ID;
            txtEditarNuevo.Visible = false;
            detActualiza(ID);
        }

        private async Task Inicio()
        {


        }
        #endregion


        #region "Carga Combo Municipio"
        private async Task cargaComboMunicipio()
        {

            try
            {
                oReq.Query = "SELECT MPO IdMunicipio, DESCR FROM MUNICIPIO";
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
                            Value = row["IdMunicipio"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboMunicipio.DataSource = comboList;
                    comboMunicipio.DisplayMember = "Text";
                    comboMunicipio.ValueMember = "Value";
                    comboMunicipio.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Carga Combo SysUser"
        private async Task cargaComboUsuarios()
        {

            try
            {
                oReq.Query = "SELECT SYSUSER FROM DGI_USR_BD";
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
                            Value = row["SYSUSER"].ToString(),
                            Text = row["SYSUSER"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboUsuarios.DataSource = comboList;
                    comboUsuarios.DisplayMember = "Text";
                    comboUsuarios.ValueMember = "Value";
                    comboUsuarios.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eUSUARIOSISTEMA();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(string ID)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            comboMunicipio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboUsuarios.DropDownStyle = ComboBoxStyle.DropDownList;
            await detNuevo();
            await detBusca(ID);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {ID} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            if(txtEditarNuevo.Text != "0")
            await detBusca(_Reg.APP_LOGIN);
        }

        private async Task detBusca(string ID)
        {
            await cargaComboMunicipio();
            await cargaComboUsuarios();
            txtEditarNuevo.Text = ID;

            if (ID == "0")
                return;

            try
            {

                oReq.Query = "SELECT APP_LOGIN, AP_PATER, AP_MATER, NOMBRE, MU.DESCR AS MUNICIPIO, USR.MPO, " +
                             "APP_PWD, RFC, SYSUSER  from DGI_USR USR " +
                             "LEFT JOIN MUNICIPIO MU ON (USR.MPO = MU.MPO) " +
                             "WHERE APP_LOGIN = :ID";
                oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "APP_LOGIN", Valor = ID}
                };

                var oRes = await WSServicio.Servicio(oReq);

                if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                    _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eUSUARIOSISTEMA>();


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

            _Reg.Nuevo_Editar = txtEditarNuevo.Text;

            if (_Reg.Nuevo_Editar == "0") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg.NOMBRE}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg.NOMBRE}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg.NOMBRE}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg.NOMBRE}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            _Reg.APP_LOGIN = txtEditarNuevo.Text;

           oReq.Tipo = 1;
           oReq.Query = $@" DELETE DGI_USR 
                            WHERE APP_LOGIN = '{_Reg.APP_LOGIN}' ";

           var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    await detNuevo();
                    await detPresenta();
                    txtLogin.Enabled = true;
                    txtEditarNuevo.Text = "0";
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

            _Reg.Nuevo_Editar = txtEditarNuevo.Text;

            if (!bExito)
                return;

            await _Main.Status($"¿Deseas {(_Reg.Nuevo_Editar == "0" ? "guardar" : "actualizar")} el registro {_Reg.NOMBRE}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(_Reg.Nuevo_Editar == "0" ? "guardar" : "actualizar")} el registro {_Reg.NOMBRE}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();
            if (_Reg.Nuevo_Editar == "0")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO DGI_USR
                                    (
                                        APP_LOGIN,
                                        AP_PATER,
                                        AP_MATER,
                                        NOMBRE,
                                        MPO,
                                        APP_PWD,
                                        RFC,
                                        SYSUSER
                                        )
                                VALUES (
                                        '{_Reg.APP_LOGIN}',
                                        '{_Reg.AP_PATER}',
                                        '{_Reg.AP_MATER}',
                                        '{_Reg.NOMBRE}',
                                        '{_Reg.MPO}',
                                        '{_Reg.APP_PWD}',
                                        '{_Reg.RFC}',
                                        '{_Reg.SYSUSER}'
                                        )
                                ";
            }
            else
            {
               
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE DGI_USR
                                    SET AP_PATER = '{_Reg.AP_PATER}',
                                        AP_MATER = '{_Reg.AP_MATER}',
                                        NOMBRE = '{_Reg.NOMBRE}',
                                        MPO = '{_Reg.MPO}',
                                        APP_PWD = '{_Reg.APP_PWD}',
                                        RFC = '{_Reg.RFC}',
                                        SYSUSER = '{_Reg.SYSUSER}'
                                WHERE APP_LOGIN = '{_Reg.APP_LOGIN}'
                             ";

            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    tabPage.Text = $"Usuario {_Reg.APP_LOGIN}";
                    tabPage.Name = $"{_Reg.APP_LOGIN}";
                    Console.WriteLine(oReq.Query);
                    txtEditarNuevo.Text = _Reg.APP_LOGIN;
                    txtLogin.Enabled = false;
                    await _Main.Status($"Registro {_Reg.NOMBRE} guardado correctamente", (int)MensajeTipo.Success);
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

            if (string.IsNullOrWhiteSpace(txtLogin.Text))
                sErr += "Ingrese datos para el login. ";
            errorProvider1.SetError(txtLogin, "Ingrese datos para el login.");

            if (string.IsNullOrWhiteSpace(txtApePaterno.Text))
                sErr += "Ingrese el apellido paterno. ";
            errorProvider1.SetError(txtApePaterno, "Ingrese el apellido paterno.");

            if (string.IsNullOrWhiteSpace(txtApeMaterno.Text))
                sErr += "Ingrese el apellido materno. ";
            errorProvider1.SetError(txtApeMaterno, "Ingrese el apellido materno.");

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                sErr += "Ingrese el nombre. ";
            errorProvider1.SetError(txtNombre, "Ingrese el nombre.");

            if (string.IsNullOrWhiteSpace(comboMunicipio.Text))
                sErr += "Seleccione el municipio. ";
            errorProvider1.SetError(comboMunicipio, "Seleccione el municipio.");

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                sErr += "Ingrese el password. ";
            errorProvider1.SetError(txtPassword, "Ingrese el password.");

            if (string.IsNullOrWhiteSpace(txtRfc.Text))
                sErr += "Ingrese el Rfc. ";
            errorProvider1.SetError(txtRfc, "Ingrese el Rfc.");

            if (string.IsNullOrWhiteSpace(comboUsuarios.Text))
                sErr += "Ingrese el usuario. ";
            errorProvider1.SetError(comboUsuarios, "Ingrese el usuario.");

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {
                _Reg.APP_LOGIN = txtLogin.Text;
                _Reg.AP_PATER = txtApePaterno.Text;
                _Reg.AP_MATER = txtApeMaterno.Text;
                _Reg.NOMBRE = txtNombre.Text;
                _Reg.MPO = comboMunicipio.SelectedValue.ToString();
                _Reg.APP_PWD = txtPassword.Text;
                _Reg.RFC = txtRfc.Text;
                _Reg.SYSUSER = comboUsuarios.SelectedValue.ToString();

            }

            return bExito;
        }




        private async Task detPresenta()
        {
            if (txtEditarNuevo.Text != "0")
                txtLogin.Enabled = false;

            txtLogin.Text = _Reg.APP_LOGIN;
            txtApePaterno.Text = _Reg.AP_PATER;
            txtApeMaterno.Text = _Reg.AP_MATER;
            txtNombre.Text = _Reg.NOMBRE;
            comboMunicipio.SelectedValue = _Reg.MPO.ToString();
            txtPassword.Text = _Reg.APP_PWD;
            txtRfc.Text = _Reg.RFC;
            comboUsuarios.SelectedValue = _Reg.SYSUSER.ToString();
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
