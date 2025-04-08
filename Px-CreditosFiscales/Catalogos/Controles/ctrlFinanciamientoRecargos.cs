using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_CreditosFiscales.Utiles.Generales;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;

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
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Globalization;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Px_CreditosFiscales.Catalogos.Controles
{
    public partial class ctrlFinanciamientoRecargos : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eFINANC_ESTATAL _Reg = new eFINANC_ESTATAL();
        eFINANC_FEDERAL _Reg2 = new eFINANC_FEDERAL();

        #region Constuctores
        public ctrlFinanciamientoRecargos()
        {
            InitializeComponent();
        }

        public ctrlFinanciamientoRecargos(xMain oMain, string ANIO, string MES, string TIPO, string valida)
        {

            InitializeComponent();

            _Main = oMain;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg.ANIO = ANIO;
            _Reg.MES = MES;
            txtValida.Text = valida;
            detActualiza(ANIO, MES, TIPO);
        }


        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eFINANC_ESTATAL();
            _Reg2 = new eFINANC_FEDERAL();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task cargaComboTipoCredito()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "E",
                Text = "ESTATAL"
            };
            var item2 = new ComboBoxItem
            {
                Value = "F",
                Text = "FEDERAL"
            };

            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboTipoCredito.DataSource = comboList;
            comboTipoCredito.DisplayMember = "Text";
            comboTipoCredito.ValueMember = "Value";
            comboTipoCredito.SelectedIndex = 0;
        }



        private async Task detActualiza(string ANIO, string MES, string TIPO)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            await detNuevo();
            await detBusca(ANIO, MES,TIPO);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {ANIO}-{MES} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            string TIPO = comboTipoCredito.SelectedValue.ToString();
            if (TIPO == "E")
                await detBusca(_Reg.ANIO, _Reg.MES, TIPO);
            else if (TIPO == "F")
                await detBusca(_Reg2.ANIO, _Reg2.MES, TIPO);

        }


        private async Task detBusca(string ANIO, string MES, string TIPO)
        {

            await cargaComboTipoCredito();

            if (txtValida.Text == "Nuevo")
            {

                await limpiaCampos();
                return;
            }
            else
            {
                try
                {

                    if (TIPO == "E")
                    {


                        oReq.Tipo = 0;
                        oReq.Query = @" SELECT 
	                                        ANIO,
	                                        MES,
	                                        IND_FINANC,
	                                        IND_RECAR,
	                                        TO_CHAR(FEC_PUBLICA_DOF, 'YYYY-MM-DD') AS FEC_PUBLICA_DOF,
	                                        IND_RECAR_DIARIO
                                    FROM INDICE_FINAN_EST
                                    WHERE ANIO = :ANIO AND MES = :MES";
                        oReq.Parametros = new List<eParametro>
                        {
                            new eParametro(){ Tipo = DbType.String, Nombre = "ANIO", Valor = ANIO},
                            new eParametro(){ Tipo = DbType.String, Nombre = "MES", Valor = MES}
                        };

                        var oRes = await WSServicio.Servicio(oReq);

                        if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                            _Reg = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eFINANC_ESTATAL>();

                    }
                    else if (TIPO == "F")
                    {
                        oReq.Tipo = 0;
                        oReq.Query = @" SELECT  
                                            ANIO, 
		                                    MES,
		                                    IND_FINANC,
		                                    IND_RECAR,
		                                    TO_CHAR(FEC_PUBLICA_DOF, 'YYYY-MM-DD') AS FEC_PUBLICA_DOF,
		                                    IND_FED_MINIMO,
		                                    IND_RECAR_MINIMO
                                    FROM INDICE_FINAN_FED
                                    WHERE ANIO = :ANIO AND MES = :MES";
                        oReq.Parametros = new List<eParametro>
                        {
                            new eParametro(){ Tipo = DbType.String, Nombre = "ANIO", Valor = ANIO},
                            new eParametro(){ Tipo = DbType.String, Nombre = "MES", Valor = MES}
                        };

                        var oRes = await WSServicio.Servicio(oReq);

                        if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                            _Reg2 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eFINANC_FEDERAL>();
                    }

                    txtValida.Text = "Editar";
                    await detPresenta(TIPO);

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
        public async Task detBorra(TabPage tabPage)
        {

            string TIPO = comboTipoCredito.SelectedValue.ToString();

            if (TIPO == "E")
            {
                if (txtValida.Text == "Nuevo") return;
                await _Main.Status($"¿Deseas borrar el registro con año {_Reg.ANIO} y mes {_Reg.MES}?", (int)MensajeTipo.Question);
                if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro con año {_Reg.ANIO} y mes {_Reg.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro con año {_Reg.ANIO} y mes {_Reg.MES}?", (int)MensajeTipo.Error);
                if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que deseas borrar el registro {_Reg.ANIO}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            if (TIPO == "F")
            {
                if (txtValida.Text == "Nuevo") return;
                await _Main.Status($"¿Deseas borrar el registro con año {_Reg2.ANIO} y mes {_Reg2.MES}?", (int)MensajeTipo.Question);
                if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro con año {_Reg2.ANIO} y mes {_Reg2.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro con año {_Reg2.ANIO} y mes {_Reg2.MES}?", (int)MensajeTipo.Error);
                if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que deseas borrar el registro {_Reg2.ANIO}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

                Cursor = Cursors.AppStarting;

            if (TIPO == "E")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                DELETE INDICE_FINAN_EST
                                WHERE ANIO = {_Reg.ANIO} AND MES = {_Reg.MES} ";
            }
            else if (TIPO == "F")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                DELETE INDICE_FINAN_FED
                                WHERE ANIO = {_Reg2.ANIO} AND MES = {_Reg2.MES} ";
            }
                

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "Nuevo";
                    tabPage.Name = $"000";
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
            string TIPO = comboTipoCredito.SelectedValue.ToString();

            bool bExito = await detValida(TIPO);

            if (txtValida.Text == "Nuevo")
            {

                bool existe = await conValidaSiExisteRegistro(TIPO);

                if (existe)
                {
                    if (TIPO == "E")
                        await _Main.Status($"Ya existe un Financiamiento para el año {_Reg.ANIO} y el Mes {_Reg.MES}", (int)MensajeTipo.Error);
                    if (TIPO == "F")
                        await _Main.Status($"Ya existe un Financiamiento para el año {_Reg2.ANIO} y el Mes {_Reg2.MES}", (int)MensajeTipo.Error);

                    return;
                }

            }

            if (!bExito)
                return;

            if (TIPO == "E") { 
                await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}?", (int)MensajeTipo.Question);
                if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Año {_Reg.ANIO} y Mes {_Reg.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            else if (TIPO == "F")
            {
                await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Año {_Reg2.ANIO} y Mes {_Reg2.MES}?", (int)MensajeTipo.Question);
                if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro con Año {_Reg2.ANIO} y Mes {_Reg2.MES}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {

                if (TIPO == "E")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                                INSERT INTO INDICE_FINAN_EST
                                    (
                                        ANIO,
	                                    MES,
	                                    IND_FINANC,
	                                    IND_RECAR,
	                                    FEC_PUBLICA_DOF,
	                                    IND_RECAR_DIARIO
                                        )
                                VALUES (
                                        {_Reg.ANIO},
                                        {_Reg.MES},
                                        {_Reg.IND_FINANC},
                                        {_Reg.IND_RECAR},  
                                        TO_DATE('{_Reg.FEC_PUBLICA_DOF}','YYYY-MM-DD'),
                                        {_Reg.IND_RECAR_DIARIO}
                                        )
                                ";
                }
                else if (TIPO == "F")
                {
                    oReq.Tipo = 1;
                    oReq.Query = $@"
                                INSERT INTO INDICE_FINAN_FED
                                    (
                                        ANIO,
	                                    MES,
	                                    IND_FINANC,
	                                    IND_RECAR,
	                                    FEC_PUBLICA_DOF,
	                                    IND_FED_MINIMO,
                                        IND_RECAR_MINIMO
                                        )
                                VALUES (
                                        {_Reg2.ANIO},
                                        {_Reg2.MES},
                                        {_Reg2.IND_FINANC},
                                        {_Reg2.IND_RECAR},  
                                        TO_DATE('{_Reg2.FEC_PUBLICA_DOF}','YYYY-MM-DD'),
                                        {_Reg2.IND_FED_MINIMO},
                                        {_Reg2.IND_RECAR_MINIMO}
                                        )
                                ";
                }
            }
            else
            {
                if (TIPO == "E")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                                UPDATE INDICE_FINAN_EST
                                    SET 
                                        IND_FINANC = {_Reg.IND_FINANC},
                                        IND_RECAR = {_Reg.IND_RECAR},
                                        FEC_PUBLICA_DOF = TO_DATE('{_Reg.FEC_PUBLICA_DOF}','YYYY-MM-DD'),
                                        IND_RECAR_DIARIO = {_Reg.IND_RECAR_DIARIO}
                                WHERE ANIO = {_Reg.ANIO} AND MES = {_Reg.MES}";
                }
                else if (TIPO == "F")
                {
                    oReq.Tipo = 1;
                    oReq.Query = $@"
                                UPDATE INDICE_FINAN_FED
                                    SET 
                                        IND_FINANC = {_Reg2.IND_FINANC},
                                        IND_RECAR = {_Reg2.IND_RECAR},
                                        FEC_PUBLICA_DOF = TO_DATE('{_Reg2.FEC_PUBLICA_DOF}','YYYY-MM-DD'),
                                        IND_FED_MINIMO = {_Reg2.IND_FED_MINIMO},
                                        IND_RECAR_MINIMO = {_Reg2.IND_RECAR_MINIMO}
                                WHERE ANIO = {_Reg2.ANIO} AND MES = {_Reg2.MES}";
                }

            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    if (TIPO == "E") {
                        tabPage.Text = $"               {_Reg.ANIO}-{_Reg.MES}";
                        tabPage.Name = $"{_Reg.ANIO}{_Reg.MES}{TIPO}";
                    }
                    else if (TIPO == "F")
                    {
                        tabPage.Text = $"               {_Reg2.ANIO}-{_Reg2.MES}";
                        tabPage.Name = $"{_Reg2.ANIO}{_Reg2.MES}{TIPO}";
                    }


                    if (txtValida.Text == "Nuevo") { 
                    
                        comboTipoCredito.Enabled = false ;
                        txtAnio.Enabled = false;
                        txtMes.Enabled = false;
                    }

                    txtValida.Text = "Editar";

                    if (TIPO == "E")
                        await _Main.Status($"Registro con año {_Reg.ANIO} y mes {_Reg.MES} guardado correctamente", (int)MensajeTipo.Success);
                    else if (TIPO == "F")
                        await _Main.Status($"Registro con año {_Reg2.ANIO} y mes {_Reg2.MES} guardado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    //MessageBox.Show(oReq.Query, "Título de la alerta");

                    await _Main.Status($"{oRes.Message}", (int)MensajeTipo.Error);
                }
            }
            Cursor = Cursors.Default;
        }


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro(string TIPO)
        {
            var existe = false;

            try
            {
                if (TIPO == "E")
                {
                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT 
	                                ANIO,
	                                MES,
	                                IND_FINANC,
	                                IND_RECAR,
	                                FEC_PUBLICA_DOF,
	                                IND_RECAR_DIARIO
                                FROM INDICE_FINAN_EST
                                WHERE ANIO = " + _Reg.ANIO + " AND MES = " + _Reg.MES + " ";
                    var oRes = await WSServicio.Servicio(oReq);

                    if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                        existe = true;
                }
                else if (TIPO == "F")
                {
                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT 
	                                ANIO, 
		                            MES,
		                            IND_FINANC,
		                            IND_RECAR,
		                            FEC_PUBLICA_DOF,
		                            IND_FED_MINIMO,
		                            IND_RECAR_MINIMO
                                FROM INDICE_FINAN_FED
                                WHERE ANIO = " + _Reg2.ANIO + " AND MES = " + _Reg2.MES + " ";
                    var oRes = await WSServicio.Servicio(oReq);

                    if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                        existe = true;
                }

                 return existe;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");
                return existe;
            }

        }
        #endregion


        private async Task<bool> detValida(string TIPO)
        {
            bool bExito = true;
            string sErr = string.Empty;
            errorProvider1.Clear();

            if (!decimal.TryParse(txtAnio.Text, out _))
                sErr += "Debes de ingresar el Año, solo se permiten números. ";
            errorProvider1.SetError(txtAnio, "Debes de ingresar el Año, solo se permiten números.");

            if (!decimal.TryParse(txtMes.Text, out _))
                sErr += "Debes de ingresar el Mes, solo se permiten números. ";
            errorProvider1.SetError(txtMes, "Debes de ingresar el Mes, solo se permiten números.");

            if (!Regex.IsMatch(txtFinanciamiento.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$") || (string.IsNullOrWhiteSpace(txtFinanciamiento.Text)))
            {
                sErr += "Debes de ingresar el Financiamiento, solo se permiten números. ";
                errorProvider1.SetError(txtFinanciamiento, "Debes de ingresar el Financiamiento, solo se permiten números.");
            }
            else
            {
                decimal valor = decimal.Parse(txtFinanciamiento.Text);
                if (valor < 0 || valor > 999)
                {
                    sErr += "Financiamiento, el valor debe estar entre 0 y 999. ";
                    errorProvider1.SetError(txtFinanciamiento, "Financiamiento, el valor debe estar entre 0 y 999");
                }
            }

            if (!Regex.IsMatch(txtRecargos.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$") || (string.IsNullOrWhiteSpace(txtRecargos.Text)))
            { 
                sErr += "Debes de ingresar el los Recargos, solo se permiten números. ";
                errorProvider1.SetError(txtRecargos, "Debes de ingresar los Recargos, solo se permiten números.");
            }
            else
            {
                decimal valor = decimal.Parse(txtRecargos.Text);
                if (valor < 0 || valor > 999)
                {
                    sErr += "Recargos, el valor debe estar entre 0 y 999. ";
                    errorProvider1.SetError(txtRecargos, "Recargos, el valor debe estar entre 0 y 999");
                }
            }

            if (TIPO == "E")
            {

                if (!string.IsNullOrWhiteSpace(txtRecargoDiario.Text))
                {
                    if (!Regex.IsMatch(txtRecargoDiario.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$"))
                    {
                        sErr += "Recargo Diario, solo se permiten números. ";
                        errorProvider1.SetError(txtRecargoDiario, "Recargo Diario, solo se permiten números");
                    }
                    else
                    {
                        decimal valor = decimal.Parse(txtRecargoDiario.Text);
                        if (valor < 0 || valor > 9)
                        {
                            sErr += "Recargo Diario, el valor debe estar entre 0 y 9. ";
                            errorProvider1.SetError(txtRecargoDiario, "Recargo Diario, el valor debe estar entre 0 y 9");
                        }
                    }
                }

                if (sErr.Length > 0)
                {
                    bExito = false;
                    await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
                }
                else
                {

                    _Reg.ANIO = txtAnio.Text.Trim();
                    _Reg.MES = txtMes.Text.Trim();
                    _Reg.IND_FINANC = txtFinanciamiento.Text.Trim();
                    _Reg.IND_RECAR = txtRecargos.Text.Trim();
                    if (txtFechaPublicacion.Text.Trim() != "")
                    {
                        DateTime fechaConvertida = DateTime.ParseExact(txtFechaPublicacion.Text, "dd-MM-yyyy", null);
                        _Reg.FEC_PUBLICA_DOF = fechaConvertida.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        _Reg.FEC_PUBLICA_DOF = null;
                    }

                    _Reg.IND_RECAR_DIARIO = txtRecargoDiario.Text.Trim() != "" ? txtRecargoDiario.Text.Trim() : "null";

                }
            }
            else if (TIPO == "F")
            {
                if (!string.IsNullOrWhiteSpace(txtFinancMin.Text))
                {
                    if (!Regex.IsMatch(txtFinancMin.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$"))
                    {
                        sErr += "Financ Min, solo se permiten números. ";
                        errorProvider1.SetError(txtFinancMin, "Financ Min, solo se permiten números");
                    }
                    else
                    {
                        decimal valor = decimal.Parse(txtFinancMin.Text);
                        if (valor < 0 || valor > 9)
                        {
                            sErr += "Financ Min, el valor debe estar entre 0 y 9. ";
                            errorProvider1.SetError(txtFinancMin, "Financ Min, el valor debe estar entre 0 y 9");
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtRecarMin.Text))
                {
                    if (!Regex.IsMatch(txtRecarMin.Text, "^$|^[0-9]+(\\.[0-9]{1,9})?$"))
                    {
                        sErr += "Recarg Min, solo se permiten números. ";
                        errorProvider1.SetError(txtRecarMin, "Recarg Min, solo se permiten números");
                    }
                    else
                    {
                        decimal valor = decimal.Parse(txtRecarMin.Text);
                        if (valor < 0 || valor > 9)
                        {
                            sErr += "Recarg Min, el valor debe estar entre 0 y 9. ";
                            errorProvider1.SetError(txtRecarMin, "Recarg Min, el valor debe estar entre 0 y 9");
                        }
                    }
                }

                if (sErr.Length > 0)
                {
                    bExito = false;
                    await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
                }
                else
                {

                    _Reg2.ANIO = txtAnio.Text.Trim();
                    _Reg2.MES = txtMes.Text.Trim();
                    _Reg2.IND_FINANC = txtFinanciamiento.Text.Trim();
                    _Reg2.IND_RECAR = txtRecargos.Text.Trim();
                    if (txtPublicado.Text.Trim() != "")
                    {
                        DateTime fechaConvertida = DateTime.ParseExact(txtPublicado.Text, "dd-MM-yyyy", null);
                        _Reg2.FEC_PUBLICA_DOF = fechaConvertida.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        _Reg2.FEC_PUBLICA_DOF = null;
                    }

                    _Reg2.IND_FED_MINIMO = txtFinancMin.Text.Trim() != "" ? txtFinancMin.Text.Trim() : "null";
                    _Reg2.IND_RECAR_MINIMO = txtRecarMin.Text.Trim() != "" ? txtRecarMin.Text.Trim() : "null";

                }
            }



              return bExito;
        }


        private async Task detPresenta(string TIPO)
        {

            comboTipoCredito.SelectedValue = TIPO;

            comboTipoCredito.Enabled = false;
            txtAnio.Enabled = false;
            txtMes.Enabled = false;

            if (TIPO == "E")
            {
                txtAnio.Text = _Reg.ANIO;
                txtMes.Text = _Reg.MES;
                txtFinanciamiento.Text = _Reg.IND_FINANC;
                txtRecargos.Text = _Reg.IND_RECAR;

                if (_Reg.FEC_PUBLICA_DOF == "")
                {
                    txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
                    txtFechaPublicacion.CustomFormat = " ";
                    txtFechaPublicacion.ShowCheckBox = true;
                    txtFechaPublicacion.Checked = false;
                }
                else {
                    

                    txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
                    DateTime fecha = DateTime.ParseExact(_Reg.FEC_PUBLICA_DOF, "yyyy-MM-dd", null);
                    txtFechaPublicacion.Value = fecha;
                    txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
                    txtFechaPublicacion.ShowCheckBox = true;
                    txtFechaPublicacion.Checked = true;
                }
                
                txtRecargoDiario.Text = _Reg.IND_RECAR_DIARIO;
            }
            else if (TIPO == "F")
            {
                txtAnio.Text = _Reg2.ANIO;
                txtMes.Text = _Reg2.MES;
                txtFinanciamiento.Text = _Reg2.IND_FINANC;
                txtRecargos.Text = _Reg2.IND_RECAR;

                if (_Reg2.FEC_PUBLICA_DOF == "")
                {
                    txtPublicado.Format = DateTimePickerFormat.Custom;
                    txtPublicado.CustomFormat = " ";
                    txtPublicado.ShowCheckBox = true;
                    txtPublicado.Checked = false;
                }
                else
                {


                    txtPublicado.Format = DateTimePickerFormat.Custom;
                    DateTime fecha = DateTime.ParseExact(_Reg2.FEC_PUBLICA_DOF, "yyyy-MM-dd", null);
                    txtPublicado.Value = fecha;
                    txtPublicado.CustomFormat = "dd-MM-yyyy";
                    txtPublicado.ShowCheckBox = true;
                    txtPublicado.Checked = true;
                }

                txtFinancMin.Text = _Reg2.IND_FED_MINIMO;
                txtRecarMin.Text = _Reg2.IND_RECAR_MINIMO;
            }

         }

        #endregion

        private async Task limpiaCampos()
        {
            txtAnio.Enabled = true;
            txtMes.Enabled = true;
            comboTipoCredito.Enabled = true;
            txtAnio.Text = "";
            txtMes.Text = "";
            txtFinanciamiento.Text = "";
            txtRecargos.Text = "";
            txtRecargoDiario.Text = "";

            txtFechaPublicacion.Format = DateTimePickerFormat.Custom;
            txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
            txtFechaPublicacion.ShowCheckBox = true;
            txtFechaPublicacion.Checked = true;
            txtFechaPublicacion.Value = DateTime.Now;

            txtPublicado.Format = DateTimePickerFormat.Custom;
            txtPublicado.CustomFormat = "dd-MM-yyyy";
            txtPublicado.ShowCheckBox = true;
            txtPublicado.Checked = true;
            txtPublicado.Value = DateTime.Now;

            txtFinancMin.Text = "";
            txtRecarMin.Text = "";

        }

        private void dateTimePicker1_CheckedChanged(object sender, EventArgs e)
        {
            string TIPO = comboTipoCredito.SelectedValue.ToString();

            if (TIPO == "E")
            {
                if (txtFechaPublicacion.Checked)
                {

                    txtFechaPublicacion.CustomFormat = "dd-MM-yyyy";
                }
                else
                {
                    txtFechaPublicacion.CustomFormat = " ";
                }

            }
            else if (TIPO == "F")
            {
                if (txtPublicado.Checked)
                {

                    txtPublicado.CustomFormat = "dd-MM-yyyy";
                }
                else
                {
                    txtPublicado.CustomFormat = " ";
                }
            }

        }

        public class ComboBoxItem
        {
            public string Text { get; set; } // Texto que se muestra en el ComboBox
            public object Value { get; set; } // Valor asociado al item

            public override string ToString()
            {
                return Text; // Esto es lo que se muestra en el ComboBox
            }
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

        private void comboTipoCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TIPO = comboTipoCredito.SelectedValue.ToString();

            if (TIPO == "E")
            {

                lblFechaPublicacion.Visible = true;
                txtFechaPublicacion.Visible = true;
                lblRecargoDiario.Visible = true;
                txtRecargoDiario.Visible = true;

                lblPublicado.Visible = false;
                txtPublicado.Visible = false;
                lblFinanMin.Visible = false;
                txtFinancMin.Visible = false;
                lblRecarMin.Visible = false;
                txtRecarMin.Visible = false;

            }
            else if (TIPO == "F")
            {

                lblFechaPublicacion.Visible = false;
                txtFechaPublicacion.Visible = false;
                lblRecargoDiario.Visible = false;
                txtRecargoDiario.Visible = false;

                lblPublicado.Visible = true;
                txtPublicado.Visible = true;
                lblFinanMin.Visible = true;
                txtFinancMin.Visible = true;
                lblRecarMin.Visible = true;
                txtRecarMin.Visible = true;


            }
        }
    }
}
