/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 NavigationMenuItemBase.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para Items del menu

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Controls.NavigationMenu
{
    public class NavigationMenuItemBase
    {
        /// <summary>
        /// Icono
        /// </summary>
        private Image icon;
        /// <summary>
        /// Obtiene o establece el icono.
        /// </summary>
        /// <value>El icono.</value>
        [Description("Icono, solo válido para nodos de nivel superior")]
        public Image Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// Texto
        /// </summary>
        private string text;
        /// <summary>
        /// Obtiene o establece el texto.
        /// </summary>
        /// <value>El texto.</value>
        [Description("texto")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Marca tooltip
        /// </summary>
        private bool showTip;
        /// <summary>
        /// Obtiene o establece un valor que indica si[mostrar sugerencia]. Cuando TipText está vacío, solo se muestra un pequeño punto; de lo contrario, se muestra el texto TipText.
        /// /// </summary>
        /// <value><c>true</c> si[mostrar marca tooltip]; de lo contrario, <c>false</c>.</value>
        [Description("Ya sea para mostrar la marca tooltip de la esquina, solo válido para nodos de nivel superior")]
        public bool ShowTip
        {
            get { return showTip; }
            set { showTip = value; }
        }

        /// <summary>
        /// Texto del tooltip
        /// </summary>
        private string tipText;
        /// <summary>
        /// Obtiene o establece el texto del tooltip.
        /// </summary>
        /// <value>The tip text.</value>
        [Description("Texto de subíndice, solo válido para nodos de nivel superior")]
        public string TipText
        {
            get { return tipText; }
            set { tipText = value; }
        }

        /// <summary>
        /// Ajuste a la derecha
        /// </summary>
        private bool anchorRight;

        /// <summary>
        /// Obtiene o establece un valor que indica si se ajusta a la derecha].
        /// </summary>
        /// <value><c>true</c> se ajusta a la derecha; si no, <c>false</c>.</value>
        [Description("Ya sea para alinearse a la derecha")]
        public bool AnchorRight
        {
            get { return anchorRight; }
            set { anchorRight = value; }
        }

        /// <summary>
        /// Ancho de item
        /// </summary>
        private int itemWidth = 100;

        /// <summary>
        /// Obtiene o establece el ancho del elemento..
        /// </summary>
        /// <value>El ancho del item.</value>
        [Description("ancho")]
        public int ItemWidth
        {
            get { return itemWidth; }
            set { itemWidth = value; }
        }

        /// <summary>
        /// Obtiene o establece el origen de datos.
        /// </summary>
        /// <value>El origen de datos.</value>
        [Description("fuente de datos")]
        public object DataSource { get; set; }
    }
}
