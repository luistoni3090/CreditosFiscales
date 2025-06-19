/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmTransparent.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma transparente para mensajes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Forms
{
    /// <summary>
    /// Forma transparente.
    /// Heredado de <see cref="Px_Controles.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="Px_Controles.Forms.FrmBase" />
    public partial class FrmTransparent : FrmBase
    {
        /// <summary>
        /// wm_activa
        /// </summary>
        private const int WM_ACTIVATE = 6;

        /// <summary>
        /// wm_activateapp
        /// </summary>
        private const int WM_ACTIVATEAPP = 28;

        /// <summary>
        /// wm_nc_activa
        /// </summary>
        private const int WM_NCACTIVATE = 134;

        /// <summary>
        /// wa_inactiva
        /// </summary>
        private const int WA_INACTIVE = 0;

        /// <summary>
        /// wm_mouseactivate
        /// </summary>
        private const int WM_MOUSEACTIVATE = 33;

        /// <summary>
        /// ma_noactivate
        /// </summary>
        private const int MA_NOACTIVATE = 3;
        /// <summary>
        /// Obtiene los parámetros de creación.
        /// </summary>
        /// <value>Parámetros de creación.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        /// <summary>
        /// Obtiene o establece la forma hija.
        /// </summary>
        /// <value>Forma hija.</value>
        public FrmBase frmchild
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor <see cref="FrmTransparent" />.
        /// </summary>
        public FrmTransparent()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            MethodInfo method = base.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
            method.Invoke(this, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new object[]
            {
                ControlStyles.Selectable,
                false
            }, Application.CurrentCulture);
        }

        /// <summary>
        /// Genera el evento <see cref="E:System.Windows.Forms.Form.Load" />.
        /// </summary>
        /// <param name="e">un que contiene datos del evento <see cref="T:System.EventArgs" />.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.ShowInTaskbar = false;
            base.ShowIcon = true;
        }
        /// <summary>
        /// Establece la ventana activa.
        /// </summary>
        /// <param name="handle">handle.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">para ser procesadoWindows <see cref="T:System.Windows.Forms.Message" />.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 33)
            {
                m.Result = new IntPtr(3);
            }
            else
            {
                if (m.Msg == 134)
                {
                    if (((int)m.WParam & 65535) != 0)
                    {
                        if (m.LParam != IntPtr.Zero)
                        {
                            FrmTransparent.SetActiveWindow(m.LParam);
                        }
                        else
                        {
                            FrmTransparent.SetActiveWindow(IntPtr.Zero);
                        }
                    }
                }
                else if (m.Msg == 2000)
                {
                }
                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// Maneja el evento Load del control FrmTransparent.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmTransparent_Load(object sender, EventArgs e)
        {
            if (frmchild != null)
            {
                frmchild.IsShowMaskDialog = false;
                var dia = frmchild.ShowDialog(this);
                this.DialogResult = dia;
            }
        }
    }
}
