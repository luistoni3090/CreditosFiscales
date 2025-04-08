/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 MessageBoxMX.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// MessageBox personalizado

using Px_Controles.Colors;
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

namespace Px_Controles.Forms.Msg
{
    /// <summary>
    /// Class MessageBoxMX.
    /// Implements the <see cref="Px_Controles.Forms.Msg.FrmBase" />
    /// </summary>
    /// <seealso cref="Px_Controles.Forms.Msg.FrmBase" />
    public partial class MessageBoxMX : FrmBase
    {
        /// <summary>
        /// Bandera para cerrar mensaje
        /// </summary>
        bool blnEnterClose = true;
        /// <summary>
        /// Constructor <see cref="MessageBoxMX" />.
        /// </summary>
        /// <param name="strMessage">Mensaje.</param>
        /// <param name="strTitle">Titulo del mensaje.</param>
        /// <param name="blnShowCancel">Muestra botón de cancelar.</param>
        /// <param name="blnShowClose">Muestra el botón de cerrar.</param>
        /// <param name="blnisEnterClose">Bandera para cerrar mensaje.</param>
        private MessageBoxMX(
            string strMessage,
            string strTitle,
            Int16 iTipo,
            bool blnShowCancel = false,
            bool blnShowClose = false,
            bool blnisEnterClose = true)
        {
            InitializeComponent();
            InitFormMove(this.lblTitle);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            if (!string.IsNullOrWhiteSpace(strTitle))
                lblTitle.Text = strTitle;
            lblMsg.Text = strMessage;

            btnOk.Click += btnOK_BtnClick;
            btnCancel.Click += btnCancel_BtnClick;

            picTipo.BackColor = Color.Transparent;
            imageList1.TransparentColor = Color.Transparent;
            picTipo.Image = imageList1.Images[iTipo];
            switch (iTipo)
            {
                case (int)StatusColorsTypes.Default:
                case (int)StatusColorsTypes.Primary:
                    uiSplitLine_H1.BackColor = Color.White;
                    break;
                case (int)StatusColorsTypes.Success:
                    uiSplitLine_H1.BackColor = Color.FromArgb(64, 196, 100);
                    break;
                case (int)StatusColorsTypes.Warning:
                    uiSplitLine_H1.BackColor = Color.FromArgb(189, 43, 71);
                    break;
                case (int)StatusColorsTypes.Danger:
                    uiSplitLine_H1.BackColor = Color.FromArgb(226, 212, 22);
                    break;
                case (int)StatusColorsTypes.Info:
                    uiSplitLine_H1.BackColor = Color.FromArgb(0, 146, 255);
                    break;
                case (int)StatusColorsTypes.Question:
                    uiSplitLine_H1.BackColor = Color.FromArgb(28, 88, 207);
                    break;
            }

            btnCancel.Visible = blnShowCancel;
            uiSplitLine_V1.Visible = blnShowCancel;
            btnClose.Visible = blnShowClose;
            blnEnterClose = blnisEnterClose;



            //if (blnShowCancel)
            //{
            //    btnOK.BtnForeColor = Color.FromArgb(255, 85, 51);
            //}
        }

        #region Mostrar un cuadro de mensaje modal
        /// <summary>
        /// Mostrar un cuadro de mensaje modal
        /// </summary>
        /// <param name="owner">owner</param>
        /// <param name="strMessage">strMessage</param>
        /// <param name="strTitle">strTitle</param>
        /// <param name="blnShowCancel">blnShowCancel</param>
        /// <param name="isShowMaskDialog">isShowMaskDialog</param>
        /// <param name="blnShowClose">blnShowClose</param>
        /// <param name="blnIsEnterClose">Para cerrar el mensaje.</param>
        /// <param name="deviationSize">Compensación de tamaño, cuando el tamaño predeterminado es demasiado grande o demasiado pequeño, puede ajustarlo (incrementar)</param>
        /// <returns>valor de retorno</returns>
        public static DialogResult ShowDialog(
            IWin32Window owner,
            string strMessage,
            string strTitle = "Titulo",
            Int16 iTipo = 0,
            bool blnShowCancel = false,
            bool isShowMaskDialog = true,
            bool blnShowClose = false,
            bool blnIsEnterClose = true,
            Size? deviationSize = null
            )
        {

            DialogResult result = DialogResult.Cancel;
            if (owner == null || (owner is Control && (owner as Control).IsDisposed))
            {
                var frm = new MessageBoxMX(strMessage, strTitle, iTipo, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is Control)
                {
                    owner = (owner as Control).FindForm();
                }
                var frm = new MessageBoxMX(strMessage, strTitle, iTipo, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog(owner);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Maneja el evento BtnClick del control btnOK.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instance containing the event data.</param>
        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Maneja el evento BtnClick del control btnCancel.
        /// </summary>
        /// <param name="sender">Mostrar un cuadro de mensaje modal.</param>
        /// <param name="e"><see cref="EventArgs" /> instance containing the event data.</param>
        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the MouseDown event of the btnClose control.
        /// </summary>
        /// <param name="sender">Mostrar un cuadro de mensaje modal.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Si responde afirmatuvo al dialogo.
        /// </summary>
        protected override void DoEnter()
        {
            if (blnEnterClose)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Maneja el evento VisibleChanged del control MessageBoxMX.
        /// </summary>
        /// <param name="sender">Mostrar un cuadro de mensaje modal.</param>
        /// <param name="e"><see cref="EventArgs" /> instance containing the event data.</param>
        private void MessageBoxMX_VisibleChanged(object sender, EventArgs e)
        {

        }

        //private void picTipo_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;

        //    var controls = this.Controls.OfType<PictureBox>();
        //    foreach (var c in controls)
        //    {
        //        g.DrawImage(c.Image, c.Location.X, c.Location.Y, c.Width, c.Height);
        //    }
        //}
    }
}
