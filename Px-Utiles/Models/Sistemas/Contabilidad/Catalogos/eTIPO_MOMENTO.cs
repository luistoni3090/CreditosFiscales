/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eTIPO_MOMENTO.cs
/// Creación: 	 2024.07.19
/// Ult Mod: 	 2024.07.19
/// Descripción:
/// Clase para estructura de la tabla TIPO_MOMENTO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eTIPO_MOMENTO
    {
        public decimal TIPO_MOMENTO { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string IDEN { get; set; } = "";
        public string APLICACION_AUTOMATICA { get; set; } = "";
    }
}
