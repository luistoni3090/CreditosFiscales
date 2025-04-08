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
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    public partial class ctrlCuentasBancarias : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eCUENTASBANCARIAS _Reg = new eCUENTASBANCARIAS();
        eCUENTASBANCARIAS2 _Reg2 = new eCUENTASBANCARIAS2();
        eValida _Reg3 = new eValida();

        #region Constuctores
        public ctrlCuentasBancarias()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlCuentasBancarias(xMain oMain, Int32 ID, Int32 CUENTA ,string valida)
        {

            

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.BANCO = ID;
            _Reg.CUENTA = CUENTA;
            txtCuenta.Text = CUENTA.ToString();
            txtValida.Text = valida;
            detActualiza(ID,CUENTA);
        }

        private async Task Inicio()
        {


        }
        #endregion

        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eCUENTASBANCARIAS();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(Int32 ID,Int32 CUENTA)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            comboMunicipio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboDelegacion.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTipoCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCtaContable.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSubCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSubSubCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            txtCuenta.Visible = false;
            txtValida.Visible = false;
            comboSubSubCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            await detNuevo();
            await detBusca(ID,CUENTA);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {ID} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            return;
            await detBusca((Int32)_Reg2.BANCO, (Int32)_Reg2.CUENTA);
        }

        #region "Carga Combo Banco"
        private async Task cargaComboBanco()
        {

            try
            {
                oReq.Query = "SELECT BANCO IDBanco, LPAD(BANCO, 2, '0') BANCO, DESCR FROM BANCO ORDER BY BANCO";
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
                            Value = row["IDBanco"].ToString(),
                            Text = "(" + row["BANCO"].ToString() + ") " + row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboBanco.DataSource = comboList;
                    comboBanco.DisplayMember = "Text";
                    comboBanco.ValueMember = "Value";
                    comboBanco.Enabled = false;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Carga Combo Municipio"
        private async Task cargaComboMunicipio()
        {

            comboMunicipio.SelectedIndexChanged -= new EventHandler(comboMunicipio_SelectedIndexChanged);

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

                    comboMunicipio.SelectedIndexChanged += new EventHandler(comboMunicipio_SelectedIndexChanged);

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        private async void comboMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado
            string IdMunicipio = comboMunicipio.SelectedValue.ToString();
            await cargaComboDelegacion(IdMunicipio);


        }


        #region "Carga Combo Delegacion"
        private async Task cargaComboDelegacion(string IdMunicipio)
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "select DEL IdDelegacion, DESCR FROM DELEGACION WHERE MPO = "+ IdMunicipio + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables.Count > 0)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IdDelegacion"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboDelegacion.DataSource = comboList;
                    comboDelegacion.DisplayMember = "Text";
                    comboDelegacion.ValueMember = "Value";

                }
                else {
                    comboDelegacion.DataSource = null;
                    comboDelegacion.Items.Clear();
                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region "Carga Combo Tipo cuenta"
        private async Task cargaComboTipoCuenta()
        {

            try
            {
                oReq.Query = "SELECT TIPO_CUENTA IdTipoCuenta, DESCR FROM TIPO_CUENTA";
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
                            Value = row["IdTipoCuenta"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboTipoCuenta.DataSource = comboList;
                    comboTipoCuenta.DisplayMember = "Text";
                    comboTipoCuenta.ValueMember = "Value";

                    comboTipoCuenta.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        private async Task cargaComboMoneda()
        {

            var comboList = new List<ComboBoxItem>();

                var item1 = new ComboBoxItem
                {
                    Value = "P",
                    Text = "Pesos"
                };
                var item2 = new ComboBoxItem
                {
                    Value = "D",
                    Text = "Dólares"
                };
            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboMoneda.DataSource = comboList;
            comboMoneda.DisplayMember = "Text";
            comboMoneda.ValueMember = "Value";
            comboMoneda.SelectedIndex = -1;

        }


        #region "Carga Combo Cta Contable"
        private async Task cargaComboCtaContable()
        {

            comboCtaContable.SelectedIndexChanged -= new EventHandler(comboCtaContable_SelectedIndexChanged);

            try
            {
                oReq.Query = "SELECT CTACONTABLE, DESCR FROM CTACONTABLE";
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
                            Value = row["CTACONTABLE"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCtaContable.DataSource = comboList;
                    comboCtaContable.DisplayMember = "Text";
                    comboCtaContable.ValueMember = "Value";
                    comboCtaContable.SelectedIndex = -1;

                    comboCtaContable.SelectedIndexChanged += new EventHandler(comboCtaContable_SelectedIndexChanged);

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        private async void comboCtaContable_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado
            string IdCtaContable = "0";
            if (comboCtaContable.SelectedValue != null) {
                IdCtaContable = comboCtaContable.SelectedValue.ToString();
            }
            comboSubSubCuenta.SelectedIndex = -1;
            comboSubSubCuenta.DataSource = null;
            comboSubSubCuenta.Items.Clear();
            await cargaComboSubcuenta(IdCtaContable);
            

        }

        #region "Carga Combo Sub cuenta"
        private async Task cargaComboSubcuenta(string IdCtaContable)
        {

            comboSubCuenta.SelectedIndexChanged -= new EventHandler(comboSubCuenta_SelectedIndexChanged);

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT CTACONTABLE, SUBCTA, DESCR FROM SUBCUENTA WHERE CTACONTABLE = "+ IdCtaContable + "";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables.Count > 0)
                {
                    var dataTable = oRes.Data.Tables[0];

                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["SUBCTA"].ToString(),
                            Text = "(" + row["SUBCTA"].ToString() + ") " + row["DESCR"].ToString() + ""
                        };
                        comboList.Add(item);
                    }
                    comboSubCuenta.DataSource = comboList;
                    comboSubCuenta.DisplayMember = "Text";
                    comboSubCuenta.ValueMember = "Value";
                    if (txtValida.Text == "Nuevo")
                    {
                        comboSubCuenta.SelectedIndex = -1;
                        comboSubCuenta.SelectedIndexChanged += new EventHandler(comboSubCuenta_SelectedIndexChanged);
                    }
                    else
                    {
                        if (_Reg2.ValidaCombos == "1")
                        {
                            comboSubCuenta.SelectedIndex = -1;
                            comboSubCuenta.SelectedIndexChanged += new EventHandler(comboSubCuenta_SelectedIndexChanged);
                            comboSubCuenta.SelectedValue = _Reg2.SUBCTA;
                        }
                        else
                        {
                            comboSubCuenta.SelectedIndex = -1;
                            comboSubCuenta.SelectedIndexChanged += new EventHandler(comboSubCuenta_SelectedIndexChanged);
                        }

                    }
                }
                else {
                    comboSubCuenta.DataSource = null;
                    comboSubCuenta.Items.Clear(); ;
                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        private async void comboSubCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdCtaContable = "0";
            string IdSubCuenta = "0";
            if (comboCtaContable.SelectedValue != null) {
                 IdCtaContable = comboCtaContable.SelectedValue.ToString();
            }

            if (comboSubCuenta.SelectedValue != null) {
                 IdSubCuenta = comboSubCuenta.SelectedValue.ToString();
            }
                
            await cargaComboSubSubcuenta(IdSubCuenta, IdCtaContable);

        }

        #region "Carga Combo Sub Sub cuenta"
        private async Task cargaComboSubSubcuenta(string IdSubCuenta, string IdCtaContable)
        {

            try
            {
                oReq.Query = "SELECT CTACONTABLE, SUBCTA, SUBSUBCTA, DESCR FROM SUB_SUBCTA WHERE CTACONTABLE = "+ IdCtaContable + " AND SUBCTA = "+ IdSubCuenta + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables.Count > 0)
                {
                    var dataTable = oRes.Data.Tables[0];

                    var comboList = new List<ComboBoxItem>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["SUBSUBCTA"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    comboSubSubCuenta.DataSource = comboList;
                    comboSubSubCuenta.DisplayMember = "Text";
                    comboSubSubCuenta.ValueMember = "Value";
                    if (txtValida.Text == "Nuevo")
                    {
                        comboSubSubCuenta.SelectedIndex = -1;
                    }
                    else
                    {
                        if (_Reg2.ValidaCombos == "1")
                        {
                            comboSubSubCuenta.SelectedValue = _Reg2.SUBSUBCTA;
                            _Reg2.ValidaCombos = "";
                        }
                        else
                        comboSubSubCuenta.SelectedIndex = -1;

                    }
                }
                else
                {
                    comboSubSubCuenta.DataSource = null;
                    comboSubSubCuenta.Items.Clear(); ;
                }

            }
            catch (Exception ex)
            { }

        }
        #endregion


        private async Task detBusca(Int32 ID, Int32 CUENTA)
        {

            await cargaComboBanco();
            await cargaComboMunicipio();
            await cargaComboTipoCuenta();
            await cargaComboMoneda();
            await cargaComboCtaContable();

            comboBanco.SelectedValue = ID.ToString();
            if (txtValida.Text == "Nuevo")
            {
                
                //await detPresenta();

                return;
            }
            else
            {
                try
                {

                    oReq.Query = "SELECT CTA.BANCO, CTA.CUENTA, MU.MPO, CTA.DEL, CTA.DESCR, CTA.TIPO, " +
                       " CTA.MONEDA, CTA.CTACONTABLE, CTA.SUBCTA, CTA.SUBSUBCTA " +
                        "From CUENTA CTA " +
                        "LEFT JOIN MUNICIPIO MU ON (CTA.MPO = MU.MPO) " +
                        "LEFT JOIN TIPO_CUENTA TCTA ON (CTA.TIPO = TCTA.TIPO_CUENTA)" +
                        "WHERE CTA.BANCO = :BANCO AND CUENTA = :CUENTA ";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "BANCO", Valor = ID},
                    new eParametro(){ Tipo = DbType.String, Nombre = "CUENTA", Valor = CUENTA}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg2 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eCUENTASBANCARIAS2>();

                    _Reg2.ValidaCombos = "1";
                    txtValida.Text = "Editar";
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
        public async Task detBorra()
        {

            if (txtValida.Text == "Nuevo") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg2.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg2.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg2.DESCR}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg2.DESCR}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;

            _Reg2.CUENTA = int.Parse(txtCuenta.Text);
            
            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE CUENTA
                                WHERE CUENTA = {_Reg2.CUENTA} AND BANCO = {_Reg2.BANCO}
                             ";
            
            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
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

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} la cuenta {_Reg2.DESCR}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} la cuenta {_Reg2.DESCR}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO CUENTA
                                    (
                                        BANCO,
                                        CUENTA,
                                        MPO,
                                        DEL,
                                        DESCR,
                                        TIPO,
                                        MONEDA,
                                        CTACONTABLE,
                                        SUBCTA,
                                        SUBSUBCTA
                                        )
                                VALUES
                                        (
                                        '{_Reg2.BANCO}',
                                        (SELECT NVL(MAX(CUENTA), 0) + 1 FROM CUENTA),
                                        '{_Reg2.MPO}',
                                        '{_Reg2.DEL}',
                                        '{_Reg2.DESCR}',
                                        '{_Reg2.TIPO}',
                                        '{_Reg2.MONEDA}',
                                        '{_Reg2.CTACONTABLE}',
                                        '{_Reg2.SUBCTA}',
                                        '{_Reg2.SUBSUBCTA}'
                                        )
                                ";
            }
            else
            {
                _Reg2.CUENTA = int.Parse(txtCuenta.Text);
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE CUENTA
                                    SET MPO = '{_Reg2.MPO}',
                                        DEL = '{_Reg2.DEL}',
                                        DESCR = '{_Reg2.DESCR}',
                                        TIPO = '{_Reg2.TIPO}',
                                        MONEDA = '{_Reg2.MONEDA}',
                                        CTACONTABLE = '{_Reg2.CTACONTABLE}',
                                        SUBCTA = '{_Reg2.SUBCTA}',
                                        SUBSUBCTA = '{_Reg2.SUBSUBCTA}'
                                WHERE BANCO = '{_Reg2.BANCO}' AND CUENTA = '{_Reg2.CUENTA}'
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    tabPage.Text = $"CUENTA {_Reg2.DESCR}";
                    tabPage.Name = $"{_Reg2.DESCR}";

                    if (txtValida.Text == "Nuevo") {
                        oReq.Tipo = 0;
                        oReq.Query = $@"SELECT MAX(CUENTA) AS CuentaId FROM CUENTA";
                        var oRes2 = await WSServicio.Servicio(oReq);

                        if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                        {
                            int maxId = Convert.ToInt32(oRes2.Data.Tables[0].Rows[0][0]);
                            txtCuenta.Text = (maxId.ToString());
                        }
                    }

                    txtValida.Text = "Editar";

                    await _Main.Status($"Registro {_Reg2.DESCR} guardado correctamente", (int)MensajeTipo.Success);
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

            if (string.IsNullOrWhiteSpace(txtCuentaBancaria.Text))
                sErr += "Debes de ingresar la cuenta bancaria. ";
            errorProvider1.SetError(txtCuentaBancaria, "Debes de ingresar la cuenta bancaria.");

            if (string.IsNullOrWhiteSpace(comboMunicipio.Text))
                sErr += "Debes seleccionar el municipio. ";
            errorProvider1.SetError(comboMunicipio, "Debes seleccionar el municipio.");

            if (string.IsNullOrWhiteSpace(comboDelegacion.Text))
                sErr += "Debes seleccionar la delegación. ";
            errorProvider1.SetError(comboDelegacion, "Debes seleccionar la delegación.");

            if (string.IsNullOrWhiteSpace(comboTipoCuenta.Text))
                sErr += "Debes seleccionar el tipo de cuenta. ";
            errorProvider1.SetError(comboTipoCuenta, "Debes seleccionar el tipo de cuenta.");

            if (string.IsNullOrWhiteSpace(comboMoneda.Text))
                sErr += "Debes de seleccionar la moneda. ";
            errorProvider1.SetError(comboMoneda, "Debes de seleccionar la moneda.");

            if (string.IsNullOrWhiteSpace(comboCtaContable.Text))
                sErr += "Debes ingresar la cuenta contable. ";
            errorProvider1.SetError(comboCtaContable, "Debes ingresar la cuenta contable.");

            if (string.IsNullOrWhiteSpace(comboSubCuenta.Text))
                sErr += "Debes ingrear la subcuenta. ";
            errorProvider1.SetError(comboSubCuenta, "Debes ingrear la subcuenta.");

            if (string.IsNullOrWhiteSpace(comboSubSubCuenta.Text))
                sErr += "Debes ingrear la Sub subcuenta. ";
            errorProvider1.SetError(comboSubSubCuenta, "Debes ingrear la Sub subcuenta.");

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {

                _Reg2.BANCO = int.Parse(comboBanco.SelectedValue.ToString());
                _Reg2.MPO = comboMunicipio.SelectedValue.ToString();
                _Reg2.DEL = comboDelegacion.SelectedValue.ToString();   
                _Reg2.DESCR = txtCuentaBancaria.Text;
                _Reg2.TIPO = comboTipoCuenta.SelectedValue.ToString();
                _Reg2.MONEDA = comboMoneda.SelectedValue.ToString();
                _Reg2.CTACONTABLE = comboCtaContable.SelectedValue.ToString();
                _Reg2.SUBCTA = comboSubCuenta.SelectedValue.ToString();
                _Reg2.SUBSUBCTA = comboSubSubCuenta.SelectedValue.ToString();


            }

            return bExito;
        }


        private async Task detPresenta()
        {
            comboMunicipio.SelectedValue = _Reg2.MPO.ToString();
            comboDelegacion.SelectedValue = _Reg2.DEL.ToString();
            txtCuentaBancaria.Text = _Reg2.DESCR;
            comboTipoCuenta.SelectedValue = _Reg2.TIPO;
            comboMoneda.SelectedValue = _Reg2.MONEDA;
            comboCtaContable.SelectedValue = _Reg2.CTACONTABLE;
            txtCuenta.Text = _Reg2.CUENTA.ToString();
        }

        #endregion

        private async Task limpiaCampos()
        {

            comboCtaContable.SelectedIndexChanged -= new EventHandler(comboCtaContable_SelectedIndexChanged);
            comboSubCuenta.SelectedIndexChanged -= new EventHandler(comboSubCuenta_SelectedIndexChanged);
            comboMunicipio.SelectedIndexChanged -= new EventHandler(comboMunicipio_SelectedIndexChanged);

            comboMunicipio.DataSource = null;
            comboMunicipio.Items.Clear();
            comboDelegacion.DataSource = null;
            comboDelegacion.Items.Clear();
            txtCuentaBancaria.Text = "";
            comboTipoCuenta.DataSource = null;
            comboTipoCuenta.Items.Clear();
            comboMoneda.DataSource = null;
            comboMoneda.Items.Clear();
            comboCtaContable.DataSource = null;
            comboCtaContable.Items.Clear();
            comboSubCuenta.DataSource = null;
            comboSubCuenta.Items.Clear();
            comboSubSubCuenta.DataSource = null;
            comboSubSubCuenta.Items.Clear();

            txtCuenta.Text = "0";

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
