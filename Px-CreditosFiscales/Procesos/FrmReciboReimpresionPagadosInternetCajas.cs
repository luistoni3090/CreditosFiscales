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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;



namespace Px_CreditosFiscales
{
    public partial class FrmReciboReimpresionPagadosInternetCajas : FormaGenBar
    {

        private DataGridView dataGridView1;


        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();

        string fecha = DateTime.Now.ToString("dd/MMMM/yyyy",
                        new System.Globalization.CultureInfo("es-ES"));


        public FrmReciboReimpresionPagadosInternetCajas()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Reimpresión de Recibo Pagados Por Internet y Cajas";

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

            

            //txtFechaDel.Text = fecha;

            //txtFechaAl.Text = fecha;

            await cargaComboMunicipio();


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
            dataGridView1.Columns.Add("Recibo", "Recibo"); 
            dataGridView1.Columns.Add("Credito", "Credito");
            dataGridView1.Columns.Add("Fecha de Emisión", "Fecha de Emisión");

            DataGridViewColumn Importe = new DataGridViewTextBoxColumn();
            Importe.Name = "Tipo Pago";
            Importe.HeaderText = "Tipo Pago";
            Importe.DefaultCellStyle.Format = "C2";
            dataGridView1.Columns.Add(Importe);

            /*
            dataGridView1.Columns.Add("Financiamiento", "Financiamiento");
            dataGridView1.Columns.Add("Recargo", "Recargo");
            dataGridView1.Columns.Add("Actualización", "Actualización");

            DataGridViewColumn Total = new DataGridViewTextBoxColumn();
            Total.Name = "Total";
            Total.HeaderText = "Total";
            Total.DefaultCellStyle.Format = "C2";
            Total.ReadOnly = true;
            dataGridView1.Columns.Add(Total);*/

            panelGrid.Controls.Add(dataGridView1);

        }


        private void panContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private async Task cargaComboMunicipio()
        {

            var comboList = new List<ComboBoxItem>();

            var item1 = new ComboBoxItem
            {
                Value = "1",
                Text = "MEXICALI"
            };
            var item2 = new ComboBoxItem
            {
                Value = "2",
                Text = "TIJUANA"
            };
            var item3 = new ComboBoxItem
            {
                Value = "3",
                Text = "ENSENADA"
            };
            var item4 = new ComboBoxItem
            {
                Value = "4",
                Text = "TECATE"
            };
            var item5 = new ComboBoxItem
            {
                Value = "5",
                Text = "ROSARITO"
            };
          

            comboList.Add(item1);
            comboList.Add(item2);
            comboList.Add(item3);
            comboList.Add(item4);
            comboList.Add(item5);

            // Asignar la lista al ComboBox
            comboMunicipio.DataSource = comboList;
            comboMunicipio.DisplayMember = "Text";
            comboMunicipio.ValueMember = "Value";
            comboMunicipio.SelectedIndex = -1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeriodo.Checked)
            {
                txtCredito.Enabled = false;
                txtFechaAl.Enabled = true;
                txtFechaDel.Enabled = true;

            }
            else
            {
                txtCredito.Enabled = true;
                txtFechaAl.Enabled = false;
                txtFechaDel.Enabled = false;
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (chkPeriodo.Checked)
            {
                MessageBoxMX.ShowDialog(null,  $"No se encontró Recibo(s) dentro del rango " + txtFechaDel.Text + " - " + txtFechaAl.Text + "", "Aviso", (int)StatusColorsTypes.Warning, false);
            }
            else if (rdInternet.Checked && rdConvenio.Checked || rdInternet.Checked && rdLiquidacion.Checked)
            {
                MessageBoxMX.ShowDialog(null,  $"No se encontró Recibo para el Crédito " + txtCredito.Text + " con fecha "+ fecha +" ", "Aviso", (int)StatusColorsTypes.Warning, false);
            }
            else if (rdCajas.Checked && rdConvenio.Checked || rdCajas.Checked && rdLiquidacion.Checked)
            {
                MessageBoxMX.ShowDialog(null, $"No se encontró Recibo para el Crédito " + txtCredito.Text + " con fecha " + fecha + " ", "Aviso", (int)StatusColorsTypes.Warning, false);
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            MessageBoxMX.ShowDialog(null, $"No existe información a seleccionar", "Aviso", (int)StatusColorsTypes.Warning, false);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCajas.Checked)
            {
                rdLiquidacion.Enabled = true;
            }
            else
            {
                rdLiquidacion.Enabled = false;
            }
        }
    }
}
