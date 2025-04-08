/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 TableColors.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para de colores de estado

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase TableColors.
    /// </summary>
    public class TableColors
    {
        /// <summary>
        /// Color para green
        /// </summary>
        private static Color green = ColorTranslator.FromHtml("#c2e7b0");

        /// <summary>
        /// Obtiene el color para green.
        /// </summary>
        /// <value>Color green.</value>
        public static Color Green
        {
            get { return green; }
            internal set { green = value; }
        }
        /// <summary>
        /// Color blue
        /// </summary>
        private static Color blue = ColorTranslator.FromHtml("#a3d0fd");

        /// <summary>
        /// Obtiene el color para blue.
        /// </summary>
        /// <value>Color blue.</value>
        public static Color Blue
        {
            get { return blue; }
            internal set { blue = value; }
        }
        /// <summary>
        /// Color red
        /// </summary>
        private static Color red = ColorTranslator.FromHtml("#fbc4c4");

        /// <summary>
        /// Obtiene el color para red.
        /// </summary>
        /// <value>Color red.</value>
        public static Color Red
        {
            get { return red; }
            internal set { red = value; }
        }
        /// <summary>
        /// Color yellow
        /// </summary>
        private static Color yellow = ColorTranslator.FromHtml("#f5dab1");

        /// <summary>
        /// Obtiene el color para yellow.
        /// </summary>
        /// <value>Color yellow.</value>
        public static Color Yellow
        {
            get { return yellow; }
            internal set { yellow = value; }
        }
        /// <summary>
        /// color gray
        /// </summary>
        private static Color gray = ColorTranslator.FromHtml("#d3d4d6");

        /// <summary>
        /// Obtiene el color para gray.
        /// </summary>
        /// <value>Color gray.</value>
        public static Color Gray
        {
            get { return gray; }
            internal set { gray = value; }
        }
    }
}
