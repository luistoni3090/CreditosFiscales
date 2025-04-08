namespace Px_Contabilidad.Catalogos.Controles
{
    partial class ctrlSecuencias
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
            this.cmbEjercicio = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoSecuencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbEjercicio
            // 
            this.cmbEjercicio.FormattingEnabled = true;
            this.cmbEjercicio.Location = new System.Drawing.Point(20, 40);
            this.cmbEjercicio.Name = "cmbEjercicio";
            this.cmbEjercicio.Size = new System.Drawing.Size(454, 21);
            this.cmbEjercicio.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Ejercicio";
            // 
            // cmbTP
            // 
            this.cmbTP.FormattingEnabled = true;
            this.cmbTP.Location = new System.Drawing.Point(20, 83);
            this.cmbTP.Name = "cmbTP";
            this.cmbTP.Size = new System.Drawing.Size(454, 21);
            this.cmbTP.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Tipo de póliza";
            // 
            // txtNoSecuencia
            // 
            this.txtNoSecuencia.Location = new System.Drawing.Point(20, 127);
            this.txtNoSecuencia.Name = "txtNoSecuencia";
            this.txtNoSecuencia.Size = new System.Drawing.Size(174, 20);
            this.txtNoSecuencia.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "No. Actual de la secuencia";
            // 
            // ctrlSecuencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNoSecuencia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbEjercicio);
            this.Controls.Add(this.label7);
            this.Name = "ctrlSecuencias";
            this.Size = new System.Drawing.Size(578, 259);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEjercicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoSecuencia;
        private System.Windows.Forms.Label label5;
    }
}
