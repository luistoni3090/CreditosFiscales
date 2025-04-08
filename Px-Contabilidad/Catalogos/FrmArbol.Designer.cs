namespace Px_Contabilidad.Catalogos
{
    partial class FrmArbol
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
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArbol));
            this.treeListView1 = new System.Windows.Forms.TreeListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeListView1.Comparer = treeListViewItemCollectionComparer1;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView1.HideSelection = false;
            this.treeListView1.Location = new System.Drawing.Point(0, 0);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(800, 450);
            this.treeListView1.SmallImageList = this.imageList1;
            this.treeListView1.Sorting = System.Windows.Forms.SortOrder.None;
            this.treeListView1.TabIndex = 0;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "nodeSel.png");
            this.imageList1.Images.SetKeyName(1, "node01.png");
            this.imageList1.Images.SetKeyName(2, "node02.png");
            this.imageList1.Images.SetKeyName(3, "node03.png");
            this.imageList1.Images.SetKeyName(4, "node04.png");
            this.imageList1.Images.SetKeyName(5, "node05.png");
            this.imageList1.Images.SetKeyName(6, "node06.png");
            this.imageList1.Images.SetKeyName(7, "nodeClase.png");
            this.imageList1.Images.SetKeyName(8, "nodeGrupo.png");
            this.imageList1.Images.SetKeyName(9, "nodeMayor.png");
            this.imageList1.Images.SetKeyName(10, "nodeTipo.png");
            // 
            // FrmArbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeListView1);
            this.Name = "FrmArbol";
            this.Text = "FrmArbol";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeListView treeListView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}