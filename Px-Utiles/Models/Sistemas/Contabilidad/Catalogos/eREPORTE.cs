/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eREPORTE.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla REPORTE


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eREPORTE
    {
        public decimal REPORTE { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string ENCABE1 { get; set; } = "";
        public string ENCABE2 { get; set; } = "";
        public string ENCABE3 { get; set; } = "";
    }
}
