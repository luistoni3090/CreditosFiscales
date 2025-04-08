using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
//using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
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

namespace Px_CreditosFiscales.Catalogos.Controles
{
    public partial class ctrlGrupoConcepto : UserControl
    {
        private xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        eGRUPO _Reg = new eGRUPO();
        eINCISO _Reg2 = new eINCISO();

        #region Constuctores
        public ctrlGrupoConcepto()
        {
            InitializeComponent();
            Inicio();
        }

        public ctrlGrupoConcepto(xMain oMain, Int32 CLAVE, Int32 GRUPO, string valida, string CONCEPTO)
        {

            InitializeComponent();

            _Main = oMain;

            Inicio();

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            _Reg2.GRUPO = GRUPO;
            _Reg2.CLAVE = CLAVE;
            _Reg2.CONCEPTO = CONCEPTO;
            txtValida.Text = valida;
            detActualiza(CLAVE,GRUPO);
        }

        private async Task Inicio()
        {


        }
        #endregion

        #region "Detalle"
        private async Task detNuevo()
        {
            Cursor = Cursors.AppStarting;

            _Reg = new eGRUPO();

            await Task.Delay(0);

            Cursor = Cursors.Default;
        }


        private async Task detActualiza(Int32 CLAVE, Int32 GRUPO)
        {
            DateTime startTime = DateTime.Now;
            await _Main.Status($"Buscando detalle . . .", (int)MensajeTipo.Info);

            Cursor = Cursors.AppStarting;

            txtValida.Visible = false;
            await detNuevo();
            await detBusca(CLAVE, GRUPO);

            TimeSpan ts = DateTime.Now - startTime;
            await _Main.Status($"Detalle {CLAVE}-{GRUPO}-{_Reg2.CONCEPTO.Trim()} cargado correctamente. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }

        public async Task detActualiza()
        {
            await detBusca((Int32)_Reg2.CLAVE, (Int32)_Reg2.GRUPO);
        }

        private async Task cargaComboGrupo()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT GRUPO,DESCR FROM GRUPO_IMPTO ORDER BY GRUPO";
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
                            Value = row["GRUPO"].ToString(),
                            Text = "("+row["GRUPO"].ToString()+") "+row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboGrupo.DataSource = comboList;
                    comboGrupo.DisplayMember = "Text";
                    comboGrupo.ValueMember = "Value";
                    comboGrupo.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            { }
        }


        private async Task cargaComboTipo()
        {

            var comboList = new List<ComboBoxItem>();

            var item0 = new ComboBoxItem
            {
                Value = "0",
                Text = ""
            };
            var item1 = new ComboBoxItem
            {
                Value = "A",
                Text = "ACCESORIOS"
            };
            var item2 = new ComboBoxItem
            {
                Value = "I",
                Text = "IMPUESTO"
            };
            comboList.Add(item0);
            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboTipo.DataSource = comboList;
            comboTipo.DisplayMember = "Text";
            comboTipo.ValueMember = "Value";
            comboTipo.SelectedIndex = 0;

        }

        private async Task cargaComboCapital()
        {

            var comboList = new List<ComboBoxItem>();

            var item0 = new ComboBoxItem
            {
                Value = "0",
                Text = ""
            };
            var item1 = new ComboBoxItem
            {
                Value = "S",
                Text = "SI"
            };
            var item2 = new ComboBoxItem
            {
                Value = "N",
                Text = "NO"
            };
            comboList.Add(item0);
            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboCapital.DataSource = comboList;
            comboCapital.DisplayMember = "Text";
            comboCapital.ValueMember = "Value";
            comboCapital.SelectedIndex = 0;

        }


        #region
        private async Task cargaComboIdentificador()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = "SELECT IDEN, DESCR FROM IDEN WHERE IDEN = IDEN2";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["IDEN"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboIdentificador.DataSource = comboList;
                    comboIdentificador.DisplayMember = "Text";
                    comboIdentificador.ValueMember = "Value";
                    comboIdentificador.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }

        }
        #endregion

