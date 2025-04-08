namespace Px_Contabilidad.Catalogos.Controles
{
    partial class ctrlCuentaDepreciacion
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
            this.txtDepreciacion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAñosVida = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCD = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCA = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbEjercicio
            // 
            this.cmbEjercicio.FormattingEnabled = true;
            this.cmbEjercicio.Location = new System.Drawing.Point(15, 25);
            this.cmbEjercicio.Name = "cmbEjercicio";
            this.cmbEjercicio.Size = new System.Drawing.Size(454, 21);
            this.cmbEjercicio.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Ejercicio";
            // 
            // txtDepreciacion
            // 
            this.txtDepreciacion.Location = new System.Drawing.Point(198, 187);
            this.txtDepreciacion.Name = "txtDepreciacion";
            this.txtDepreciacion.Size = new System.Drawing.Size(174, 20);
            this.txtDepreciacion.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "%Depreciación";
            // 
            // txtAñosVida
            // 
            this.txtAñosVida.Location = new System.Drawing.Point(18, 187);
            this.txtAñosVida.Name = "txtAñosVida";
            this.txtAñosVida.Size = new System.Drawing.Size(174, 20);
            this.txtAñosVida.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Años de vida";
            // 
            // cmbCD
            // 
            this.cmbCD.FormattingEnabled = true;
            this.cmbCD.Location = new System.Drawing.Point(15, 145);
            this.cmbCD.Name = "cmbCD";
            this.cmbCD.Size = new System.Drawing.Size(454, 21);
            this.cmbCD.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Cuenta Depreciación";
            // 
            // cmbCC
            // 
            this.cmbCC.FormattingEnabled = true;
            this.cmbCC.Location = new System.Drawing.Point(15, 105);
            this.cmbCC.Name = "cmbCC";
            this.cmbCC.Size = new System.Drawing.Size(454, 21);
            this.cmbCC.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Contra Cuenta";
            // 
            // cmbCA
            // 
            this.cmbCA.FormattingEnabled = true;
            this.cmbCA.Location = new System.Drawing.Point(15, 65);
            this.cmbCA.Name = "cmbCA";
            this.cmbCA.Size = new System.Drawing.Size(454, 21);
            this.cmbCA.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Cuenta Activo";
            // 
            // ctrlCuentaDepreciacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbEjercicio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDepreciacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAñosVida);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCA);
            this.Controls.Add(this.label1);
            this.Name = "ctrlCuentaDepreciacion";
            this.Size = new System.Drawing.Size(578, 259);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEjercicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDepreciacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAñosVida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCA;
        private System.Windows.Forms.Label label1;
    }
}
