using Px_Contabilidad.Utiles.Formas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Px_Contabilidad.Catalogos
{
    public partial class FrmArbol : FormaGen
    {
        public FrmArbol()
        {
            InitializeComponent();

            arbolInicia();
            arbol();
        }

        private void arbolInicia() 
        {
            ColumnHeader oCH = new ColumnHeader();
            oCH.Width = 300;
            oCH.Text = "Uno";
            treeListView1.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 200;
            oCH.Text = "Dos";
            treeListView1.Columns.Add(oCH);

            oCH = new ColumnHeader();
            oCH.Width = 100;
            oCH.Text = "Tres";
            treeListView1.Columns.Add(oCH);

            treeListView1.MultiSelect = false;
            treeListView1.BorderStyle = BorderStyle.None;
            treeListView1.PlusMinusLineColor = Color.FromArgb(224, 224, 224);
            treeListView1.ForeColor = Color.FromArgb(61, 61, 61);
            //treeListView1.ShowPlusMinus = false;
            treeListView1.CollapseAll();

        }


        private void arbol()
        {

            TreeListViewItem oAbue = new TreeListViewItem();
            TreeListViewItem oHijo = new TreeListViewItem();
            TreeListViewItem oNieto = new TreeListViewItem();

            for (int abuelo = 0; abuelo <= 5; abuelo++)
            {

                oAbue = new TreeListViewItem();

                oAbue.Text = $"Abue-{abuelo}";
                oAbue.ImageIndex = 0;
                oAbue.SubItems.Add("s1");
                oAbue.SubItems.Add("s2");
                treeListView1.Items.Add(oAbue);

                for (int hijo = 0; hijo <= 5; hijo++)
                {
                    oHijo = new TreeListViewItem();

                    oHijo.Text = $"hijo-{abuelo}-{hijo}";
                    oHijo.ImageIndex = 5;
                    oHijo.SubItems.Add("sh1");
                    oHijo.SubItems.Add("sh2");
                    oAbue.Items.Add(oHijo);

                    for (int nieto = 0; nieto <= 5; nieto++)
                    {
                        oNieto = new TreeListViewItem();

                        oNieto.Text = $"nieto-{abuelo}-{hijo}-{nieto}";
                        oNieto.ImageIndex = 6;
                        oNieto.SubItems.Add("shn1");
                        oNieto.SubItems.Add("shn2");
                        oHijo.Items.Add(oNieto);
                    }
                }
            }






        }



        //private void PopulateTreeView()
        //{
        //    TreeNode rootNode;

        //    DirectoryInfo info = new DirectoryInfo(@"../..");
        //    if (info.Exists)
        //    {
        //        rootNode = new TreeNode(info.Name);
        //        rootNode.Tag = info;
        //        GetDirectories(info.GetDirectories(), rootNode);
        //        treeView1.Nodes.Add(rootNode);
        //    }
        //}

        //private void GetDirectories(DirectoryInfo[] subDirs,
        //    TreeNode nodeToAddTo)
        //{
        //    TreeNode aNode;
        //    DirectoryInfo[] subSubDirs;
        //    foreach (DirectoryInfo subDir in subDirs)
        //    {
        //        aNode = new TreeNode(subDir.Name, 0, 0);
        //        aNode.Tag = subDir;
        //        aNode.ImageKey = "folder";
        //        subSubDirs = subDir.GetDirectories();
        //        if (subSubDirs.Length != 0)
        //        {
        //            GetDirectories(subSubDirs, aNode);
        //        }
        //        nodeToAddTo.Nodes.Add(aNode);
        //    }
        //}

    }



}
