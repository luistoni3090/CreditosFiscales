/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 StatusColors.cs
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
    /// Color de estado
    /// </summary>
    public class StatusColors
    {
        /// <summary>
        /// Color primary
        /// </summary>
        private static Color _Primary = ColorTranslator.FromHtml("#409eff");

        /// <summary>
        /// Obtiene el color para primary.
        /// </summary>
        /// <value>Color primary.</value>
        public static Color Primary
        {
            get { return _Primary; }
            internal set { _Primary = value; }
        }
        /// <summary>
        /// Color para success
        /// </summary>
        private static Color _Success = ColorTranslator.FromHtml("#67c23a");

        /// <summary>
        /// Obtiene el color para success.
        /// </summary>
        /// <value>Color para success.</value>
        public static Color Success
        {
            get { return _Success; }
            internal set { _Success = value; }
        }
        /// <summary>
        /// Color para warning
        /// </summary>
        private static Color _Warning = ColorTranslator.FromHtml("#e6a23c");

        /// <summary>
        /// Obtiene el color para warning.
        /// </summary>
        /// <value>Color para warning.</value>
        public static Color Warning
        {
            get { return _Warning; }
            internal set { _Warning = value; }
        }
        /// <summary>
        /// Color para danger
        /// </summary>
        private static Color _Danger = ColorTranslator.FromHtml("#f56c6c");

        /// <summary>
        /// Obtiene el color para danger.
        /// </summary>
        /// <value>Color para danger.</value>
        public static Color Danger
        {
            get { return _Danger; }
            internal set { _Danger = value; }
        }
        /// <summary>
        /// Color para information
        /// </summary>
        private static Color _Info = ColorTranslator.FromHtml("#909399");

        /// <summary>
        /// Obtiene el color para information.
        /// </summary>
        /// <value>Color para information.</value>
        public static Color Info
        {
            get { return _Info; }
            internal set { _Info = value; }
        }
    }
}
