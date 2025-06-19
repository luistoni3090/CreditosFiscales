using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.Menu
{
    public partial class CustomMenu : MenuStrip
    {


        private Font _rootMenuFont = new Font("Microsoft YaHei", 10, FontStyle.Regular);
        private Color _rootForeColor = Color.White;
        private Color _rootBackgroundColor = Color.FromArgb(106, 28, 50);

        private Color _subMenuBackgroundColor = Color.White;
        private Font _subMenuFont = new Font("Microsoft YaHei", 10, FontStyle.Regular);
        private Color _subMenuForeColor = Color.FromArgb(106, 28, 50);

        private int _rootItemHeight = 80;
        private bool _alignRootToRight = false;

        private ImageList _imageList;

        private Color _imageBackgroundColor = Color.Navy;


        public ImageList MenuImageList
        {
            get => _imageList;
            set
            {
                _imageList = value;
                if (_imageList != null)
                {
                    _imageList.ImageSize = new Size(32, 32); // Ajustar el tamaño de la imagen a 32x32
                }
            }
        }

        public Color ImageBackgroundColor
        {
            get => _imageBackgroundColor;
            set
            {
                _imageBackgroundColor = value;
                UpdateImageBackgrounds();
                RefreshMenu(); // Refrescar el menú después de cambiar la imagen de fondo
            }
        }

        public Color RootBackgroundColor
        {
            get => _rootBackgroundColor;
            set
            {
                _rootBackgroundColor = value;
                UpdateRootMenuItems(); // Actualizar solo los nodos raíz
                RefreshMenu(); // Refrescar todo el menú
            }
        }

        public Color SubMenuBackgroundColor
        {
            get => _subMenuBackgroundColor;
            set
            {
                _subMenuBackgroundColor = value;
                foreach (ToolStripMenuItem item in this.Items)
                {
                    SetItemProperties(item, true);
                }
            }
        }

        public Font RootMenuFont
        {
            get => _rootMenuFont;
            set
            {
                _rootMenuFont = value;
                foreach (ToolStripMenuItem item in this.Items)
                {
                    SetItemProperties(item, true);
                }
            }
        }

        public Font SubMenuFont
        {
            get => _subMenuFont;
            set
            {
                _subMenuFont = value;
                foreach (ToolStripMenuItem item in this.Items)
                {
                    SetItemProperties(item, true);
                }
            }
        }

        public Color RootForeColor
        {
            get => _rootForeColor;
            set
            {
                _rootForeColor = value;
                UpdateRootMenuItems(); // Actualizar solo los nodos raíz
                RefreshMenu(); // Refrescar todo el menú
            }
        }

        public Color SubMenuForeColor
        {
            get => _subMenuForeColor;
            set
            {
                _subMenuForeColor = value;
                foreach (ToolStripMenuItem item in this.Items)
                {
                    SetItemProperties(item, true);
                }
            }
        }

        public int RootItemHeight
        {
            get => _rootItemHeight;
            set
            {
                _rootItemHeight = value;
                foreach (ToolStripMenuItem item in this.Items)
                {
                    SetItemProperties(item, true);
                }
            }
        }

        public bool AlignRootToRight
        {
            get => _alignRootToRight;
            set
            {
                _alignRootToRight = value;
                AlignRootMenuItems();
                RefreshMenu();
            }
        }

        private void UpdateImageBackgrounds()
        {
            if (_imageList == null)
            {
                return;
            }

            for (int i = 0; i < _imageList.Images.Count; i++)
            {
                Bitmap originalImage = new Bitmap(_imageList.Images[i]);
                Bitmap updatedImage = new Bitmap(originalImage.Width, originalImage.Height);
                using (Graphics g = Graphics.FromImage(updatedImage))
                {
                    // Aplicar el color de fondo a las imágenes
                    g.Clear(_imageBackgroundColor);
                    g.DrawImage(originalImage, 0, 0);
                }
                _imageList.Images[i] = updatedImage;
            }
        }

        private void UpdateRootMenuItems()
        {
            foreach (ToolStripMenuItem item in this.Items)
            {
                item.BackColor = _rootBackgroundColor;
                item.ForeColor = _rootForeColor;
                item.Font = _rootMenuFont;
                item.Invalidate(); // Forzar el redibujado del nodo raíz
            }
        }

        private void AlignRootMenuItems()
        {
            if (_alignRootToRight)
            {
                foreach (ToolStripMenuItem item in this.Items)
                {
                    item.Alignment = ToolStripItemAlignment.Right;
                }
            }
            else
            {
                foreach (ToolStripMenuItem item in this.Items)
                {
                    item.Alignment = ToolStripItemAlignment.Left;
                }
            }
        }

        public void AlignRootMenuItem(ToolStripMenuItem menuItem, bool alignToRight)
        {
            menuItem.Alignment = alignToRight ? ToolStripItemAlignment.Right : ToolStripItemAlignment.Left;
            RefreshMenu();
        }

        private void RefreshMenu()
        {
            this.PerformLayout();  // Asegurar el rediseño del menú
            this.Refresh();        // Asegurar el redibujado visual de todos los nodos
        }

        private void UpdateAllMenuItems()
        {
            foreach (ToolStripMenuItem item in this.Items)
            {
                UpdateMenuItemProperties(item, true);
            }
        }

        private void UpdateMenuItemProperties(ToolStripMenuItem item, bool isRoot)
        {
            if (isRoot)
            {
                item.Font = _rootMenuFont;
                item.BackColor = _rootBackgroundColor;
                item.ForeColor = _rootForeColor;

                // Redibujar explícitamente el menú raíz para asegurarse de que los cambios se reflejen
                item.Owner?.Invalidate();
            }
            else
            {
                item.Font = _subMenuFont;
                item.BackColor = _subMenuBackgroundColor;
                item.ForeColor = _subMenuForeColor;

                // Redibujar explícitamente el submenú
                item.Owner?.Invalidate();
            }

            // Aplicar recursivamente a los subitems
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    UpdateMenuItemProperties(subMenuItem, false);
                }
            }
        }

        private Color GetContrastColor(Color backgroundColor, Color originalForeColor)
        {
            // Verifica si el contraste entre el fondo y el color original es adecuado, si no ajusta el color
            double luminance = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        public CustomMenu()
        {
            this.RenderMode = ToolStripRenderMode.Professional;
            this.Renderer = new CustomMenuRenderer(this);
        }

        public void SetDataSource(string jsonData)
        {
            try
            {
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonData);
                this.Items.Clear();
                int index = 1;
                foreach (var property in jsonObject.Properties())
                {
                    ToolStripMenuItem menuItem = CreateMenuItem(property, ref index);
                    this.Items.Add(menuItem);
                }
                AlignRootMenuItems(); // Ajustar la alineación de los menús root después de agregar el datasource
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting data source: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ToolStripMenuItem CreateMenuItem(JProperty property, ref int index)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem(property.Name)
            {
                Font = _rootMenuFont,
                BackColor = _rootBackgroundColor,
                ForeColor = _rootForeColor,
                ShowShortcutKeys = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = index,
                ImageScaling = ToolStripItemImageScaling.None // Mantener el tamaño original de la imagen
            };

            // Asignar imagen del ImageList si está disponible
            if (_imageList != null && index < _imageList.Images.Count)
            {
                Bitmap originalImage = new Bitmap(_imageList.Images[index]);
                Bitmap updatedImage = new Bitmap(originalImage.Width, originalImage.Height);
                using (Graphics g = Graphics.FromImage(updatedImage))
                {
                    g.Clear(_imageBackgroundColor);
                    g.DrawImage(originalImage, 0, 0);
                }
                menuItem.Image = updatedImage;
            }
            index++;

            if (property.Value is JObject subItems)
            {
                foreach (var subItemProperty in subItems.Properties())
                {
                    ToolStripMenuItem subItem = CreateMenuItem(subItemProperty, ref index);
                    subItem.Font = _subMenuFont;
                    subItem.BackColor = _subMenuBackgroundColor;
                    subItem.ForeColor = _subMenuForeColor;
                    subItem.ImageScaling = ToolStripItemImageScaling.None; // Mantener el tamaño original de la imagen
                    menuItem.DropDownItems.Add(subItem);
                }
            }

            return menuItem;
        }

        private void SetItemProperties(ToolStripMenuItem item, bool isRoot)
        {
            if (isRoot)
            {
                item.Font = _rootMenuFont;
                item.BackColor = _rootBackgroundColor;
                item.ForeColor = _rootForeColor; // Asegurar que el color del texto se actualice correctamente
                item.Padding = new Padding(10, (_rootItemHeight - item.Height) / 2 + 10, 10, (_rootItemHeight - item.Height) / 2 + 10);
                item.Margin = new Padding(0);
                item.DropDown.BackColor = _subMenuBackgroundColor;
            }
            else
            {
                item.Font = _subMenuFont;
                item.BackColor = _subMenuBackgroundColor;
                item.ForeColor = _subMenuForeColor; // Asegurar que el color del texto de los submenús también se actualice
                item.Padding = new Padding(5);
                item.DropDown.BackColor = _subMenuBackgroundColor;
            }

            // Asegurarse de aplicar las propiedades a los subitems de forma recursiva
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem toolStripSubItem)
                {
                    SetItemProperties(toolStripSubItem, false);
                }
            }
        }

        private class CustomMenuRenderer : ToolStripProfessionalRenderer
        {
            private CustomMenu _menu;

            public CustomMenuRenderer(CustomMenu menu)
            {
                _menu = menu;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, bounds);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(e.Item.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, bounds);
                    }
                }

                // Dibujar la franja vertical para la imagen si hay una imagen
                if (e.Item is ToolStripMenuItem menuItem && menuItem.Image != null)
                {
                    Rectangle imageBounds = new Rectangle(0, 0, 40, e.Item.Height); // Ajusta el ancho según necesites
                    using (SolidBrush imageBrush = new SolidBrush(_menu.ImageBackgroundColor))
                    {
                        e.Graphics.FillRectangle(imageBrush, imageBounds);
                    }

                    // Dibujar la imagen dentro de la franja
                    e.Graphics.DrawImage(menuItem.Image, new Rectangle(4, (e.Item.Height - _menu.MenuImageList.ImageSize.Height) / 2, _menu.MenuImageList.ImageSize.Width, _menu.MenuImageList.ImageSize.Height));
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item.Owner is MenuStrip)
                {
                    e.TextFont = _menu.RootMenuFont;
                    e.TextColor = _menu.RootForeColor;
                    e.TextFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                }
                else
                {
                    e.TextFont = _menu.SubMenuFont;
                    e.TextColor = _menu.SubMenuForeColor;
                }
                base.OnRenderItemText(e);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = e.Item.ForeColor; // Cambiar el color del triángulo al color de la fuente
                base.OnRenderArrow(e);
            }
        }
    }





    //private Font _rootMenuFont = new Font("Microsoft YaHei", 12, FontStyle.Regular);
    //private Color _rootForeColor = Color.White;
    //private Color _rootBackgroundColor = Color.FromArgb(106, 28, 50);

    //private Color _subMenuBackgroundColor = Color.White;
    //private Font _subMenuFont = new Font("Microsoft YaHei", 12, FontStyle.Regular);
    //private Color _subMenuForeColor = Color.FromArgb(106, 28, 50);

    //public Color RootBackgroundColor
    //{
    //    get => _rootBackgroundColor;
    //    set
    //    {
    //        _rootBackgroundColor = value;
    //        this.BackColor = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public Color SubMenuBackgroundColor
    //{
    //    get => _subMenuBackgroundColor;
    //    set
    //    {
    //        _subMenuBackgroundColor = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public Font RootMenuFont
    //{
    //    get => _rootMenuFont;
    //    set
    //    {
    //        _rootMenuFont = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public Font SubMenuFont
    //{
    //    get => _subMenuFont;
    //    set
    //    {
    //        _subMenuFont = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public Color RootForeColor
    //{
    //    get => _rootForeColor;
    //    set
    //    {
    //        _rootForeColor = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public Color SubMenuForeColor
    //{
    //    get => _subMenuForeColor;
    //    set
    //    {
    //        _subMenuForeColor = value;
    //        foreach (ToolStripMenuItem item in this.Items)
    //        {
    //            SetItemProperties(item, true);
    //        }
    //    }
    //}

    //public CustomMenu()
    //{
    //    this.RenderMode = ToolStripRenderMode.Professional;
    //    this.Renderer = new CustomMenuRenderer();
    //}

    //public void SetDataSource(string jsonData)
    //{
    //    try
    //    {
    //        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonData);
    //        this.Items.Clear();
    //        foreach (var property in jsonObject.Properties())
    //        {
    //            ToolStripMenuItem menuItem = CreateMenuItem(property);
    //            this.Items.Add(menuItem);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show($"Error setting data source: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}

    //private ToolStripMenuItem CreateMenuItem(JProperty property)
    //{
    //    ToolStripMenuItem menuItem = new ToolStripMenuItem(property.Name)
    //    {
    //        Font = _rootMenuFont,
    //        BackColor = _rootBackgroundColor,
    //        ForeColor = _rootForeColor,
    //        ShowShortcutKeys = false,
    //        ImageAlign = ContentAlignment.MiddleRight
    //    };

    //    if (property.Value is JObject subItems)
    //    {
    //        foreach (var subItemProperty in subItems.Properties())
    //        {
    //            ToolStripMenuItem subItem = CreateMenuItem(subItemProperty);
    //            subItem.Font = _subMenuFont;
    //            subItem.BackColor = _subMenuBackgroundColor;
    //            subItem.ForeColor = _subMenuForeColor;
    //            menuItem.DropDownItems.Add(subItem);
    //        }
    //    }

    //    return menuItem;
    //}

    //private void SetItemProperties(ToolStripMenuItem item, bool isRoot)
    //{
    //    if (isRoot)
    //    {
    //        item.Font = _rootMenuFont;
    //        item.BackColor = _rootBackgroundColor;
    //        item.ForeColor = _rootForeColor;
    //        item.DropDown.BackColor = _subMenuBackgroundColor;
    //    }
    //    else
    //    {
    //        item.Font = _subMenuFont;
    //        item.BackColor = _subMenuBackgroundColor;
    //        item.ForeColor = _subMenuForeColor;
    //        item.DropDown.BackColor = _subMenuBackgroundColor;
    //    }

    //    foreach (ToolStripItem subItem in item.DropDownItems)
    //    {
    //        if (subItem is ToolStripMenuItem toolStripSubItem)
    //        {
    //            SetItemProperties(toolStripSubItem, false);
    //        }
    //    }
    //}

    //private class CustomMenuRenderer : ToolStripProfessionalRenderer
    //{
    //    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    //    {
    //        Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
    //        if (e.Item.Selected)
    //        {
    //            e.Graphics.FillRectangle(Brushes.LightGray, bounds);
    //        }
    //        else
    //        {
    //            using (SolidBrush brush = new SolidBrush(e.Item.BackColor))
    //            {
    //                e.Graphics.FillRectangle(brush, bounds);
    //            }
    //        }
    //    }

    //    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    //    {
    //        e.ArrowColor = e.Item.ForeColor; // Cambiar el color del triángulo al color de la fuente
    //        base.OnRenderArrow(e);
    //    }
    //}










}
