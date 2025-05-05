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
    public partial class FrmAutorizacionPagoPartes : FormaGenBar
    {
        // Variables
        private long il_numpago, il_parcial, il_maxpago, il_uno, il_dos, il_numpagos, il_temp,
                     il_maxnumpago, il_maxparcial, il_pago_actual;
        private string is_credito, is_tipo, is_maxtipo, is_diferido;
        private decimal ide_mens, id_totmenv, id_saldo, id_tasa, id_finpp;
        private bool ib_buscar, ib_actualizar, ib_existe, ib_recibocero, ib_caso1, ib_caso2;
        private DateTime idt_fhoy, idt_fven;


        // Controls
        private DataGridView dw_importes;
        private Label st_msn;
        private Label st_1;
        private Label st_2;
        private MaskedTextBox em_fecha;
        private GroupBox gb_2;
        private GroupBox gb_1;
        private Label st_3;
        private MaskedTextBox em_rfc;
        private TextBox sle_num_cred;
        private DataGridView dw_autoriza;
        private DataGridView dw_contribuyente;
        private Label st_4;
        private MaskedTextBox em_suc;
        private DataGridView dw_mens;
        private CheckBox cbx_autesp;
        private Label st_autesp;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutorizacion.Checked) 
                txtParcialidad.Enabled = false;
            else
                txtParcialidad.Enabled = true;

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        eRequest oReq = new eRequest();
        DataGridView oGrid = new DataGridView();


        public FrmAutorizacionPagoPartes()
        {
            InitializeComponent();
            Start();
        }

      
        private async Task Start()
        {
           _Titulo = "Conciliación Automática Por Similitud de Referencia";

            oReq.Base = Generales._AppState.Base;
            oReq.EndPoint = Generales._AppState.EndPoint;

            await StartForm();
            //await LoadMainGrid();

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

            txtAnio.Text = DateTime.Now.Year.ToString();

            txtFecha.Text = DateTime.Now.ToString("dd/MMMM/yyyy",
                         new System.Globalization.CultureInfo("es-ES"));


        }



    }
}
