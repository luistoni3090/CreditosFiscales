/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eFIRMA_REPORTE.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla FIRMA_REPORTE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eFIRMA_REPORTE
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal REPORTE { get; set; } = 0;
        public decimal FIRMA { get; set; } = 0;
        public decimal ORDEN_REP { get; set; } = 0;

        public string NOMBRE { get; set; } = "";


    }
}
