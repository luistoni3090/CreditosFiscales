namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    partial class ctrlReglasValidacion
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBanco = new System.Windows.Forms.ComboBox();
            this.comboCuenta = new System.Windows.Forms.ComboBox();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtValidacion = new System.Windows.Forms.TextBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.txtTipoDatoRegresa = new System.Windows.Forms.TextBox();
            this.txtEditarNuevo = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Orden";
            // 
            // lbldireccion
            // 
            this.lbldireccion.AutoSize = true;
            this.lbldireccion.Location = new System.Drawing.Point(305, 57);
            this.lbldireccion.Name = "lbldireccion";
            this.lbldireccion.Size = new System.Drawing.Size(63, 13);
            this.lbldireccion.TabIndex = 4;
            this.lbldireccion.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Validación";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Query";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tipo Dato regresa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Banco";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(306, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Cuenta";
            // 
            // comboBanco
            // 
            this.comboBanco.FormattingEnabled = true;
            this.comboBanco.Location = new System.Drawing.Point(27, 27);
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(246, 21);
            this.comboBanco.TabIndex = 17;
            // 
            // comboCuenta
            // 
            this.comboCuenta.FormattingEnabled = true;
            this.comboCuenta.Location = new System.Drawing.Point(308, 27);
            this.comboCuenta.Name = "comboCuenta";
            this.comboCuenta.Size = new System.Drawing.Size(244, 21);
            this.comboCuenta.TabIndex = 18;
            // 
            // txtOrden
            // 
            this.txtOrden.Location = new System.Drawing.Point(28, 73);
            this.txtOrden.MaxLength = 3;
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Size = new System.Drawing.Size(245, 20);
            this.txtOrden.TabIndex = 19;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(308, 73);
            this.txtDescripcion.MaxLength = 30;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(244, 20);
            this.txtDescripcion.TabIndex = 21;
            // 
            // txtValidacion
            // 
            this.txtValidacion.Location = new System.Drawing.Point(27, 119);
            this.txtValidacion.MaxLength = 1000;
            this.txtValidacion.Name = "txtValidacion";
            this.txtValidacion.Size = new System.Drawing.Size(525, 20);
            this.txtValidacion.TabIndex = 22;
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(27, 170);
            this.txtQuery.MaxLength = 1000;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(356, 20);
            this.txtQuery.TabIndex = 23;
            // 
            // txtTipoDatoRegresa
            // 
            this.txtTipoDatoRegresa.Location = new System.Drawing.Point(402, 170);
            this.txtTipoDatoRegresa.MaxLength = 7;
            this.txtTipoDatoRegresa.Name = "txtTipoDatoRegresa";
            this.txtTipoDatoRegresa.Size = new System.Drawing.Size(150, 20);
            this.txtTipoDatoRegresa.TabIndex = 24;
            // 
            // txtEditarNuevo
            // 
            this.txtEditarNuevo.Location = new System.Drawing.Point(247, 96);
            this.txtEditarNuevo.Name = "txtEditarNuevo";
            this.txtEditarNuevo.Size = new System.Drawing.Size(100, 20);
            this.txtEditarNuevo.TabIndex = 25;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlReglasValidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEditarNuevo);
            this.Controls.Add(this.txtTipoDatoRegresa);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.txtValidacion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtOrden);
            this.Controls.Add(this.comboCuenta);
            this.Controls.Add(this.comboBanco);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbldireccion);
            this.Controls.Add(this.label1);
            this.Name = "ctrlReglasValidacion";
            this.Size = new System.Drawing.Size(583, 312);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbldireccion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBanco;
        private System.Windows.Forms.ComboBox comboCuenta;
        private System.Windows.Forms.TextBox txtOrden;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtValidacion;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.TextBox txtTipoDatoRegresa;
        private System.Windows.Forms.TextBox txtEditarNuevo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
