/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmMsgBase.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma base otras formas de mensajes


using System;
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

namespace Px_Controles.Forms.Msg
{
    /// <summary>
    /// FrmMsgBase.
    /// Implements the <see cref="Px_Controles.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="Px_Controles.Forms.FrmBase" />
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmMsgBase : FrmBase
    {
        /// <summary>
        /// Obtiene o establece Título de la ventana.
        /// </summary>
        /// <value>Título del mensaje.</value>
        [Description("Título del formulario de mensaje"), Category("personalizar")]
        public string Title
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }
        /// <summary>
        /// Muestra el botón de cerrar
        /// </summary>
        private bool _isShowCloseBtn = false;
        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia se muestra cerca de BTN.
        /// </summary>
        /// <value><c>true</c> si esta instancia se muestra cerca de BTN; de lo contrario, <c>false</c>.</value>
        [Description("Si mostrar el botón de cerrar en la esquina superior derecha"), Category("personalizar")]
        public bool IsShowCloseBtn
        {
            get
            {
                return _isShowCloseBtn;
            }
            set
            {
                _isShowCloseBtn = value;
                btnClose.Visible = value;
                if (value)
                {
                    btnClose.Location = new Point(this.Width - btnClose.Width - 10, 0);
                    btnClose.BringToFront();
                }
            }
        }
        /// <summary>
        /// Muestra la imagen del tipo de mensaje default, success, error, info, warning
        /// </summary>
        private Int16 _TipoMsg = 0;
        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia se muestra cerca de BTN.
        /// </summary>
        /// <value><c>true</c> si esta instancia se muestra cerca de BTN; de lo contrario, <c>false</c>.</value>
        [Description("Si mostrar el botón de cerrar en la esquina superior derecha"), Category("personalizar")]
        public Int16 TipoMsg
        {
            get
            {
                return _TipoMsg;
            }
            set
            {
                _TipoMsg = value;

                picTipo.Image = imageList1.Images[value];

            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmMsgBase()
        {
            InitializeComponent();
            InitFormMove(this.lblTitle);
        }


        /// <summary>
        /// Maneja el evento Mostrado del control FrmMsgBase.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmMsgBase_Shown(object sender, EventArgs e)
        {
            if (IsShowCloseBtn)
            {
                btnClose.Location = new Point(this.Width - btnClose.Width - 10, 0);
                btnClose.BringToFront();
            }
        }


        /// <summary>
        /// Maneja el evento VisibleChanged del control FrmMsgBase.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmMsgBase_VisibleChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Evento click de cerrar ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento ajustar tamaño
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMsgBase_SizeChanged(object sender, EventArgs e)
        {
            btnClose.Location = new Point(this.Width - btnClose.Width, btnClose.Location.Y);
        }
    }
}
