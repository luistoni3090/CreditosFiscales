using Px_CreditosFiscales.Utiles.Formas;
using Px_Utiles.Models.Api;
using Px_Utiles.Servicio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Px_CreditosFiscales.Utiles.Emun.Enumerados;

using Px_CreditosFiscales.Utiles.Generales;
using Px_Controles.Colors;
using Px_Controles.Forms.Msg;
using System.Data.SqlClient;
using Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos;
using Px_Utiles.Models.Sistemas.Contabilidad.Catalogos;
using System.Diagnostics.Eventing.Reader;
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using Px_CreditosFiscales.Utiles.Win32;
using System.Diagnostics;



namespace Px_CreditosFiscales
{
    public partial class FrmCierreMes : FormaGenBar
    {

        private DataGridView dataGridView1;


        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmCierreMes()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Cierre del Mes";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task StartForm()
        {

            
            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 450);

            this.BackColor = Color.White;

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            if (_Main != null)
                await _Main.Status($"{_Titulo}", (int)MensajeTipo.Info);

        }


        private void panContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxMX.ShowDialog(null,  $"Se realizó el cierre del mes ", "Aviso", (int)StatusColorsTypes.Success, false);
        }
    }
}
