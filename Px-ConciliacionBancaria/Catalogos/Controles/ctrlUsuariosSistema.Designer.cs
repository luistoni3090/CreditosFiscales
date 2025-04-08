namespace Px_ConciliacionBancaria.Catalogos.Controles
{
    partial class ctrlUsuariosSistema
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtApePaterno = new System.Windows.Forms.TextBox();
            this.txtApeMaterno = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.comboMunicipio = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboUsuarios = new System.Windows.Forms.ComboBox();
            this.txtEditarNuevo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // lbldireccion
            // 
            this.lbldireccion.AutoSize = true;
            this.lbldireccion.Location = new System.Drawing.Point(197, 24);
            this.lbldireccion.Name = "lbldireccion";
            this.lbldireccion.Size = new System.Drawing.Size(84, 13);
            this.lbldireccion.TabIndex = 4;
            this.lbldireccion.Text = "Apellido Paterno";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(364, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Apellido Materno";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nombre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Municipio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(363, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Rfc";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(274, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Usuario";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(26, 40);
            this.txtLogin.MaxLength = 20;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(136, 20);
            this.txtLogin.TabIndex = 26;
            // 
            // txtApePaterno
            // 
            this.txtApePaterno.Location = new System.Drawing.Point(200, 40);
            this.txtApePaterno.MaxLength = 15;
            this.txtApePaterno.Name = "txtApePaterno";
            this.txtApePaterno.Size = new System.Drawing.Size(132, 20);
            this.txtApePaterno.TabIndex = 27;
            // 
            // txtApeMaterno
            // 
            this.txtApeMaterno.Location = new System.Drawing.Point(366, 40);
            this.txtApeMaterno.MaxLength = 15;
            this.txtApeMaterno.Name = "txtApeMaterno";
            this.txtApeMaterno.Size = new System.Drawing.Size(131, 20);
            this.txtApeMaterno.TabIndex = 28;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(26, 94);
            this.txtNombre.MaxLength = 15;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(136, 20);
            this.txtNombre.TabIndex = 29;
            // 
            // comboMunicipio
            // 
            this.comboMunicipio.FormattingEnabled = true;
            this.comboMunicipio.Location = new System.Drawing.Point(200, 94);
            this.comboMunicipio.Name = "comboMunicipio";
            this.comboMunicipio.Size = new System.Drawing.Size(132, 21);
            this.comboMunicipio.TabIndex = 30;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(367, 95);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(130, 20);
            this.txtPassword.TabIndex = 31;
            // 
            // txtRfc
            // 
            this.txtRfc.Location = new System.Drawing.Point(26, 144);
            this.txtRfc.MaxLength = 13;
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(223, 20);
            this.txtRfc.TabIndex = 32;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // comboUsuarios
            // 
            this.comboUsuarios.FormattingEnabled = true;
            this.comboUsuarios.Location = new System.Drawing.Point(277, 143);
            this.comboUsuarios.Name = "comboUsuarios";
            this.comboUsuarios.Size = new System.Drawing.Size(220, 21);
            this.comboUsuarios.TabIndex = 33;
            // 
            // txtEditarNuevo
            // 
            this.txtEditarNuevo.Location = new System.Drawing.Point(217, 170);
            this.txtEditarNuevo.Name = "txtEditarNuevo";
            this.txtEditarNuevo.Size = new System.Drawing.Size(100, 20);
            this.txtEditarNuevo.TabIndex = 34;
            // 
            // ctrlUsuariosSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEditarNuevo);
            this.Controls.Add(this.comboUsuarios);
            this.Controls.Add(this.txtRfc);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.comboMunicipio);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtApeMaterno);
            this.Controls.Add(this.txtApePaterno);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbldireccion);
            this.Controls.Add(this.label1);
            this.Name = "ctrlUsuariosSistema";
            this.Size = new System.Drawing.Size(535, 275);
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtApePaterno;
        private System.Windows.Forms.TextBox txtApeMaterno;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox comboMunicipio;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRfc;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox comboUsuarios;
        private System.Windows.Forms.TextBox txtEditarNuevo;
    }
}
