using Px_CreditosFiscales.Utiles.Formas;
using System;
using System.Windows.Forms;

namespace Px_CreditosFiscales
{
    public partial class FrmReciboReimpresionPagadosInternetCajas : FormaGenBar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReciboReimpresionPagadosInternetCajas));
            this.panContent = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdCajas = new System.Windows.Forms.RadioButton();
            this.rdInternet = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboMunicipio = new System.Windows.Forms.ComboBox();
            this.txtFechaAl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaDel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkPeriodo = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rdLiquidacion = new System.Windows.Forms.RadioButton();
            this.rdConvenio = new System.Windows.Forms.RadioButton();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensajes = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFormatoPoliza = new System.Windows.Forms.ToolStripStatusLabel();
            this.stFormato = new System.Windows.Forms.Label();
            this.stErroneos = new System.Windows.Forms.Label();
            this.stCuentasErroneas = new System.Windows.Forms.Label();
            this.panMenu = new System.Windows.Forms.Panel();
            this.btnmnuImprime = new System.Windows.Forms.Button();
            this.btnmnuAyuda = new System.Windows.Forms.Button();
            this.panContent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.groupBox1);
            this.panContent.Controls.Add(this.checkBox1);
            this.panContent.Controls.Add(this.panelGrid);
            this.panContent.Controls.Add(this.groupBox4);
            this.panContent.Controls.Add(this.statusStrip1);
            this.panContent.Controls.Add(this.stFormato);
            this.panContent.Controls.Add(this.stErroneos);
            this.panContent.Controls.Add(this.stCuentasErroneas);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 76);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(784, 475);
            this.panContent.TabIndex = 10;
            this.panContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panContent_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdCajas);
            this.groupBox1.Controls.Add(this.rdInternet);
            this.groupBox1.Location = new System.Drawing.Point(16, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(746, 65);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recibo Pagado por:";
            // 
            // rdCajas
            // 
            this.rdCajas.AutoSize = true;
            this.rdCajas.Location = new System.Drawing.Point(527, 28);
            this.rdCajas.Name = "rdCajas";
            this.rdCajas.Size = new System.Drawing.Size(51, 17);
            this.rdCajas.TabIndex = 63;
            this.rdCajas.Text = "Cajas";
            this.rdCajas.UseVisualStyleBackColor = true;
            this.rdCajas.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rdInternet
            // 
            this.rdInternet.AutoSize = true;
            this.rdInternet.Checked = true;
            this.rdInternet.Location = new System.Drawing.Point(103, 28);
            this.rdInternet.Name = "rdInternet";
            this.rdInternet.Size = new System.Drawing.Size(61, 17);
            this.rdInternet.TabIndex = 62;
            this.rdInternet.TabStop = true;
            this.rdInternet.Text = "Internet";
            this.rdInternet.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 423);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 52;
            this.checkBox1.Text = "Todos";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // panelGrid
            // 
            this.panelGrid.Location = new System.Drawing.Point(16, 243);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(751, 163);
            this.panelGrid.TabIndex = 51;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.rdLiquidacion);
            this.groupBox4.Controls.Add(this.rdConvenio);
            this.groupBox4.Controls.Add(this.txtCredito);
            this.groupBox4.Location = new System.Drawing.Point(16, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(746, 130);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Busqueda por:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboMunicipio);
            this.groupBox2.Controls.Add(this.txtFechaAl);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtFechaDel);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkPeriodo);
            this.groupBox2.Location = new System.Drawing.Point(345, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 102);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Municipio:";
            // 
            // comboMunicipio
            // 
            this.comboMunicipio.FormattingEnabled = true;
            this.comboMunicipio.Location = new System.Drawing.Point(161, 66);
            this.comboMunicipio.Name = "comboMunicipio";
            this.comboMunicipio.Size = new System.Drawing.Size(156, 21);
            this.comboMunicipio.TabIndex = 70;
            // 
            // txtFechaAl
            // 
            this.txtFechaAl.Enabled = false;
            this.txtFechaAl.Location = new System.Drawing.Point(246, 27);
            this.txtFechaAl.Name = "txtFechaAl";
            this.txtFechaAl.Size = new System.Drawing.Size(99, 20);
            this.txtFechaAl.TabIndex = 69;
            this.txtFechaAl.Text = "00/00/0000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Al:";
            // 
            // txtFechaDel
            // 
            this.txtFechaDel.Enabled = false;
            this.txtFechaDel.Location = new System.Drawing.Point(81, 27);
            this.txtFechaDel.Name = "txtFechaDel";
            this.txtFechaDel.Size = new System.Drawing.Size(99, 20);
            this.txtFechaDel.TabIndex = 67;
            this.txtFechaDel.Text = "00/00/0000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Del:";
            // 
            // chkPeriodo
            // 
            this.chkPeriodo.AutoSize = true;
            this.chkPeriodo.Location = new System.Drawing.Point(20, 0);
            this.chkPeriodo.Name = "chkPeriodo";
            this.chkPeriodo.Size = new System.Drawing.Size(65, 17);
            this.chkPeriodo.TabIndex = 0;
            this.chkPeriodo.Text = "Periodo:";
            this.chkPeriodo.UseVisualStyleBackColor = true;
            this.chkPeriodo.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 64;
            this.button1.Text = "buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdLiquidacion
            // 
            this.rdLiquidacion.AutoSize = true;
            this.rdLiquidacion.Enabled = false;
            this.rdLiquidacion.Location = new System.Drawing.Point(111, 43);
            this.rdLiquidacion.Name = "rdLiquidacion";
            this.rdLiquidacion.Size = new System.Drawing.Size(79, 17);
            this.rdLiquidacion.TabIndex = 63;
            this.rdLiquidacion.Text = "Liquidación";
            this.rdLiquidacion.UseVisualStyleBackColor = true;
            // 
            // rdConvenio
            // 
            this.rdConvenio.AutoSize = true;
            this.rdConvenio.Checked = true;
            this.rdConvenio.Location = new System.Drawing.Point(20, 43);
            this.rdConvenio.Name = "rdConvenio";
            this.rdConvenio.Size = new System.Drawing.Size(70, 17);
            this.rdConvenio.TabIndex = 62;
            this.rdConvenio.TabStop = true;
            this.rdConvenio.Text = "Convenio";
            this.rdConvenio.UseVisualStyleBackColor = true;
            // 
            // txtCredito
            // 
            this.txtCredito.Location = new System.Drawing.Point(14, 74);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(198, 20);
            this.txtCredito.TabIndex = 55;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes,
            this.lblFormatoPoliza});
            this.statusStrip1.Location = new System.Drawing.Point(0, 453);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensajes
            // 
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(0, 17);
            // 
            // lblFormatoPoliza
            // 
            this.lblFormatoPoliza.Name = "lblFormatoPoliza";
            this.lblFormatoPoliza.Size = new System.Drawing.Size(0, 17);
            // 
            // stFormato
            // 
            this.stFormato.AutoSize = true;
            this.stFormato.Location = new System.Drawing.Point(1250, 1060);
            this.stFormato.Name = "stFormato";
            this.stFormato.Size = new System.Drawing.Size(0, 13);
            this.stFormato.TabIndex = 18;
            // 
            // stErroneos
            // 
            this.stErroneos.AutoSize = true;
            this.stErroneos.Location = new System.Drawing.Point(3220, 900);
            this.stErroneos.Name = "stErroneos";
            this.stErroneos.Size = new System.Drawing.Size(0, 13);
            this.stErroneos.TabIndex = 19;
            // 
            // stCuentasErroneas
            // 
            this.stCuentasErroneas.AutoSize = true;
            this.stCuentasErroneas.Location = new System.Drawing.Point(3120, 800);
            this.stCuentasErroneas.Name = "stCuentasErroneas";
            this.stCuentasErroneas.Size = new System.Drawing.Size(91, 13);
            this.stCuentasErroneas.TabIndex = 20;
            this.stCuentasErroneas.Text = "Cuentas Erroneas";
            // 
            // panMenu
            // 
            this.panMenu.Controls.Add(this.btnmnuImprime);
            this.panMenu.Controls.Add(this.btnmnuAyuda);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMenu.Location = new System.Drawing.Point(0, 37);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(784, 39);
            this.panMenu.TabIndex = 9;
            // 
            // btnmnuImprime
            // 
            this.btnmnuImprime.BackColor = System.Drawing.Color.White;
            this.btnmnuImprime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmnuImprime.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnmnuImprime.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.btnmnuImprime.FlatAppearance.BorderSize = 0;
            this.btnmnuImprime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmnuImprime.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmnuImprime.ForeColor = System.Drawing.Color.White;
            this.btnmnuImprime.Image = ((System.Drawing.Image)(resources.GetObject("btnmnuImprime.Image")));
            this.btnmnuImprime.Location = new System.Drawing.Point(714, 0);
            this.btnmnuImprime.Name = "btnmnuImprime";
            this.btnmnuImprime.Size = new System.Drawing.Size(35, 39);
            this.btnmnuImprime.TabIndex = 9;
            this.btnmnuImprime.Text = "&i";
            this.btnmnuImprime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuImprime.UseVisualStyleBackColor = false;
            // 
            // btnmnuAyuda
            // 
            this.btnmnuAyuda.BackColor = System.Drawing.Color.White;
            this.btnmnuAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmnuAyuda.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnmnuAyuda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.btnmnuAyuda.FlatAppearance.BorderSize = 0;
            this.btnmnuAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmnuAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmnuAyuda.ForeColor = System.Drawing.Color.White;
            this.btnmnuAyuda.Image = ((System.Drawing.Image)(resources.GetObject("btnmnuAyuda.Image")));
            this.btnmnuAyuda.Location = new System.Drawing.Point(749, 0);
            this.btnmnuAyuda.Name = "btnmnuAyuda";
            this.btnmnuAyuda.Size = new System.Drawing.Size(35, 39);
            this.btnmnuAyuda.TabIndex = 8;
            this.btnmnuAyuda.Text = "&h";
            this.btnmnuAyuda.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuAyuda.UseVisualStyleBackColor = false;
            // 
            // FrmReciboReimpresionPagadosInternetCajas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 551);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(798, 444);
            this.Name = "FrmReciboReimpresionPagadosInternetCajas";
            this.Text = "Conciliación Automática";
            this.Controls.SetChildIndex(this.panMenu, 0);
            this.Controls.SetChildIndex(this.panContent, 0);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Panel panContent;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblMensajes;
        private ToolStripStatusLabel lblFormatoPoliza;
        private Label stFormato;
        private Label stErroneos;
        private Label stCuentasErroneas;
        private Panel panMenu;
        private Button btnmnuImprime;
        private Button btnmnuAyuda;
        private GroupBox groupBox4;
        private TextBox txtCredito;
        private RadioButton rdLiquidacion;
        private RadioButton rdConvenio;
        private CheckBox chkPeriodo;
        private Panel panelGrid;
        private Button button1;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private RadioButton rdCajas;
        private RadioButton rdInternet;
        private GroupBox groupBox2;
        private TextBox txtFechaDel;
        private Label label8;
        private TextBox txtFechaAl;
        private Label label1;
        private Label label2;
        private ComboBox comboMunicipio;
    }
}
