/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eFIRMA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla FIRMA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eFIRMA
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal FIRMA { get; set; } = 0;
        public string NOMBRE { get; set; } = "";
        public string PUESTO { get; set; } = "";
        public string CURP { get; set; } = "";
        public string RFC { get; set; } = "";
    }
}
