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

using Px_ConciliacionBancaria.Utiles.Generales;
using Px_ConciliacionBancaria.Catalogos.Controles;
using static Px_ConciliacionBancaria.Utiles.Emun.Enumerados;

using Px_Utiles.Servicio;
using Px_Utiles.Models.Api;
using Px_Utiles.Utiles.DataTables;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Px_ConciliacionBancaria.Catalogos
{
    public partial class FrmAccesoRol : Form
    {
        public xMain _Main { get; set; }

        eRequest oReq = new eRequest();
        List<object> itemsOriginales = new List<object>();

        #region Contructor
        public FrmAccesoRol()
        {
            InitializeComponent();

            Inicio();
        }

        private async Task Inicio()
        {
            Cursor = Cursors.WaitCursor;

            //_Main = (xMain)this.Parent;

            lblTitulo.Text = "Mantenimiento Acceso-Rol";

            btnXCerrar.Click += btnXCerrar_Click;
            btnXMax.Click += btnXMax_Click;
            btnXMin.Click += btnXMin_Click;

            panTittle.MouseDown += panTittle_MouseDown;
            lblTitulo.MouseDown += panTittle_MouseDown;
            picIco.MouseDown += panTittle_MouseDown;

            uiTabControlExt1.SelectedIndexChanged += uiTabControlExt1_SelectedIndexChanged;

            btnmnuImprime.Click += btnmnuImprime_Click;
            btnmnuAyuda.Click += btnmnuAyuda_Click;

            txtBusca.TextChanged += txtBusca_TextChanged;

            await InicioForma();
            await conActualiza();

            List<string> listaOriginal = new List<string>();

            Cursor = Cursors.Default;
        }

        private async Task InicioForma()
        {

            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            tabPage1.Text = "Consulta";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{lblTitulo.Text}", (int)MensajeTipo.Info);

            panFiltros.Visible = false;
            await cargarListaUsuariosRoles();
            await cargarListaProcesosRoles();

        }
        #endregion

        #region Eventos
        private void panTittle_MouseDown(object sender, MouseEventArgs e)
        {
            Utiles.Win32.User.ReleaseCapture();
            Utiles.Win32.User.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnXCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXMax_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void btnXMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private async void uiTabControlExt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await conMenu();
        }

        private async void btnmnuActualizaDet_Click(object sender, EventArgs e)
        {
            txtBusca.Text = "";
            panFiltros.Visible = false;
            await cargarListaUsuariosRoles();
            await cargarListaProcesosRoles();
        }

        private void btnmnuFiltros_Click(object sender, EventArgs e)
        {
            panFiltros.Visible = !panFiltros.Visible;
        }
        private void btnmnuConsulta_Click(object sender, EventArgs e)
        {
            uiTabControlExt1.SelectedIndex = 0;
        }
        private void btnmnuImprime_Click(object sender, EventArgs e)
        {
        }
        private void btnmnuAyuda_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region "Cargar la lista de usuarios y roles"
        private async Task cargarListaUsuariosRoles()
        {
            listUsuariosRoles.Items.Clear();
            listUsuariosRoles.Click -= listUsuariosRoles_Click;

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT USR.APP_LOGIN AS LOGIN, 
                                        USR.AP_PATER || ' ' || 
                                        USR.AP_MATER || ' ' || 
                                        USR.NOMBRE AS NOMBRE, 'USUARIO' AS TIPO
                                        FROM DGI_USR USR 
                                        ORDER BY AP_PATER ASC, AP_MATER ASC, NOMBRE ASC";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                {

                    listUsuariosRoles.DisplayMember = "NOMBRE";
                    listUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes.Data.Tables[0].Rows)
                    {
                        listUsuariosRoles.Items.Add(new { ID = row["LOGIN"], NOMBRE = row["NOMBRE"], TIPO = row["TIPO"] });
                    }

                }

                oReq.Tipo = 0;
                oReq.Query = @"SELECT DISTINCT TO_CHAR(DROL.ROL) AS ROL, DROL.DESCR, 'ROL' AS TIPO
                                        FROM DGI_ROL DROL
                                        ORDER BY DROL.ROL";
                var oRes2 = await WSServicio.Servicio(oReq);

                if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                {

                    listUsuariosRoles.DisplayMember = "NOMBRE";
                    listUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes2.Data.Tables[0].Rows)
                    {
                        listUsuariosRoles.Items.Add(new { ID = row["ROL"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }

                }

                if (listUsuariosRoles.Items.Count > 0)
                {
                    listUsuariosRoles.SelectedIndex = 0;
                    listUsuariosRoles_Click(listUsuariosRoles, EventArgs.Empty);
                }

                listUsuariosRoles.Click += listUsuariosRoles_Click;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");

            }

            await Task.Delay(0);
        }
        #endregion


        #region "Obtener los Roles o Procesos del usuario o rol seleccionado"
        private async void listUsuariosRoles_Click(object sender, EventArgs e)
        {
            if (listUsuariosRoles.SelectedItem != null)
            {
                var selectedItem = (dynamic)listUsuariosRoles.SelectedItem;
                var ID = selectedItem.ID;
                string NOMBRE = selectedItem.NOMBRE;
                txtUsuarioRol.Text = NOMBRE;

                await cargarListaProcesosRolesUsuariosRoles(ID);

            }

        }
        #endregion


        #region "Cargar la lista de procesos roles - usuarios roles"
        private async Task cargarListaProcesosRolesUsuariosRoles(string ID)
        {
            listProcesosRolesUsuariosRoles.Items.Clear();

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT USR.APP_LOGIN AS LOGIN, USR.AP_PATER, USR.AP_MATER, USR.NOMBRE, DP.PROCESO, DP.DESCR AS DESCR, 'PROCESO' AS TIPO
                                        FROM DGI_USR USR
                                        JOIN DGI_USR_PROCESO DUP ON (USR.APP_LOGIN = DUP.APP_LOGIN)
                                        JOIN DGI_PROCESO DP ON (DUP.PROCESO = DP.PROCESO)
                                        WHERE USR.APP_LOGIN = '" + ID + "' ORDER BY DP.DESCR ASC";
                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRolesUsuariosRoles.DisplayMember = "NOMBRE";
                    listProcesosRolesUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes.Data.Tables[0].Rows)
                    {
                        listProcesosRolesUsuariosRoles.Items.Add(new { ID = row["PROCESO"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }

                }

                oReq.Tipo = 0;
                oReq.Query = @"SELECT USR.APP_LOGIN AS LOGIN, USR.AP_PATER, USR.AP_MATER, USR.NOMBRE, DR.ROL, DR.DESCR AS DESCR, 'ROL' AS TIPO
                                    FROM DGI_USR USR
                                    JOIN DGI_ROL_USR DRU ON (USR.APP_LOGIN = DRU.APP_LOGIN)
                                    JOIN DGI_ROL DR ON (DRU.ROL = DR.ROL)
                                    AND USR.APP_LOGIN = '" + ID + "' ORDER BY DR.DESCR ASC";
                var oRes2 = await WSServicio.Servicio(oReq);

                if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRolesUsuariosRoles.DisplayMember = "NOMBRE";
                    listProcesosRolesUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes2.Data.Tables[0].Rows)
                    {
                        listProcesosRolesUsuariosRoles.Items.Add(new { ID = row["ROL"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }

                }

                oReq.Tipo = 0;
                oReq.Query = @"SELECT DROL.ROL, DROL.DESCR, DP.PROCESO, DP.DESCR AS NOMBRE, 'PROCESO' AS TIPO
                                    FROM DGI_ROL DROL
                                    JOIN DGI_ROL_PROCESO DRP ON (DROL.ROL = DRP.ROL)
                                    JOIN DGI_PROCESO DP ON (DRP.PROCESO = DP.PROCESO)
                                    WHERE DROL.ROL = " + ID + " ORDER BY DP.DESCR ASC";
                var oRes3 = await WSServicio.Servicio(oReq);

                if (oRes3.Data.Tables.Count > 0 && oRes3.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRolesUsuariosRoles.DisplayMember = "NOMBRE";
                    listProcesosRolesUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes3.Data.Tables[0].Rows)
                    {
                        listProcesosRolesUsuariosRoles.Items.Add(new { ID = row["PROCESO"], NOMBRE = row["NOMBRE"], TIPO = row["TIPO"] });
                    }
                }

                oReq.Tipo = 0;
                oReq.Query = @"SELECT DROL.ROL, DROL.DESCR, 'ROL' AS TIPO  
                                FROM DGI_ROL DROL
                                JOIN DGI_ROL_ROL DRR ON (DRR.ROL_ASIG = DROL.ROL)
                                WHERE DRR.ROL = "+ID+" ORDER BY DESCR ASC";
                var oRes4 = await WSServicio.Servicio(oReq);

                if (oRes4.Data.Tables.Count > 0 && oRes4.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRolesUsuariosRoles.DisplayMember = "NOMBRE";
                    listProcesosRolesUsuariosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes4.Data.Tables[0].Rows)
                    {
                        listProcesosRolesUsuariosRoles.Items.Add(new { ID = row["ROL"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");

            }

            await Task.Delay(0);
        }
        #endregion


        #region "Cargar la lista de procesos roles"
        private async Task cargarListaProcesosRoles()
        {
            listProcesosRoles.Items.Clear();

            try
            {
                oReq.Tipo = 0;
                oReq.Query = @"SELECT ROL, DESCR,'ROL' AS TIPO FROM DGI_ROL ORDER BY DESCR ASC";

                var oRes = await WSServicio.Servicio(oReq);

                if (oRes.Data.Tables.Count > 0 && oRes.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRoles.DisplayMember = "NOMBRE";
                    listProcesosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes.Data.Tables[0].Rows)
                    {
                        listProcesosRoles.Items.Add(new { ID = row["ROL"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }

                }

                oReq.Tipo = 0;
                oReq.Query = @"SELECT PROCESO, DESCR, 'PROCESO' AS TIPO FROM DGI_PROCESO ORDER BY SYSMENUNAME DESC, DESCR ASC";
                var oRes2 = await WSServicio.Servicio(oReq);

                if (oRes2.Data.Tables.Count > 0 && oRes2.Data.Tables[0].Rows.Count > 0)
                {

                    listProcesosRoles.DisplayMember = "NOMBRE";
                    listProcesosRoles.ValueMember = "ID";

                    foreach (DataRow row in oRes2.Data.Tables[0].Rows)
                    {
                        listProcesosRoles.Items.Add(new { ID = row["PROCESO"], NOMBRE = row["DESCR"], TIPO = row["TIPO"] });
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los parámetros: {ex.Message}");

            }

            await Task.Delay(0);
        }
        #endregion

        #region "Crear Menu"
        private async Task conMenu()
        {
            bool bConsulta = uiTabControlExt1.SelectedIndex == 0 ? true : false;

            btnmnuFiltros.Visible = bConsulta;

            btnmnuImprime.Visible = true;
            btnmnuAyuda.Visible = true;
        }
        #endregion


        #region "Generando Consulta"
        private async Task conActualiza()
        {
            Cursor = Cursors.WaitCursor;

            DateTime startTime = DateTime.Now;
            if (_Main != null)
                await _Main.Status($"Generando consulta . . .", (int)MensajeTipo.Info);

            await conMenu();

            foreach (object item in listUsuariosRoles.Items)
            {
                itemsOriginales.Add(item);
            }

            TimeSpan ts = DateTime.Now - startTime;
            if (_Main != null)
                await _Main.Status($"Catálogo de Acceso a Roles. Tiempo: {ts}", (int)MensajeTipo.Success);

            Cursor = Cursors.Default;
        }
        #endregion

        #region Buscar
        private async void txtBusca_TextChanged(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {

            listUsuariosRoles.SelectedItem = null;
            listProcesosRolesUsuariosRoles.Items.Clear();
            txtUsuarioRol.Text = "";

            string filtro = txtBusca.Text.ToLower();
            listUsuariosRoles.Items.Clear();

            foreach (object item in itemsOriginales)
            {
                if (item.ToString().ToLower().Contains(filtro))
                {
                    listUsuariosRoles.Items.Add(item);
                }
            }

        }
        #endregion

        private async void button1_Click(object sender, EventArgs e)
        {
            await conActualiza();
        }

        #region "Eliminar Procesos o Roles a Usuarios"
        private async void eliminarProcesosRolesUsuarios(object sender, EventArgs e)
        {

            if (listUsuariosRoles.SelectedItem != null)
            {
                var selectedItem = (dynamic)listUsuariosRoles.SelectedItem;
                var IdUsuario_Rol = selectedItem.ID;
                var TIPO1 = selectedItem.TIPO;
                string NOMBRE = selectedItem.NOMBRE;

                if (listProcesosRolesUsuariosRoles.SelectedItem != null)
                {

                    if (MessageBox.Show("¿Estás seguro de eliminar el Proceso/Rol seleccionado?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        selectedItem = (dynamic)listProcesosRolesUsuariosRoles.SelectedItem;
                        var IdProceso_Rol = selectedItem.ID;
                        var TIPO2 = selectedItem.TIPO;

                        var mensaje = await deleteProcesoRolAUsuarios(IdUsuario_Rol.ToString(), TIPO1, IdProceso_Rol.ToString(), TIPO2);

                        if (mensaje == "Ejecución satisfactoria")
                        {
                            int indiceSeleccionado = listProcesosRolesUsuariosRoles.SelectedIndex;
                            if (indiceSeleccionado != -1)
                            {
                                listProcesosRolesUsuariosRoles.Items.RemoveAt(indiceSeleccionado);
                            }

                        }

                    }

                }
                else
                {

                    MessageBox.Show($"Seleccione un Proceso/Rol a eliminar de {NOMBRE}");
                }

            }
            else {
                MessageBox.Show($"Seleccione un Usuario/Rol");
            }

            
        }
        #endregion

        #region "Agregar Procesos o Roles a Usuarios"
        private async void agregarProcesoRolAUsuarios(object sender, EventArgs e)
        {
            if (listUsuariosRoles.SelectedItem != null)
            {
                if (listProcesosRoles.SelectedItem != null)
                {
                    var selectedItem1 = (dynamic)listUsuariosRoles.SelectedItem;
                    var IdUsuario_Rol = selectedItem1.ID;
                    var TIPO1 = selectedItem1.TIPO;

                    var selectedItem2 = (dynamic)listProcesosRoles.SelectedItem;
                    var IdProceso_Rol = selectedItem2.ID;
                    var NOMBRE = selectedItem2.NOMBRE;
                    var TIPO2 = selectedItem2.TIPO;

                    //MessageBox.Show($"Id de Usuario (Login) o Rol: {IdUsuario_Rol} Y el Tipo es: {TIPO1} --- Id del Proceso o Rol seleccionado: {IdProceso_Rol} Y el Tipo es: {TIPO2}");

                    if (MessageBox.Show("¿Estás seguro de agregar el Proceso/Rol seleccionado?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!listProcesosRolesUsuariosRoles.Items.Cast<dynamic>().Any(item => $"{item.ID}{item.NOMBRE}" == $"{IdProceso_Rol}{NOMBRE}"))
                        {

                            var mensaje = await insertarProcesoRolAUsuarios(IdUsuario_Rol.ToString(), TIPO1, IdProceso_Rol.ToString(), TIPO2);

                            if (mensaje == "Ejecución satisfactoria")
                            {

                                listProcesosRolesUsuariosRoles.DisplayMember = "NOMBRE";
                                listProcesosRolesUsuariosRoles.ValueMember = "ID";
                                listProcesosRolesUsuariosRoles.Items.Add(new { ID = IdProceso_Rol, NOMBRE = NOMBRE, TIPO = TIPO2 });

                            }
                        }
                        else
                        {
                            MessageBox.Show("El Proceso/Rol seleccionado ya existe en la lista.");
                        }

                    }

                }
                else
                {
                    MessageBox.Show($"Seleccione un Proceso/Rol para agregarle a {txtUsuarioRol.Text}");
                }
            }
            else
            {
                MessageBox.Show($"Seleccione un Usuario/Rol");
            }

        }
        #endregion


        public async Task<string> insertarProcesoRolAUsuarios(string IdUsuario_Rol, string TIPO1, string IdProceso_Rol, string TIPO2)
        {

            oReq.Parametros = new List<eParametro>();

            var mensaje = "";

            if (TIPO1 == "USUARIO")
            {
                if (TIPO2 == "ROL")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            INSERT INTO DGI_ROL_USR
                                (
                                    APP_LOGIN,
                                    ROL
                                    )
                            VALUES
                                    (
                                    '{IdUsuario_Rol}',
                                    {IdProceso_Rol}
                                    )
                            ";

                }
                else if (TIPO2 == "PROCESO") {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            INSERT INTO DGI_USR_PROCESO
                                (
                                    APP_LOGIN,
                                    PROCESO
                                    )
                            VALUES
                                    (
                                    '{IdUsuario_Rol}',
                                    {IdProceso_Rol}
                                    )
                            ";

                }


            }
            else if (TIPO1 == "ROL") {

                if (TIPO2 == "PROCESO")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            INSERT INTO DGI_ROL_PROCESO
                                (
                                    ROL,
                                    PROCESO
                                    )
                            VALUES
                                    (
                                    {IdUsuario_Rol},
                                    {IdProceso_Rol}
                                    )
                            ";

                }
                else if (TIPO2 == "ROL") {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            INSERT INTO DGI_ROL_ROL
                                (
                                    ROL,
                                    ROL_ASIG
                                    )
                            VALUES
                                    (
                                    {IdUsuario_Rol},
                                    {IdProceso_Rol}
                                    )
                            ";

                }

            }

            var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    Console.WriteLine(oReq.Query);
                    await _Main.Status($"Proceso/Rol guardado correctamente", (int)MensajeTipo.Success);

                    mensaje = oRes.Message;

                    return mensaje;

                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);

                    return mensaje;
                }
            }

            return mensaje;
        }


        public async Task<string> deleteProcesoRolAUsuarios(string IdUsuario_Rol, string TIPO1, string IdProceso_Rol, string TIPO2)
        {

            oReq.Parametros = new List<eParametro>();

            var mensaje = "";

            if (TIPO1 == "USUARIO")
            {
                if (TIPO2 == "ROL")
                {
                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            DELETE DGI_ROL_USR
                                    WHERE APP_LOGIN = '{IdUsuario_Rol}' AND ROL = {IdProceso_Rol}
                            ";
                }
                else if (TIPO2 == "PROCESO")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            DELETE DGI_USR_PROCESO
                                    WHERE APP_LOGIN = '{IdUsuario_Rol}' AND PROCESO = {IdProceso_Rol}
                            ";

                }

            }
            else if (TIPO1 == "ROL")
            {
                if (TIPO2 == "ROL")
                {
                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            DELETE DGI_ROL_ROL
                                    WHERE ROL = '{IdUsuario_Rol}' AND ROL_ASIG = {IdProceso_Rol}
                            ";
                }
                else if (TIPO2 == "PROCESO")
                {

                    oReq.Tipo = 1;
                    oReq.Query = $@"
                            DELETE DGI_ROL_PROCESO
                                    WHERE ROL = '{IdUsuario_Rol}' AND PROCESO = {IdProceso_Rol}
                            ";

                }
            }

                var oRes = await WSServicio.Servicio(oReq);
            if (!Object.ReferenceEquals(null, oRes))
            {
                if (oRes.Message == "Ejecución satisfactoria")
                {

                    Console.WriteLine(oReq.Query);
                    await _Main.Status($"Proceso/Rol eliminado correctamente", (int)MensajeTipo.Success);

                    mensaje = oRes.Message;

                    return mensaje;

                }
                else
                {
                    await _Main.Status($"{oRes.Err}", (int)MensajeTipo.Error);

                    return mensaje;
                }
            }

            return mensaje;
        }


    }
}
