/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 NavigationMenuItem.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para Items del menu

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Controls.NavigationMenu
{
    /// <summary>
    /// Clase NavigationMenuItem.
    /// </summary>
    public class NavigationMenuItem : NavigationMenuItemBase
    {
        /// <summary>
        /// Items
        /// </summary>
        private NavigationMenuItem[] items;
        /// <summary>
        /// Obtiene el color para los items.
        /// </summary>
        /// <value>Items.</value>
        [Description("lista de subelementos")]
        public NavigationMenuItem[] Items
        {
            get { return items; }
            set
            {
                items = value;
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        item.ParentItem = this;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si esta instancia tiene pelusa dividida en la parte superior.
        /// </summary>
        /// <value><c>true</c> si esta instancia tiene pelusa partida en la parte superior; de lo contrario, <c>false</c>.</value>
        [Description("Si se debe mostrar una línea divisoria en la parte superior de este elemento")]
        public bool HasSplitLintAtTop { get; set; }

        /// <summary>
        /// Obtiene el elemento principal.
        /// </summary>
        /// <value>Item padre.</value>
        [Description("nodo padre")]
        public NavigationMenuItem ParentItem { get; private set; }
    }


}
