using Px_Licencias.Models.Api;
using Px_Licencias.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Licencias
{
    public partial class Form1 : Form
    {
        eRequest oReq = new eRequest();

        public Form1()
        {
            InitializeComponent();


            oReq.Base = "licencias";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.DataSource = null;

            oReq.Query = "SELECT * FROM DGI_USR WHERE APP_LOGIN = :APP_LOGIN";
            oReq.Parametros = new List<eParametro> {
                new eParametro(){ Tipo = DbType.String, Nombre = "APP_LOGIN", Valor = "ABARCELO"}
            };

            var oRes = await WSServicio.Servicio(oReq);

            lblRes.Text = oRes.Message;

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                dataGridView1.DataSource = oRes.Data.Tables[0];

            Cursor = Cursors.Default;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.DataSource = null;

            oReq.Query = "SELECT * FROM DGI_USR";

            var oRes = await WSServicio.Servicio(oReq);

            lblRes.Text = oRes.Message;

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                dataGridView1.DataSource = oRes.Data.Tables[0];

            Cursor = Cursors.Default;

        }

        private async void button3_Click(object sender, EventArgs e)
        {


            Cursor = Cursors.WaitCursor;

            dataGridView1.DataSource = null;

            oReq.Query = @"
            SELECT DGI_USR.RFC, DGI_USR.SYSUSER, DGI_USR_BD.SYSPWD, DGI_ROL_USR.ROL,
                   DGI_USR.OFI_REC, DGI_USR.MUNICIPIO, DGI_USR.DELEGACION, DGI_USR.STATUS,
                   P.ESTADO, NO_SISTEMA_GRAL, CAMBIA_LIC_DUP, FECHA_COBRO_CAJAS, CONCEPTO_CAJAS, SYS_CLAVE_CAJAS 
            FROM DGI.DGI_ROL_USR
            JOIN DGI.DGI_USR ON DGI_ROL_USR.APP_LOGIN = DGI_USR.APP_LOGIN
            JOIN DGI.DGI_USR_BD ON DGI_USR.SYSUSER = DGI_USR_BD.SYSUSER
            JOIN DGI.PARAMETROS P ON 1 = 1
            WHERE DGI_ROL_USR.SYS_CLAVE = :li_sistema
              AND DGI_USR_BD.SYS_CLAVE = :li_sistema
              AND DGI_USR.APP_LOGIN = :lc_applog
              AND DGI_USR.APP_PWD = :lc_apppwd
              AND DGI_USR.SYS_CLAVE = :li_sistema";

            oReq.Parametros = new List<eParametro>
                {
                    new eParametro() { Tipo = DbType.Int32, Nombre = "li_sistema", Valor = 1},
                    new eParametro() { Tipo = DbType.String, Nombre = "lc_applog", Valor = ""},
                    new eParametro() { Tipo = DbType.String, Nombre = "lc_apppwd", Valor = ""}
                };

            var oRes = await WSServicio.Servicio(oReq);

            lblRes.Text = oRes.Message;

            if (oRes.Data.Tables.Count != 0)
                dataGridView1.DataSource = oRes.Data.Tables[0];

            Cursor = Cursors.Default;


        }
    }
}
