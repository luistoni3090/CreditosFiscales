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
    public partial class FrmCancelacionCreditoPreinscripcion : FormaGenBar
    {

        private DataGridView dataGridView1;


        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmCancelacionCreditoPreinscripcion()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Cancelación del Crédito por Preinscripción";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            await cargaComboMotivos();

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

        private async Task cargaComboMotivos()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "1",
                Text = "INCONCISTENCIA EN LA ALTA DEL CREDITO"
            };
            var item2 = new ComboBoxItem
            {
                Value = "2",
                Text = "PRESCRIPCION"
            };
            var item3 = new ComboBoxItem
            {
                Value = "3",
                Text = "DECRETO FEDERAL"
            };
            var item4 = new ComboBoxItem
            {
                Value = "4",
                Text = "JUICIO"
            };
            var item5 = new ComboBoxItem
            {
                Value = "5",
                Text = "POR FALTA DE GARANTIA"
            };
            var item6 = new ComboBoxItem
            {
                Value = "6",
                Text = "DESCONOC DEL PROCED DEL USUARIO"
            };
            var item7 = new ComboBoxItem
            {
                Value = "7",
                Text = "ACUERDOS ESTATALES"
            };

            comboList.Add(item1);
            comboList.Add(item2);
            comboList.Add(item3);
            comboList.Add(item4);
            comboList.Add(item5);
            comboList.Add(item6);
            comboList.Add(item7);

            // Asignar la lista al ComboBox
            comboMotivos.DataSource = comboList;
            comboMotivos.DisplayMember = "Text";
            comboMotivos.ValueMember = "Value";
            comboMotivos.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string credito = txtCredito.Text;

            MessageBoxMX.ShowDialog(null, $"El crédito " + credito + " Ya esta cancelado ", "Aviso", (int)StatusColorsTypes.Danger, false);
        }
    }
}
