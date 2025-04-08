namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlGarantias
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
            this.txtTipoDetalle = new System.Windows.Forms.TextBox();
            this.comboTipoGarantia = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tipo Detalle";
            // 
            // txtTipoDetalle
            // 
            this.txtTipoDetalle.Location = new System.Drawing.Point(45, 79);
            this.txtTipoDetalle.MaxLength = 2;
            this.txtTipoDetalle.Name = "txtTipoDetalle";
            this.txtTipoDetalle.Size = new System.Drawing.Size(622, 20);
            this.txtTipoDetalle.TabIndex = 24;
            // 
            // comboTipoGarantia
            // 
            this.comboTipoGarantia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipoGarantia.FormattingEnabled = true;
            this.comboTipoGarantia.Location = new System.Drawing.Point(45, 144);
            this.comboTipoGarantia.Name = "comboTipoGarantia";
            this.comboTipoGarantia.Size = new System.Drawing.Size(622, 21);
            this.comboTipoGarantia.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Tipo Garantía";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(316, 269);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(45, 210);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(622, 20);
            this.txtDescripcion.TabIndex = 36;
            // 
            // ctrlGarantias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.comboTipoGarantia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTipoDetalle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlGarantias";
            this.Size = new System.Drawing.Size(728, 470);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTipoDetalle;
        private System.Windows.Forms.ComboBox comboTipoGarantia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.TextBox txtDescripcion;
    }
}
