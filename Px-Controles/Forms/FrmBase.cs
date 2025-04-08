/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmBase.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma base


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
    /// Forma base.
    /// Heredada <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmBase : Form
    {
        /// <summary>
        /// Obtiene o establece las teclas de acceso rápido.
        /// </summary>
        /// <value>las teclas de acceso rápido.</value>
        [Description("Lista de teclas de acceso rápido definidas"), Category("Personalizar")]
        public Dictionary<int, string> HotKeys { get; set; }
        /// <summary>
        /// Delegar HotKeyEventHandler
        /// </summary>
        /// <param name="strHotKey">La tecla de acceso rápido a la cadena.</param>
        /// <returns><c>true</c> si XXXX, <c>false</c> en caso contrario.</returns>
        public delegate bool HotKeyEventHandler(string strHotKey);
        /// <summary>
        /// eventos de teclas de acceso rápido
        /// </summary>
        [Description("eventos de teclas de acceso rápido"), Category("Personalizar")]
        public event HotKeyEventHandler HotKeyDown;
        #region Propiedades de campo

        /// <summary>
        /// Perder el foco
        /// </summary>
        bool _isLoseFocusClose = false;
        /// <summary>
        /// Ya sea para volver a dibujar el estilo del borde
        /// </summary>
        private bool _redraw = false;
        /// <summary>
        /// Ya sea para mostrar esquinas redondeadas
        /// </summary>
        private bool _isShowRegion = false;
        /// <summary>
        /// Tamaño de filete de borde
        /// </summary>
        private int _regionRadius = 10;
        /// <summary>
        /// color del borde
        /// </summary>
        private Color _borderStyleColor;
        /// <summary>
        /// ancho del borde
        /// </summary>
        private int _borderStyleSize;
        /// <summary>
        /// estilo de borde
        /// </summary>
        private ButtonBorderStyle _borderStyleType;
        /// <summary>
        /// Ya sea para mostrar modal
        /// </summary>
        private bool _isShowMaskDialog = false;
        /// <summary>
        /// Forma de máscara
        /// </summary>
        /// <value><c>true</c> si esta instancia es mostrar diálogo de máscara; de lo contrario, <c>false</c>.</value>
        [Description("Ya sea para mostrar el formulario de máscara")]
        public bool IsShowMaskDialog
        {
            get
            {
                return this._isShowMaskDialog;
            }
            set
            {
                this._isShowMaskDialog = value;
            }
        }
        /// <summary>
        /// ancho del borde
        /// </summary>
        /// <value>El tamaño del estilo del borde.</value>
        [Description("ancho del borde")]
        public int BorderStyleSize
        {
            get
            {
                return this._borderStyleSize;
            }
            set
            {
                this._borderStyleSize = value;
            }
        }
        /// <summary>
        /// color del borde
        /// </summary>
        /// <value>El color del estilo del borde.</value>
        [Description("color del borde")]
        public Color BorderStyleColor
        {
            get
            {
                return this._borderStyleColor;
            }
            set
            {
                this._borderStyleColor = value;
            }
        }
        /// <summary>
        /// estilo de borde
        /// </summary>
        /// <value>El tipo de estilo de borde.</value>
        [Description("estilo de borde")]
        public ButtonBorderStyle BorderStyleType
        {
            get
            {
                return this._borderStyleType;
            }
            set
            {
                this._borderStyleType = value;
            }
        }
        /// <summary>
        /// Borde de esquinas redondeadas
        /// </summary>
        /// <value>El radio de la región.</value>
        [Description("Borde de esquinas redondeadas")]
        public int RegionRadius
        {
            get
            {
                return this._regionRadius;
            }
            set
            {
                this._regionRadius = Math.Max(value, 1);
            }
        }
        /// <summary>
        /// Si se debe mostrar el contenido del dibujo de Personalizar
        /// </summary>
        /// <value><c>true</c> if esta instancia es mostrar región; de lo contrario, <c>false</c>.</value>
        [Description("Si se debe mostrar el contenido del dibujo de Personalizar")]
        public bool IsShowRegion
        {
            get
            {
                return this._isShowRegion;
            }
            set
            {
                this._isShowRegion = value;
            }
        }
        /// <summary>
        /// Ya sea para mostrar el borde redibujado
        /// </summary>
        /// <value><c>true</c> si vuelve a dibujar; de lo contrario, <c>false</c>.</value>
        [Description("Ya sea para mostrar el borde redibujado")]
        public bool Redraw
        {
            get
            {
                return this._redraw;
            }
            set
            {
                this._redraw = value;
            }
        }

        /// <summary>
        /// El es de tamaño completo
        /// </summary>
        private bool _isFullSize = true;
        /// <summary>
        /// Ya sea en pantalla completa
        /// </summary>
        /// <value><c>true</c> si esta instancia es de tamaño completo; de lo contrario, <c>false</c>.</value>
        [Description("Ya sea en pantalla completa")]
        public bool IsFullSize
        {
            get { return _isFullSize; }
            set { _isFullSize = value; }
        }
        /// <summary>
        /// Cierra automáticamente cuando se pierde el enfoque
        /// </summary>
        /// <value><c>true</c> si esta instancia se pierde el foco cerca; de lo contrario, <c>false</c>.</value>
        [Description("Cierra automáticamente cuando se pierde el enfoque")]
        public bool IsLoseFocusClose
        {
            get
            {
                return this._isLoseFocusClose;
            }
            set
            {
                this._isLoseFocusClose = value;
            }
        }
        #endregion

        /// <summary>
        /// Obtiene un valor que indica si esta instancia está en modo de diseño.
        /// </summary>
        /// <value><c>true</c> si esta instancia está en modo diseño; de lo contrario, <c>false</c>.</value>
        private bool IsDesingMode
        {
            get
            {
                bool ReturnFlag = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    ReturnFlag = true;
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                    ReturnFlag = true;
                return ReturnFlag;
            }
        }

        #region Constructor
        /// <summary>
        /// Constructor <see cref="FrmBase" />.
        /// </summary>
        public FrmBase()
        {
            InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            //base.HandleCreated += new EventHandler(this.FrmBase_HandleCreated);
            //base.HandleDestroyed += new EventHandler(this.FrmBase_HandleDestroyed);        
            this.KeyDown += FrmBase_KeyDown;
            this.FormClosing += FrmBase_FormClosing;
        }

        /// <summary>
        /// Maneja el evento FormClosing del control FrmBase.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="FormClosingEventArgs" /> instancia que contiene los datos del evento.</param>
        void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLoseFocusClose)
            {
                MouseHook.OnMouseActivity -= hook_OnMouseActivity;
            }
        }


        /// <summary>
        /// Maneja el evento Load del control FrmBase.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmBase_Load(object sender, EventArgs e)
        {
            if (!IsDesingMode)
            {
                if (_isFullSize)
                    SetFullSize();
            }
            if (_isLoseFocusClose)
            {
                MouseHook.OnMouseActivity += hook_OnMouseActivity;
            }
        }

        #endregion

        #region Métodos


        /// <summary>
        /// Maneja el evento OnMouseActivity del control de enlace.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="MouseEventArgs" /> instancia que contiene los datos del evento.</param>
        void hook_OnMouseActivity(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._isLoseFocusClose && e.Clicks > 0)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        if (!this.IsDisposed)
                        {
                            if (!this.ClientRectangle.Contains(this.PointToClient(e.Location)))
                            {
                                base.Close();
                            }
                        }
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// pantalla completa
        /// </summary>
        public void SetFullSize()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        /// <summary>
        /// Escape?.
        /// </summary>
        protected virtual void DoEsc()
        {
            base.Close();
        }

        /// <summary>
        /// Ingreso?.
        /// </summary>
        protected virtual void DoEnter()
        {
        }

        /// <summary>
        /// Establecer área de repintado
        /// </summary>
        public void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this._regionRadius);
            base.Region = new Region(path);
        }
        /// <summary>
        /// Obtener área de repintado
        /// </summary>
        /// <param name="rect">Area de repintado.</param>
        /// <param name="radius">redondeo.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// Mostrar un formulario como un cuadro de diálogo modal con un propietario específico.
        /// </summary>
        /// <param name="owner">Cualquier objeto que implemente <see cref="T:System.Windows.Forms.IWin32Window" /> (que indica una ventana de nivel superior que tendrá un cuadro de diálogo modal).</param>
        /// <returns><see cref="T:System.Windows.Forms.DialogResult" /> uno de los valores.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            try
            {
                if (this._isShowMaskDialog && owner != null)
                {
                    var frmOwner = (Control)owner;
                    FrmTransparent _frmTransparent = new FrmTransparent();
                    _frmTransparent.Width = frmOwner.Width;
                    _frmTransparent.Height = frmOwner.Height;
                    Point location = frmOwner.PointToScreen(new Point(0, 0));
                    _frmTransparent.Location = location;
                    _frmTransparent.frmchild = this;
                    _frmTransparent.IsShowMaskDialog = false;
                    return _frmTransparent.ShowDialog(owner);
                }
                else
                {
                    return base.ShowDialog(owner);
                }
            }
            catch (NullReferenceException)
            {
                return System.Windows.Forms.DialogResult.None;
            }
        }

        /// <summary>
        /// Mostrar el formulario como un cuadro de diálogo modal y establecer la ventana actualmente activa como su propietario.
        /// </summary>
        /// <returns><see cref="T:System.Windows.Forms.DialogResult" /> uno de los valores.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }
        #endregion

        #region 事件区


        /// <summary>
        /// Ocurre al apagar
        /// </summary>
        /// <param name="e">un que contiene datos del evento <see cref="T:System.EventArgs" />.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (base.Owner != null && base.Owner is FrmTransparent)
            {
                (base.Owner as FrmTransparent).Close();
            }
        }

        /// <summary>
        /// tecla de acceso directo
        /// </summary>
        /// <param name="msg"><see cref="T:System.Windows.Forms.Message" /> pasado por referencia, que representa el mensaje Win32 que se procesará.</param>
        /// <param name="keyData"><see cref="T:System.Windows.Forms.Keys" /> Uno de los valores que representa la clave del proceso.</param>
        /// <returns>Es true si el control maneja y utiliza pulsaciones de teclas; en caso contrario, es false para permitir un procesamiento posterior.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int num = 256;
            int num2 = 260;
            bool result;
            if (msg.Msg == num | msg.Msg == num2)
            {
                if (keyData == (Keys)262259)
                {
                    result = true;
                    return result;
                }
                if (keyData != Keys.Enter)
                {
                    if (keyData == Keys.Escape)
                    {
                        this.DoEsc();
                    }
                }
                else
                {
                    this.DoEnter();
                }
            }
            result = false;
            if (result)
                return result;
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Maneja el evento KeyDown del control FrmBase.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="KeyEventArgs" /> instancia que contiene los datos del evento.</param>
        protected void FrmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyDown != null && HotKeys != null)
            {
                bool blnCtrl = false;
                bool blnAlt = false;
                bool blnShift = false;
                if (e.Control)
                    blnCtrl = true;
                if (e.Alt)
                    blnAlt = true;
                if (e.Shift)
                    blnShift = true;
                if (HotKeys.ContainsKey(e.KeyValue))
                {
                    string strKey = string.Empty;
                    if (blnCtrl)
                    {
                        strKey += "Ctrl+";
                    }
                    if (blnAlt)
                    {
                        strKey += "Alt+";
                    }
                    if (blnShift)
                    {
                        strKey += "Shift+";
                    }
                    strKey += HotKeys[e.KeyValue];

                    if (HotKeyDown(strKey))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        /// <summary>
        /// volver a re pintar evento
        /// </summary>
        /// <param name="e">Contiene datos de eventos <see cref="T:System.Windows.Forms.PaintEventArgs" />.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this._isShowRegion)
            {
                this.SetWindowRegion();
            }
            base.OnPaint(e);
            if (this._redraw)
            {
                ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType);
            }
        }
        #endregion


        #region Arrastre
        /// <summary>
        /// Libera la captura.
        /// </summary>
        /// <returns><c>true</c> si XXXX, <c>false</c> en caso contrario.</returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="wMsg">The w MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// The wm syscommand
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary>
        /// The sc move
        /// </summary>
        public const int SC_MOVE = 0xF010;
        /// <summary>
        /// The htcaption
        /// </summary>
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// Controlar el arrastre del formulario a través de la API de Windows
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        public static void MouseDown(IntPtr hwnd)
        {
            ReleaseCapture();
            SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        /// <summary>
        /// Llame al constructor para configurar el movimiento del formulario
        /// </summary>
        /// <param name="cs">cs.</param>
        protected void InitFormMove(params Control[] cs)
        {
            foreach (Control c in cs)
            {
                if (c != null && !c.IsDisposed)
                    c.MouseDown += c_MouseDown;
            }
        }

        /// <summary>
        /// Maneja el evento MouseDown del control c.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="MouseEventArgs" /> instancia que contiene los datos del evento.</param>
        void c_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(this.Handle);
        }
    }
}
