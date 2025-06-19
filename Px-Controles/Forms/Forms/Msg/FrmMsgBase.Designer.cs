namespace Px_Controles.Forms.Msg
{
    partial class FrmMsgBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsgBase));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Panel();
            this.uiSplitLine_H1 = new Px_Controles.Controls.Split.UISplitLine_H();
            this.picTipo = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picTipo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 78);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Wero MX";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Location = new System.Drawing.Point(407, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 43);
            this.btnClose.TabIndex = 7;
            this.btnClose.Visible = false;
            // 
            // uiSplitLine_H1
            // 
            this.uiSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiSplitLine_H1.Location = new System.Drawing.Point(0, 78);
            this.uiSplitLine_H1.Margin = new System.Windows.Forms.Padding(4);
            this.uiSplitLine_H1.Name = "uiSplitLine_H1";
            this.uiSplitLine_H1.Size = new System.Drawing.Size(450, 1);
            this.uiSplitLine_H1.TabIndex = 8;
            this.uiSplitLine_H1.TabStop = false;
            // 
            // picTipo
            // 
            this.picTipo.BackColor = System.Drawing.Color.Transparent;
            this.picTipo.Image = ((System.Drawing.Image)(resources.GetObject("picTipo.Image")));
            this.picTipo.Location = new System.Drawing.Point(6, 51);
            this.picTipo.Margin = new System.Windows.Forms.Padding(4);
            this.picTipo.Name = "picTipo";
            this.picTipo.Size = new System.Drawing.Size(56, 63);
            this.picTipo.TabIndex = 9;
            this.picTipo.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "btnMsgDefault.png");
            this.imageList1.Images.SetKeyName(1, "btnMsgSuccess.png");
            this.imageList1.Images.SetKeyName(2, "btnMsgError.png");
            this.imageList1.Images.SetKeyName(3, "btnMsgWarning.png");
            this.imageList1.Images.SetKeyName(4, "btnMsgInfo.png");
            // 
            // FrmMsgBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.picTipo);
            this.Controls.Add(this.uiSplitLine_H1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.IsFullSize = false;
            this.IsShowMaskDialog = true;
            this.IsShowRegion = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMsgBase";
            this.Redraw = true;
            this.Text = "FrmMsgBase";
            ((System.ComponentModel.ISupportInitialize)(this.picTipo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel btnClose;
        private Controls.Split.UISplitLine_H uiSplitLine_H1;
        private System.Windows.Forms.PictureBox picTipo;
        private System.Windows.Forms.ImageList imageList1;
    }
}