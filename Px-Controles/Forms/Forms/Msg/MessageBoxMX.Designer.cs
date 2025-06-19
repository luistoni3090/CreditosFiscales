namespace Px_Controles.Forms.Msg
{
    partial class MessageBoxMX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxMX));
            this.picTipo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.uiSplitLine_V1 = new Px_Controles.Controls.Split.UISplitLine_V();
            this.btnOk = new System.Windows.Forms.Button();
            this.uiSplitLine_H1 = new Px_Controles.Controls.Split.UISplitLine_H();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picTipo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picTipo
            // 
            this.picTipo.BackColor = System.Drawing.Color.Transparent;
            this.picTipo.Image = ((System.Drawing.Image)(resources.GetObject("picTipo.Image")));
            this.picTipo.Location = new System.Drawing.Point(7, 42);
            this.picTipo.Margin = new System.Windows.Forms.Padding(4);
            this.picTipo.Name = "picTipo";
            this.picTipo.Size = new System.Drawing.Size(48, 48);
            this.picTipo.TabIndex = 13;
            this.picTipo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(28)))), ((int)(((byte)(50)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 57);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Wero MX";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "btnMsgDefault.png");
            this.imageList1.Images.SetKeyName(1, "btnMsgDefault.png");
            this.imageList1.Images.SetKeyName(2, "btnMsgSuccess.png");
            this.imageList1.Images.SetKeyName(3, "btnMsgWarning.png");
            this.imageList1.Images.SetKeyName(4, "btnMsgError.png");
            this.imageList1.Images.SetKeyName(5, "btnMsgInfo.png");
            this.imageList1.Images.SetKeyName(6, "btnMsgQuestion.png");
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(414, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 14;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.uiSplitLine_V1);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 66);
            this.panel1.TabIndex = 15;
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(5, -1);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(181, 64);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 17;
            this.picLogo.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Transparent;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(251, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 66);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&n";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // uiSplitLine_V1
            // 
            this.uiSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiSplitLine_V1.Location = new System.Drawing.Point(350, 0);
            this.uiSplitLine_V1.Name = "uiSplitLine_V1";
            this.uiSplitLine_V1.Size = new System.Drawing.Size(1, 66);
            this.uiSplitLine_V1.TabIndex = 1;
            this.uiSplitLine_V1.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Transparent;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(351, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 66);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&s";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // uiSplitLine_H1
            // 
            this.uiSplitLine_H1.BackColor = System.Drawing.Color.Black;
            this.uiSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiSplitLine_H1.Location = new System.Drawing.Point(0, 57);
            this.uiSplitLine_H1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.uiSplitLine_H1.Name = "uiSplitLine_H1";
            this.uiSplitLine_H1.Size = new System.Drawing.Size(450, 3);
            this.uiSplitLine_H1.TabIndex = 16;
            this.uiSplitLine_H1.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblMsg.Location = new System.Drawing.Point(0, 57);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(450, 177);
            this.lblMsg.TabIndex = 19;
            this.lblMsg.Text = "Wero MX";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageBoxMX
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.picTipo);
            this.Controls.Add(this.uiSplitLine_H1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsFullSize = false;
            this.Name = "MessageBoxMX";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MessageBoxMX";
            ((System.ComponentModel.ISupportInitialize)(this.picTipo)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTipo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private Controls.Split.UISplitLine_V uiSplitLine_V1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox picLogo;
        private Controls.Split.UISplitLine_H uiSplitLine_H1;
        private System.Windows.Forms.Label lblMsg;
    }
}