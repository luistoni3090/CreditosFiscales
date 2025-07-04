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



namespace Px_CreditosFiscales
{
    public partial class FrmConceptosObligacionesFiscales : FormaGenBar
    {
        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmConceptosObligacionesFiscales()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Conceptos por Obligaciones Fiscales";

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

            TabControl.TabPages[0].Text = "Datos del Contribuyente";
            TabControl.TabPages[1].Text = "Conceptos";
            TabControl.TabPages[2].Text = "Representante Legal";
            TabControl.TabPages[3].Text = "Acta Constitutiva";


        }
    }
}
