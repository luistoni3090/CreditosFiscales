/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 BorderColors.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para bordes de controles basada en colores primarios

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase BorderColors.
    /// </summary>
    public class BorderColors
    {
        /// <summary>
        /// Verde
        /// </summary>
        private static Color green = ColorTranslator.FromHtml("#f0f9ea");

        /// <summary>
        /// Obtiene el color para green.
        /// </summary>
        /// <value>Verde.</value>
        public static Color Green
        {
            get { return green; }
            internal set { green = value; }
        }
        /// <summary>
        /// Azul
        /// </summary>
        private static Color blue = ColorTranslator.FromHtml("#ecf5ff");

        /// <summary>
        /// Obtiene el color para blue.
        /// </summary>
        /// <value>Blue.</value>
        public static Color Blue
        {
            get { return blue; }
            internal set { blue = value; }
        }
        /// <summary>
        /// Rojo
        /// </summary>
        private static Color red = ColorTranslator.FromHtml("#fef0f0");

        /// <summary>
        /// Obtiene el color para red.
        /// </summary>
        /// <value>Rojo.</value>
        public static Color Red
        {
            get { return red; }
            internal set { red = value; }
        }
        /// <summary>
        /// Amarillo
        /// </summary>
        private static Color yellow = ColorTranslator.FromHtml("#fdf5e6");

        /// <summary>
        /// Obtiene el color para yellow.
        /// </summary>
        /// <value>Amarillo.</value>
        public static Color Yellow
        {
            get { return yellow; }
            internal set { yellow = value; }
        }
    }
}
