namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlDescuentos
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
            this.lbldireccion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboTipoCredito = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtVigenciaInicial = new System.Windows.Forms.DateTimePicker();
            this.txtMaxId = new System.Windows.Forms.TextBox();
            this.txtVigenciaFinal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbldireccion
            // 
            this.lbldireccion.AutoSize = true;
            this.lbldireccion.Location = new System.Drawing.Point(39, 18);
            this.lbldireccion.Name = "lbldireccion";
            this.lbldireccion.Size = new System.Drawing.Size(64, 13);
            this.lbldireccion.TabIndex = 4;
            this.lbldireccion.Text = "Tipo Crédito";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Descripción";
            // 
            // comboTipoCredito
            // 
            this.comboTipoCredito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipoCredito.FormattingEnabled = true;
            this.comboTipoCredito.Location = new System.Drawing.Point(41, 34);
            this.comboTipoCredito.Name = "comboTipoCredito";
            this.comboTipoCredito.Size = new System.Drawing.Size(636, 21);
            this.comboTipoCredito.TabIndex = 30;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(42, 95);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(635, 20);
            this.txtDescripcion.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Vigencia Inicial";
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(245, 265);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtVigenciaInicial
            // 
            this.txtVigenciaInicial.Location = new System.Drawing.Point(42, 160);
            this.txtVigenciaInicial.Name = "txtVigenciaInicial";
            this.txtVigenciaInicial.Size = new System.Drawing.Size(636, 20);
            this.txtVigenciaInicial.TabIndex = 40;
            // 
            // txtMaxId
            // 
            this.txtMaxId.Location = new System.Drawing.Point(316, 265);
            this.txtMaxId.Name = "txtMaxId";
            this.txtMaxId.Size = new System.Drawing.Size(54, 20);
            this.txtMaxId.TabIndex = 41;
            // 
            // txtVigenciaFinal
            // 
            this.txtVigenciaFinal.Location = new System.Drawing.Point(42, 230);
            this.txtVigenciaFinal.Name = "txtVigenciaFinal";
            this.txtVigenciaFinal.Size = new System.Drawing.Size(636, 20);
            this.txtVigenciaFinal.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Vigencia Final";
            // 
            // ctrlDescuentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtVigenciaFinal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaxId);
            this.Controls.Add(this.txtVigenciaInicial);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.comboTipoCredito);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbldireccion);
            this.Name = "ctrlDescuentos";
            this.Size = new System.Drawing.Size(728, 470);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbldireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTipoCredito;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxId;
        private System.Windows.Forms.DateTimePicker txtVigenciaInicial;
        private System.Windows.Forms.DateTimePicker txtVigenciaFinal;
        private System.Windows.Forms.Label label1;
    }
}
