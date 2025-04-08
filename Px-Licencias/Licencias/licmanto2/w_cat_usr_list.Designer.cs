namespace Px_Licencias.Licencias.licmanto2
{
    partial class w_cat_usr_list
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AppLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCajaAsignada = new System.Windows.Forms.Label();
            this.labelSyssuser = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelOficina = new System.Windows.Forms.Label();
            this.labelDomicilio = new System.Windows.Forms.Label();
            this.labelMaterno = new System.Windows.Forms.Label();
            this.labelPaterno = new System.Windows.Forms.Label();
            this.labelNombre = new System.Windows.Forms.Label();
            this.labelRfc = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelAppLogin = new System.Windows.Forms.Label();
            this.comboBoxCajaAsignada = new System.Windows.Forms.ComboBox();
            this.textBoxSyssuser = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.textBoxOficina = new System.Windows.Forms.TextBox();
            this.textBoxDomicilio = new System.Windows.Forms.TextBox();
            this.textBoxMaterno = new System.Windows.Forms.TextBox();
            this.textBoxPaterno = new System.Windows.Forms.TextBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxRfc = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxAppLogin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppLogin,
            this.Nombre,
            this.Rfc,
            this.Status});
            this.dataGridView1.Location = new System.Drawing.Point(12, 13);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(620, 180);
            this.dataGridView1.TabIndex = 11;
            // 
            // AppLogin
            // 
            this.AppLogin.DataPropertyName = "APP_LOGIN";
            this.AppLogin.HeaderText = "App Login";
            this.AppLogin.Name = "AppLogin";
            this.AppLogin.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "NOMBRE";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Rfc
            // 
            this.Rfc.DataPropertyName = "RFC";
            this.Rfc.HeaderText = "Rfc";
            this.Rfc.Name = "Rfc";
            this.Rfc.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "STATUS";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelCajaAsignada);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelSyssuser);
            this.panel1.Controls.Add(this.labelStatus);
            this.panel1.Controls.Add(this.labelOficina);
            this.panel1.Controls.Add(this.labelDomicilio);
            this.panel1.Controls.Add(this.labelMaterno);
            this.panel1.Controls.Add(this.labelPaterno);
            this.panel1.Controls.Add(this.labelNombre);
            this.panel1.Controls.Add(this.labelRfc);
            this.panel1.Controls.Add(this.labelPassword);
            this.panel1.Controls.Add(this.labelAppLogin);
            this.panel1.Controls.Add(this.comboBoxCajaAsignada);
            this.panel1.Controls.Add(this.textBoxSyssuser);
            this.panel1.Controls.Add(this.comboBoxStatus);
            this.panel1.Controls.Add(this.textBoxOficina);
            this.panel1.Controls.Add(this.textBoxDomicilio);
            this.panel1.Controls.Add(this.textBoxMaterno);
            this.panel1.Controls.Add(this.textBoxPaterno);
            this.panel1.Controls.Add(this.textBoxNombre);
            this.panel1.Controls.Add(this.textBoxRfc);
            this.panel1.Controls.Add(this.textBoxPassword);
            this.panel1.Controls.Add(this.textBoxAppLogin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 197);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 184);
            this.panel1.TabIndex = 46;
            // 
            // labelCajaAsignada
            // 
            this.labelCajaAsignada.AutoSize = true;
            this.labelCajaAsignada.Location = new System.Drawing.Point(242, 147);
            this.labelCajaAsignada.Name = "labelCajaAsignada";
            this.labelCajaAsignada.Size = new System.Drawing.Size(78, 13);
            this.labelCajaAsignada.TabIndex = 66;
            this.labelCajaAsignada.Text = "Caja Asignada:";
            // 
            // labelSyssuser
            // 
            this.labelSyssuser.AutoSize = true;
            this.labelSyssuser.Location = new System.Drawing.Point(36, 148);
            this.labelSyssuser.Name = "labelSyssuser";
            this.labelSyssuser.Size = new System.Drawing.Size(52, 13);
            this.labelSyssuser.TabIndex = 65;
            this.labelSyssuser.Text = "Syssuser:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(242, 117);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(40, 13);
            this.labelStatus.TabIndex = 64;
            this.labelStatus.Text = "Status:";
            // 
            // labelOficina
            // 
            this.labelOficina.AutoSize = true;
            this.labelOficina.Location = new System.Drawing.Point(36, 117);
            this.labelOficina.Name = "labelOficina";
            this.labelOficina.Size = new System.Drawing.Size(43, 13);
            this.labelOficina.TabIndex = 63;
            this.labelOficina.Text = "Oficina:";
            // 
            // labelDomicilio
            // 
            this.labelDomicilio.AutoSize = true;
            this.labelDomicilio.Location = new System.Drawing.Point(36, 86);
            this.labelDomicilio.Name = "labelDomicilio";
            this.labelDomicilio.Size = new System.Drawing.Size(52, 13);
            this.labelDomicilio.TabIndex = 62;
            this.labelDomicilio.Text = "Domicilio:";
            // 
            // labelMaterno
            // 
            this.labelMaterno.AutoSize = true;
            this.labelMaterno.Location = new System.Drawing.Point(474, 53);
            this.labelMaterno.Name = "labelMaterno";
            this.labelMaterno.Size = new System.Drawing.Size(49, 13);
            this.labelMaterno.TabIndex = 61;
            this.labelMaterno.Text = "Materno:";
            // 
            // labelPaterno
            // 
            this.labelPaterno.AutoSize = true;
            this.labelPaterno.Location = new System.Drawing.Point(242, 53);
            this.labelPaterno.Name = "labelPaterno";
            this.labelPaterno.Size = new System.Drawing.Size(47, 13);
            this.labelPaterno.TabIndex = 60;
            this.labelPaterno.Text = "Paterno:";
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(36, 53);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(47, 13);
            this.labelNombre.TabIndex = 59;
            this.labelNombre.Text = "Nombre:";
            // 
            // labelRfc
            // 
            this.labelRfc.AutoSize = true;
            this.labelRfc.Location = new System.Drawing.Point(474, 22);
            this.labelRfc.Name = "labelRfc";
            this.labelRfc.Size = new System.Drawing.Size(27, 13);
            this.labelRfc.TabIndex = 58;
            this.labelRfc.Text = "Rfc:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(242, 22);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 57;
            this.labelPassword.Text = "Password:";
            // 
            // labelAppLogin
            // 
            this.labelAppLogin.AutoSize = true;
            this.labelAppLogin.Location = new System.Drawing.Point(36, 22);
            this.labelAppLogin.Name = "labelAppLogin";
            this.labelAppLogin.Size = new System.Drawing.Size(58, 13);
            this.labelAppLogin.TabIndex = 56;
            this.labelAppLogin.Text = "App Login:";
            // 
            // comboBoxCajaAsignada
            // 
            this.comboBoxCajaAsignada.FormattingEnabled = true;
            this.comboBoxCajaAsignada.Location = new System.Drawing.Point(308, 144);
            this.comboBoxCajaAsignada.Name = "comboBoxCajaAsignada";
            this.comboBoxCajaAsignada.Size = new System.Drawing.Size(100, 21);
            this.comboBoxCajaAsignada.TabIndex = 55;
            // 
            // textBoxSyssuser
            // 
            this.textBoxSyssuser.Location = new System.Drawing.Point(108, 145);
            this.textBoxSyssuser.Name = "textBoxSyssuser";
            this.textBoxSyssuser.Size = new System.Drawing.Size(100, 20);
            this.textBoxSyssuser.TabIndex = 54;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(308, 113);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(100, 21);
            this.comboBoxStatus.TabIndex = 53;
            // 
            // textBoxOficina
            // 
            this.textBoxOficina.Location = new System.Drawing.Point(108, 114);
            this.textBoxOficina.Name = "textBoxOficina";
            this.textBoxOficina.Size = new System.Drawing.Size(100, 20);
            this.textBoxOficina.TabIndex = 52;
            // 
            // textBoxDomicilio
            // 
            this.textBoxDomicilio.Location = new System.Drawing.Point(108, 83);
            this.textBoxDomicilio.Name = "textBoxDomicilio";
            this.textBoxDomicilio.Size = new System.Drawing.Size(500, 20);
            this.textBoxDomicilio.TabIndex = 51;
            // 
            // textBoxMaterno
            // 
            this.textBoxMaterno.Location = new System.Drawing.Point(508, 50);
            this.textBoxMaterno.Name = "textBoxMaterno";
            this.textBoxMaterno.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaterno.TabIndex = 50;
            // 
            // textBoxPaterno
            // 
            this.textBoxPaterno.Location = new System.Drawing.Point(308, 50);
            this.textBoxPaterno.Name = "textBoxPaterno";
            this.textBoxPaterno.Size = new System.Drawing.Size(100, 20);
            this.textBoxPaterno.TabIndex = 49;
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(108, 50);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(100, 20);
            this.textBoxNombre.TabIndex = 48;
            // 
            // textBoxRfc
            // 
            this.textBoxRfc.Location = new System.Drawing.Point(508, 19);
            this.textBoxRfc.Name = "textBoxRfc";
            this.textBoxRfc.Size = new System.Drawing.Size(100, 20);
            this.textBoxRfc.TabIndex = 47;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(308, 19);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 46;
            // 
            // textBoxAppLogin
            // 
            this.textBoxAppLogin.Location = new System.Drawing.Point(108, 19);
            this.textBoxAppLogin.Name = "textBoxAppLogin";
            this.textBoxAppLogin.Size = new System.Drawing.Size(100, 20);
            this.textBoxAppLogin.TabIndex = 45;
            // 
            // w_cat_usr_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 381);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "w_cat_usr_list";
            this.Text = "Catálogo de Usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rfc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCajaAsignada;
        private System.Windows.Forms.Label labelSyssuser;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelOficina;
        private System.Windows.Forms.Label labelDomicilio;
        private System.Windows.Forms.Label labelMaterno;
        private System.Windows.Forms.Label labelPaterno;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelRfc;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelAppLogin;
        private System.Windows.Forms.ComboBox comboBoxCajaAsignada;
        private System.Windows.Forms.TextBox textBoxSyssuser;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.TextBox textBoxOficina;
        private System.Windows.Forms.TextBox textBoxDomicilio;
        private System.Windows.Forms.TextBox textBoxMaterno;
        private System.Windows.Forms.TextBox textBoxPaterno;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxRfc;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxAppLogin;
    }
}