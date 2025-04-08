/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 BasisColors.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para colores de controles

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase BasisColors.
    /// </summary>
    public class BasisColors
    {
        /// <summary>
        /// Propiedad light
        /// </summary>
        private static Color light = ColorTranslator.FromHtml("#f5f7fa");

        /// <summary>
        /// Obtiene el color para light.
        /// </summary>
        /// <value>Light.</value>
        public static Color Light
        {
            get { return light; }
            internal set { light = value; }
        }
        /// <summary>
        /// Propiedad medium.
        /// </summary>
        private static Color medium = ColorTranslator.FromHtml("#f0f2f5");

        /// <summary>
        /// Obtiene el color para medium.
        /// </summary>
        /// <value>Medium.</value>
        public static Color Medium
        {
            get { return medium; }
            internal set { medium = value; }
        }
        /// <summary>
        /// Propiedad dark
        /// </summary>
        private static Color dark = ColorTranslator.FromHtml("#000000");

        /// <summary>
        /// Obtiene el color para dark.
        /// </summary>
        /// <value>Dark.</value>
        public static Color Dark
        {
            get { return dark; }
            internal set { dark = value; }
        }

    }
}
