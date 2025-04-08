/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eTIPO_POLIZA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla TIPO_POLIZA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eTIPO_POLIZA
    {
        public string TIPO_POLIZA { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string INSERCION { get; set; } = "";
        public string CTA_ORFIS { get; set; } = "";
    }
}
