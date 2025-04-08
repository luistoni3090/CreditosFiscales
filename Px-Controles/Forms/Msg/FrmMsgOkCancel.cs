/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmMsgOkCancel.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Mensajes de ok cancel

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
    /// Forma para mensajes FrmMsgOkCancel.
    /// Heredado de <see cref="Px_Controles.Forms.Msg.FrmMsgBase" />
    /// </summary>
    /// <seealso cref="Px_Controles.Forms.Msg.FrmMsgBase" />
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmMsgOkCancel : FrmMsgBase
    {
        /// <summary>
        /// Constructor <see cref="FrmMsgOkCancel" /> .
        /// </summary>
        public FrmMsgOkCancel()
        {
            InitializeComponent();

            btnOk.Click += btnOK_BtnClick;
            btnCancel.Click += btnCancel_BtnClick;
        }

        /// <summary>
        /// Cerrar el dialogo de ventana.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            DoEnter();
        }

        /// <summary>
        /// Evento cancela para salir del dialogo.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DoEsc();
        }

        /// <summary>
        /// Respuesta aceptada o afirmativa.
        /// </summary>
        protected override void DoEnter()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento VisibleChanged del control FrmMsgOkCancel.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmMsgOkCancel_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
