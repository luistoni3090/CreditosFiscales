/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eTIPO_CUENTA.cs
/// Creación: 	 2024.07.19
/// Ult Mod: 	 2024.07.19
/// Descripción:
/// Clase para estructura de la tabla TIPO_CUENTA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eTIPO_CUENTA
    {
        public string TIPO_CUENTA { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string CLASE { get; set; } = "";
        public decimal ORDEN_REP { get; set; } = 0;
        public string TIPO_CTA_ORFIS { get; set; } = "";
    }
}
