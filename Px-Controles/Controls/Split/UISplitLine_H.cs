/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 UISplitLine_H.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Control de línea horizontal

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Controls.Split
{
    /// <summary>
    /// Class UISplitLine_H.
    /// Implementa de <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class UISplitLine_H : UserControl
    {
        /// <summary>
        /// Constructor <see cref="UISplitLine_H" />.
        /// </summary>
        public UISplitLine_H()
        {
            InitializeComponent();
            this.TabStop = false;
        }
    }
}
