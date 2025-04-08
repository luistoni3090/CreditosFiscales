/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmAnchor.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Ajuste de ventanas

using Px_Controles.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Forms
{
    /// <summary>
    /// Descripción de la función: formulario de anclaje
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    /// <seealso cref="System.Windows.Forms.IMessageFilter" />
    public partial class FrmAnchor : Form, IMessageFilter
    {

        /// <summary>
        /// Control padre m
        /// </summary>
        Control m_parentControl = null;
        /// <summary>
        /// Boton de abajo
        /// </summary>
        private bool blnDown = true;
        /// <summary>
        /// Tamaño
        /// </summary>
        Size m_size;
        /// <summary>
        /// Desviación de m
        /// </summary>
        Point? m_deviation;
        /// <summary>
        /// Si m no tiene el foco
        /// </summary>
        bool m_isNotFocus = true;


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentControl">Control padre</param>
        /// <param name="childControl">Control hijo</param>
        /// <param name="deviation">Compensación</param>
        /// <param name="isNotFocus">Si el formulario no tiene el foco</param>
        public FrmAnchor(Control parentControl, Control childControl, Point? deviation = null, bool isNotFocus = true)
        {
            m_isNotFocus = isNotFocus;
            m_parentControl = parentControl;
            InitializeComponent();
            this.Size = childControl.Size;
            this.HandleCreated += FrmDownBoard_HandleCreated;
            this.HandleDestroyed += FrmDownBoard_HandleDestroyed;
            this.Controls.Add(childControl);
            childControl.Dock = DockStyle.Fill;

            m_size = childControl.Size;
            m_deviation = deviation;

            if (parentControl.FindForm() != null)
            {
                Form frmP = parentControl.FindForm();
                if (!frmP.IsDisposed)
                {
                    frmP.LocationChanged += frmP_LocationChanged;
                }
            }
            parentControl.LocationChanged += frmP_LocationChanged;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FrmAnchor" /> class.
        /// </summary>
        /// <param name="parentControl">Control padre.</param>
        /// <param name="size">Tamaño.</param>
        /// <param name="deviation">Desviación.</param>
        /// <param name="isNotFocus"> El el control cuenta con el foco.</param>
        public FrmAnchor(Control parentControl, Size size, Point? deviation = null, bool isNotFocus = true)
        {
            m_isNotFocus = isNotFocus;
            m_parentControl = parentControl;
            InitializeComponent();
            this.Size = size;
            this.HandleCreated += FrmDownBoard_HandleCreated;
            this.HandleDestroyed += FrmDownBoard_HandleDestroyed;

            m_size = size;
            m_deviation = deviation;
        }

        /// <summary>
        /// Maneja el evento LocationChanged del control frmP.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        void frmP_LocationChanged(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        /// <summary>
        /// Maneja el evento HandleDestroyed del control FrmDownBoard.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmDownBoard_HandleDestroyed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        /// <summary>
        /// Maneja el evento HandleCreated del control FrmDownBoard.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmDownBoard_HandleCreated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        #region forma desenfocada

        /// <summary>
        /// Establece la ventana activa.
        /// </summary>
        /// <param name="handle">El handle.</param>
        /// <returns>IntPtr.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);
        /// <summary>
        /// wm activa
        /// </summary>
        private const int WM_ACTIVATE = 0x006;
        /// <summary>
        /// wm activa
        /// </summary>
        private const int WM_ACTIVATEAPP = 0x01C;
        /// <summary>
        /// wm activa
        /// </summary>
        private const int WM_NCACTIVATE = 0x086;
        /// <summary>
        /// wa inactiva
        /// </summary>
        private const int WA_INACTIVE = 0;
        /// <summary>
        /// wm mouse activado
        /// </summary>
        private const int WM_MOUSEACTIVATE = 0x21;
        /// <summary>
        /// ma no activa
        /// </summary>
        private const int MA_NOACTIVATE = 3;
        /// <summary>
        /// WNDs el proc.
        /// </summary>
        /// <param name="m">para ser procesado Windows<see cref="T:System.Windows.Forms.Message" />.</param>
        protected override void WndProc(ref Message m)
        {
            if (m_isNotFocus)
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
            }
            base.WndProc(ref m);
        }

        #endregion

        /// <summary>
        /// Filtre los mensajes antes de enviarlos.
        /// </summary>
        /// <param name="m">Mensaje a programar.Este mensaje no se puede modificar.</param>
        /// <returns>Es verdadero si los mensajes se filtran y se impide que se programen.Es verdadero si se permite que los mensajes continúen con el siguiente filtro o control;false.</returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != 0x0201 || this.Visible == false)
                return false;
            var pt = this.PointToClient(MousePosition);
            this.Visible = this.ClientRectangle.Contains(pt);
            return false;
        }

        /// <summary>
        /// Maneja el evento Load del control FrmAnchor.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmAnchor_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Maneja el evento VisibleChanged del control FrmAnchor.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            timer1.Enabled = this.Visible;
            if (this.Visible)
            {
                Screen currentScreen = Screen.FromControl(m_parentControl);
                Point p = m_parentControl.Parent.PointToScreen(m_parentControl.Location);
                int intX = 0;
                int intY = 0;
                if (p.Y + m_parentControl.Height + m_size.Height > currentScreen.Bounds.Height)
                {
                    intY = p.Y - m_size.Height - 1;
                    blnDown = false;
                }
                else
                {
                    intY = p.Y + m_parentControl.Height + 1;
                    blnDown = true;
                }


                if (p.X + m_size.Width > currentScreen.Bounds.Width)
                {
                    intX = currentScreen.Bounds.Width - m_size.Width;

                }
                else
                {
                    intX = p.X;
                }
                if (m_deviation.HasValue)
                {
                    intX += m_deviation.Value.X;
                    intY += m_deviation.Value.Y;
                }
                this.Location = ControlHelper.GetScreenLocation(currentScreen, intX, intY);
            }
        }

        /// <summary>
        /// Maneja el evento Tick del control timer1.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                Form frm = this.Owner as Form;
                IntPtr _ptr = ControlHelper.GetForegroundWindow();
                if (_ptr != frm.Handle && _ptr != this.Handle)
                {
                    this.Hide();
                }
            }
        }
    }
}
