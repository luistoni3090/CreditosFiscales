using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.TreeListView
{

    // Clase para el Data de los nodos
    public class NodoData
    {
        //public Int64 ID { get; set; }
        //public Int64? ParentID { get; set; }

        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public object Objeto { get; set; }

        public string InformacionAdicional { get; set; }

        // Otros campos según necesites
    }

    // Clase de nodo genérico
    public class TreeNodeItem<T>
    {
        public Int64 ID { get; set; }
        public Int64? ParentID { get; set; }
        public T Data { get; set; }
        public List<TreeNodeItem<T>> Children { get; set; } = new List<TreeNodeItem<T>>();
        public bool IsExpanded { get; set; } = true;
        public bool HasChildren => Children.Count > 0;
    }

    //public class TreeNodeItem<T>
    //{
    //    public Int64 ID { get; set; }
    //    public Int64? ParentID { get; set; }
    //    public T Data { get; set; }
    //    public List<TreeNodeItem<T>> Children { get; set; } = new List<TreeNodeItem<T>>();

    //    // Propiedad para manejar la expansión y contracción
    //    public bool IsExpanded { get; set; } = true;

    //    // Propiedad para saber si el nodo tiene hijos
    //    public bool HasChildren => Children.Count > 0;
    //}

    public partial class UITreeListView<T> : ListView
    {
        public List<TreeNodeItem<T>> DataSource { get; set; } = new List<TreeNodeItem<T>>();

        // Lista de nodos visibles
        private List<TreeNodeItem<T>> visibleNodes = new List<TreeNodeItem<T>>();

        // Delegados para acceso en modo virtual
        private Func<T, string[]> columnSelector;
        private Func<T, int> imageIndexSelector;

        // Recursos gráficos reutilizables
        private readonly Pen linePen = new Pen( Color.FromArgb(220, 220, 220));
        private readonly Pen expandCollapsePen = new Pen(Color.FromArgb(220, 220, 220));

        // Color de selección personalizado
        public Color SelectionBackColor { get; set; } = SystemColors.Highlight;
        public Color SelectionForeColor { get; set; } = SystemColors.HighlightText;

        // Colores de Columnas
        public Dictionary<int, Color> ColumnBackColors { get; set; } = new Dictionary<int, Color>();


        public UITreeListView()
        {
            OwnerDraw = true;
            View = View.Details;
            FullRowSelect = true;
            DoubleBuffered = true;

            VirtualMode = true;
            RetrieveVirtualItem += TreeListView_RetrieveVirtualItem;
            DrawItem += TreeListView_DrawItem;
            DrawSubItem += TreeListView_DrawSubItem;
            DrawColumnHeader += TreeListView_DrawColumnHeader;
            MouseDown += TreeListView_MouseDown;

            // Habilitar la recepción de eventos de teclado
            this.KeyDown += TreeListView_KeyDown;
        }

        public event EventHandler<TreeNodeItem<T>> NodeDoubleClicked;

        protected virtual void OnNodeDoubleClicked(TreeNodeItem<T> node)
        {
            NodeDoubleClicked?.Invoke(this, node);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (SelectedIndices.Count > 0)
            {
                var selectedNode = visibleNodes[SelectedIndices[0]];
                OnNodeDoubleClicked(selectedNode);
            }
        }

        // Sobrescribir ProcessDialogKey para manejar la tecla Enter
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (SelectedIndices.Count > 0)
                {
                    var selectedNode = visibleNodes[SelectedIndices[0]];
                    OnNodeDoubleClicked(selectedNode);
                    return true; // Indicar que la tecla ha sido procesada
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void TreeListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedIndices.Count > 0)
            {
                int selectedIndex = SelectedIndices[0];
                var selectedNode = visibleNodes[selectedIndex];

                if (e.KeyCode == Keys.Left)
                {
                    // Si el nodo está expandido, lo contraemos
                    if (selectedNode.IsExpanded)
                    {
                        selectedNode.IsExpanded = false;
                        RefreshData(columnSelector, imageIndexSelector);
                    }
                    else
                    {
                        // Si el nodo no está expandido, seleccionamos el padre si existe
                        var parentNode = GetParentNode(selectedNode);
                        if (parentNode != null)
                        {
                            int parentIndex = visibleNodes.IndexOf(parentNode);
                            if (parentIndex >= 0)
                            {
                                SelectedIndices.Clear();
                                SelectedIndices.Add(parentIndex);
                                EnsureVisible(parentIndex);
                            }
                        }
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    // Si el nodo no está expandido y tiene hijos, lo expandimos
                    if (!selectedNode.IsExpanded && selectedNode.HasChildren)
                    {
                        selectedNode.IsExpanded = true;
                        RefreshData(columnSelector, imageIndexSelector);
                        // Reajustar columnas si es necesario
                        AutoResizeColumns();
                    }
                    else if (selectedNode.IsExpanded)
                    {
                        // Si el nodo ya está expandido, seleccionamos el primer hijo
                        var childNode = selectedNode.Children.FirstOrDefault();
                        if (childNode != null)
                        {
                            int childIndex = visibleNodes.IndexOf(childNode);
                            if (childIndex >= 0)
                            {
                                SelectedIndices.Clear();
                                SelectedIndices.Add(childIndex);
                                EnsureVisible(childIndex);
                            }
                        }
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    // La navegación estándar ya está manejada por el control
                    // Opcionalmente, puedes asegurarte de que el ítem es visible
                    EnsureVisible(selectedIndex);
                }
            }
        }

        private void TreeListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void TreeListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // No hacemos nada aquí para evitar parpadeos
            // El dibujo se maneja en DrawSubItem
            e.DrawDefault = false;
        }

        private void TreeListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var node = (TreeNodeItem<T>)e.Item.Tag;
            int indentLevel = GetNodeLevel(node);
            int indentWidth = 20; // Ancho de la indentación

            Rectangle bounds = e.Bounds;

            // Determinar si el ítem está seleccionado
            bool isSelected = e.Item.Selected;

            // Definir los colores de fondo y texto
            Color backColor;
            if (isSelected)
            {
                backColor = SelectionBackColor;
            }
            else if (ColumnBackColors.ContainsKey(e.ColumnIndex))
            {
                backColor = ColumnBackColors[e.ColumnIndex];
            }
            else
            {
                backColor = e.Item.BackColor;
            }

            Color foreColor = isSelected ? SelectionForeColor : e.Item.ForeColor;

            // Configurar la calidad de renderizado
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            // Dibujar el fondo del subitem
            using (SolidBrush backgroundBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, bounds);
            }

            // Ajustar bounds para la primera columna (indentación, glifo, icono)
            if (e.ColumnIndex == 0)
            {
                bounds.X += indentLevel * indentWidth;

                int glyphWidth = 16;
                int glyphHeight = 16;
                Rectangle glyphRect = new Rectangle(bounds.X, bounds.Y + (bounds.Height - glyphHeight) / 2, glyphWidth, glyphHeight);

                if (node.HasChildren)
                {
                    // Dibujar el glifo de expansión/contracción
                    e.Graphics.DrawRectangle(expandCollapsePen, glyphRect);

                    if (node.IsExpanded)
                    {
                        // Dibujar símbolo '-'
                        e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
                    }
                    else
                    {
                        // Dibujar símbolo '+'
                        e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
                        e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + glyphWidth / 2, glyphRect.Top + 4, glyphRect.Left + glyphWidth / 2, glyphRect.Bottom - 4);
                    }
                }

                bounds.X += glyphWidth + 2;

                // Dibujar el icono si existe
                if (SmallImageList != null)
                {
                    int imageIndex = imageIndexSelector(node.Data);
                    if (imageIndex >= 0 && imageIndex < SmallImageList.Images.Count)
                    {
                        Image img = SmallImageList.Images[imageIndex];
                        // Dibujar la imagen a su tamaño natural sin redimensionarla
                        e.Graphics.DrawImage(img, bounds.X, bounds.Y + (bounds.Height - img.Height) / 2);
                        bounds.X += img.Width + 4;
                    }
                }

                // Ajustar bounds para el texto
                bounds.Width = e.Bounds.Right - bounds.X;
            }
            else
            {
                // Para otras columnas, ajustar bounds
                bounds = e.Bounds;
            }

            // Obtener la alineación de la columna
            HorizontalAlignment alignment = this.Columns[e.ColumnIndex].TextAlign;

            // Convertir la alineación a TextFormatFlags
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.Left;
                    break;
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    break;
            }

            // Dibujar el texto con la alineación especificada
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                e.Item.Font,
                bounds,
                foreColor,
                flags);

            // Dibujar líneas para la primera columna
            if (e.ColumnIndex == 0)
            {
                DrawLines(e.Graphics, e.Bounds, node, indentLevel, indentWidth);
            }


            //var node = (TreeNodeItem<T>)e.Item.Tag;
            //int indentLevel = GetNodeLevel(node);
            //int indentWidth = 20; // Ancho de la indentación

            //Rectangle bounds = e.Bounds;

            //// Determinar si el ítem está seleccionado
            //bool isSelected = e.Item.Selected;

            //// Definir los colores de fondo y texto
            //Color backColor;
            //if (isSelected)
            //{
            //    backColor = SelectionBackColor;
            //}
            //else if (ColumnBackColors.ContainsKey(e.ColumnIndex))
            //{
            //    backColor = ColumnBackColors[e.ColumnIndex];
            //}
            //else
            //{
            //    backColor = e.Item.BackColor;
            //}

            //Color foreColor = isSelected ? SelectionForeColor : e.Item.ForeColor;

            //// Dibujar el fondo del subitem
            //using (SolidBrush backgroundBrush = new SolidBrush(backColor))
            //{
            //    e.Graphics.FillRectangle(backgroundBrush, bounds);
            //}

            //// Ajustar bounds para la primera columna (indentación, glifo, icono)
            //if (e.ColumnIndex == 0)
            //{
            //    bounds.X += indentLevel * indentWidth;

            //    int glyphWidth = 12;
            //    int glyphHeight = 12;
            //    Rectangle glyphRect = new Rectangle(bounds.X, bounds.Y + (bounds.Height - glyphHeight) / 2, glyphWidth, glyphHeight);

            //    if (node.HasChildren)
            //    {
            //        // Dibujar el glifo de expansión/contracción
            //        e.Graphics.DrawRectangle(expandCollapsePen, glyphRect);

            //        if (node.IsExpanded)
            //        {
            //            // Dibujar símbolo '-'
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
            //        }
            //        else
            //        {
            //            // Dibujar símbolo '+'
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + glyphWidth / 2, glyphRect.Top + 4, glyphRect.Left + glyphWidth / 2, glyphRect.Bottom - 4);
            //        }
            //    }

            //    bounds.X += glyphWidth + 2;

            //    // Dibujar el icono si existe
            //    if (e.Item.ImageList != null && e.Item.ImageIndex >= 0)
            //    {
            //        Image img = e.Item.ImageList.Images[e.Item.ImageIndex];
            //        e.Graphics.DrawImage(img, bounds.X, bounds.Y + (bounds.Height - img.Height) / 2, img.Width, img.Height);
            //        bounds.X += img.Width + 4;
            //    }

            //    // Ajustar bounds para el texto
            //    bounds.Width = e.Bounds.Right - bounds.X;
            //}
            //else
            //{
            //    // Para otras columnas, ajustar bounds
            //    bounds = e.Bounds;
            //}

            //// Obtener la alineación de la columna
            //HorizontalAlignment alignment = this.Columns[e.ColumnIndex].TextAlign;

            //// Convertir la alineación a TextFormatFlags
            //TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            //switch (alignment)
            //{
            //    case HorizontalAlignment.Left:
            //        flags |= TextFormatFlags.Left;
            //        break;
            //    case HorizontalAlignment.Center:
            //        flags |= TextFormatFlags.HorizontalCenter;
            //        break;
            //    case HorizontalAlignment.Right:
            //        flags |= TextFormatFlags.Right;
            //        break;
            //}

            //// Dibujar el texto con la alineación especificada
            //TextRenderer.DrawText(
            //    e.Graphics,
            //    e.SubItem.Text,
            //    e.Item.Font,
            //    bounds,
            //    foreColor,
            //    flags);

            //// Dibujar líneas para la primera columna
            //if (e.ColumnIndex == 0)
            //{
            //    DrawLines(e.Graphics, e.Bounds, node, indentLevel, indentWidth);
            //}

            //var node = (TreeNodeItem<T>)e.Item.Tag;
            //int indentLevel = GetNodeLevel(node);
            //int indentWidth = 20; // Ancho de la indentación

            //Rectangle bounds = e.Bounds;

            //// Determinar si el ítem está seleccionado
            //bool isSelected = e.Item.Selected;

            //// Definir los colores de fondo y texto
            //Color backColor = isSelected ? SelectionBackColor : e.Item.BackColor;
            //Color foreColor = isSelected ? SelectionForeColor : e.Item.ForeColor;

            //// Dibujar el fondo del subitem
            //using (SolidBrush backgroundBrush = new SolidBrush(backColor))
            //{
            //    e.Graphics.FillRectangle(backgroundBrush, bounds);
            //}

            //// Ajustar bounds para la primera columna (indentación, glifo, icono)
            //if (e.ColumnIndex == 0)
            //{
            //    bounds.X += indentLevel * indentWidth;

            //    int glyphWidth = 16;
            //    int glyphHeight = 16;
            //    Rectangle glyphRect = new Rectangle(bounds.X, bounds.Y + (bounds.Height - glyphHeight) / 2, glyphWidth, glyphHeight);

            //    if (node.HasChildren)
            //    {
            //        // Dibujar el glifo de expansión/contracción
            //        e.Graphics.DrawRectangle(expandCollapsePen, glyphRect);

            //        if (node.IsExpanded)
            //        {
            //            // Dibujar símbolo '-'
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
            //        }
            //        else
            //        {
            //            // Dibujar símbolo '+'
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
            //            e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + glyphWidth / 2, glyphRect.Top + 4, glyphRect.Left + glyphWidth / 2, glyphRect.Bottom - 4);
            //        }
            //    }

            //    bounds.X += glyphWidth + 2;

            //    // Dibujar el icono si existe
            //    if (e.Item.ImageList != null && e.Item.ImageIndex >= 0)
            //    {
            //        Image img = e.Item.ImageList.Images[e.Item.ImageIndex];
            //        e.Graphics.DrawImage(img, bounds.X, bounds.Y + (bounds.Height - img.Height) / 2, img.Width, img.Height);
            //        bounds.X += img.Width + 4;
            //    }

            //    // Ajustar bounds para el texto
            //    bounds.Width = e.Bounds.Right - bounds.X;
            //}
            //else
            //{
            //    // Para otras columnas, ajustar bounds
            //    bounds = e.Bounds;
            //}

            //// Obtener la alineación de la columna
            //HorizontalAlignment alignment = this.Columns[e.ColumnIndex].TextAlign;

            //// Convertir la alineación a TextFormatFlags
            //TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            //switch (alignment)
            //{
            //    case HorizontalAlignment.Left:
            //        flags |= TextFormatFlags.Left;
            //        break;
            //    case HorizontalAlignment.Center:
            //        flags |= TextFormatFlags.HorizontalCenter;
            //        break;
            //    case HorizontalAlignment.Right:
            //        flags |= TextFormatFlags.Right;
            //        break;
            //}

            //// Dibujar el texto con la alineación especificada
            //TextRenderer.DrawText(
            //    e.Graphics,
            //    e.SubItem.Text,
            //    e.Item.Font,
            //    bounds,
            //    foreColor,
            //    flags);

            //// Dibujar líneas para la primera columna
            //if (e.ColumnIndex == 0)
            //{
            //    DrawLines(e.Graphics, e.Bounds, node, indentLevel, indentWidth);
            //}
        }

        private void DrawLines(Graphics g, Rectangle bounds, TreeNodeItem<T> node, int indentLevel, int indentWidth)
        {
            int glyphWidth = 10;
            int halfHeight = bounds.Top + bounds.Height / 2;

            // Dibuja líneas verticales y horizontales
            for (int i = 0; i <= indentLevel; i++)
            {
                int x = bounds.Left + i * indentWidth + glyphWidth / 2;

                // Línea vertical
                if (i < indentLevel)
                {
                    g.DrawLine(linePen, x, bounds.Top, x, bounds.Bottom);
                }
                else
                {
                    // Línea horizontal hacia el glifo
                    g.DrawLine(linePen, x, halfHeight, x + glyphWidth / 2, halfHeight);
                }
            }
        }

        private int GetNodeLevel(TreeNodeItem<T> node)
        {
            int level = 0;
            var currentNode = node;
            while (currentNode.ParentID.HasValue)
            {
                level++;
                currentNode = DataSource.FirstOrDefault(n => n.ID == currentNode.ParentID.Value);
                if (currentNode == null)
                {
                    break;
                }
            }
            return level;
        }

        public void RefreshData(Func<T, string[]> columnSelector, Func<T, int> imageIndexSelector)
        {
            this.columnSelector = columnSelector;
            this.imageIndexSelector = imageIndexSelector;

            BeginUpdate();
            visibleNodes.Clear();

            if (DataSource != null)
            {
                var tree = BuildTree(DataSource);
                foreach (var node in tree)
                {
                    AddVisibleNode(node);
                }
            }

            VirtualListSize = visibleNodes.Count;
            EndUpdate();
            Invalidate();
        }

        private void AddVisibleNode(TreeNodeItem<T> node)
        {
            visibleNodes.Add(node);

            if (node.IsExpanded && node.Children != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    AddVisibleNode(child);
                }
            }
        }
        private void AddVisibleNode(TreeNodeItem<T> node, HashSet<TreeNodeItem<T>> nodesToDisplay)
        {
            if (!nodesToDisplay.Contains(node))
                return;

            visibleNodes.Add(node);

            if (node.IsExpanded && node.Children != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    AddVisibleNode(child, nodesToDisplay);
                }
            }
        }


        private List<TreeNodeItem<T>> BuildTree(List<TreeNodeItem<T>> items)
        {
            // Verificar unicidad de IDs
            var duplicateIds = items.GroupBy(i => i.ID)
                                    .Where(g => g.Count() > 1)
                                    .Select(g => g.Key)
                                    .ToList();

            if (duplicateIds.Any())
            {
                throw new InvalidOperationException($"Se encontraron IDs duplicados: {string.Join(", ", duplicateIds)}");
            }

            // Crear el diccionario de búsqueda
            var lookup = items.ToDictionary(item => item.ID);
            var rootNodes = new List<TreeNodeItem<T>>();

            // Inicializar la lista de IDs disponibles
            var availableIds = new HashSet<long>(items.Select(i => i.ID));

            foreach (var item in items)
            {
                // Inicializar Children si es necesario
                if (item.Children == null)
                    item.Children = new List<TreeNodeItem<T>>();
                else
                    item.Children.Clear(); // Limpiar hijos existentes

                if (item.ParentID.HasValue)
                {
                    if (availableIds.Contains(item.ParentID.Value))
                    {
                        if (lookup.TryGetValue(item.ParentID.Value, out var parent))
                        {
                            if (parent.Children == null)
                                parent.Children = new List<TreeNodeItem<T>>();

                            parent.Children.Add(item);
                        }
                    }
                    else
                    {
                        // El ParentID no existe en items
                        // Manejar este caso según tus necesidades
                        rootNodes.Add(item);
                        // O registrar una advertencia
                        // Console.WriteLine($"Advertencia: No se encontró el padre con ID {item.ParentID.Value} para el item con ID {item.ID}");
                    }
                }
                else
                {
                    rootNodes.Add(item);
                }
            }

            return rootNodes;
        }


        private TreeNodeItem<T> GetParentNode(TreeNodeItem<T> node)
        {
            if (node.ParentID.HasValue)
            {
                return DataSource.FirstOrDefault(n => n.ID == node.ParentID.Value);
            }
            return null;
        }



        //private List<TreeNodeItem<T>> BuildTree(List<TreeNodeItem<T>> items)
        //{
        //    var lookup = items.ToDictionary(item => item.ID);
        //    var rootNodes = new List<TreeNodeItem<T>>();

        //    foreach (var item in items)
        //    {
        //        item.Children.Clear(); // Limpiar hijos existentes

        //        if (item.ParentID.HasValue && lookup.TryGetValue(item.ParentID.Value, out var parent))
        //        {
        //            parent.Children.Add(item);
        //        }
        //        else
        //        {
        //            rootNodes.Add(item);
        //        }
        //    }

        //    return rootNodes;
        //}

        public void SetDataSource(List<T> list, Func<T, long> idSelector, Func<T, long?> parentIdSelector)
        {
            DataSource = list.Select(item => new TreeNodeItem<T>
            {
                ID = idSelector(item),
                ParentID = parentIdSelector(item),
                Data = item
            }).ToList();
        }

        //public void SetDataSource(List<T> list, Func<T, long> idSelector, Func<T, long?> parentIdSelector)
        //{
        //    DataSource = list.Select(item => new TreeNodeItem<T>
        //    {
        //        ID = idSelector(item),
        //        ParentID = parentIdSelector(item),
        //        Data = item
        //    }).ToList();
        //}

        public void Search(string searchTerm)
        {
            if (columnSelector == null)
                throw new InvalidOperationException("El columnSelector no está asignado.");

            BeginUpdate();

            // Restablecer el estado de expansión de todos los nodos
            foreach (var node in DataSource)
            {
                node.IsExpanded = false;
            }

            // Crear un conjunto para los nodos que deben mostrarse
            var nodesToDisplay = new HashSet<TreeNodeItem<T>>();

            // Buscar nodos que coinciden y sus padres
            foreach (var node in DataSource)
            {
                // Obtener los valores de todas las columnas
                var columnValues = columnSelector(node.Data);

                // Verificar si alguno de los valores de las columnas contiene el término de búsqueda
                bool isMatch = columnValues.Any(value => value != null && value.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

                if (isMatch)
                {
                    // Agregar el nodo y sus padres al conjunto
                    var currentNode = node;
                    while (currentNode != null)
                    {
                        nodesToDisplay.Add(currentNode);
                        currentNode.IsExpanded = true; // Expandir el nodo para que sus hijos sean visibles
                        if (currentNode.ParentID.HasValue)
                        {
                            currentNode = DataSource.FirstOrDefault(n => n.ID == currentNode.ParentID.Value);
                        }
                        else
                        {
                            currentNode = null;
                        }
                    }
                }
            }

            // Reconstruir el árbol completo
            var tree = BuildTree(DataSource);

            // Limpiar los nodos visibles
            visibleNodes.Clear();

            // Agregar los nodos visibles basados en el conjunto nodesToDisplay
            foreach (var node in tree)
            {
                AddVisibleNode(node, nodesToDisplay);
            }

            VirtualListSize = visibleNodes.Count;
            EndUpdate();
            Invalidate();
        }

        private void TreeListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < visibleNodes.Count)
            {
                var node = visibleNodes[e.ItemIndex];
                var subItems = columnSelector(node.Data);

                // Verificar que el número de subitems coincide con el número de columnas
                if (subItems.Length != this.Columns.Count)
                {
                    throw new InvalidOperationException("El número de subitems debe coincidir con el número de columnas.");
                }

                // Crear el ListViewItem con el primer subitem
                var item = new ListViewItem(subItems[0])
                {
                    ImageIndex = imageIndexSelector(node.Data),
                    Tag = node,
                    BackColor = this.BackColor,
                    ForeColor = this.ForeColor,
                    Font = this.Font
                };

                // Agregar los subitems restantes
                for (int i = 1; i < subItems.Length; i++)
                {
                    item.SubItems.Add(subItems[i]);
                }

                e.Item = item;
            }
        }

        private void TreeListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTestInfo = HitTest(e.Location);
            if (hitTestInfo.Item != null)
            {
                int index = hitTestInfo.Item.Index;
                var node = visibleNodes[index];
                int indentLevel = GetNodeLevel(node);
                int indentWidth = 20;
                int glyphWidth = 16;

                Rectangle glyphRect = new Rectangle(
                    hitTestInfo.Item.Bounds.Left + indentLevel * indentWidth,
                    hitTestInfo.Item.Bounds.Top + (hitTestInfo.Item.Bounds.Height - glyphWidth) / 2,
                    glyphWidth,
                    glyphWidth);

                if (glyphRect.Contains(e.Location))
                {
                    // Clic en el símbolo de expansión/contracción
                    bool wasExpanded = node.IsExpanded; // Guardar el estado anterior
                    node.IsExpanded = !node.IsExpanded;
                    RefreshData(columnSelector, imageIndexSelector);

                    if (!wasExpanded && node.IsExpanded)
                    {
                        // El nodo fue expandido
                        AutoResizeColumns();
                    }
                }
            }
        }





        // ... (Código existente del control)


        public void AutoResizeColumns()
            {
                if (Columns.Count == 0)
                    return;

                using (Graphics g = CreateGraphics())
                {
                    // Crear una matriz para almacenar el ancho máximo de cada columna
                    int[] maxColumnWidths = new int[Columns.Count];

                    // Medir el ancho de los encabezados de las columnas
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        ColumnHeader column = Columns[i];
                        SizeF headerSize = g.MeasureString(column.Text, Font);
                        maxColumnWidths[i] = (int)Math.Ceiling(headerSize.Width) + 20; // Añadir espacio adicional
                    }

                    // Definir constantes utilizadas en el dibujo
                    int indentWidthPerLevel = 20;
                    int glyphWidth = 16;
                    int glyphSpacing = 2;
                    int iconSpacing = 4;

                    // Recorrer los nodos visibles y calcular el ancho máximo para cada columna
                    foreach (var node in visibleNodes)
                    {
                        var subItems = columnSelector(node.Data);

                        for (int i = 0; i < subItems.Length; i++)
                        {
                            string text = subItems[i];

                            if (string.IsNullOrEmpty(text))
                                text = " "; // Asegurarse de que hay al menos un carácter para medir

                            SizeF textSize = g.MeasureString(text, Font);

                            if (i == 0)
                            {
                                // Cálculo para la primera columna
                                int indentLevel = GetNodeLevel(node);
                                int totalIndentWidth = indentLevel * indentWidthPerLevel;

                                // Añadir el ancho del glifo y el espacio después del glifo
                                totalIndentWidth += glyphWidth + glyphSpacing;

                                // Determinar si hay un icono
                                int imageIndex = imageIndexSelector(node.Data);
                                int iconWidth = 0;
                                if (SmallImageList != null && imageIndex >= 0)
                                {
                                    iconWidth = SmallImageList.ImageSize.Width;
                                    totalIndentWidth += iconWidth + iconSpacing;
                                }

                                // Calcular el ancho total necesario
                                int totalWidth = totalIndentWidth + (int)Math.Ceiling(textSize.Width);

                                if (totalWidth > maxColumnWidths[i])
                                    maxColumnWidths[i] = totalWidth;
                            }
                            else
                            {
                                // Cálculo para las demás columnas
                                int totalWidth = (int)Math.Ceiling(textSize.Width) + 20; // Añadir espacio adicional

                                if (totalWidth > maxColumnWidths[i])
                                    maxColumnWidths[i] = totalWidth;
                            }
                        }
                    }

                    // Ajustar el ancho de las columnas
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        Columns[i].Width = maxColumnWidths[i];
                    }
                }
            }



        public void ExpandAll()
        {
            SetExpansionState(true);
            RefreshData(columnSelector, imageIndexSelector);
        }

        public void CollapseAll()
        {
            SetExpansionState(false);
            RefreshData(columnSelector, imageIndexSelector);
        }

        private void SetExpansionState(bool isExpanded)
        {
            foreach (var node in DataSource)
            {
                node.IsExpanded = isExpanded;
            }
        }


        //public List<TreeNodeItem<T>> DataSource { get; set; } = new List<TreeNodeItem<T>>();

        //// Lista de nodos visibles
        //private List<TreeNodeItem<T>> visibleNodes = new List<TreeNodeItem<T>>();

        //// Delegados para acceso en modo virtual
        //private Func<T, string[]> columnSelector;
        //private Func<T, int> imageIndexSelector;

        //// Recursos gráficos reutilizables
        //private readonly Pen linePen = Pens.Gray;
        //private readonly Pen expandCollapsePen = Pens.Black;

        //public UITreeListView()
        //{
        //    OwnerDraw = true;
        //    View = View.Details;
        //    FullRowSelect = true;
        //    DoubleBuffered = true;

        //    VirtualMode = true;
        //    RetrieveVirtualItem += TreeListView_RetrieveVirtualItem;
        //    DrawItem += TreeListView_DrawItem;
        //    DrawSubItem += TreeListView_DrawSubItem;
        //    DrawColumnHeader += TreeListView_DrawColumnHeader;
        //    MouseDown += TreeListView_MouseDown;
        //}

        //public event EventHandler<TreeNodeItem<T>> NodeDoubleClicked;

        //protected virtual void OnNodeDoubleClicked(TreeNodeItem<T> node)
        //{
        //    NodeDoubleClicked?.Invoke(this, node);
        //}

        //protected override void OnDoubleClick(EventArgs e)
        //{
        //    base.OnDoubleClick(e);

        //    if (SelectedIndices.Count > 0)
        //    {
        //        var selectedNode = visibleNodes[SelectedIndices[0]];
        //        OnNodeDoubleClicked(selectedNode);
        //    }
        //}

        //private void TreeListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        //{
        //    e.DrawDefault = true;
        //}

        //private void TreeListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
        //    // Necesario para OwnerDraw en modo virtual
        //    e.DrawDefault = false;
        //}

        //private void TreeListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        //{
        //    var node = (TreeNodeItem<T>)e.Item.Tag;
        //    int indentLevel = GetNodeLevel(node);
        //    int indentWidth = 20; // Ancho de la indentación

        //    // Usar el BackColor del ítem
        //    e.Graphics.FillRectangle(new SolidBrush(e.Item.BackColor), e.Bounds);

        //    Rectangle bounds = e.Bounds;

        //    // Ajustar bounds para la primera columna (indentación, glifo, icono)
        //    if (e.ColumnIndex == 0)
        //    {
        //        bounds.X += indentLevel * indentWidth;

        //        int glyphWidth = 16;
        //        int glyphHeight = 16;
        //        Rectangle glyphRect = new Rectangle(bounds.X, bounds.Y + (bounds.Height - glyphHeight) / 2, glyphWidth, glyphHeight);

        //        if (node.HasChildren)
        //        {
        //            // Dibujar el glifo de expansión/contracción
        //            e.Graphics.DrawRectangle(expandCollapsePen, glyphRect);

        //            if (node.IsExpanded)
        //            {
        //                // Dibujar símbolo '-'
        //                e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
        //            }
        //            else
        //            {
        //                // Dibujar símbolo '+'
        //                e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + 4, glyphRect.Top + glyphHeight / 2, glyphRect.Right - 4, glyphRect.Top + glyphHeight / 2);
        //                e.Graphics.DrawLine(expandCollapsePen, glyphRect.Left + glyphWidth / 2, glyphRect.Top + 4, glyphRect.Left + glyphWidth / 2, glyphRect.Bottom - 4);
        //            }
        //        }

        //        bounds.X += glyphWidth + 2;

        //        // Dibujar el icono si existe
        //        if (e.Item.ImageList != null && e.Item.ImageIndex >= 0)
        //        {
        //            Image img = e.Item.ImageList.Images[e.Item.ImageIndex];
        //            e.Graphics.DrawImage(img, bounds.X, bounds.Y + (bounds.Height - img.Height) / 2, img.Width, img.Height);
        //            bounds.X += img.Width + 4;
        //        }

        //        // Ajustar bounds para el texto
        //        bounds.Width = e.Bounds.Right - bounds.X;
        //    }
        //    else
        //    {
        //        // Para otras columnas, ajustar bounds
        //        bounds = e.Bounds;
        //    }

        //    // Obtener la alineación de la columna
        //    HorizontalAlignment alignment = this.Columns[e.ColumnIndex].TextAlign;

        //    // Convertir la alineación a TextFormatFlags
        //    TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

        //    switch (alignment)
        //    {
        //        case HorizontalAlignment.Left:
        //            flags |= TextFormatFlags.Left;
        //            break;
        //        case HorizontalAlignment.Center:
        //            flags |= TextFormatFlags.HorizontalCenter;
        //            break;
        //        case HorizontalAlignment.Right:
        //            flags |= TextFormatFlags.Right;
        //            break;
        //    }

        //    // Dibujar el texto con la alineación especificada
        //    TextRenderer.DrawText(
        //        e.Graphics,
        //        e.SubItem.Text,
        //        e.Item.Font,
        //        bounds,
        //        e.Item.ForeColor,
        //        flags);

        //    // Dibujar líneas para la primera columna
        //    if (e.ColumnIndex == 0)
        //    {
        //        DrawLines(e.Graphics, e.Bounds, node, indentLevel, indentWidth);
        //    }
        //}

        //private void DrawLines(Graphics g, Rectangle bounds, TreeNodeItem<T> node, int indentLevel, int indentWidth)
        //{
        //    int glyphWidth = 16;
        //    int halfHeight = bounds.Top + bounds.Height / 2;

        //    // Dibuja líneas verticales y horizontales
        //    for (int i = 0; i <= indentLevel; i++)
        //    {
        //        int x = bounds.Left + i * indentWidth + glyphWidth / 2;

        //        // Línea vertical
        //        if (i < indentLevel)
        //        {
        //            g.DrawLine(linePen, x, bounds.Top, x, bounds.Bottom);
        //        }
        //        else
        //        {
        //            // Línea horizontal hacia el glifo
        //            g.DrawLine(linePen, x, halfHeight, x + glyphWidth / 2, halfHeight);
        //        }
        //    }
        //}

        //private int GetNodeLevel(TreeNodeItem<T> node)
        //{
        //    int level = 0;
        //    var currentNode = node;
        //    while (currentNode.ParentID.HasValue)
        //    {
        //        level++;
        //        currentNode = DataSource.FirstOrDefault(n => n.ID == currentNode.ParentID.Value);
        //        if (currentNode == null)
        //        {
        //            break;
        //        }
        //    }
        //    return level;
        //}

        //public void RefreshData(Func<T, string[]> columnSelector, Func<T, int> imageIndexSelector)
        //{
        //    this.columnSelector = columnSelector;
        //    this.imageIndexSelector = imageIndexSelector;

        //    BeginUpdate();
        //    visibleNodes.Clear();

        //    if (DataSource != null)
        //    {
        //        var tree = BuildTree(DataSource);
        //        foreach (var node in tree)
        //        {
        //            AddVisibleNode(node);
        //        }
        //    }

        //    VirtualListSize = visibleNodes.Count;
        //    EndUpdate();
        //    Invalidate();
        //}

        //private void AddVisibleNode(TreeNodeItem<T> node)
        //{
        //    visibleNodes.Add(node);

        //    if (node.IsExpanded && node.Children != null && node.Children.Count > 0)
        //    {
        //        foreach (var child in node.Children)
        //        {
        //            AddVisibleNode(child);
        //        }
        //    }
        //}

        //private List<TreeNodeItem<T>> BuildTree(List<TreeNodeItem<T>> items)
        //{
        //    var lookup = items.ToDictionary(item => item.ID);
        //    var rootNodes = new List<TreeNodeItem<T>>();

        //    foreach (var item in items)
        //    {
        //        item.Children.Clear(); // Limpiar hijos existentes

        //        if (item.ParentID.HasValue && lookup.TryGetValue(item.ParentID.Value, out var parent))
        //        {
        //            parent.Children.Add(item);
        //        }
        //        else
        //        {
        //            rootNodes.Add(item);
        //        }
        //    }

        //    return rootNodes;
        //}

        //public void SetDataSource(List<T> list, Func<T, long> idSelector, Func<T, long?> parentIdSelector)
        //{
        //    DataSource = list.Select(item => new TreeNodeItem<T>
        //    {
        //        ID = idSelector(item),
        //        ParentID = parentIdSelector(item),
        //        Data = item
        //    }).ToList();
        //}

        //public void Search(Func<T, bool> predicate, Func<T, string[]> columnSelector, Func<T, int> imageIndexSelector)
        //{
        //    this.columnSelector = columnSelector;
        //    this.imageIndexSelector = imageIndexSelector;

        //    var matchingNodes = DataSource.Where(item => predicate(item.Data)).ToList();
        //    var nodesToDisplay = new HashSet<TreeNodeItem<T>>();

        //    foreach (var node in matchingNodes)
        //    {
        //        var currentNode = node;
        //        while (currentNode != null)
        //        {
        //            nodesToDisplay.Add(currentNode);
        //            if (currentNode.ParentID.HasValue)
        //            {
        //                currentNode = DataSource.FirstOrDefault(item => item.ID == currentNode.ParentID.Value);
        //            }
        //            else
        //            {
        //                currentNode = null;
        //            }
        //        }
        //    }

        //    BeginUpdate();
        //    visibleNodes.Clear();

        //    var tree = BuildTree(nodesToDisplay.ToList());
        //    foreach (var node in tree)
        //    {
        //        AddVisibleNode(node);
        //    }

        //    VirtualListSize = visibleNodes.Count;
        //    EndUpdate();
        //    Invalidate();
        //}

        //private void TreeListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        //{
        //    if (e.ItemIndex >= 0 && e.ItemIndex < visibleNodes.Count)
        //    {
        //        var node = visibleNodes[e.ItemIndex];
        //        var subItems = columnSelector(node.Data);

        //        // Verificar que el número de subitems coincide con el número de columnas
        //        if (subItems.Length != this.Columns.Count)
        //        {
        //            throw new InvalidOperationException("El número de subitems debe coincidir con el número de columnas.");
        //        }

        //        // Crear el ListViewItem con el primer subitem
        //        var item = new ListViewItem(subItems[0])
        //        {
        //            ImageIndex = imageIndexSelector(node.Data),
        //            Tag = node,
        //            BackColor = this.BackColor,
        //            ForeColor = this.ForeColor,
        //            Font = this.Font
        //        };

        //        // Agregar los subitems restantes
        //        for (int i = 1; i < subItems.Length; i++)
        //        {
        //            item.SubItems.Add(subItems[i]);
        //        }

        //        e.Item = item;
        //    }
        //}

        //private void TreeListView_MouseDown(object sender, MouseEventArgs e)
        //{
        //    ListViewHitTestInfo hitTestInfo = HitTest(e.Location);
        //    if (hitTestInfo.Item != null)
        //    {
        //        int index = hitTestInfo.Item.Index;
        //        var node = visibleNodes[index];
        //        int indentLevel = GetNodeLevel(node);
        //        int indentWidth = 20;
        //        int glyphWidth = 16;

        //        Rectangle glyphRect = new Rectangle(
        //            hitTestInfo.Item.Bounds.Left + indentLevel * indentWidth,
        //            hitTestInfo.Item.Bounds.Top + (hitTestInfo.Item.Bounds.Height - glyphWidth) / 2,
        //            glyphWidth,
        //            glyphWidth);

        //        if (glyphRect.Contains(e.Location))
        //        {
        //            // Clic en el símbolo de expansión/contracción
        //            node.IsExpanded = !node.IsExpanded;
        //            RefreshData(columnSelector, imageIndexSelector);
        //        }
        //    }
        //}


    }

}



