/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eMONEDA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla MONEDA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos
{
    public class ePARAMETROSISTEMA
    {
        public string GOBIERNO { get; set; } = "";
        public string SECRETARIA { get; set; } = "";
        public decimal TOLERANCIA { get; set; } = 0;
        public decimal TOLERANCIADLLS { get; set; } = 0;
        public decimal VIGENCIAAUX { get; set; } = 0;
        public decimal VIGENCIABCO { get; set; } = 0;
        public string FECHACARGA { get; set; } = "";
        public decimal ANIOS_CONCILIAR { get; set; } = 0;
        public string VALIDA { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }
}
