/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 FrmWaiting.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Forma de espera

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Forms
{
    /// <summary>
    /// FrmWaiting.
    /// Heredada de <see cref="Px_Controles.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="Px_Controles.Forms.FrmBase" />
    public partial class FrmWaiting : FrmBase
    {
        /// <summary>
        /// Mensaje.
        /// </summary>
        /// <value>The MSG.</value>
        public string Msg { get { return label2.Text; } set { label2.Text = value; } }
        /// <summary>
        /// Constructor <see cref="FrmWaiting" />.
        /// </summary>
        public FrmWaiting()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Tick del control timer1.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.label1.ImageIndex == this.imageList1.Images.Count - 1)
                this.label1.ImageIndex = 0;
            else
                this.label1.ImageIndex++;

        }

        /// <summary>
        /// Maneja el evento VisibleChanged del control FrmWaiting.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">}<see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void FrmWaiting_VisibleChanged(object sender, EventArgs e)
        {
            //this.timer1.Enabled = this.Visible;
        }

        /// <summary>
        /// se escapa?.
        /// </summary>
        protected override void DoEsc()
        {

        }

        /// <summary>
        /// Maneja el evento Tick del control timer2.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e"><see cref="EventArgs" /> instancia que contiene los datos del evento.</param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            base.Opacity = 1.0;
            this.timer2.Enabled = false;
        }

        /// <summary>
        /// Muestra la forma.
        /// </summary>
        /// <param name="intSleep">Tiempo de inactividad.</param>
        public void ShowForm(int intSleep = 1)
        {
            base.Opacity = 0.0;
            if (intSleep <= 0)
            {
                intSleep = 1;
            }
            base.Show();
            this.timer2.Interval = intSleep;
            this.timer2.Enabled = true;
        }
    }
}
