/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UINavigationMenu.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Control menú de tipo navegación es un menú normal

using Px_Controles.Colors;
using Px_Controles.Forms;
using Px_Controles.Helpers;
using Px_Controles.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Px_Controles.Controls.Split;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Px_Controles.Controls.NavigationMenu
{
    /// <summary>
    /// Class UINavigationMenu.
    /// Implementado de UserControl <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [DefaultEvent("ClickItemed")]
    public partial class UINavigationMenu : UserControl
    {
        /// <summary>
        /// Ocurre cuando dan click.
        /// </summary>
        [Description("Haz clic en evento de nodo"), Category("personalizar")]

        public event EventHandler ClickItemed;
        /// <summary>
        /// Selecciona el item
        /// </summary>
        private NavigationMenuItem selectItem = null;

        /// <summary>
        /// Obtienen el item.
        /// </summary>
        /// <value>Obtiene el item.</value>
        [Description("Nodo seleccionado"), Category("Personalizar")]
        public NavigationMenuItem SelectItem
        {
            get { return selectItem; }
            private set { selectItem = value; }
        }


        /// <summary>
        /// Arreglo de items
        /// </summary>
        NavigationMenuItem[] items;

        /// <summary>
        /// Obtiene o establece el arreglo de items.
        /// </summary>
        /// <value>Arreglo de items.</value>
        [Description("Lista de nodos"), Category("Personalizar")]
        public NavigationMenuItem[] Items
        {
            get { return items; }
            set
            {
                items = value;
                ReloadMenu();
            }
        }

        /// <summary>
        /// Color del tooltip
        /// </summary>
        private Color tipColor = Color.FromArgb(255, 87, 34);

        /// <summary>
        /// Obtiene o establece el color del tooltip.
        /// </summary>
        /// <value>Color del tooltip.</value>
        [Description("Color de etiqueta"), Category("Personalizar")]
        public Color TipColor
        {
            get { return tipColor; }
            set { tipColor = value; }
        }

        /// <summary>
        /// Obtener o establecer el color de primer plano del control.
        /// </summary>
        /// <value>El color del frente [Fore].</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreach (Control c in this.Controls)
                {
                    c.ForeColor = value;
                }
            }
        }
        /// <summary>
        /// Obtiene o establece la fuente del texto mostrado por el control..
        /// </summary>
        /// <value>El tipo de letra [Font].</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                foreach (Control c in this.Controls)
                {
                    c.Font = value;
                }
            }
        }

        /// <summary>
        /// Anclajes
        /// </summary>
        Dictionary<NavigationMenuItem, FrmAnchor> m_lstAnchors = new Dictionary<NavigationMenuItem, FrmAnchor>();

        /// <summary>
        /// Constructor del control <see cref="UINavigationMenu"/>.
        /// </summary>
        public UINavigationMenu()
        {
            InitializeComponent();
            items = new NavigationMenuItem[0];
            if (ControlHelper.IsDesignMode())
            {
                items = new NavigationMenuItem[4];
                for (int i = 0; i < 4; i++)
                {
                    items[i] = new NavigationMenuItem()
                    {
                        Text = "Menú Item" + (i + 1),
                        AnchorRight = i >= 2
                    };
                }
            }
        }

        /// <summary>
        /// Re carga el menu.
        /// </summary>
        private void ReloadMenu()
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                this.Controls.Clear();
                if (items != null && items.Length > 0)
                {
                    foreach (var item in items)
                    {
                        var menu = (NavigationMenuItem)item;
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Width = menu.ItemWidth + 15;
                        lbl.Padding = new Padding(10, 1, 10, 1);
                        lbl.Text = $"{menu.Text}" ;
                        lbl.Font = Font;
                        lbl.ForeColor = ForeColor;

                        lbl.Paint += lbl_Paint;
                        lbl.MouseEnter += lbl_MouseEnter;
                        lbl.Tag = menu;
                        lbl.Click += lbl_Click;
                        if (menu.AnchorRight)
                        {
                            lbl.Dock = DockStyle.Right;
                        }
                        else
                        {
                            lbl.Dock = DockStyle.Left;
                        }
                        this.Controls.Add(lbl);

                        lbl.BringToFront();
                    }


                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }



        /// <summary>
        /// Evento Click de la etiqueta label [lbl].
        /// </summary>
        /// <param name="sender">Origen del event.</param>
        /// <param name="e"><see cref="EventArgs"/> instancia que contiene los datos del evento.</param>
        void lbl_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Tag != null)
            {
                var menu = (NavigationMenuItem)lbl.Tag;
                if (menu.Items == null || menu.Items.Length <= 0)
                {
                    selectItem = menu;

                    while (m_lstAnchors.Count > 0)
                    {
                        try
                        {
                            foreach (var item in m_lstAnchors)
                            {
                                item.Value.Close();
                                m_lstAnchors.Remove(item.Key);
                            }
                        }
                        catch { }
                    }

                    if (ClickItemed != null)
                    {
                        ClickItemed(this, e);
                    }
                }
                else
                {
                    CloseList(menu);
                    if (m_lstAnchors.ContainsKey(menu))
                    {
                        if (m_lstAnchors[menu] != null && !m_lstAnchors[menu].IsDisposed)
                        {
                            m_lstAnchors[menu].Close();
                        }
                        m_lstAnchors.Remove(menu);
                    }
                    ShowMoreMenu(lbl);
                }
            }
        }

        /// <summary>
        /// Maneja el evento MouseEnter del control lbl.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs"/> instancia que contiene los datos del evento.</param>
        void lbl_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            ShowMoreMenu(lbl);
        }

        /// <summary>
        /// Comprueba si se muestra el item.
        /// </summary>
        /// <param name="menu">Menu.</param>
        /// <returns><c>true</c> si XXXX, <c>false</c> si no.</returns>
        private bool CheckShow(NavigationMenuItem menu)
        {
            //Buscar nodos expandidos
            if (m_lstAnchors.ContainsKey(menu))
            {
                CloseList(menu);
                return false;
            }
            if (HasInCacheChild(menu))
            {
                if (m_lstAnchors.ContainsKey(menu.ParentItem))
                {
                    CloseList(menu.ParentItem);
                    return true;
                }
                return false;
            }
            else
            {
                for (int i = 0; i < 1;)
                {
                    try
                    {
                        foreach (var item in m_lstAnchors)
                        {
                            if (m_lstAnchors[item.Key] != null && !m_lstAnchors[item.Key].IsDisposed)
                            {
                                m_lstAnchors[item.Key].Close();
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    i++;
                }
                m_lstAnchors.Clear();
                return true;
            }
        }

        /// <summary>
        /// Determina si [tiene en caché hijo] [el menú especificado].
        /// </summary>
        /// <param name="menu">Menu.</param>
        /// <returns><c>true</c> si [tiene en caché hijo] [el menú especificado]; de lo contrario, <c>false</c>.</returns>
        private bool HasInCacheChild(NavigationMenuItem menu)
        {
            foreach (var item in m_lstAnchors)
            {
                if (item.Key == menu)
                {
                    return true;
                }
                else
                {
                    if (item.Key.Items != null)
                    {
                        if (item.Key.Items.Contains(menu))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Cierra la lista.
        /// </summary>
        /// <param name="menu">El menu.</param>
        private void CloseList(NavigationMenuItem menu)
        {
            if (menu.Items != null)
            {
                foreach (var item in menu.Items)
                {
                    CloseList(item);
                    if (m_lstAnchors.ContainsKey(item))
                    {
                        if (m_lstAnchors[item] != null && !m_lstAnchors[item].IsDisposed)
                        {
                            m_lstAnchors[item].Close();
                            m_lstAnchors[item] = null;
                            m_lstAnchors.Remove(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Despliega el menú.
        /// </summary>
        /// <param name="lbl">Item que esn label.</param>
        private void ShowMoreMenu(Label lbl)
        {
            var menu = (NavigationMenuItem)lbl.Tag;
            if (CheckShow(menu))
            {
                if (menu.Items != null && menu.Items.Length > 0)
                {
                    Panel panel = new Panel();
                    panel.BackColor = Color.White;
                    panel.Paint += panel_Paint;
                    panel.Padding = new System.Windows.Forms.Padding(1);
                    Size size = GetItemsSize(menu.Items);
                    var height = size.Height * menu.Items.Length + 2;
                    height += menu.Items.Count(p => p.HasSplitLintAtTop);   //Línea divisoria
                    if (size.Width < lbl.Width)
                        size.Width = lbl.Width;
                    panel.Size = new Size(size.Width, height);

                    foreach (var item in menu.Items)
                    {
                        if (item.HasSplitLintAtTop)
                        {
                            UISplitLine_H line = new UISplitLine_H();
                            line.Dock = DockStyle.Top;
                            panel.Controls.Add(line);
                            line.BringToFront();
                        }
                        Label _lbl = new Label();
                        _lbl.Font = Font;
                        _lbl.ForeColor = this.BackColor;
                        _lbl.AutoSize = false;
                        _lbl.TextAlign = ContentAlignment.MiddleLeft;
                        _lbl.Height = size.Height;
                        _lbl.Width = size.Width + 15;
                        _lbl.Padding = new Padding(30, 1, 10, 1);
                        _lbl.Text = $"{item.Text}";
                        _lbl.Dock = DockStyle.Top;
                        _lbl.BringToFront();
                        _lbl.Paint += lbl_Paint;
                        _lbl.MouseEnter += lbl_MouseEnter;
                        _lbl.Tag = item;
                        _lbl.Click += lbl_Click;
                        _lbl.Size = new System.Drawing.Size(size.Width, size.Height);
                        panel.Controls.Add(_lbl);
                        _lbl.BringToFront();
                    }
                    Point point = Point.Empty;

                    if (menu.ParentItem != null)
                    {
                        Point p = lbl.Parent.PointToScreen(lbl.Location);
                        if (p.X + lbl.Width + panel.Width > Screen.PrimaryScreen.Bounds.Width)
                        {
                            point = new Point(-1 * panel.Width - 2, -1 * lbl.Height);
                        }
                        else
                        {
                            point = new Point(panel.Width + 2, -1 * lbl.Height);
                        }
                    }
                    m_lstAnchors[menu] = new FrmAnchor(lbl, panel, point);
                    m_lstAnchors[menu].FormClosing += UINavigationMenu_FormClosing;
                    m_lstAnchors[menu].Show(this);
                    m_lstAnchors[menu].Size = new Size(size.Width, height);
                }
            }

        }

        /// <summary>
        /// Maneja el evento FormClosing del control UINavigationMenu.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="FormClosingEventArgs"/> instancia que contiene los datos del evento.</param>
        void UINavigationMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmAnchor frm = sender as FrmAnchor;
            if (m_lstAnchors.ContainsValue(frm))
            {
                foreach (var item in m_lstAnchors)
                {
                    if (item.Value == frm)
                    {
                        m_lstAnchors.Remove(item.Key);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Maneja el evento Paint del control del panel.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="PaintEventArgs"/> instancia que contiene los datos del evento.</param>
        void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetGDIHigh();
            Rectangle rect = new Rectangle(0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            var path = rect.CreateRoundedRectanglePath(2);
            e.Graphics.DrawPath(new Pen(new SolidBrush(LineColors.Light)), path);
        }



        /// <summary>
        /// Obtiene el tamaño de los artículos..
        /// </summary>
        /// <param name="items">Arreglo de items.</param>
        /// <returns>Tamaño.</returns>
        private Size GetItemsSize(NavigationMenuItem[] items)
        {
            Size size = Size.Empty;
            if (items != null && items.Length > 0)
            {
                using (var g = this.CreateGraphics())
                {
                    foreach (NavigationMenuItem item in items)
                    {
                        var s = g.MeasureString(item.Text, Font);
                        if (s.Width + 25 > size.Width)
                        {
                            size.Width = (int)s.Width + 25;
                        }
                        if (s.Height + 10 > size.Height)
                        {
                            size.Height = (int)s.Height + 10;
                        }
                    }
                }
            }
            return size;
        }


        /// <summary>
        /// Handles the Paint event of the lbl control.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="PaintEventArgs"/> instancia que contiene los datos del evento.</param>
        void lbl_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Tag != null)
            {
                var menu = (NavigationMenuItem)lbl.Tag;
                e.Graphics.SetGDIHigh();
                if (menu.ParentItem == null)    // Los nodos de nivel superior admiten iconos e insignias
                {
                    if (menu.ShowTip)
                    {
                        if (!string.IsNullOrEmpty(menu.TipText))
                        {
                            var rect = new Rectangle(lbl.Width - 25, lbl.Height / 2 - 10, 20, 20);
                            var path = rect.CreateRoundedRectanglePath(5);
                            e.Graphics.FillPath(new SolidBrush(tipColor), path);
                            e.Graphics.DrawString(menu.TipText, new Font("Microsoft Yahei", 8f), new SolidBrush(Color.White), rect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(tipColor), new Rectangle(lbl.Width - 20, lbl.Height / 2 - 10, 10, 10));
                        }
                    }
                    if (menu.Icon != null)
                    {
                        e.Graphics.DrawImage(menu.Icon, new Rectangle(1, (lbl.Height - 25) / 2, 25, 25), 0, 0, menu.Icon.Width, menu.Icon.Height, GraphicsUnit.Pixel);
                    }
                }
                if (menu.ParentItem != null && menu.Items != null && menu.Items.Length > 0)
                {
                    ControlHelper.PaintTriangle(e.Graphics, new SolidBrush(this.BackColor), new Point(lbl.Width - 11, (lbl.Height - 5) / 2), 5, GraphDirection.Rightward);
                }
            }
        }
    }
}
