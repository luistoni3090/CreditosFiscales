/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 NavigationMenuItemExt.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para Items del menu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Controls.NavigationMenu
{
    /// <summary>
    /// Clase NavigationMenuItemExt
    /// </summary>
    public class NavigationMenuItemExt : NavigationMenuItemBase
    {
        /// <summary>
        /// Indica si el control se muestra.
        /// </summary>
        public System.Windows.Forms.Control ShowControl { get; set; }
    }
}
