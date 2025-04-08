/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 GradientColors.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para de colores degradados

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase GradientColors.
    /// </summary>
    public class GradientColors
    {
        /// <summary>
        /// Color orange
        /// </summary>
        private static Color[] orange = new Color[] { Color.FromArgb(252, 196, 136), Color.FromArgb(243, 138, 159) };

        /// <summary>
        /// Obtiene el color para orange.
        /// </summary>
        /// <value>TColor orange.</value>
        public static Color[] Orange
        {
            get { return GradientColors.orange; }
            internal set { GradientColors.orange = value; }
        }
        /// <summary>
        /// Color light green
        /// </summary>
        private static Color[] lightGreen = new Color[] { Color.FromArgb(210, 251, 123), Color.FromArgb(152, 231, 160) };

        /// <summary>
        /// Obtiene el color para light green.
        /// </summary>
        /// <value>Color light green.</value>
        public static Color[] LightGreen
        {
            get { return GradientColors.lightGreen; }
            internal set { GradientColors.lightGreen = value; }
        }
        /// <summary>
        /// Color green
        /// </summary>
        private static Color[] green = new Color[] { Color.FromArgb(138, 241, 124), Color.FromArgb(32, 190, 179) };

        /// <summary>
        /// Obtiene el color para green.
        /// </summary>
        /// <value>Color green.</value>
        public static Color[] Green
        {
            get { return GradientColors.green; }
            internal set { GradientColors.green = value; }
        }
        /// <summary>
        /// Color blue
        /// </summary>
        private static Color[] blue = new Color[] { Color.FromArgb(193, 232, 251), Color.FromArgb(162, 197, 253) };

        /// <summary>
        /// Obtiene el color para blue.
        /// </summary>
        /// <value>Color blue.</value>
        public static Color[] Blue
        {
            get { return GradientColors.blue; }
            internal set { GradientColors.blue = value; }
        }
        /// <summary>
        /// Color blue green
        /// </summary>
        private static Color[] blueGreen = new Color[] { Color.FromArgb(122, 251, 218), Color.FromArgb(16, 193, 252) };

        /// <summary>
        /// Obtiene el color para blue green.
        /// </summary>
        /// <value>Color blue green.</value>
        public static Color[] BlueGreen
        {
            get { return GradientColors.blueGreen; }
            internal set { GradientColors.blueGreen = value; }
        }
        /// <summary>
        /// Color light violet
        /// </summary>
        private static Color[] lightViolet = new Color[] { Color.FromArgb(248, 192, 234), Color.FromArgb(164, 142, 210) };

        /// <summary>
        /// Obtiene el color para light violet.
        /// </summary>
        /// <value>Color light violet.</value>
        public static Color[] LightViolet
        {
            get { return GradientColors.lightViolet; }
            internal set { GradientColors.lightViolet = value; }
        }
        /// <summary>
        /// Color violet
        /// </summary>
        private static Color[] violet = new Color[] { Color.FromArgb(185, 154, 241), Color.FromArgb(137, 124, 242) };

        /// <summary>
        /// Obtiene el color para violet.
        /// </summary>
        /// <value>Color violet.</value>
        public static Color[] Violet
        {
            get { return GradientColors.violet; }
            internal set { GradientColors.violet = value; }
        }
        /// <summary>
        /// Color gray
        /// </summary>
        private static Color[] gray = new Color[] { Color.FromArgb(233, 238, 239), Color.FromArgb(147, 162, 175) };

        /// <summary>
        /// Obtiene el color para gray.
        /// </summary>
        /// <value>Color gray.</value>
        public static Color[] Gray
        {
            get { return GradientColors.gray; }
            internal set { GradientColors.gray = value; }
        }
    }
}
