namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlResponsableSolidario
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
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtFraccion = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTipoOrigen = new System.Windows.Forms.TextBox();
            this.txtLeyOrigen = new System.Windows.Forms.TextBox();
            this.txtDocto = new System.Windows.Forms.TextBox();
            this.comboLey = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fracción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tipo";
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(43, 106);
            this.txtTipo.MaxLength = 4;
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(622, 20);
            this.txtTipo.TabIndex = 24;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(430, 365);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtFraccion
            // 
            this.txtFraccion.Location = new System.Drawing.Point(43, 258);
            this.txtFraccion.MaxLength = 4;
            this.txtFraccion.Name = "txtFraccion";
            this.txtFraccion.Size = new System.Drawing.Size(622, 20);
            this.txtFraccion.TabIndex = 36;
            this.txtFraccion.TextChanged += new System.EventHandler(this.txtFracción_TextChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(43, 183);
            this.txtDescripcion.MaxLength = 25;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(622, 20);
            this.txtDescripcion.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Descripción";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Docto";
            // 
            // txtTipoOrigen
            // 
            this.txtTipoOrigen.Location = new System.Drawing.Point(342, 365);
            this.txtTipoOrigen.Name = "txtTipoOrigen";
            this.txtTipoOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtTipoOrigen.TabIndex = 43;
            // 
            // txtLeyOrigen
            // 
            this.txtLeyOrigen.Location = new System.Drawing.Point(261, 365);
            this.txtLeyOrigen.Name = "txtLeyOrigen";
            this.txtLeyOrigen.Size = new System.Drawing.Size(54, 20);
            this.txtLeyOrigen.TabIndex = 44;
            // 
            // txtDocto
            // 
            this.txtDocto.Location = new System.Drawing.Point(43, 339);
            this.txtDocto.MaxLength = 2;
            this.txtDocto.Name = "txtDocto";
            this.txtDocto.Size = new System.Drawing.Size(622, 20);
            this.txtDocto.TabIndex = 45;
            // 
            // comboLey
            // 
            this.comboLey.FormattingEnabled = true;
            this.comboLey.Location = new System.Drawing.Point(43, 37);
            this.comboLey.Name = "comboLey";
            this.comboLey.Size = new System.Drawing.Size(622, 21);
            this.comboLey.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Ley";
            // 
            // ctrlResponsableSolidario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboLey);
            this.Controls.Add(this.txtDocto);
            this.Controls.Add(this.txtLeyOrigen);
            this.Controls.Add(this.txtTipoOrigen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFraccion);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.txtTipo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlResponsableSolidario";
            this.Size = new System.Drawing.Size(728, 470);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTipo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.TextBox txtFraccion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLeyOrigen;
        private System.Windows.Forms.TextBox txtTipoOrigen;
        private System.Windows.Forms.TextBox txtDocto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboLey;
    }
}
