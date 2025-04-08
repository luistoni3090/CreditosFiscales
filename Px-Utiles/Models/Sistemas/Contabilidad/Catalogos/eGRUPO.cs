/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eGRUPO.cs
/// Creación: 	 2024.07.19
/// Ult Mod: 	 2024.07.19
/// Descripción:
/// Clase para estructura de la tabla GRUPO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eGRUPO
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal GRUPO { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string TIPO_CUENTA { get; set; } = "";
        public decimal ORDEN_REP { get; set; } = 0;
    }
}
