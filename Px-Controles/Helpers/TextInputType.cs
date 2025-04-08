/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 TextInputType.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Tipos de entrada de datos

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Helpers
{
    /// <summary>
    /// Descripción de la función: tipo de entrada de control de texto
    /// </summary>
    public enum TextInputType
    {
        /// <summary>
        /// Sin control sobre la entrada
        /// </summary>
        [Description("Sin control sobre la entrada")]
        NotControl = 1,
        /// <summary>
        /// Cualquier número
        /// </summary>
        [Description("Cualquier número")]
        Number = 2,
        /// <summary>
        /// Número no negativo
        /// </summary>
        [Description("Número no negativo")]
        UnsignNumber = 4,
        /// <summary>
        /// Número positivo
        /// </summary>
        [Description("Número positivo")]
        PositiveNumber = 8,
        /// <summary>
        /// Entero
        /// </summary>
        [Description("Entero")]
        Integer = 16,
        /// <summary>
        /// Entero positivo
        /// </summary>
        [Description("Entero positivo")]
        PositiveInteger = 32,
        /// <summary>
        /// Verificación periódica
        /// </summary>
        [Description("Verificación periódica")]
        Regex = 64
    }
}
