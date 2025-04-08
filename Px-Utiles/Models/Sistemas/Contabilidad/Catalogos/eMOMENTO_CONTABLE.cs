/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eMOMENTO_CONTABLE.cs
/// Creación: 	 2024.07.19
/// Ult Mod: 	 2024.07.19
/// Descripción:
/// Clase para estructura de la tabla MOMENTO_CONTABLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eMOMENTO_CONTABLE
    {
        public decimal TIPO_MOMENTO { get; set; } = 0;
        public decimal MOMENTO { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string TIPO_POLIZA { get; set; } = "";
        public decimal ORDEN { get; set; } = 0;
        public string GENERA_IDTRAMITE { get; set; } = "";
        public string NATURALEZA_MOMENTO { get; set; } = "";
        public string OBLIGATORIO_DIARIO { get; set; } = "";
    }
}
