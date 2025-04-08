namespace Px_Contabilidad.Catalogos
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panTittle = new System.Windows.Forms.Panel();
            this.btnXMin = new System.Windows.Forms.Button();
            this.btnXMax = new System.Windows.Forms.Button();
            this.btnXCerrar = new System.Windows.Forms.Button();
            this.panTittle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTittle
            // 
            this.panTittle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.panTittle.Controls.Add(this.btnXMin);
            this.panTittle.Controls.Add(this.btnXMax);
            this.panTittle.Controls.Add(this.btnXCerrar);
            this.panTittle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTittle.Location = new System.Drawing.Point(0, 0);
            this.panTittle.Name = "panTittle";
            this.panTittle.Size = new System.Drawing.Size(784, 37);
            this.panTittle.TabIndex = 2;
            // 
            // btnXMin
            // 
            this.btnXMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXMin.FlatAppearance.BorderSize = 0;
            this.btnXMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXMin.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXMin.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnXMin.Image = ((System.Drawing.Image)(resources.GetObject("btnXMin.Image")));
            this.btnXMin.Location = new System.Drawing.Point(676, 3);
            this.btnXMin.Name = "btnXMin";
            this.btnXMin.Size = new System.Drawing.Size(30, 30);
            this.btnXMin.TabIndex = 2;
            this.btnXMin.UseVisualStyleBackColor = true;
            // 
            // btnXMax
            // 
            this.btnXMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXMax.FlatAppearance.BorderSize = 0;
            this.btnXMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXMax.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXMax.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnXMax.Image = ((System.Drawing.Image)(resources.GetObject("btnXMax.Image")));
            this.btnXMax.Location = new System.Drawing.Point(712, 3);
            this.btnXMax.Name = "btnXMax";
            this.btnXMax.Size = new System.Drawing.Size(30, 30);
            this.btnXMax.TabIndex = 1;
            this.btnXMax.UseVisualStyleBackColor = true;
            // 
            // btnXCerrar
            // 
            this.btnXCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXCerrar.FlatAppearance.BorderSize = 0;
            this.btnXCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXCerrar.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXCerrar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnXCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnXCerrar.Image")));
            this.btnXCerrar.Location = new System.Drawing.Point(748, 3);
            this.btnXCerrar.Name = "btnXCerrar";
            this.btnXCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnXCerrar.TabIndex = 0;
            this.btnXCerrar.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.panTittle);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panTittle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTittle;
        private System.Windows.Forms.Button btnXMin;
        private System.Windows.Forms.Button btnXMax;
        private System.Windows.Forms.Button btnXCerrar;
    }
}