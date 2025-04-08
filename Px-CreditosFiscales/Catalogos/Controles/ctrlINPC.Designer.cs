namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlINPC
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
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMesOrigen = new System.Windows.Forms.TextBox();
            this.txtAnioOrigen = new System.Windows.Forms.TextBox();
            this.txtFechaPublicacion = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Factor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Año";
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(43, 57);
            this.txtAnio.MaxLength = 4;
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(622, 20);
            this.txtAnio.TabIndex = 24;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(436, 341);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtFactor
            // 
            this.txtFactor.Location = new System.Drawing.Point(43, 211);
            this.txtFactor.MaxLength = 6;
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(622, 20);
            this.txtFactor.TabIndex = 36;
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(43, 136);
            this.txtMes.MaxLength = 2;
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(622, 20);
            this.txtMes.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Mes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Fecha Publicación";
            // 
            // txtMesOrigen
            // 
            this.txtMesOrigen.Location = new System.Drawing.Point(348, 341);
            this.txtMesOrigen.Name = "txtMesOrigen";
            this.txtMesOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtMesOrigen.TabIndex = 43;
            // 
            // txtAnioOrigen
            // 
            this.txtAnioOrigen.Location = new System.Drawing.Point(267, 341);
            this.txtAnioOrigen.Name = "txtAnioOrigen";
            this.txtAnioOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtAnioOrigen.TabIndex = 44;
            // 
            // txtFechaPublicacion
            // 
            this.txtFechaPublicacion.Location = new System.Drawing.Point(43, 298);
            this.txtFechaPublicacion.Name = "txtFechaPublicacion";
            this.txtFechaPublicacion.Size = new System.Drawing.Size(622, 20);
            this.txtFechaPublicacion.TabIndex = 45;
            // 
            // ctrlINPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtFechaPublicacion);
            this.Controls.Add(this.txtAnioOrigen);
            this.Controls.Add(this.txtMesOrigen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFactor);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlINPC";
            this.Size = new System.Drawing.Size(728, 470);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAnioOrigen;
        private System.Windows.Forms.TextBox txtMesOrigen;
        private System.Windows.Forms.DateTimePicker txtFechaPublicacion;
    }
}
