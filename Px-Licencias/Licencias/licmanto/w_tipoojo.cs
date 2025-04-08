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

namespace Px_Licencias.Licencias.licmanto
{
    public partial class w_tipoojo : Form
    {
        eRequest oReq = new eRequest();

        public w_tipoojo()
        {
            InitializeComponent();

           Inicio();
            Consulta();
        }

        private async Task Inicio() 
        {
            btnGuardar.Click += btnGuardar_Click;
            dataGridView1.AutoGenerateColumns = false;
        }


        private async Task Consulta()
        {
            oReq.Query = $@"
                    SELECT 
                        OJO,
                        DESCR
                    FROM OJO
            ";


            var oRes = await WSServicio.Servicio(oReq);

            if (!Object.ReferenceEquals(null, oRes.Data.Tables[0]))
                dataGridView1.DataSource = oRes.Data.Tables[0];

        }


        private async Task Guardar() 
        {

            oReq.Tipo = 1;
            oReq.Query = "Insert Into OJO ( OJO, DESCR ) Values ( :OJO, :DESCR )";
            oReq.Parametros = new List<eParametro> {
                new eParametro(){ Tipo = DbType.Int32, Nombre = "OJO,", Valor = 11},
                new eParametro(){ Tipo = DbType.String, Nombre = "DESCR", Valor = txtColor.Text}
            };

            var oRes = await WSServicio.Servicio(oReq);

        }



        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await Guardar();
        }
    }
}
