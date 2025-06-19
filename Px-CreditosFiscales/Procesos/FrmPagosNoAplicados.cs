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
    public partial class FrmPagosNoAplicados: FormaGenBar
    {

        private DataGridView dataGridView1;


        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmPagosNoAplicados()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Aplicación Individual de Pagos";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            await LoadMainGrid();

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

        private async Task LoadMainGrid()
        {


            dataGridView1 = new DataGridView
            {
                Dock = DockStyle.Fill,
                Name = "dataGridView1",
                TabIndex = 0
            };


            // Configurar las columnas del DataGridView
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.ReadOnly = false;

            // Crear y agregar columnas
            dataGridView1.Columns.Add("Inciso", "Inciso");
            dataGridView1.Columns.Add("Descripción", "Descripción");

            DataGridViewColumn Importe = new DataGridViewTextBoxColumn();
            Importe.Name = "Importe";
            Importe.HeaderText = "Importe";
            Importe.DefaultCellStyle.Format = "C2";
            dataGridView1.Columns.Add(Importe);

            panelGrid.Controls.Add(dataGridView1);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            MessageBoxMX.ShowDialog(null,  $"No se encontraron recibos para el crédito indicado ", "Aviso", (int)StatusColorsTypes.Warning, false);
        }

        private void panContent_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
