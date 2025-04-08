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

namespace Px_Licencias.Licencias.licmanto2
{
    public partial class w_cat_usr_list : Form
    {

        eRequest oReq = new eRequest();

        public w_cat_usr_list()
        {
            InitializeComponent();
       }


        public async Task  GetUserDetails()
        {
            DataTable dt = new DataTable();

            dataGridView1.AutoGenerateColumns = false;

            oReq.Query = $@"
            SELECT APP_LOGIN,   
                 APP_PWD,   
                 SYSUSER,   
                 RFC,   
                 NVL(NOMBRE,' ') AS NOMBRE,   
                 NVL(AP_PATER,' ') AS AP_PATER,   
                 NVL(AP_MATER,' ') AS AP_MATER,   
                 DOMICILIO,   
                 STATUS,   
                 OFI_REC,   
                 SYS_CLAVE,
                MUNICIPIO,
                CAJA_ASIGNADA,
	            'N' AS PASS_MODIFICADO
            FROM DGI.DGI_USR  
            ";


            var oRes = await WSServicio.Servicio(oReq);

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                dataGridView1.DataSource = oRes.Data.Tables[0];


        }

        private async void button1_Click(object sender, EventArgs e)
        {

            await GetUserDetails();
        }
    }
}