        private async Task cargaComboTipoImpto()
        {

            var comboList = new List<ComboBoxItem>();

            var item0 = new ComboBoxItem
            {
                Value = "0",
                Text = ""
            };
            var item1 = new ComboBoxItem
            {
                Value = "P",
                Text = "PROPIO"
            };
            var item2 = new ComboBoxItem
            {
                Value = "R",
                Text = "RETENIDO"
            };
            comboList.Add(item0);
            comboList.Add(item1);
            comboList.Add(item2);

            // Asignar la lista al ComboBox
            comboTipoImpto.DataSource = comboList;
            comboTipoImpto.DisplayMember = "Text";
            comboTipoImpto.ValueMember = "Value";
            comboTipoImpto.SelectedIndex = 0;

        }


        private async Task cargaComboCCCP()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCCCP.DataSource = comboList;
                    comboCCCP.DisplayMember = "Text";
                    comboCCCP.ValueMember = "Value";
                    comboCCCP.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }

        }

        private async Task cargaComboCCLP()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCCLP.DataSource = comboList;
                    comboCCLP.DisplayMember = "Text";
                    comboCCLP.ValueMember = "Value";
                    comboCCLP.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }           
        }

        private async Task cargaComboCING()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboCING.DataSource = comboList;
                    comboCING.DisplayMember = "Text";
                    comboCING.ValueMember = "Value";
                    comboCING.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task cargaComboPPTO_EJER()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboPPTO_EJER.DataSource = comboList;
                    comboPPTO_EJER.DisplayMember = "Text";
                    comboPPTO_EJER.ValueMember = "Value";
                    comboPPTO_EJER.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task cargaComboDevengado()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboDevengado.DataSource = comboList;
                    comboDevengado.DisplayMember = "Text";
                    comboDevengado.ValueMember = "Value";
                    comboDevengado.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task cargaComboPasivoCP()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboPasivoCP.DataSource = comboList;
                    comboPasivoCP.DisplayMember = "Text";
                    comboPasivoCP.ValueMember = "Value";
                    comboPasivoCP.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task cargaComboPasivoLPP()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT (NIVEL1 || '.' || NIVEL2 || '.' || NIVEL3 || '.' || NIVEL4 || '.' || NIVEL5 || '.' || NIVEL6) AS DATOS, 
                                DESCR 
                                FROM MVW_CUENTA 
                                ORDER BY NIVEL1, NIVEL2, NIVEL3, NIVEL4, NIVEL5 , NIVEL6";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["DATOS"].ToString(),
                            Text = row["DATOS"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboPasivoLP.DataSource = comboList;
                    comboPasivoLP.DisplayMember = "Text";
                    comboPasivoLP.ValueMember = "Value";
                    comboPasivoLP.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task cargaComboObligacion()
        {

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT OBLIGACION, DESCR FROM OBLIGACION";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes?.Data?.Tables[0] != null)
                {
                    var dataTable = oRes.Data.Tables[0];

                    // Crear una lista para los items del ComboBox
                    var comboList = new List<ComboBoxItem>();

                    var item0 = new ComboBoxItem
                    {
                        Value = "0",
                        Text = ""
                    };
                    comboList.Add(item0);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var item = new ComboBoxItem
                        {
                            Value = row["OBLIGACION"].ToString(),
                            Text = row["DESCR"].ToString()
                        };
                        comboList.Add(item);
                    }
                    // Asignar la lista al ComboBox
                    comboObligacion.DataSource = comboList;
                    comboObligacion.DisplayMember = "Text";
                    comboObligacion.ValueMember = "Value";
                    comboObligacion.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            { }
        }

        private async Task detBusca(Int32 CLAVE, Int32 GRUPO)
        {

            await cargaComboGrupo();
            await cargaComboTipo();
            await cargaComboCapital();
            await cargaComboIdentificador();
            await cargaComboTipoImpto();
            await cargaComboCCCP();
            await cargaComboCCLP();
            await cargaComboCING();
            await cargaComboPPTO_EJER();
            await cargaComboDevengado();
            await cargaComboPasivoCP();
            await cargaComboPasivoLPP();
            await cargaComboObligacion();

            if (txtValida.Text == "Nuevo")
            {
                await limpiaCampos();
                return;
            }
            else
            {
                try
                {
                    oReq.Tipo = 0;
                    oReq.Query = @"SELECT 
                                    CLAVE,
                                    GRUPO,
                                    DESCR AS CONCEPTO,
                                    TIPO,
                                    INCISO, 
                                    INC_ARMONIZ AS INCISO_2012,
                                    ORDEN, 
                                    CAPITAL,
                                    IDEN AS IDENTIFICADOR,
                                    TIPO_IMPTO,
                                    GRUPO_EQ AS GRUPO_EQUIVALENTE,
                                    INCISO_EQ AS INCISO_EQUIVALENTE,
                                    CCCP,
                                    CCLP,
                                    CING,
                                    PPTO_EJER,
                                    DEVENGADO,
                                    OBLIGACION,
                                    PASDIF_CP,
                                    PASDIF_LP 
                                    FROM INCISO
                                    WHERE CLAVE = :CLAVE  AND GRUPO = :GRUPO ";
                    oReq.Parametros = new List<eParametro>
                {
                    new eParametro(){ Tipo = DbType.String, Nombre = "CLAVE", Valor = CLAVE},
                    new eParametro(){ Tipo = DbType.String, Nombre = "GRUPO", Valor = GRUPO}
                };

                    var oRes = await WSServicio.Servicio(oReq);

                    if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                        _Reg2 = oRes.Data.Tables[0].Rows[0].RowAObjetoDe<eINCISO>();

                    txtValida.Text = "Editar";
                    txtClave.Enabled = false;
                    comboGrupo.Enabled = false;
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
        public async Task detBorra(TabPage tabPage)
        {

            if (txtValida.Text == "Nuevo") return;
            await _Main.Status($"¿Deseas borrar el registro {_Reg2.CONCEPTO}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas borrar el registro {_Reg2.CONCEPTO}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            await _Main.Status($"¿Realmente estas seguro que Deseas borrar el registro {_Reg2.CONCEPTO}?", (int)MensajeTipo.Error);
            if (MessageBoxMX.ShowDialog(null, $"¿Realmente estas seguro que Deseas borrar el registro {_Reg2.CONCEPTO}?", "Precaución", (int)StatusColorsTypes.Danger, true) == System.Windows.Forms.DialogResult.Cancel)
                return;

            Cursor = Cursors.AppStarting;
            oReq.Tipo = 1;
            oReq.Query = $@"
                                DELETE INCISO
                                WHERE CLAVE = {_Reg2.CLAVE} AND GRUPO = {_Reg2.GRUPO}
                             ";

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    txtValida.Text = "Nuevo";
                    tabPage.Text = "0-0-0";
                    tabPage.Name = "000";
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

            if (txtValida.Text == "Nuevo") {

                bool existe = await conValidaSiExisteRegistro();

                if (existe)
                {

                    await _Main.Status($"La Clave {_Reg2.CLAVE} y el Grupo {_Reg2.GRUPO} ya estan en uso", (int)MensajeTipo.Error);
                    return;
                }

            }

            await _Main.Status($"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg2.CONCEPTO}?", (int)MensajeTipo.Question);
            if (MessageBoxMX.ShowDialog(null, $"¿Deseas {(txtValida.Text == "Nuevo" ? "guardar" : "actualizar")} el registro {_Reg2.CONCEPTO}?", "Precaución", (int)StatusColorsTypes.Question, true) == System.Windows.Forms.DialogResult.Cancel)
                return;
            Cursor = Cursors.AppStarting;

            oReq.Parametros = new List<eParametro>();

            if (txtValida.Text == "Nuevo")
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                INSERT INTO INCISO
                                    (
                                        CLAVE,
                                        GRUPO,
                                        DESCR,
                                        TIPO,
                                        INCISO,
                                        INC_ARMONIZ,
                                        ORDEN,
                                        CAPITAL,
                                        IDEN,
                                        TIPO_IMPTO,
                                        GRUPO_EQ,
                                        INCISO_EQ,
                                        CCCP,
                                        CCLP,
                                        CING,
                                        PPTO_EJER,
                                        DEVENGADO,
                                        OBLIGACION,
                                        PASDIF_CP,
                                        PASDIF_LP 
                                        )
                                VALUES
                                        (
                                        {_Reg2.CLAVE},
                                        {_Reg2.GRUPO},
                                        '{_Reg2.CONCEPTO}',
                                        '{_Reg2.TIPO}',
                                        '{_Reg2.INCISO}',
                                        '{_Reg2.INCISO_2012}',
                                         {_Reg2.ORDEN},
                                        '{_Reg2.CAPITAL}',
                                        '{_Reg2.IDENTIFICADOR}',
                                        '{_Reg2.TIPO_IMPTO}',
                                        '{_Reg2.GRUPO_EQUIVALENTE}',
                                        '{_Reg2.INCISO_EQUIVALENTE}',
                                        '{_Reg2.CCCP}',
                                        '{_Reg2.CCLP}',
                                        '{_Reg2.CING}',
                                        '{_Reg2.PPTO_EJER}',
                                        '{_Reg2.DEVENGADO}',
                                        '{_Reg2.OBLIGACION}',
                                        '{_Reg2.PASDIF_CP}',
                                        '{_Reg2.PASDIF_LP}'
                                        )
                                ";
            }
            else
            {
                oReq.Tipo = 1;
                oReq.Query = $@"
                                UPDATE INCISO
                                    SET 
                                        DESCR = '{_Reg2.CONCEPTO}',
                                        TIPO = '{_Reg2.TIPO}',
                                        INCISO = '{_Reg2.INCISO}',
                                        INC_ARMONIZ = '{_Reg2.INCISO_2012}',
                                        ORDEN = {_Reg2.ORDEN},
                                        CAPITAL = '{_Reg2.CAPITAL}',
                                        IDEN = '{_Reg2.IDENTIFICADOR}',
                                        TIPO_IMPTO = '{_Reg2.TIPO_IMPTO}',
                                        GRUPO_EQ = '{_Reg2.GRUPO_EQUIVALENTE}',
                                        INCISO_EQ = '{_Reg2.INCISO_EQUIVALENTE}',
                                        CCCP = '{_Reg2.CCCP}',
                                        CCLP = '{_Reg2.CCLP}',
                                        CING = '{_Reg2.CING}',
                                        PPTO_EJER = '{_Reg2.PPTO_EJER}',
                                        DEVENGADO = '{_Reg2.DEVENGADO}',
                                        OBLIGACION = '{_Reg2.OBLIGACION}',
                                        PASDIF_CP = '{_Reg2.PASDIF_CP}',
                                        PASDIF_LP = '{_Reg2.PASDIF_LP}'
                                WHERE CLAVE = {_Reg2.CLAVE} AND GRUPO = {_Reg2.GRUPO}
                             ";
            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {
                    
                    tabPage.Text = "               " + _Reg2.CLAVE + "-" + _Reg2.GRUPO + "-" + _Reg2.CONCEPTO.Trim();
                    tabPage.Name = $"{_Reg2.CLAVE}{_Reg2.GRUPO}{_Reg2.CONCEPTO.Trim()}";

                    txtValida.Text = "Editar";
                    txtClave.Enabled = false;
                    comboGrupo.Enabled = false;

                    await _Main.Status($"Registro {_Reg2.CONCEPTO} guardado correctamente", (int)MensajeTipo.Success);
                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);
                }
            }
            Cursor = Cursors.Default;
        }


        #region Valida si esxiste registro en BD
        private async Task<bool> conValidaSiExisteRegistro()
        {
            var existe = false;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT CLAVE, GRUPO
                                FROM INCISO WHERE CLAVE = " + _Reg2.CLAVE + " AND GRUPO = " + _Reg2.GRUPO + " ";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                    existe = true;

                return existe;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");
                return existe;
            }

        }
        #endregion


        /// <summary>
        /// Valido que los campos estén ingresados
        /// </summary>
        /// <returns></returns>
        private async Task<bool> detValida()
        {
            bool bExito = true;
            string sErr = string.Empty;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtClave.Text) || !decimal.TryParse(txtClave.Text, out _))
                sErr += "Debes de ingresar la clave, solo se permiten números ";
            errorProvider1.SetError(txtClave, "Debes de ingresar la clave, solo se permiten números");

            if (string.IsNullOrWhiteSpace(comboGrupo.Text))
                sErr += "Debes seleccionar el grupo. ";
            errorProvider1.SetError(comboGrupo, "Debes seleccionar el grupo.");

            if (string.IsNullOrWhiteSpace(txtConcepto.Text))
                sErr += "Debes de ingresar el concepto. ";
            errorProvider1.SetError(txtConcepto, "Debes de ingresar el concepto.");

            if (string.IsNullOrEmpty(comboTipo.SelectedValue.ToString()))
                sErr += "Debes seleccionar el Tipo. ";
            errorProvider1.SetError(comboTipo, "Debes seleccionar el Tipo.");

            if (string.IsNullOrWhiteSpace(txtInciso.Text))
                sErr += "Debes de ingresar el inciso. ";
            errorProvider1.SetError(txtInciso, "Debes de ingresar el inciso.");

            if (!decimal.TryParse(txtOrden.Text, out _))
                sErr += "Debes de ingresar el Orden, solo se permiten números. ";
            errorProvider1.SetError(txtOrden, "Debes de ingresar el Orden, solo se permiten números.");

            if (!string.IsNullOrWhiteSpace(txtGrupoEquivalente.Text)) {
                if (!Regex.IsMatch(txtGrupoEquivalente.Text, "^$|^[0-9]+$"))
                {
                    sErr += "Grupo equivalente, solo se permiten números. ";
                    errorProvider1.SetError(txtGrupoEquivalente, "Grupo equivalente, solo se permiten números");
                }
            }
            

            if (sErr.Length > 0)
            {
                bExito = false;
                await _Main.Status($"Favor de atender los siguientes mensajes: {sErr}", (int)MensajeTipo.Warning);
            }
            else
            {
                
                _Reg2.CLAVE = int.Parse(txtClave.Text.Trim());
                _Reg2.GRUPO = int.Parse(comboGrupo.SelectedValue.ToString());
                _Reg2.CONCEPTO = txtConcepto.Text.Trim();
                _Reg2.TIPO = comboTipo.SelectedValue.ToString() == "0" ? null: comboTipo.SelectedValue.ToString();
                _Reg2.INCISO = txtInciso.Text.Trim();
                _Reg2.INCISO_2012 = txtInciso2012.Text.Trim() == "" ? null : txtInciso2012.Text.Trim();
                _Reg2.ORDEN = int.Parse(txtOrden.Text.Trim());
                _Reg2.CAPITAL = comboCapital.SelectedValue.ToString() == "0" ? null : comboCapital.SelectedValue.ToString();
                _Reg2.IDENTIFICADOR = comboIdentificador.SelectedValue.ToString() == "0" ? null : comboIdentificador.SelectedValue.ToString();
                _Reg2.TIPO_IMPTO = comboTipoImpto.SelectedValue.ToString() == "0" ? null : comboTipoImpto.SelectedValue.ToString();
                _Reg2.GRUPO_EQUIVALENTE = txtGrupoEquivalente.Text.Trim() == "" ? null : txtGrupoEquivalente.Text.Trim();
                _Reg2.INCISO_EQUIVALENTE = txtIncisoEquivalente.Text.Trim() == "" ? null : txtIncisoEquivalente.Text.Trim();
                _Reg2.CCCP = comboCCCP.SelectedValue.ToString() == "0" ? null : comboCCCP.SelectedValue.ToString();
                _Reg2.CCLP = comboCCLP.SelectedValue.ToString() == "0" ? null : comboCCLP.SelectedValue.ToString();
                _Reg2.CING = comboCING.SelectedValue.ToString() == "0" ? null : comboCING.SelectedValue.ToString();
                _Reg2.PPTO_EJER = comboPPTO_EJER.SelectedValue.ToString() == "0" ? null : comboPPTO_EJER.SelectedValue.ToString();
                _Reg2.DEVENGADO = comboDevengado.SelectedValue.ToString() == "0" ? null : comboDevengado.SelectedValue.ToString();
                _Reg2.OBLIGACION = comboObligacion.SelectedValue.ToString() == "0" ? null : comboObligacion.SelectedValue.ToString();
                _Reg2.PASDIF_CP = comboPasivoCP.SelectedValue.ToString() == "0" ? null : comboPasivoCP.SelectedValue.ToString();
                _Reg2.PASDIF_LP = comboPasivoLP.SelectedValue.ToString() == "0" ? null : comboPasivoLP.SelectedValue.ToString();


            }

            return bExito;
        }


        private async Task detPresenta()
        {
            txtClave.Text = _Reg2.CLAVE.ToString();
            comboGrupo.SelectedValue = _Reg2.GRUPO.ToString();
            txtConcepto.Text = _Reg2.CONCEPTO;
            comboTipo.SelectedValue = _Reg2.TIPO == "" ? "0" : _Reg2.TIPO;
            txtInciso.Text = _Reg2.INCISO;
            txtInciso2012.Text = _Reg2.INCISO_2012;
            txtOrden.Text = _Reg2.ORDEN.ToString();
            comboCapital.SelectedValue = _Reg2.CAPITAL == "" ? "0" : _Reg2.CAPITAL;
            comboIdentificador.SelectedValue = _Reg2.IDENTIFICADOR == "" ? "0" : _Reg2.IDENTIFICADOR;
            comboTipoImpto.SelectedValue = _Reg2.TIPO_IMPTO == "" ? "0" : _Reg2.TIPO_IMPTO;
            txtGrupoEquivalente.Text = _Reg2.GRUPO_EQUIVALENTE;
            txtIncisoEquivalente.Text = _Reg2.INCISO_EQUIVALENTE;
            comboCCCP.SelectedValue = _Reg2.CCCP == "" ? "0" : _Reg2.CCCP;
            comboCCLP.SelectedValue = _Reg2.CCLP == "" ? "0" : _Reg2.CCLP;
            comboCING.SelectedValue = _Reg2.CING == "" ? "0" : _Reg2.CING;
            comboPPTO_EJER.SelectedValue = _Reg2.PPTO_EJER == "" ? "0" : _Reg2.PPTO_EJER;
            comboDevengado.SelectedValue = _Reg2.DEVENGADO == "" ? "0" : _Reg2.DEVENGADO;
            comboObligacion.SelectedValue = _Reg2.OBLIGACION == "" ? "0" : _Reg2.OBLIGACION;
            comboPasivoCP.SelectedValue = _Reg2.PASDIF_CP == "" ? "0" : _Reg2.PASDIF_CP;
            comboPasivoLP.SelectedValue = _Reg2.PASDIF_LP == "" ? "0" : _Reg2.PASDIF_LP;
        }

        #endregion

        private async Task limpiaCampos()
        {

            txtClave.Enabled = true;
            comboGrupo.Enabled = true;
            txtClave.Text = "";
            comboGrupo.SelectedIndex = -1;
            txtConcepto.Text = "";
            comboTipo.SelectedValue = "0";
            txtInciso.Text = "";
            txtInciso2012.Text = "";
            txtOrden.Text = "";
            comboCapital.SelectedValue = "0";
            comboIdentificador.SelectedValue = "0";
            comboTipoImpto.SelectedValue = "0";
            txtGrupoEquivalente.Text = "";
            txtIncisoEquivalente.Text = "";
            comboCCCP.SelectedValue = "0";
            comboCCLP.SelectedValue = "0";
            comboCING.SelectedValue = "0";
            comboPPTO_EJER.SelectedValue = "0";
            comboDevengado.SelectedValue = "0";
            comboObligacion.SelectedValue = "0";
            comboPasivoCP.SelectedValue = "0";
            comboPasivoLP.SelectedValue = "0";

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
    }
}
