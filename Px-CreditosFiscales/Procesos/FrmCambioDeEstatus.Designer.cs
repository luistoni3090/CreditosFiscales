using Px_CreditosFiscales.Utiles.Formas;
using System;
using System.Windows.Forms;

namespace Px_CreditosFiscales
{
    public partial class FrmCambioDeEstatus : FormaGenBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCambioDeEstatus));
            this.panContent = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelGrid2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelGrid1 = new System.Windows.Forms.Panel();
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
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.button4);
            this.panContent.Controls.Add(this.button3);
            this.panContent.Controls.Add(this.button2);
            this.panContent.Controls.Add(this.button1);
            this.panContent.Controls.Add(this.groupBox1);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Controls.Add(this.txtFecha);
            this.panContent.Controls.Add(this.groupBox3);
            this.panContent.Controls.Add(this.statusStrip1);
            this.panContent.Controls.Add(this.stFormato);
            this.panContent.Controls.Add(this.stErroneos);
            this.panContent.Controls.Add(this.stCuentasErroneas);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 76);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(1174, 373);
            this.panContent.TabIndex = 10;
            this.panContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panContent_Paint);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(981, 308);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 60;
            this.button4.Text = "Detalle";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(421, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 59;
            this.button3.Text = "Detalle";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(519, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 58;
            this.button2.Text = "<<";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 57;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelGrid2);
            this.groupBox1.Location = new System.Drawing.Point(602, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 242);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recibos Inconsistentes";
            // 
            // panelGrid2
            // 
            this.panelGrid2.Location = new System.Drawing.Point(18, 31);
            this.panelGrid2.Name = "panelGrid2";
            this.panelGrid2.Size = new System.Drawing.Size(419, 194);
            this.panelGrid2.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Fecha:";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(76, 10);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(116, 20);
            this.txtFecha.TabIndex = 55;
            this.txtFecha.Text = "02/05/2005";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelGrid1);
            this.groupBox3.Location = new System.Drawing.Point(25, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 242);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Recibos no pagados";
            // 
            // panelGrid1
            // 
            this.panelGrid1.Location = new System.Drawing.Point(18, 31);
            this.panelGrid1.Name = "panelGrid1";
            this.panelGrid1.Size = new System.Drawing.Size(436, 194);
            this.panelGrid1.TabIndex = 51;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes,
            this.lblFormatoPoliza});
            this.statusStrip1.Location = new System.Drawing.Point(0, 351);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1174, 22);
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
            this.panMenu.Size = new System.Drawing.Size(1174, 39);
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
            this.btnmnuImprime.Location = new System.Drawing.Point(1104, 0);
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
            this.btnmnuAyuda.Location = new System.Drawing.Point(1139, 0);
            this.btnmnuAyuda.Name = "btnmnuAyuda";
            this.btnmnuAyuda.Size = new System.Drawing.Size(35, 39);
            this.btnmnuAyuda.TabIndex = 8;
            this.btnmnuAyuda.Text = "&h";
            this.btnmnuAyuda.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuAyuda.UseVisualStyleBackColor = false;
            // 
            // FrmCambioDeEstatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 449);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(798, 444);
            this.Name = "FrmCambioDeEstatus";
            this.Text = "Conciliación Automática";
            this.Controls.SetChildIndex(this.panMenu, 0);
            this.Controls.SetChildIndex(this.panContent, 0);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private GroupBox groupBox3;
        private Panel panelGrid1;
        private TextBox txtFecha;
        private Label label1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private GroupBox groupBox1;
        private Panel panelGrid2;
    }
}
