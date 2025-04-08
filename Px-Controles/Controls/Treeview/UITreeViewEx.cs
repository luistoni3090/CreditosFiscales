/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UITreeViewEx.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Control de menú árbol vertical y esta mono

using Px_Controles.Helpers;
using Px_Controles.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.Treeview
{
    /// <summary>
    /// Clase UITreeViewEx.
    /// Heredado del arbolito <see cref="System.Windows.Forms.TreeView" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.TreeView" />
    public partial class UITreeViewEx : TreeView
    {

        /// <summary>
        /// Scroll vertical WS
        /// </summary>
        private const int WS_VSCROLL = 2097152;

        /// <summary>
        /// Estilo GML
        /// </summary>
        private const int GWL_STYLE = -16;

        /// <summary>
        /// Diccionario LST
        /// </summary>
        private Dictionary<string, string> _lstTips = new Dictionary<string, string>();

        /// <summary>
        /// Tipo de letra [font]
        /// </summary>
        private Font _tipFont = new Font("Arial Unicode MS", 14f);

        /// <summary>
        /// Imagen del item
        /// </summary>
        private Image _tipImage = Resources.tips;

        /// <summary>
        /// Si muestra el item
        /// </summary>
        private bool _isShowTip = false;

        /// <summary>
        /// Se muestra por modelo personalizado.
        /// </summary>
        private bool _isShowByCustomModel = true;

        /// <summary>
        /// Altura del nodo
        /// </summary>
        private int _nodeHeight = 50;

        /// <summary>
        /// Imagen del nodo que se puede contraer
        /// </summary>
        private Image _nodeDownPic = Resources.Down;

        /// <summary>
        /// Imagen del nodo que se puede expandir
        /// </summary>
        private Image _nodeUpPic = Resources.Up;

        /// <summary>
        /// Color de fondo de los nodos
        /// </summary>
        private Color _nodeBackgroundColor = Color.White;

        /// <summary>
        /// Color del nodo
        /// </summary>
        private Color _nodeForeColor = Color.FromArgb(62, 62, 62);

        /// <summary>
        /// El nodo muestra la línea dividida.
        /// </summary>
        private bool _nodeIsShowSplitLine = false;

        /// <summary>
        /// El color de la línea dividida del nodo.
        /// </summary>
        private Color _nodeSplitLineColor = Color.FromArgb(232, 232, 232);

        /// <summary>
        /// El color seleccionado del nodo m
        /// </summary>
        private Color m_nodeSelectedColor = Color.FromArgb(106, 28, 50); //255, 77, 59);

        /// <summary>
        /// El nodo m seleccionado color anterior.
        /// </summary>
        private Color m_nodeSelectedForeColor = Color.White;

        /// <summary>
        /// El nodo padre puede seleccionar
        /// </summary>
        private bool _parentNodeCanSelect = true;

        /// <summary>
        /// Tipo de letra del arbolito
        /// </summary>
        private SizeF treeFontSize = SizeF.Empty;

        /// <summary>
        /// La BLN tiene barra v??
        /// </summary>
        private bool blnHasVBar = false;

        /// <summary>
        /// Obtiene o establece las sugerencias de LST.
        /// </summary>
        /// <value>Tooltip de LST.</value>
        public Dictionary<string, string> LstTips
        {
            get
            {
                return this._lstTips;
            }
            set
            {
                this._lstTips = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la fuente del tooltip arbolito.
        /// </summary>
        /// <value>The tip font.</value>
        [Category("Atributos personalizados"), Description("Fuente del texto de los subtítulos")]
        public Font TipFont
        {
            get
            {
                return this._tipFont;
            }
            set
            {
                this._tipFont = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la imagen de la sugerencia..
        /// </summary>
        /// <value>Tooltip de la imagen.</value>
        [Category("Atributos personalizados"), Description("Ya sea para mostrar la marca de la esquina")]
        public Image TipImage
        {
            get
            {
                return this._tipImage;
            }
            set
            {
                this._tipImage = value;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia es una sugerencia para mostrar.
        /// </summary>
        /// <value><c>true</c>Obtiene o establece un valor que indica si esta instancia es una sugerencia para mostrar, <c>false</c>.</value>
        [Category("Atributos personalizados"), Description("Ya sea para mostrar la marca de la esquina")]
        public bool IsShowTip
        {
            get
            {
                return this._isShowTip;
            }
            set
            {
                this._isShowTip = value;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia se muestra mediante un modelo personalizado..
        /// </summary>
        /// <value><c>true</c> si esta instancia se muestra mediante un modelo personalizado; de lo contrario, <c>false</c>.</value>
        [Category("Atributos personalizados"), Description("Usar el modo Personalizar")]
        public bool IsShowByCustomModel
        {
            get
            {
                return this._isShowByCustomModel;
            }
            set
            {
                this._isShowByCustomModel = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la altura del nodo..
        /// </summary>
        /// <value>Obtiene o establece la altura del nodo..</value>
        [Category("Atributos personalizados"), Description("Altura del nodo (efectivo cuando IsShowByCustomModel=true）")]
        public int NodeHeight
        {
            get
            {
                return this._nodeHeight;
            }
            set
            {
                this._nodeHeight = value;
                base.ItemHeight = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la imagen del nodo.
        /// </summary>
        /// <value>La foto del nodo caído.</value>
        [Category("Atributos personalizados"), Description("Icono de desplazamiento hacia abajo（IsShowByCustomModel=true）")]
        public Image NodeDownPic
        {
            get
            {
                return this._nodeDownPic;
            }
            set
            {
                this._nodeDownPic = value;
            }
        }

        /// <summary>
        /// Obtiene o configura la imagen del nodo.
        /// </summary>
        /// <value>La foto del nodo arriba.</value>
        [Category("Atributos personalizados"), Description("上翻图标（IsShowByCustomModel=true时生效）")]
        public Image NodeUpPic
        {
            get
            {
                return this._nodeUpPic;
            }
            set
            {
                this._nodeUpPic = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el color del fondo del nodo..
        /// </summary>
        /// <value>El color del fondo del nodo.</value>
        [Category("Atributos personalizados"), Description("Color de fondo del nodo（IsShowByCustomModel=true）")]
        public Color NodeBackgroundColor
        {
            get
            {
                return this._nodeBackgroundColor;
            }
            set
            {
                this._nodeBackgroundColor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el color del nodo anterior..
        /// </summary>
        /// <value>El color del nodo anterior..</value>
        [Category("Atributos personalizados"), Description("Color de fuente del nodo（IsShowByCustomModel=true）")]
        public Color NodeForeColor
        {
            get
            {
                return this._nodeForeColor;
            }
            set
            {
                this._nodeForeColor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si [el nodo muestra línea dividida].
        /// </summary>
        /// <value><c>true</c> si[el nodo muestra la línea dividida]; de lo contrario, <c>false</c>.</value>
        [Category("Atributos personalizados"), Description("Si el nodo muestra líneas divisorias（IsShowByCustomModel=true）")]
        public bool NodeIsShowSplitLine
        {
            get
            {
                return this._nodeIsShowSplitLine;
            }
            set
            {
                this._nodeIsShowSplitLine = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el color de la línea dividida del nodo..
        /// </summary>
        /// <value>El color de la línea dividida del nodo..</value>
        [Category("Atributos personalizados"), Description("Color de la línea divisoria del nodo（IsShowByCustomModel=true）")]
        public Color NodeSplitLineColor
        {
            get
            {
                return this._nodeSplitLineColor;
            }
            set
            {
                this._nodeSplitLineColor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el color del nodo seleccionado..
        /// </summary>
        /// <value>El color del nodo seleccionado..</value>
        [Category("Atributos personalizados"), Description("Color de fondo del nodo seleccionado（IsShowByCustomModel=true）")]
        public Color NodeSelectedColor
        {
            get
            {
                return this.m_nodeSelectedColor;
            }
            set
            {
                this.m_nodeSelectedColor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el color del nodo seleccionado anteriormente..
        /// </summary>
        /// <value>El color del nodo seleccionado anteriormente..</value>
        [Category("Atributos personalizados"), Description("Color de fuente del nodo seleccionado（IsShowByCustomModel=true）")]
        public Color NodeSelectedForeColor
        {
            get
            {
                return this.m_nodeSelectedForeColor;
            }
            set
            {
                this.m_nodeSelectedForeColor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si [el nodo principal puede seleccionar].
        /// </summary>
        /// <value><c>true</c> Obtiene o establece la altura del nodo., <c>false</c>.</value>
        [Category("Atributos personalizados"), Description("Si el nodo padre es seleccionable")]
        public bool ParentNodeCanSelect
        {
            get
            {
                return this._parentNodeCanSelect;
            }
            set
            {
                this._parentNodeCanSelect = value;
            }
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase<see cref="UITreeViewEx" />.
        /// </summary>
        public UITreeViewEx()
        {
            base.HideSelection = false;
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.DrawNode += new DrawTreeNodeEventHandler(this.UITreeViewEx_DrawNode);
            base.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.UITreeViewEx_NodeMouseClick);
            base.SizeChanged += new EventHandler(this.UITreeViewEx_SizeChanged);
            base.AfterSelect += new TreeViewEventHandler(this.UITreeViewEx_AfterSelect);
            base.FullRowSelect = true;
            base.ShowLines = false;
            base.ShowPlusMinus = false;
            base.ShowRootLines = false;
            this.BackColor = Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            DoubleBuffered = true;
        }
        /// <summary>
        /// Utilice<see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.
        /// </summary>
        /// <param name="m"> m para ser procesadoWindows<see cref="T:System.Windows.Forms.Message" />.</param>
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // Deshabilitar la eliminación del mensaje en segundo plano WM_ERASEBKGND

                return;

            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
        /// <summary>
        /// Maneja el evento AfterSelect del control UITreeViewEx.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="TreeViewEventArgs" /> instancia que contiene los datos del evento.</param>
        private void UITreeViewEx_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    if (!this._parentNodeCanSelect)
                    {
                        if (e.Node.Nodes.Count > 0)
                        {
                            e.Node.Expand();
                            base.SelectedNode = e.Node.Nodes[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Maneja el evento SizeChanged del control UITreeViewEx.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void UITreeViewEx_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Maneja el evento NodeMouseClick del control UITreeViewEx.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="TreeNodeMouseClickEventArgs" /> instancia que contiene los datos del evento.</param>
        private void UITreeViewEx_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        if (e.Node.IsExpanded)
                        {
                            e.Node.Collapse();
                        }
                        else
                        {
                            e.Node.Expand();
                        }
                    }
                    if (base.SelectedNode != null)
                    {
                        if (base.SelectedNode == e.Node && e.Node.IsExpanded)
                        {
                            if (!this._parentNodeCanSelect)
                            {
                                if (e.Node.Nodes.Count > 0)
                                {
                                    base.SelectedNode = e.Node.Nodes[0];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Maneja el evento DrawNode del control de vista de árbol.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="DrawTreeNodeEventArgs" /> instancia que contiene los datos del evento.</param>
        private void UITreeViewEx_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {

                if (e.Node == null || !this._isShowByCustomModel || (e.Node.Bounds.Width <= 0 && e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 && e.Node.Bounds.Y <= 0))
                {
                    e.DrawDefault = true;
                }
                else
                {
                    e.Graphics.SetGDIHigh();
                    if (base.Nodes.IndexOf(e.Node) == 0)
                    {
                        this.blnHasVBar = this.IsVerticalScrollBarVisible();
                    }
                    Font font = e.Node.NodeFont;
                    if (font == null)
                    {
                        font = ((TreeView)sender).Font;
                    }
                    if (this.treeFontSize == SizeF.Empty)
                    {
                        this.treeFontSize = this.GetFontSize(font, e.Graphics);
                    }
                    bool flag = false;
                    int intLeft = 0;
                    if (CheckBoxes)
                    {
                        intLeft = 20;
                    }
                    int num = 0;
                    if (base.ImageList != null && base.ImageList.Images.Count > 0 && e.Node.ImageIndex >= 0 && e.Node.ImageIndex < base.ImageList.Images.Count)
                    {
                        flag = true;
                        num = (e.Bounds.Height - base.ImageList.ImageSize.Height) / 2;
                        intLeft += base.ImageList.ImageSize.Width;
                    }

                    intLeft += e.Node.Level * Indent;

                    if ((e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused || e.State == (TreeNodeStates.Focused | TreeNodeStates.Selected)) && (this._parentNodeCanSelect || e.Node.Nodes.Count <= 0))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(this.m_nodeSelectedColor), new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height)));
                        e.Graphics.DrawString(e.Node.Text, font, new SolidBrush(this.m_nodeSelectedForeColor), (float)e.Bounds.X + intLeft, (float)e.Bounds.Y + ((float)this._nodeHeight - this.treeFontSize.Height) / 2f);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(this._nodeBackgroundColor), new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height)));
                        e.Graphics.DrawString(e.Node.Text, font, new SolidBrush(this._nodeForeColor), (float)e.Bounds.X + intLeft, (float)e.Bounds.Y + ((float)this._nodeHeight - this.treeFontSize.Height) / 2f);
                    }
                    if (CheckBoxes)
                    {
                        Rectangle rectCheck = new Rectangle(e.Bounds.X + 3 + e.Node.Level * Indent, e.Bounds.Y + (e.Bounds.Height - 16) / 2, 16, 16);
                        GraphicsPath pathCheck = rectCheck.CreateRoundedRectanglePath(3);
                        e.Graphics.FillPath(new SolidBrush(Color.FromArgb(247, 247, 247)), pathCheck);
                        if (e.Node.Checked)
                        {
                            e.Graphics.DrawLines(new Pen(new SolidBrush(m_nodeSelectedColor), 2), new Point[]
                            {
                                new Point(rectCheck.Left+2,rectCheck.Top+8),
                                new Point(rectCheck.Left+6,rectCheck.Top+12),
                                new Point(rectCheck.Right-4,rectCheck.Top+4)
                            });
                        }

                        e.Graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(200, 200, 200))), pathCheck);
                    }
                    if (flag)
                    {
                        int num2 = e.Bounds.X - num - base.ImageList.ImageSize.Width;
                        if (num2 < 0)
                        {
                            num2 = 3;
                        }
                        e.Graphics.DrawImage(base.ImageList.Images[e.Node.ImageIndex], new Rectangle(new Point(num2 + intLeft - base.ImageList.ImageSize.Width, e.Bounds.Y + num), base.ImageList.ImageSize));
                    }
                    if (this._nodeIsShowSplitLine)
                    {
                        e.Graphics.DrawLine(new Pen(this._nodeSplitLineColor, 1f), new Point(0, e.Bounds.Y + this._nodeHeight - 1), new Point(base.Width, e.Bounds.Y + this._nodeHeight - 1));
                    }
                    bool flag2 = false;
                    if (e.Node.Nodes.Count > 0)
                    {
                        if (e.Node.IsExpanded && this._nodeUpPic != null)
                        {
                            e.Graphics.DrawImage(this._nodeUpPic, new Rectangle(base.Width - (this.blnHasVBar ? 50 : 30), e.Bounds.Y + (this._nodeHeight - 20) / 2, 20, 20));
                        }
                        else if (this._nodeDownPic != null)
                        {
                            e.Graphics.DrawImage(this._nodeDownPic, new Rectangle(base.Width - (this.blnHasVBar ? 50 : 30), e.Bounds.Y + (this._nodeHeight - 20) / 2, 20, 20));
                        }
                        flag2 = true;
                    }
                    if (this._isShowTip && this._lstTips.ContainsKey(e.Node.Name) && !string.IsNullOrWhiteSpace(this._lstTips[e.Node.Name]))
                    {
                        int num3 = base.Width - (this.blnHasVBar ? 50 : 30) - (flag2 ? 20 : 0);
                        int num4 = e.Bounds.Y + (this._nodeHeight - 20) / 2;
                        e.Graphics.DrawImage(this._tipImage, new Rectangle(num3, num4, 20, 20));
                        SizeF sizeF = e.Graphics.MeasureString(this._lstTips[e.Node.Name], this._tipFont, 100, StringFormat.GenericTypographic);
                        e.Graphics.DrawString(this._lstTips[e.Node.Name], this._tipFont, new SolidBrush(Color.White), (float)(num3 + 10) - sizeF.Width / 2f - 3f, (float)(num4 + 10) - sizeF.Height / 2f);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el tamaño de la fuente.
        /// </summary>
        /// <param name="font">El font.</param>
        /// <param name="g">Graphics.</param>
        /// <returns>SizeF.</returns>
        private SizeF GetFontSize(Font font, Graphics g = null)
        {
            SizeF result;
            try
            {
                bool flag = false;
                if (g == null)
                {
                    g = base.CreateGraphics();
                    flag = true;
                }
                SizeF sizeF = g.MeasureString("a", font, 100, StringFormat.GenericTypographic);
                if (flag)
                {
                    g.Dispose();
                }
                result = sizeF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Se alarga la ventana.
        /// </summary>
        /// <param name="hwnd">HWND.</param>
        /// <param name="nIndex">Índice de n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        /// <summary>
        /// Determina si[está visible la barra de desplazamiento vertical].
        /// </summary>
        /// <returns><c>true</c> si [es visible la barra de desplazamiento vertical]; de lo contrario, <c>false</c>.</returns>
        private bool IsVerticalScrollBarVisible()
        {
            return base.IsHandleCreated && (UITreeViewEx.GetWindowLong(base.Handle, -16) & 2097152) != 0;
        }
    }
}
