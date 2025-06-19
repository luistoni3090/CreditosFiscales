/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmAnchorTips.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Para tooltips

using Px_Controles.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Forms
{
    /// <summary>
    /// Forma FrmAnchorTips.
    /// Herencia de <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FrmAnchorTips : Form
    {
        /// <summary>
        /// Mensaje
        /// </summary>
        private string m_strMsg = string.Empty;

        /// <summary>
        /// Obtiene o establece MSG.
        /// </summary>
        /// <value>Mensaje.</value>
        public string StrMsg
        {
            get { return m_strMsg; }
            set
            {
                m_strMsg = value;
                if (string.IsNullOrEmpty(value))
                    return;
                ResetForm(value);
            }
        }
        /// <summary>
        /// El handle
        /// </summary>
        bool haveHandle = false;
        /// <summary>
        /// Tm area de pintado
        /// </summary>
        Rectangle m_rectControl;
        /// <summary>
        /// Obtiene o establece el area de pintado del control.
        /// </summary>
        /// <value>Area de pintado.</value>
        public Rectangle RectControl
        {
            get { return m_rectControl; }
            set
            {
                m_rectControl = value;
            }
        }
        /// <summary>
        /// Ubicación
        /// </summary>
        AnchorTipsLocation m_location;
        /// <summary>
        /// Color de fondo
        /// </summary>
        Color? m_background = null;
        /// <summary>
        /// Folor primario
        /// </summary>
        Color? m_foreColor = null;
        /// <summary>
        /// Tamaño
        /// </summary>
        int m_fontSize = 10;
        #region Constructor
        /// <summary>
        /// Constructor <see cref="FrmAnchorTips"/>.
        /// </summary>
        /// <param name="rectControl">Area de trabajo.</param>
        /// <param name="strMsg">Mensaje.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="background">Fondo del control.</param>
        /// <param name="foreColor">Color primario.</param>
        /// <param name="fontSize">Tamaño.</param>
        /// <param name="autoCloseTime">Tiempo de cierre automático.</param>
        private FrmAnchorTips(
            Rectangle rectControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000)
        {
            InitializeComponent();
            m_rectControl = rectControl;
            m_location = location;
            m_background = background;
            m_foreColor = foreColor;
            m_fontSize = fontSize;
            StrMsg = strMsg;
            if (autoCloseTime > 0)
            {
                Timer t = new Timer();
                t.Interval = autoCloseTime;
                t.Tick += (a, b) =>
                {
                    this.Close();
                };
                t.Enabled = true;
            }
        }

        /// <summary>
        /// Reestablece el control.
        /// </summary>
        /// <param name="strMsg">Mensaje.</param>
        private void ResetForm(string strMsg)
        {
            Graphics g = this.CreateGraphics();
            Font _font = new Font("微软雅黑", m_fontSize);
            Color _background = m_background == null ? Color.FromArgb(255, 77, 58) : m_background.Value;
            Color _foreColor = m_foreColor == null ? Color.White : m_foreColor.Value;
            System.Drawing.SizeF sizeText = g.MeasureString(strMsg, _font);
            g.Dispose();
            var formSize = new Size((int)sizeText.Width + 10, (int)sizeText.Height + 10);
            if (formSize.Width < 10)
                formSize.Width = 10;
            if (formSize.Height < 10)
                formSize.Height = 10;
            if (m_location == AnchorTipsLocation.LEFT || m_location == AnchorTipsLocation.RIGHT)
            {
                formSize.Width += 20;
            }
            else
            {
                formSize.Height += 20;
            }

            #region Obtener ruta del formulario
            GraphicsPath path = new GraphicsPath();
            Rectangle rect;
            switch (m_location)
            {
                case AnchorTipsLocation.TOP:
                    rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 20 - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 20);
                    break;
                case AnchorTipsLocation.RIGHT:
                    rect = new Rectangle(20, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                    this.Location = new Point(m_rectControl.Right, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                    break;
                case AnchorTipsLocation.BOTTOM:
                    rect = new Rectangle(1, 20, formSize.Width - 2, formSize.Height - 20 - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Bottom);
                    break;
                default:
                    rect = new Rectangle(1, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                    this.Location = new Point(m_rectControl.X - rect.Width - 20, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                    break;
            }
            int cornerRadius = 2;

            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);//esquina superior izquierda
            #region Arriba
            if (m_location == AnchorTipsLocation.BOTTOM)
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Left + rect.Width / 2 - 10, rect.Y);//superior
                path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Y, rect.Left + rect.Width / 2, rect.Y - 19);//superior
                path.AddLine(rect.Left + rect.Width / 2, rect.Y - 19, rect.Left + rect.Width / 2 + 10, rect.Y);//superior
                path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//superior
            }
            else
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//superior
            }
            #endregion
            path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);//esquina superior derecha
            #region inferior
            if (m_location == AnchorTipsLocation.LEFT)
            {
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height / 2 - 10);//inferior
                path.AddLine(rect.Right, rect.Y + rect.Height / 2 - 10, rect.Right + 19, rect.Y + rect.Height / 2);//inferior
                path.AddLine(rect.Right + 19, rect.Y + rect.Height / 2, rect.Right, rect.Y + rect.Height / 2 + 10);//inferior
                path.AddLine(rect.Right, rect.Y + rect.Height / 2 + 10, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//inferior            
            }
            else
            {
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//inferior
            }
            #endregion
            path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);//esquina inferior
            #region abajo
            if (m_location == AnchorTipsLocation.TOP)
            {
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Left + rect.Width / 2 + 10, rect.Bottom);
                path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Bottom, rect.Left + rect.Width / 2, rect.Bottom + 19);
                path.AddLine(rect.Left + rect.Width / 2, rect.Bottom + 19, rect.Left + rect.Width / 2 - 10, rect.Bottom);
                path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            }
            else
            {
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            }
            #endregion
            path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);//esquina inferior izquierda
            #region izquierda
            if (m_location == AnchorTipsLocation.RIGHT)
            {
                path.AddLine(rect.Left, rect.Y + cornerRadius * 2, rect.Left, rect.Y + rect.Height / 2 - 10);//izquierda
                path.AddLine(rect.Left, rect.Y + rect.Height / 2 - 10, rect.Left - 19, rect.Y + rect.Height / 2);//izquierda
                path.AddLine(rect.Left - 19, rect.Y + rect.Height / 2, rect.Left, rect.Y + rect.Height / 2 + 10);//izquierda
                path.AddLine(rect.Left, rect.Y + rect.Height / 2 + 10, rect.Left, rect.Y + rect.Height - cornerRadius * 2);//izquierda          
            }
            else
            {
                path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);//izquierda
            }
            #endregion
            path.CloseFigure();
            #endregion

            Bitmap bit = new Bitmap(formSize.Width, formSize.Height);
            this.Size = formSize;

            #region Drawing
            Graphics gBit = Graphics.FromImage(bit);
            gBit.SetGDIHigh();
            gBit.FillPath(new SolidBrush(_background), path);
            gBit.DrawString(strMsg, _font, new SolidBrush(_foreColor), rect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            gBit.Dispose();
            #endregion

            SetBits(bit);
        }
        #endregion

        #region Pista
        /// <summary>
        /// Muestra tips.
        /// </summary>
        /// <param name="anchorControl">Control padre.</param>
        /// <param name="strMsg">Mensaje.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="background">Fondo del control.</param>
        /// <param name="foreColor">Color primario.</param>
        /// <param name="deviation">Desviación.</param>
        /// <param name="fontSize">Tamaño.</param>
        /// <param name="autoCloseTime">Tiempo de cerrado automático.</param>
        /// <param name="blnTopMost">Ya sea para fijarlo en la parte superior</param>
        /// <returns>FrmAnchorTips.</returns>
        public static FrmAnchorTips ShowTips(
            Control anchorControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            Size? deviation = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            bool blnTopMost = true)
        {
            Point p;
            if (anchorControl is Form)
            {
                p = anchorControl.Location;
            }
            else
            {
                p = anchorControl.Parent.PointToScreen(anchorControl.Location);
            }
            if (deviation != null)
            {
                p = p + deviation.Value;
            }
            return ShowTips(new Rectangle(p, anchorControl.Size), strMsg, location, background, foreColor, fontSize, autoCloseTime, anchorControl.Parent, blnTopMost);
        }
        #endregion

        #region Pista
        /// <summary>
        /// Muestra el tooltips.
        /// </summary>
        /// <param name="rectControl">Control padre.</param>
        /// <param name="strMsg">Mensaje.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="background">fondo del control.</param>
        /// <param name="foreColor">Color primario.</param>
        /// <param name="fontSize">Tamaño de la fuente.</param>
        /// <param name="autoCloseTime">Tiempo de cierre automático.</param>
        /// <param name="parentForm">formulario para padres</param>
        /// <param name="blnTopMost">Ya sea para fijarlo en la parte superior</param>
        /// <returns>FrmAnchorTips.</returns>
        public static FrmAnchorTips ShowTips(
            Rectangle rectControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            Control parentControl = null,
            bool blnTopMost = true)
        {
            FrmAnchorTips frm = new FrmAnchorTips(rectControl, strMsg, location, background, foreColor, fontSize, autoCloseTime);
            frm.TopMost = blnTopMost;
            frm.Show(parentControl);
            //if (parentControl != null)
            //{               
            //    parentControl.VisibleChanged += (a, b) =>
            //    {
            //        try
            //        {
            //            Control c = a as Control;
            //            if (CheckControlClose(c))
            //            {
            //                frm.Close();
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    };               
            //}
            return frm;
        }

        private static bool CheckControlClose(Control c)
        {
            if (c.IsDisposed || !c.Visible)
                return true;
            else if (c.Parent != null)
                return CheckControlClose(c.Parent);
            else
            {
                if (c is Form)
                    return false;
                else
                    return true;
            }
        }
        #endregion

        #region Override

        /// <summary>
        /// Genera el evento <see cref="E:System.Windows.Forms.Form.Closing" />.
        /// </summary>
        /// <param name="e">un que contiene datos del evento <see cref="T:System.ComponentModel.CancelEventArgs" />.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            haveHandle = false;
            this.Dispose();
        }

        /// <summary>
        /// Maneja el evento <see cref="E:HandleCreated" />.
        /// </summary>
        /// <param name="e">Contiene datos de eventos <see cref="T:System.EventArgs" />.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            InitializeStyles();
            base.OnHandleCreated(e);
            haveHandle = true;
        }

        /// <summary>
        /// Obtiene los parámetros de creación.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }

        #endregion

        /// <summary>
        /// nicializa los estilos.
        /// </summary>
        private void InitializeStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        #region Mostrar formularios basados ​​en imágenes
        /// <summary>
        /// Mostrar formularios basados ​​en imágenes
        /// </summary>
        /// <param name="bitmap">bitmap</param>
        /// <exception cref="System.ApplicationException">La imagen debe ser de 32 bits con canal alfa..</exception>
        /// <exception cref="ApplicationException">The picture must be 32bit picture with alpha channel.</exception>
        private void SetBits(Bitmap bitmap)
        {
            if (!haveHandle) return;

            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        #endregion

        #region Manejo de formularios sin enfoque

        /// <summary>
        /// Establece la ventana activa.
        /// </summary>
        /// <param name="handle">handle.</param>
        /// <returns>IntPtr.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);
        /// <summary>
        /// The wm activate
        /// </summary>
        private const int WM_ACTIVATE = 0x006;
        /// <summary>
        /// The wm activateapp
        /// </summary>
        private const int WM_ACTIVATEAPP = 0x01C;
        /// <summary>
        /// The wm ncactivate
        /// </summary>
        private const int WM_NCACTIVATE = 0x086;
        /// <summary>
        /// The wa inactive
        /// </summary>
        private const int WA_INACTIVE = 0;
        /// <summary>
        /// The wm mouseactivate
        /// </summary>
        private const int WM_MOUSEACTIVATE = 0x21;
        /// <summary>
        /// The ma noactivate
        /// </summary>
        private const int MA_NOACTIVATE = 3;
        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">para ser procesadoWindows <see cref="T:System.Windows.Forms.Message" />.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }
            else if (m.Msg == WM_NCACTIVATE)
            {
                if (((int)m.WParam & 0xFFFF) != WA_INACTIVE)
                {
                    if (m.LParam != IntPtr.Zero)
                    {
                        SetActiveWindow(m.LParam);
                    }
                    else
                    {
                        SetActiveWindow(IntPtr.Zero);
                    }
                }
            }
            base.WndProc(ref m);
        }

        #endregion
    }

    /// <summary>
    /// Enum AnchorTipsLocation
    /// </summary>
    public enum AnchorTipsLocation
    {
        /// <summary>
        /// Izquierda
        /// </summary>
        LEFT,
        /// <summary>
        /// Arriba
        /// </summary>
        TOP,
        /// <summary>
        /// Derecha
        /// </summary>
        RIGHT,
        /// <summary>
        /// Abajo
        /// </summary>
        BOTTOM
    }

    /// <summary>
    /// Class Win32.
    /// </summary>
    class Win32
    {
        /// <summary>
        /// Struct Size
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            /// <summary>
            /// The cx
            /// </summary>
            public Int32 cx;
            /// <summary>
            /// The cy
            /// </summary>
            public Int32 cy;

            /// <summary>
            /// Initializes a new instance of the <see cref="Size" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        /// <summary>
        /// Struct BLENDFUNCTION
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            /// <summary>
            /// The blend op
            /// </summary>
            public byte BlendOp;
            /// <summary>
            /// The blend flags
            /// </summary>
            public byte BlendFlags;
            /// <summary>
            /// The source constant alpha
            /// </summary>
            public byte SourceConstantAlpha;
            /// <summary>
            /// The alpha format
            /// </summary>
            public byte AlphaFormat;
        }

        /// <summary>
        /// Struct Point
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            /// The x
            /// </summary>
            public Int32 x;
            /// <summary>
            /// The y
            /// </summary>
            public Int32 y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// The ac source over
        /// </summary>
        public const byte AC_SRC_OVER = 0;
        /// <summary>
        /// The ulw alpha
        /// </summary>
        public const Int32 ULW_ALPHA = 2;
        /// <summary>
        /// The ac source alpha
        /// </summary>
        public const byte AC_SRC_ALPHA = 1;

        /// <summary>
        /// Creates the compatible dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        /// <summary>
        /// Gets the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="hObj">The h object.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Deletes the dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObj">The h object.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        /// <summary>
        /// Updates the layered window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="hdcDst">The HDC DST.</param>
        /// <param name="pptDst">The PPT DST.</param>
        /// <param name="psize">The psize.</param>
        /// <param name="hdcSrc">The HDC source.</param>
        /// <param name="pptSrc">The PPT source.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="pblend">The pblend.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        /// <summary>
        /// Exts the create region.
        /// </summary>
        /// <param name="lpXform">The lp xform.</param>
        /// <param name="nCount">The n count.</param>
        /// <param name="rgnData">The RGN data.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);
    }
}
