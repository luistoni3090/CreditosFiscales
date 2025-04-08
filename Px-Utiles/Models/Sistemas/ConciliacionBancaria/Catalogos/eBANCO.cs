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
    public class eBANCO
    {

        public decimal BANCO { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string DIRECC { get; set; } = "";
        public decimal TEL { get; set; } = 0;
        public string EJECUTIVO { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }
}
