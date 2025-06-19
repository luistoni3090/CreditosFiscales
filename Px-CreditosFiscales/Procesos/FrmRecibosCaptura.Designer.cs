using Px_CreditosFiscales.Utiles.Formas;
using System;
using System.Windows.Forms;

namespace Px_CreditosFiscales
{
    public partial class FrmRecibosCaptura : FormaGenBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecibosCaptura));
            this.panContent = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelGrid = new System.Windows.Forms.Panel();
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
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.groupBox5);
            this.panContent.Controls.Add(this.groupBox2);
            this.panContent.Controls.Add(this.groupBox1);
            this.panContent.Controls.Add(this.groupBox4);
            this.panContent.Controls.Add(this.groupBox3);
            this.panContent.Controls.Add(this.statusStrip1);
            this.panContent.Controls.Add(this.stFormato);
            this.panContent.Controls.Add(this.stErroneos);
            this.panContent.Controls.Add(this.stCuentasErroneas);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 76);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(1017, 670);
            this.panContent.TabIndex = 10;
            this.panContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panContent_Paint);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox14);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.textBox15);
            this.groupBox5.Controls.Add(this.textBox16);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.textBox17);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Location = new System.Drawing.Point(500, 117);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(482, 95);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Recibo de Caja";
            // 
            // textBox14
            // 
            this.textBox14.Enabled = false;
            this.textBox14.Location = new System.Drawing.Point(362, 57);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(110, 20);
            this.textBox14.TabIndex = 61;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(291, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "Consecutivo:";
            // 
            // textBox15
            // 
            this.textBox15.Enabled = false;
            this.textBox15.Location = new System.Drawing.Point(87, 21);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(200, 20);
            this.textBox15.TabIndex = 59;
            // 
            // textBox16
            // 
            this.textBox16.Enabled = false;
            this.textBox16.Location = new System.Drawing.Point(362, 19);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(110, 20);
            this.textBox16.TabIndex = 55;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(315, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "Recibo:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(57, 62);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 58;
            this.label16.Text = "Caja:";
            // 
            // textBox17
            // 
            this.textBox17.Enabled = false;
            this.textBox17.Location = new System.Drawing.Point(89, 58);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(44, 20);
            this.textBox17.TabIndex = 57;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 56;
            this.label17.Text = "Recaudación:";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(21, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 111);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pago:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox12);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(500, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 95);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Último Pago";
            // 
            // textBox12
            // 
            this.textBox12.Enabled = false;
            this.textBox12.Location = new System.Drawing.Point(363, 59);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(112, 20);
            this.textBox12.TabIndex = 63;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(273, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "Fecha de Pago:";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(216, 58);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(47, 20);
            this.textBox5.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "Parcialidad:";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(62, 21);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(72, 20);
            this.textBox3.TabIndex = 59;
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(293, 19);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(110, 20);
            this.textBox6.TabIndex = 55;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(171, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Fecha de Vencimiento:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Pago:";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(62, 58);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(70, 20);
            this.textBox4.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Recibo:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox11);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.textBox10);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.textBox9);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.textBox8);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textBox7);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.txtCredito);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(21, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(459, 174);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos Generales";
            // 
            // textBox11
            // 
            this.textBox11.Enabled = false;
            this.textBox11.Location = new System.Drawing.Point(307, 135);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(127, 20);
            this.textBox11.TabIndex = 67;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(231, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Valor del UDI:";
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Location = new System.Drawing.Point(307, 95);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(127, 20);
            this.textBox10.TabIndex = 65;
            this.textBox10.Text = "00/00/0000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(222, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 64;
            this.label10.Text = "Fecha de Pago:";
            // 
            // textBox9
            // 
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(307, 58);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(127, 20);
            this.textBox9.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "No. de Pagos:";
            // 
            // textBox8
            // 
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(307, 23);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(127, 20);
            this.textBox8.TabIndex = 61;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "UDIS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Mensualidad";
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(57, 135);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(127, 20);
            this.textBox7.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Crédito:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(76, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtCredito
            // 
            this.txtCredito.Location = new System.Drawing.Point(57, 23);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(127, 20);
            this.txtCredito.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Recibo:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelGrid);
            this.groupBox3.Location = new System.Drawing.Point(21, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 307);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "incisos";
            // 
            // panelGrid
            // 
            this.panelGrid.Location = new System.Drawing.Point(18, 31);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(925, 260);
            this.panelGrid.TabIndex = 51;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensajes,
            this.lblFormatoPoliza});
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1017, 22);
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
            this.panMenu.Size = new System.Drawing.Size(1017, 39);
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
            this.btnmnuImprime.Location = new System.Drawing.Point(947, 0);
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
            this.btnmnuAyuda.Location = new System.Drawing.Point(982, 0);
            this.btnmnuAyuda.Name = "btnmnuAyuda";
            this.btnmnuAyuda.Size = new System.Drawing.Size(35, 39);
            this.btnmnuAyuda.TabIndex = 8;
            this.btnmnuAyuda.Text = "&h";
            this.btnmnuAyuda.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnmnuAyuda.UseVisualStyleBackColor = false;
            // 
            // FrmRecibosCaptura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 746);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(798, 444);
            this.Name = "FrmRecibosCaptura";
            this.Text = "Conciliación Automática";
            this.Controls.SetChildIndex(this.panMenu, 0);
            this.Controls.SetChildIndex(this.panContent, 0);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private GroupBox groupBox4;
        private Panel panelGrid;
        private TextBox txtCredito;
        private Label label3;
        private GroupBox groupBox1;
        private Label label5;
        private TextBox textBox6;
        private Label label9;
        private Label label7;
        private TextBox textBox4;
        private GroupBox groupBox2;
        private Button button1;
        private TextBox textBox7;
        private Label label1;
        private TextBox textBox9;
        private Label label6;
        private TextBox textBox8;
        private Label label4;
        private Label label2;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox3;
        private TextBox textBox12;
        private Label label12;
        private TextBox textBox5;
        private Label label8;
        private GroupBox groupBox5;
        private TextBox textBox14;
        private Label label14;
        private TextBox textBox15;
        private TextBox textBox16;
        private Label label15;
        private Label label16;
        private TextBox textBox17;
        private Label label17;
    }
}
