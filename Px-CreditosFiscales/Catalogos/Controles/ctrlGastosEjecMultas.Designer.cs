namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlGastosEjecMultas
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtLimiteInferior = new System.Windows.Forms.TextBox();
            this.comboClave = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtLimiteSuperior = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboTipo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboEquivalente = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLimiteSuperiorOrigen = new System.Windows.Forms.TextBox();
            this.txtLimiteInferiorOrigen = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Valor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Límite Inferior";
            // 
            // txtLimiteInferior
            // 
            this.txtLimiteInferior.Location = new System.Drawing.Point(45, 148);
            this.txtLimiteInferior.MaxLength = 12;
            this.txtLimiteInferior.Name = "txtLimiteInferior";
            this.txtLimiteInferior.Size = new System.Drawing.Size(622, 20);
            this.txtLimiteInferior.TabIndex = 24;
            // 
            // comboClave
            // 
            this.comboClave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClave.FormattingEnabled = true;
            this.comboClave.Location = new System.Drawing.Point(45, 90);
            this.comboClave.Name = "comboClave";
            this.comboClave.Size = new System.Drawing.Size(622, 21);
            this.comboClave.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Clave";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(544, 402);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(45, 268);
            this.txtImporte.MaxLength = 16;
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(622, 20);
            this.txtImporte.TabIndex = 36;
            // 
            // txtLimiteSuperior
            // 
            this.txtLimiteSuperior.Location = new System.Drawing.Point(45, 205);
            this.txtLimiteSuperior.MaxLength = 12;
            this.txtLimiteSuperior.Name = "txtLimiteSuperior";
            this.txtLimiteSuperior.Size = new System.Drawing.Size(622, 20);
            this.txtLimiteSuperior.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Límite Superior";
            // 
            // comboTipo
            // 
            this.comboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipo.FormattingEnabled = true;
            this.comboTipo.Location = new System.Drawing.Point(45, 37);
            this.comboTipo.Name = "comboTipo";
            this.comboTipo.Size = new System.Drawing.Size(622, 21);
            this.comboTipo.TabIndex = 40;
            this.comboTipo.SelectedValueChanged += new System.EventHandler(this.ChangeTipoValue);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Tipo";
            // 
            // comboEquivalente
            // 
            this.comboEquivalente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquivalente.FormattingEnabled = true;
            this.comboEquivalente.Location = new System.Drawing.Point(45, 331);
            this.comboEquivalente.Name = "comboEquivalente";
            this.comboEquivalente.Size = new System.Drawing.Size(622, 21);
            this.comboEquivalente.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 315);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Equivalente";
            // 
            // txtLimiteSuperiorOrigen
            // 
            this.txtLimiteSuperiorOrigen.Location = new System.Drawing.Point(343, 305);
            this.txtLimiteSuperiorOrigen.Name = "txtLimiteSuperiorOrigen";
            this.txtLimiteSuperiorOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtLimiteSuperiorOrigen.TabIndex = 43;
            // 
            // txtLimiteInferiorOrigen
            // 
            this.txtLimiteInferiorOrigen.Location = new System.Drawing.Point(262, 305);
            this.txtLimiteInferiorOrigen.Name = "txtLimiteInferiorOrigen";
            this.txtLimiteInferiorOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtLimiteInferiorOrigen.TabIndex = 44;
            // 
            // ctrlGastosEjecMultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtLimiteInferiorOrigen);
            this.Controls.Add(this.txtLimiteSuperiorOrigen);
            this.Controls.Add(this.comboEquivalente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboTipo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLimiteSuperior);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.comboClave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLimiteInferior);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlGastosEjecMultas";
            this.Size = new System.Drawing.Size(728, 470);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLimiteInferior;
        private System.Windows.Forms.ComboBox comboClave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtLimiteSuperior;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTipo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboEquivalente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLimiteInferiorOrigen;
        private System.Windows.Forms.TextBox txtLimiteSuperiorOrigen;
    }
}
