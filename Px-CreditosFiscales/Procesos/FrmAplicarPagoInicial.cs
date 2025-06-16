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
using System.Xml.Linq;



namespace Px_CreditosFiscales
{
    public partial class FrmAplicarPagoInicial : FormaGenBar
    {
        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmAplicarPagoInicial()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Aplicar Pago Inicial";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            txtFecha.Text = DateTime.Now.ToString("dd/MMMM/yyyy",
                         new System.Globalization.CultureInfo("es-ES"));

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

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmPagoInicialDosPartes_Load(object sender, EventArgs e)
        {
        }

        private void textBox24_TextChanged(object sender, KeyEventArgs e)
        {
            
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxMX.ShowDialog(null, $"El crédito no existe:", "Aviso", (int)StatusColorsTypes.Danger, false);
        }
    }
}
