using Px_ConciliacionBancaria.Utiles.Formas;
using System;
using System.Windows.Forms;

namespace PX_ConciliacionBancaria
{
    public partial class FrmRegistroDeAjustes : FormaGenBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroDeAjustes));
            this.panContent = new System.Windows.Forms.Panel();
            this.comboMes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCuenta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBanco = new System.Windows.Forms.ComboBox();
            this.stPoliza = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensajes = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFormatoPoliza = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutorizar = new System.Windows.Forms.Button();
            this.cbProcesar = new System.Windows.Forms.Button();
            this.stFormato = new System.Windows.Forms.Label();
            this.stErroneos = new System.Windows.Forms.Label();
            this.stCuentasErroneas = new System.Windows.Forms.Label();
            this.panMenu = new System.Windows.Forms.Panel();
            this.btnmnuImprime = new System.Windows.Forms.Button();
            this.btnmnuAyuda = new System.Windows.Forms.Button();
            this.panContent.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.comboMes);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.comboCuenta);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.comboBanco);
            this.panContent.Controls.Add(this.stPoliza);
            this.panContent.Controls.Add(this.statusStrip1);
            this.panContent.Controls.Add(this.txtAnio);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Controls.Add(this.cbAutorizar);
            this.panContent.Controls.Add(this.cbProcesar);
            this.panContent.Controls.Add(this.stFormato);
            this.panContent.Controls.Add(this.stErroneos);
            this.panContent.Controls.Add(this.stCuentasErroneas);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 76);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(784, 358);
            this.panContent.TabIndex = 10;
            // 
            // comboMes
            // 
            this.comboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMes.FormattingEnabled = true;
            this.comboMes.Location = new System.Drawing.Point(375, 83);
            this.comboMes.Name = "comboMes";
            this.comboMes.Size = new System.Drawing.Size(222, 21);
            this.comboMes.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(339, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Mes:";
            // 
            // comboCuenta
            // 
            this.comboCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCuenta.FormattingEnabled = true;
            this.comboCuenta.Location = new System.Drawing.Point(66, 83);
            this.comboCuenta.Name = "comboCuenta";
            this.comboCuenta.Size = new System.Drawing.Size(222, 21);
            this.comboCuenta.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Cuenta:";
            // 
            // comboBanco
            // 
            this.comboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBanco.FormattingEnabled = true;
            this.comboBanco.Location = new System.Drawing.Point(66, 28);
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(222, 21);
            this.comboBanco.TabIndex = 14;
            this.comboBanco.SelectedValueChanged += new System.EventHandler(this.comboBanco_SelectedValueChanged);
            // 
            // stPoliza
            // 
            this.stPoliza.AutoSize = true;
            this.stPoliza.Location = new System.Drawing.Point(22, 31);
            this.stPoliza.Name = "stPoliza";
            this.stPoliza.Size = new System.Drawing.Size(41, 13);
            this.stPoliza.TabIndex = 13;
            this.stPoliza.Text = "Banco:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes,
            this.lblFormatoPoliza});
            this.statusStrip1.Location = new System.Drawing.Point(0, 336);
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
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(375, 24);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(222, 20);
            this.txtAnio.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Año:";
            // 
            // cbAutorizar
            // 
            this.cbAutorizar.Location = new System.Drawing.Point(25, 142);
            this.cbAutorizar.Name = "cbAutorizar";
            this.cbAutorizar.Size = new System.Drawing.Size(100, 30);
            this.cbAutorizar.TabIndex = 11;
            this.cbAutorizar.Text = "Autorizar";
            this.cbAutorizar.UseVisualStyleBackColor = true;
            this.cbAutorizar.Click += new System.EventHandler(this.detcbAutorizar);
            // 
            // cbProcesar
            // 
            this.cbProcesar.Location = new System.Drawing.Point(144, 142);
            this.cbProcesar.Name = "cbProcesar";
            this.cbProcesar.Size = new System.Drawing.Size(100, 30);
            this.cbProcesar.TabIndex = 12;
            this.cbProcesar.Text = "Salir";
            this.cbProcesar.UseVisualStyleBackColor = true;
            this.cbProcesar.Click += new System.EventHandler(this.btnSalir_Click);
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
            this.stErroneos.Location = new System.Drawing.Point(0, 0);
            this.stErroneos.Name = "stErroneos";
            this.stErroneos.Size = new System.Drawing.Size(100, 23);
            this.stErroneos.TabIndex = 35;
            // 
            // stCuentasErroneas
            // 
            this.stCuentasErroneas.Location = new System.Drawing.Point(0, 0);
            this.stCuentasErroneas.Name = "stCuentasErroneas";
            this.stCuentasErroneas.Size = new System.Drawing.Size(100, 23);
            this.stCuentasErroneas.TabIndex = 36;
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
            // FrmRegistroDeAjustes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 434);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(798, 444);
            this.Name = "FrmRegistroDeAjustes";
            this.Text = "Registro de Ajustes";
            this.Controls.SetChildIndex(this.panMenu, 0);
            this.Controls.SetChildIndex(this.panContent, 0);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Panel panContent;
        private ComboBox comboMes;
        private Label label4;
        private ComboBox comboCuenta;
        private Label label3;
        private Label stPoliza;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblMensajes;
        private ToolStripStatusLabel lblFormatoPoliza;
        private TextBox txtAnio;
        private Label label2;
        private Button cbAutorizar;
        private Button cbProcesar;
        private Label stFormato;
        private Label stErroneos;
        private Label stCuentasErroneas;
        private Panel panMenu;
        private ComboBox comboBanco;
        private Button btnmnuImprime;
        private Button btnmnuAyuda;
    }
}
