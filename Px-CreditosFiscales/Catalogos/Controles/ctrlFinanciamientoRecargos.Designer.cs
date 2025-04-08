namespace Px_CreditosFiscales.Catalogos.Controles
{
    partial class ctrlFinanciamientoRecargos
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
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.lblFechaPublicacion = new System.Windows.Forms.Label();
            this.txtValida = new System.Windows.Forms.TextBox();
            this.txtFechaPublicacion = new System.Windows.Forms.DateTimePicker();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecargos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFinanciamiento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecargoDiario = new System.Windows.Forms.TextBox();
            this.lblRecargoDiario = new System.Windows.Forms.Label();
            this.txtFinancMin = new System.Windows.Forms.TextBox();
            this.lblFinanMin = new System.Windows.Forms.Label();
            this.txtPublicado = new System.Windows.Forms.DateTimePicker();
            this.lblPublicado = new System.Windows.Forms.Label();
            this.txtRecarMin = new System.Windows.Forms.TextBox();
            this.lblRecarMin = new System.Windows.Forms.Label();
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
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Año";
            // 
            // comboTipoCredito
            // 
            this.comboTipoCredito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipoCredito.FormattingEnabled = true;
            this.comboTipoCredito.Location = new System.Drawing.Point(41, 34);
            this.comboTipoCredito.Name = "comboTipoCredito";
            this.comboTipoCredito.Size = new System.Drawing.Size(636, 21);
            this.comboTipoCredito.TabIndex = 30;
            this.comboTipoCredito.SelectedIndexChanged += new System.EventHandler(this.comboTipoCredito_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(42, 95);
            this.txtAnio.MaxLength = 4;
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(280, 20);
            this.txtAnio.TabIndex = 39;
            // 
            // lblFechaPublicacion
            // 
            this.lblFechaPublicacion.AutoSize = true;
            this.lblFechaPublicacion.Location = new System.Drawing.Point(40, 185);
            this.lblFechaPublicacion.Name = "lblFechaPublicacion";
            this.lblFechaPublicacion.Size = new System.Drawing.Size(95, 13);
            this.lblFechaPublicacion.TabIndex = 8;
            this.lblFechaPublicacion.Text = "Fecha Publicación";
            // 
            // txtValida
            // 
            this.txtValida.Location = new System.Drawing.Point(334, 305);
            this.txtValida.Name = "txtValida";
            this.txtValida.Size = new System.Drawing.Size(54, 20);
            this.txtValida.TabIndex = 35;
            // 
            // txtFechaPublicacion
            // 
            this.txtFechaPublicacion.Location = new System.Drawing.Point(43, 204);
            this.txtFechaPublicacion.Name = "txtFechaPublicacion";
            this.txtFechaPublicacion.Size = new System.Drawing.Size(281, 20);
            this.txtFechaPublicacion.TabIndex = 40;
            this.txtFechaPublicacion.ValueChanged += new System.EventHandler(this.dateTimePicker1_CheckedChanged);
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(397, 95);
            this.txtMes.MaxLength = 2;
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(280, 20);
            this.txtMes.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(394, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Mes";
            // 
            // txtRecargos
            // 
            this.txtRecargos.Location = new System.Drawing.Point(398, 147);
            this.txtRecargos.MaxLength = 10;
            this.txtRecargos.Name = "txtRecargos";
            this.txtRecargos.Size = new System.Drawing.Size(280, 20);
            this.txtRecargos.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Recargos";
            // 
            // txtFinanciamiento
            // 
            this.txtFinanciamiento.Location = new System.Drawing.Point(43, 147);
            this.txtFinanciamiento.MaxLength = 10;
            this.txtFinanciamiento.Name = "txtFinanciamiento";
            this.txtFinanciamiento.Size = new System.Drawing.Size(280, 20);
            this.txtFinanciamiento.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Financiamiento";
            // 
            // txtRecargoDiario
            // 
            this.txtRecargoDiario.Location = new System.Drawing.Point(397, 204);
            this.txtRecargoDiario.MaxLength = 10;
            this.txtRecargoDiario.Name = "txtRecargoDiario";
            this.txtRecargoDiario.Size = new System.Drawing.Size(280, 20);
            this.txtRecargoDiario.TabIndex = 49;
            // 
            // lblRecargoDiario
            // 
            this.lblRecargoDiario.AutoSize = true;
            this.lblRecargoDiario.Location = new System.Drawing.Point(394, 187);
            this.lblRecargoDiario.Name = "lblRecargoDiario";
            this.lblRecargoDiario.Size = new System.Drawing.Size(78, 13);
            this.lblRecargoDiario.TabIndex = 48;
            this.lblRecargoDiario.Text = "Recargo Diario";
            // 
            // txtFinancMin
            // 
            this.txtFinancMin.Location = new System.Drawing.Point(397, 204);
            this.txtFinancMin.MaxLength = 100;
            this.txtFinancMin.Name = "txtFinancMin";
            this.txtFinancMin.Size = new System.Drawing.Size(280, 20);
            this.txtFinancMin.TabIndex = 53;
            this.txtFinancMin.Visible = false;
            // 
            // lblFinanMin
            // 
            this.lblFinanMin.AutoSize = true;
            this.lblFinanMin.Location = new System.Drawing.Point(394, 187);
            this.lblFinanMin.Name = "lblFinanMin";
            this.lblFinanMin.Size = new System.Drawing.Size(62, 13);
            this.lblFinanMin.TabIndex = 52;
            this.lblFinanMin.Text = "Financ. Min";
            this.lblFinanMin.Visible = false;
            // 
            // txtPublicado
            // 
            this.txtPublicado.Location = new System.Drawing.Point(43, 204);
            this.txtPublicado.Name = "txtPublicado";
            this.txtPublicado.Size = new System.Drawing.Size(281, 20);
            this.txtPublicado.TabIndex = 51;
            this.txtPublicado.Visible = false;
            this.txtPublicado.ValueChanged += new System.EventHandler(this.dateTimePicker1_CheckedChanged);
            // 
            // lblPublicado
            // 
            this.lblPublicado.AutoSize = true;
            this.lblPublicado.Location = new System.Drawing.Point(43, 188);
            this.lblPublicado.Name = "lblPublicado";
            this.lblPublicado.Size = new System.Drawing.Size(54, 13);
            this.lblPublicado.TabIndex = 50;
            this.lblPublicado.Text = "Publicado";
            this.lblPublicado.Visible = false;
            // 
            // txtRecarMin
            // 
            this.txtRecarMin.Location = new System.Drawing.Point(44, 261);
            this.txtRecarMin.MaxLength = 100;
            this.txtRecarMin.Name = "txtRecarMin";
            this.txtRecarMin.Size = new System.Drawing.Size(633, 20);
            this.txtRecarMin.TabIndex = 55;
            this.txtRecarMin.Visible = false;
            // 
            // lblRecarMin
            // 
            this.lblRecarMin.AutoSize = true;
            this.lblRecarMin.Location = new System.Drawing.Point(41, 244);
            this.lblRecarMin.Name = "lblRecarMin";
            this.lblRecarMin.Size = new System.Drawing.Size(65, 13);
            this.lblRecarMin.TabIndex = 54;
            this.lblRecarMin.Text = "Recarg. Min";
            this.lblRecarMin.Visible = false;
            // 
            // ctrlFinanciamientoRecargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtRecarMin);
            this.Controls.Add(this.lblRecarMin);
            this.Controls.Add(this.txtFinancMin);
            this.Controls.Add(this.lblFinanMin);
            this.Controls.Add(this.txtPublicado);
            this.Controls.Add(this.lblPublicado);
            this.Controls.Add(this.txtRecargoDiario);
            this.Controls.Add(this.lblRecargoDiario);
            this.Controls.Add(this.txtRecargos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFinanciamiento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFechaPublicacion);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.txtValida);
            this.Controls.Add(this.comboTipoCredito);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFechaPublicacion);
            this.Controls.Add(this.lbldireccion);
            this.Name = "ctrlFinanciamientoRecargos";
            this.Size = new System.Drawing.Size(728, 441);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbldireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTipoCredito;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.TextBox txtValida;
        private System.Windows.Forms.Label lblFechaPublicacion;
        private System.Windows.Forms.DateTimePicker txtFechaPublicacion;
        private System.Windows.Forms.TextBox txtRecargos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFinanciamiento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRecargoDiario;
        private System.Windows.Forms.Label lblRecargoDiario;
        private System.Windows.Forms.TextBox txtFinancMin;
        private System.Windows.Forms.Label lblFinanMin;
        private System.Windows.Forms.DateTimePicker txtPublicado;
        private System.Windows.Forms.Label lblPublicado;
        private System.Windows.Forms.TextBox txtRecarMin;
        private System.Windows.Forms.Label lblRecarMin;
    }
}
