/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 TextColors.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para de colores

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Colors
{
    /// <summary>
    /// Clase TextColor.
    /// </summary>
    public class TextColors
    {
        /// <summary>
        /// Color more light
        /// </summary>
        private static Color _MoreLight = ColorTranslator.FromHtml("#c0c4cc");

        /// <summary>
        /// Obtiene el color para more light.
        /// </summary>
        /// <value>Color more light.</value>
        public static Color MoreLight
        {
            get { return _MoreLight; }
            internal set { _MoreLight = value; }
        }
        /// <summary>
        /// Color light
        /// </summary>
        private static Color _Light = ColorTranslator.FromHtml("#909399");

        /// <summary>
        /// Obtiene el color para light.
        /// </summary>
        /// <value>Color light.</value>
        public static Color Light
        {
            get { return _Light; }
            internal set { _Light = value; }
        }
        /// <summary>
        /// Color dark
        /// </summary>
        private static Color _Dark = ColorTranslator.FromHtml("#606266");

        /// <summary>
        /// Obtiene el color para dark.
        /// </summary>
        /// <value>Color dark.</value>
        public static Color Dark
        {
            get { return _Dark; }
            internal set { _Dark = value; }
        }
        /// <summary>
        /// Obtiene el color para more dark
        /// </summary>
        private static Color _MoreDark = ColorTranslator.FromHtml("#303133");

        /// <summary>
        /// Obtiene el color para more dark.
        /// </summary>
        /// <value>Color more dark.</value>
        public static Color MoreDark
        {
            get { return _MoreDark; }
            internal set { _MoreDark = value; }
        }
    }
}
