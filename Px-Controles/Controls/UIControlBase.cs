/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UIControlBase.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Base para controles desarrollados por nosotros

using Px_Controles.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls
{
    /// <summary>
    /// Class UIControlBase.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// Implements the <see cref="System.Windows.Forms.IContainerControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    /// <seealso cref="System.Windows.Forms.IContainerControl" />
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class UIControlBase : UserControl, IContainerControl
    {
        /// <summary>
        /// Radio
        /// </summary>
        private bool _isRadius = false;

        /// <summary>
        /// Radio de esquina
        /// </summary>
        private int _cornerRadius = 24;


        /// <summary>
        /// La es mostrar borde
        /// </summary>
        private bool _isShowRect = false;

        /// <summary>
        /// Color del borde
        /// </summary>
        private Color _rectColor = Color.FromArgb(220, 220, 220);

        /// <summary>
        /// Tamaño del borde
        /// </summary>
        private int _rectWidth = 1;

        /// <summary>
        /// Color de relleno
        /// </summary>
        private Color _fillColor = Color.Transparent;
        /// <summary>
        /// Si las esquinas están redondeadas
        /// </summary>
        /// <value><c>true</c> si tiene radio; si no, <c>false</c>.</value>
        [Description("Si las esquinas están redondeadas"), Category("personalizar")]
        public virtual bool IsRadius
        {
            get
            {
                return this._isRadius;
            }
            set
            {
                this._isRadius = value;
                Refresh();
            }
        }
        /// <summary>
        /// ángulo 
        /// </summary>
        /// <value>Esquina del borde.</value>
        [Description("ángulo "), Category("personalizar")]
        public virtual int ConerRadius
        {
            get
            {
                return this._cornerRadius;
            }
            set
            {
                this._cornerRadius = Math.Max(value, 1);
                Refresh();
            }
        }

        /// <summary>
        /// Ya sea para mostrar bordes
        /// </summary>
        /// <value><c>true</c> si se muestra el borde; si no, <c>false</c>.</value>
        [Description("Ya sea para mostrar bordes"), Category("personalizar")]
        public virtual bool IsShowRect
        {
            get
            {
                return this._isShowRect;
            }
            set
            {
                this._isShowRect = value;
                Refresh();
            }
        }
        /// <summary>
        /// color del borde
        /// </summary>
        /// <value>El color del borde.</value>
        [Description("color del borde"), Category("personalizar")]
        public virtual Color RectColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// ancho del borde
        /// </summary>
        /// <value>El ancho dl borde.</value>
        [Description("ancho del borde"), Category("personalizar")]
        public virtual int RectWidth
        {
            get
            {
                return this._rectWidth;
            }
            set
            {
                this._rectWidth = value;
                Refresh();
            }
        }
        /// <summary>
        /// Color de relleno cuando se utiliza borde, no rellenar cuando el valor es color de fondo o color transparente o valor vacío
        /// </summary>
        /// <value>Color de relleno.</value>
        [Description("Color de relleno cuando se utiliza borde, no rellenar cuando el valor es color de fondo o color transparente o valor vacío"), Category("personalizar")]
        public virtual Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Constructor <see cref="UIControlBase" />.
        /// </summary>
        public UIControlBase()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Genera el evento <see cref="E:System.Windows.Forms.Control.Paint" />.
        /// </summary>
        /// <param name="e">Contiene datos de eventos <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Visible)
            {
                if (this._isRadius)
                {
                    this.SetWindowRegion();
                }
                else
                {
                    //Después de desactivar las esquinas redondeadas, se muestra el rectángulo original.
                    GraphicsPath g = new GraphicsPath();
                    g.AddRectangle(base.ClientRectangle);
                    g.CloseFigure();
                    base.Region = new Region(g);
                }

                GraphicsPath graphicsPath = new GraphicsPath();
                if (this._isShowRect || (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor))
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (_isRadius)
                    {
                        graphicsPath.AddArc(0, 0, _cornerRadius, _cornerRadius, 180f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, 0, _cornerRadius, _cornerRadius, 270f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 0f, 90f);
                        graphicsPath.AddArc(0, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 90f, 90f);
                        graphicsPath.CloseFigure();
                    }
                    else
                    {
                        graphicsPath.AddRectangle(clientRectangle);
                    }
                }
                e.Graphics.SetGDIHigh();
                if (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor)
                    e.Graphics.FillPath(new SolidBrush(this._fillColor), graphicsPath);
                if (this._isShowRect)
                {
                    Color rectColor = this._rectColor;
                    Pen pen = new Pen(rectColor, (float)this._rectWidth);
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Establece la región de la ventana.
        /// </summary>
        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this._cornerRadius);
            base.Region = new Region(path);
        }

        /// <summary>
        /// Obtiene la ruta recta redondeada.
        /// </summary>
        /// <param name="rect">Borde.</param>
        /// <param name="radius">Radio.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//esquina superior izquierda
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);//esquina superior derecha
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//esquina inferior derecha
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//esquina inferior izquierda
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">para ser procesadoWindows <see cref="T:System.Windows.Forms.Message" />。</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }
    }
}
