/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eLlaveValor.cs
/// Creación: 	 2024.07.19
/// Ult Mod: 	 2024.07.19
/// Descripción:
/// Para estructuras de llave valor, llave entero o llave cadena

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.General
{
    public class eLlaveValor
    {
        public int Llave { get; set; } = 0;
        public string Valor { get; set; } = "";
    }
    public class eLlaveValorString
    {
        public string Llave { get; set; } = "";
        public string OBLIGATORIO_DIARIO { get; set; } = "";
        public string Valor { get; set; } = "";
    }
}
