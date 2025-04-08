namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    partial class ctrlCuentasBancarias
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lbldireccion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboMunicipio = new System.Windows.Forms.ComboBox();
            this.comboTipoCuenta = new System.Windows.Forms.ComboBox();
            this.comboMoneda = new System.Windows.Forms.ComboBox();
            this.txtCuentaBancaria = new System.Windows.Forms.TextBox();
            this.comboBanco = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboDelegacion = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.comboCtaContable = new System.Windows.Forms.ComboBox();
            this.comboSubCuenta = new System.Windows.Forms.ComboBox();
            this.comboSubSubCuenta = new System.Windows.Forms.ComboBox();
            this.txtValida = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Municipio";
            // 
            // lbldireccion
            // 
            this.lbldireccion.AutoSize = true;
            this.lbldireccion.Location = new System.Drawing.Point(275, 68);
            this.lbldireccion.Name = "lbldireccion";
            this.lbldireccion.Size = new System.Drawing.Size(61, 13);
            this.lbldireccion.TabIndex = 4;
            this.lbldireccion.Text = "Delegación";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cuenta Bancaria";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tipo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Moneda";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Cta. Contable";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(189, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Sub. Cuenta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(361, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Subsubcuenta";
            // 
            // comboMunicipio
            // 
            this.comboMunicipio.FormattingEnabled = true;
            this.comboMunicipio.Location = new System.Drawing.Point(18, 84);
            this.comboMunicipio.Name = "comboMunicipio";
            this.comboMunicipio.Size = new System.Drawing.Size(208, 21);
            this.comboMunicipio.TabIndex = 20;
            // 
            // comboTipoCuenta
            // 
            this.comboTipoCuenta.FormattingEnabled = true;
            this.comboTipoCuenta.Location = new System.Drawing.Point(19, 139);
            this.comboTipoCuenta.Name = "comboTipoCuenta";
            this.comboTipoCuenta.Size = new System.Drawing.Size(207, 21);
            this.comboTipoCuenta.TabIndex = 21;
            // 
            // comboMoneda
            // 
            this.comboMoneda.FormattingEnabled = true;
            this.comboMoneda.Location = new System.Drawing.Point(279, 138);
            this.comboMoneda.Name = "comboMoneda";
            this.comboMoneda.Size = new System.Drawing.Size(189, 21);
            this.comboMoneda.TabIndex = 22;
            // 
            // txtCuentaBancaria
            // 
            this.txtCuentaBancaria.Location = new System.Drawing.Point(278, 31);
            this.txtCuentaBancaria.MaxLength = 40;
            this.txtCuentaBancaria.Name = "txtCuentaBancaria";
            this.txtCuentaBancaria.Size = new System.Drawing.Size(190, 20);
            this.txtCuentaBancaria.TabIndex = 24;
            // 
            // comboBanco
            // 
            this.comboBanco.FormattingEnabled = true;
            this.comboBanco.Location = new System.Drawing.Point(18, 31);
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(208, 21);
            this.comboBanco.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Banco";
            // 
            // comboDelegacion
            // 
            this.comboDelegacion.FormattingEnabled = true;
            this.comboDelegacion.Location = new System.Drawing.Point(278, 84);
            this.comboDelegacion.Name = "comboDelegacion";
            this.comboDelegacion.Size = new System.Drawing.Size(190, 21);
            this.comboDelegacion.TabIndex = 30;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(228, 116);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(41, 20);
            this.txtCuenta.TabIndex = 31;
            // 
            // comboCtaContable
            // 
            this.comboCtaContable.FormattingEnabled = true;
            this.comboCtaContable.Location = new System.Drawing.Point(18, 197);
            this.comboCtaContable.Name = "comboCtaContable";
            this.comboCtaContable.Size = new System.Drawing.Size(140, 21);
            this.comboCtaContable.TabIndex = 32;
            // 
            // comboSubCuenta
            // 
            this.comboSubCuenta.FormattingEnabled = true;
            this.comboSubCuenta.Location = new System.Drawing.Point(192, 197);
            this.comboSubCuenta.Name = "comboSubCuenta";
            this.comboSubCuenta.Size = new System.Drawing.Size(144, 21);
            this.comboSubCuenta.TabIndex = 33;
            // 
            // comboSubSubCuenta
            // 
            this.comboSubSubCuenta.FormattingEnabled = true;
            this.comboSubSubCuenta.Location = new System.Drawing.Point(364, 197);
            this.comboSubSubCuenta.Name = "comboSubSubCuenta";
            this.comboSubSubCuenta.Size = new System.Drawing.Size(133, 21);
            this.comboSubSubCuenta.TabIndex = 34;
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(228, 158);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // ctrlCuentasBancarias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.comboSubSubCuenta);
            this.Controls.Add(this.comboSubCuenta);
            this.Controls.Add(this.comboCtaContable);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.comboDelegacion);
            this.Controls.Add(this.comboBanco);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCuentaBancaria);
            this.Controls.Add(this.comboMoneda);
            this.Controls.Add(this.comboTipoCuenta);
            this.Controls.Add(this.comboMunicipio);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbldireccion);
            this.Controls.Add(this.label1);
            this.Name = "ctrlCuentasBancarias";
            this.Size = new System.Drawing.Size(535, 275);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbldireccion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboMunicipio;
        private System.Windows.Forms.ComboBox comboTipoCuenta;
        private System.Windows.Forms.ComboBox comboMoneda;
        private System.Windows.Forms.TextBox txtCuentaBancaria;
        private System.Windows.Forms.ComboBox comboBanco;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDelegacion;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.ComboBox comboCtaContable;
        private System.Windows.Forms.ComboBox comboSubCuenta;
        private System.Windows.Forms.ComboBox comboSubSubCuenta;
        private System.Windows.Forms.TextBox txtValida;
    }
}
