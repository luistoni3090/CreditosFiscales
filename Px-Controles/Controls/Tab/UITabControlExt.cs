/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UITabControlExt.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Control Tab, permite cerrar pestañas y dejar fijas los que se indiquen por índice

using Px_Controles.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Px_Controles.Controls.Tab
{
    /// <summary>
    /// Clase UITabControlExt.
    /// Implementa de <see cref="System.Windows.Forms.TabControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.TabControl" />
    public partial class UITabControlExt : TabControl
    {
        /// <summary>
        /// Constructor <see cref="TabControlExt" />.
        /// </summary>
        public UITabControlExt()
            : base()
        {
            SetStyles();
            //this.Multiline = true;
            this.ItemSize = new Size(this.ItemSize.Width, 50);
        }
        /// <summary>
        /// Establece estilos.
        /// </summary>
        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia se muestra cerca de BTN.
        /// </summary>
        /// <value><c>true</c> si esta instancia se muestra cerca de BTN; de lo contrario, <c>false</c>.</value>
        [Description("Ya sea para mostrar un botón de cierre"), Category("Personalizar")]
        public bool IsShowCloseBtn { get; set; }

        [Description("Una lista de números de etiquetas que no se pueden cerrar, subíndice 0"), Category("Personalizar")]
        public int[] UncloseTabIndexs { get; set; }
        /// <summary>
        /// Color de fondo
        /// </summary>
        private Color _backColor = Color.White;
        /// <summary>
        /// Este miembro no tiene significado para este control.
        /// </summary>
        /// <value>El color de fondo.</value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                base.Invalidate(true);
            }
        }

        private Color closeBtnColor = Color.FromArgb(106, 28, 50);

        [Description("关闭按钮颜色")]
        public Color CloseBtnColor
        {
            get { return closeBtnColor; }
            set { closeBtnColor = value; }
        }

        /// <summary>
        /// Color del borde
        /// </summary>
        private Color _borderColor = Color.FromArgb(232, 232, 232);
        /// <summary>
        /// Obtiene o establece el color del borde..
        /// </summary>
        /// <value>El color del borde..</value>
        [DefaultValue(typeof(Color), "232, 232, 232")]
        [Description("\r\nColor del borde de TabControl")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// La cabeza seleccionó el color de fondo.
        /// </summary>
        private Color _headSelectedBackColor = Color.FromArgb(106, 28, 50);
        /// <summary>
        /// Obtiene o establece el color de la cabeza seleccionada.
        /// </summary>
        /// <value>El color de la cabeza seleccionado de nuevo.</value>
        [DefaultValue(typeof(Color), "106, 28, 50")]
        [Description("Color de fondo del encabezado TabPage después de la selección")]
        public Color HeadSelectedBackColor
        {
            get { return _headSelectedBackColor; }
            set { _headSelectedBackColor = value; }
        }

        /// <summary>
        /// El color del borde seleccionado por la cabeza.
        /// </summary>
        private Color _headSelectedBorderColor = Color.FromArgb(232, 232, 232);
        /// <summary>
        /// Obtiene o establece el color del borde seleccionado de la cabeza..
        /// </summary>
        /// <value>El color del borde seleccionado de la cabeza..</value>
        [DefaultValue(typeof(Color), "232, 232, 232")]
        [Description("Color del borde después de seleccionar el encabezado TabPage")]
        public Color HeadSelectedBorderColor
        {
            get { return _headSelectedBorderColor; }
            set { _headSelectedBorderColor = value; }
        }

        /// <summary>
        /// El color de fondo del encabezado
        /// </summary>
        private Color _headerBackColor = Color.White;
        /// <summary>
        /// Obtiene o establece el color del encabezado.
        /// </summary>
        /// <value>El color de la cabecera.</value>
        [DefaultValue(typeof(Color), "White")]
        [Description("\r\nColor de fondo predeterminado del encabezado de TabPage")]
        public Color HeaderBackColor
        {
            get { return _headerBackColor; }
            set { _headerBackColor = value; }
        }

        /// <summary>
        /// Dibuja el fondo del control..
        /// </summary>
        /// <param name="pevent">Contiene información sobre el control a dibujar <see cref="T:System.Windows.Forms.PaintEventArgs" />.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.DesignMode == true)
            {
                LinearGradientBrush backBrush = new LinearGradientBrush(
                            this.Bounds,
                            SystemColors.ControlLightLight,
                            SystemColors.ControlLight,
                            LinearGradientMode.Vertical);
                pevent.Graphics.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
            else
            {
                this.PaintTransparentBackground(pevent.Graphics, this.ClientRectangle);
            }
        }

        /// <summary>
        /// Configuración del color de fondo de TabControl
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="clipRect">Zola de pintado.</param>
        protected void PaintTransparentBackground(Graphics g, Rectangle clipRect)
        {
            if ((this.Parent != null))
            {
                clipRect.Offset(this.Location);
                PaintEventArgs e = new PaintEventArgs(g, clipRect);
                GraphicsState state = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    g.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                }
                finally
                {
                    g.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                    // Nuevo fragmento agregado, para ser probado. no se si funcione
                    using (SolidBrush brush = new SolidBrush(_backColor))
                    {
                        clipRect.Inflate(1, 1);
                        g.FillRectangle(brush, clipRect);
                    }
                }
            }
            else
            {
                System.Drawing.Drawing2D.LinearGradientBrush backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(this.Bounds, SystemColors.ControlLightLight, SystemColors.ControlLight, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                g.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
        }

        /// <summary>
        /// Evento de repintado <see cref="E:System.Windows.Forms.Control.Paint" />.
        /// </summary>
        /// <param name="e">Contiene datos de eventos <see cref="T:System.Windows.Forms.PaintEventArgs" />.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Pinto el fondo Background 
            base.OnPaint(e);
            this.PaintTransparentBackground(e.Graphics, this.ClientRectangle);
            this.PaintAllTheTabs(e);
            this.PaintTheTabPageBorder(e);
            this.PaintTheSelectedTab(e);
        }

        /// <summary>
        /// Pito los tabs.
        /// </summary>
        /// <param name="e"><see cref="System.Windows.Forms.PaintEventArgs" /> instancia que contiene los datos del evento.</param>
        private void PaintAllTheTabs(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                for (int index = 0; index < this.TabCount; index++)
                {
                    this.PaintTab(e, index);
                }
            }
        }

        /// <summary>
        /// Pinto el tab.
        /// </summary>
        /// <param name="e"><see cref="System.Windows.Forms.PaintEventArgs" /> instancia que contiene los datos del evento.</param>
        /// <param name="index">Índice tel tab.</param>
        private void PaintTab(System.Windows.Forms.PaintEventArgs e, int index)
        {
            GraphicsPath path = this.GetTabPath(index);
            this.PaintTabBackground(e.Graphics, index, path);
            this.PaintTabBorder(e.Graphics, index, path);
            this.PaintTabText(e.Graphics, index);
            this.PaintTabImage(e.Graphics, index);
            if (IsShowCloseBtn)
            {
                if (UncloseTabIndexs != null)
                    if (UncloseTabIndexs.ToList().Contains(index))
                        return;

                Rectangle rect = this.GetTabRect(index);
                e.Graphics.DrawLine(new Pen(closeBtnColor, 1F), new Point(rect.Right - 15, rect.Top + 5), new Point(rect.Right - 5, rect.Top + 15));
                e.Graphics.DrawLine(new Pen(closeBtnColor, 1F), new Point(rect.Right - 5, rect.Top + 5), new Point(rect.Right - 15, rect.Top + 15));
            }
        }

        /// <summary>
        /// Establecer color de encabezado de pestaña
        /// </summary>
        /// <param name="graph">Graphics.</param>
        /// <param name="index">índice.</param>
        /// <param name="path">La ruta a dibujar.</param>
        private void PaintTabBackground(System.Drawing.Graphics graph, int index, System.Drawing.Drawing2D.GraphicsPath path)
        {
            Rectangle rect = this.GetTabRect(index);
            if (rect.Width == 0 || rect.Height == 0) return;
            System.Drawing.Brush buttonBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, _headerBackColor, _headerBackColor, LinearGradientMode.Vertical);  // El color de fondo del encabezado de la página TabPage cuando no está seleccionado
            graph.FillPath(buttonBrush, path);
            //if (index == this.SelectedIndex)
            //{
            //    //buttonBrush = new System.Drawing.SolidBrush(_headSelectedBackColor);
            //    graph.DrawLine(new Pen(_headerBackColor), rect.Right+2, rect.Bottom, rect.Left + 1, rect.Bottom);
            //}
            buttonBrush.Dispose();
        }

        /// <summary>
        /// Establecer color del borde del encabezado de la pestaña
        /// </summary>
        /// <param name="graph">Graphics.</param>
        /// <param name="index">Índice del tab.</param>
        /// <param name="path">Ruta de dibujo.</param>
        private void PaintTabBorder(System.Drawing.Graphics graph, int index, System.Drawing.Drawing2D.GraphicsPath path)
        {
            Pen borderPen = new Pen(_borderColor);// Color del borde del encabezado de TabPage cuando TabPage no está seleccionado
            if (index == this.SelectedIndex)
                borderPen = new Pen(_headSelectedBorderColor); // Color del borde del encabezado de TabPage después de seleccionar TabPage

            graph.DrawPath(borderPen, path);
            borderPen.Dispose();
        }

        /// <summary>
        /// Dibujo la imagen del tab.
        /// </summary>
        /// <param name="g">Graphics.</param>
        /// <param name="index">Índice del tab.</param>
        private void PaintTabImage(System.Drawing.Graphics g, int index)
        {
            Image tabImage = null;
            if (this.TabPages[index].ImageIndex > -1 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            }
            else if (this.TabPages[index].ImageKey.Trim().Length > 0 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }
            if (tabImage != null)
            {
                Rectangle rect = this.GetTabRect(index);
                g.DrawImage(tabImage, rect.Right - rect.Height - 4, 4, rect.Height - 2, rect.Height - 2);
            }
        }

        /// <summary>
        /// Dibujo el texto del tab.
        /// </summary>
        /// <param name="graph">Graphics.</param>
        /// <param name="index">Índice del tab.</param>
        private void PaintTabText(System.Drawing.Graphics graph, int index)
        {
            string tabtext = this.TabPages[index].Text;

            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            Brush forebrush = null;

            if (this.TabPages[index].Enabled == false)
            {
                forebrush = SystemBrushes.ControlDark;
            }
            else
            {
                forebrush = SystemBrushes.ControlText;
            }

            Font tabFont = this.Font;
            if (index == this.SelectedIndex)
            {
                if (this.TabPages[index].Enabled != false)
                {
                    forebrush = new SolidBrush(_headSelectedBackColor);
                }
            }

            Rectangle rect = this.GetTabRect(index);

            var txtSize = ControlHelper.GetStringWidth(tabtext, graph, tabFont);
            Rectangle rect2 = new Rectangle(rect.Left + (rect.Width - txtSize) / 2 - 1, rect.Top, rect.Width, rect.Height);

            graph.DrawString(tabtext, tabFont, forebrush, rect2, format);
        }

        /// <summary>
        /// Establecer el color del borde de la página de contenido de TabPage
        /// </summary>
        /// <param name="e"><see cref="System.Windows.Forms.PaintEventArgs" /> instancia que contiene los datos del evento.</param>
        private void PaintTheTabPageBorder(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                Rectangle borderRect = this.TabPages[0].Bounds;
                //borderRect.Inflate(1, 1);
                Rectangle rect = new Rectangle(borderRect.X - 2, borderRect.Y - 1, borderRect.Width + 5, borderRect.Height + 2);
                ControlPaint.DrawBorder(e.Graphics, rect, this.BorderColor, ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
        /// Redibujo el tab seleccionado.
        /// </summary>
        /// <param name="e"><see cref="System.Windows.Forms.PaintEventArgs" /> instancia que contiene los datos del evento.</param>
        private void PaintTheSelectedTab(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.SelectedIndex == -1)
                return;
            Rectangle selrect;
            int selrectRight = 0;
            selrect = this.GetTabRect(this.SelectedIndex);
            selrectRight = selrect.Right;
            e.Graphics.DrawLine(new Pen(_headSelectedBackColor), selrect.Left, selrect.Bottom + 1, selrectRight, selrect.Bottom + 1);
        }

        /// <summary>
        /// Obtiene la ruta del tab.
        /// </summary>
        /// <param name="index">Índice del tab.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath GetTabPath(int index)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.Reset();

            Rectangle rect = this.GetTabRect(index);

            switch (Alignment)
            {
                case TabAlignment.Top:

                    break;
                case TabAlignment.Bottom:

                    break;
                case TabAlignment.Left:

                    break;
                case TabAlignment.Right:

                    break;
            }

            path.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom + 1);
            path.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
            path.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom + 1);
            path.AddLine(rect.Right, rect.Bottom + 1, rect.Left, rect.Bottom + 1);

            return path;
        }

        /// <summary>
        /// Mensajes de win32. Esta madre debo sacarla a una clase estática aparte para que se a de uso común mientras aquí, obvio es sacado de internet
        /// </summary>
        /// <param name="hWnd">h WND.</param>
        /// <param name="Msg">MSG.</param>
        /// <param name="wParam">w.</param>
        /// <param name="lParam">l.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The wm setfont
        /// </summary>
        private const int WM_SETFONT = 0x30;
        /// <summary>
        /// The wm fontchange
        /// </summary>
        private const int WM_FONTCHANGE = 0x1d;

        /// <summary>
        /// 引发 <see cref="M:System.Windows.Forms.Control.CreateControl" /> 方法.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Evento <see cref="E:System.Windows.Forms.Control.FontChanged" />.
        /// </summary>
        /// <param name="e">Contiene datos de eventos <see cref="T:System.EventArgs" />.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = this.Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
        }

        /// <summary>
        /// Este madre anula <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.
        /// </summary>
        /// <param name="m">Un objeto de mensaje de Windows.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0201) // WM_LBUTTONDOWN
            {
                if (!DesignMode)
                {
                    if (IsShowCloseBtn)
                    {
                        var mouseLocation = this.PointToClient(Control.MousePosition);
                        int index = GetMouseDownTabHead(mouseLocation);
                        if (index >= 0)
                        {
                            if (UncloseTabIndexs != null)
                            {
                                if (UncloseTabIndexs.ToList().Contains(index))
                                    return;
                            }
                            Rectangle rect = this.GetTabRect(index);
                            var closeRect = new Rectangle(rect.Right - 15, rect.Top + 5, 10, 10);
                            if (closeRect.Contains(mouseLocation))
                            {
                                this.TabPages.RemoveAt(index);
                                return;
                            }
                        }
                    }
                }
            }


        }
        /// <summary>
        /// Preproceso el teclado o ingresar mensajes dentro del bucle de mensajes antes de enviarlos.
        /// </summary>
        /// <param name="msg"><see cref = "T:System.Windows.Forms.Message" /> pasado por referencia, que representa el mensaje a procesar.Los valores posibles son WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR y WM_SYSCHAR.</param>
        /// <returns>true si el control ha manejado el mensaje; false en caso contrario.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override bool PreProcessMessage(ref Message msg)
        {

            return base.PreProcessMessage(ref msg);
        }

        /// <summary>
        /// Baja el mouse hacia la cabecera de la pestaña.
        /// </summary>
        /// <param name="point">Puntero.</param>
        /// <returns>System.Int32.</returns>
        private int GetMouseDownTabHead(Point point)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle rect = this.GetTabRect(i);
                if (rect.Contains(point))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
