/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eNIVEL.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla NIVEL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eNIVEL
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal NIVEL { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public decimal POSICIONES { get; set; } = 0;
        public decimal EJERCICIO { get; set; } = 0;
        public string RELLENO { get; set; } = "";
    }
}
