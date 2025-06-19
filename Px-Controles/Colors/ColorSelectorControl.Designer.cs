namespace Px_Controles.Colors
{
    partial class ColorSelectorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorSelectorControl));
            this.xbtnSelectColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // xbtnSelectColor
            // 
            this.xbtnSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xbtnSelectColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xbtnSelectColor.FlatAppearance.BorderSize = 0;
            this.xbtnSelectColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xbtnSelectColor.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbtnSelectColor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.xbtnSelectColor.Image = ((System.Drawing.Image)(resources.GetObject("xbtnSelectColor.Image")));
            this.xbtnSelectColor.Location = new System.Drawing.Point(0, 0);
            this.xbtnSelectColor.Name = "xbtnSelectColor";
            this.xbtnSelectColor.Size = new System.Drawing.Size(30, 30);
            this.xbtnSelectColor.TabIndex = 3;
            this.xbtnSelectColor.UseVisualStyleBackColor = true;
            // 
            // ColorSelectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.xbtnSelectColor);
            this.Name = "ColorSelectorControl";
            this.Size = new System.Drawing.Size(30, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button xbtnSelectColor;
    }
}
