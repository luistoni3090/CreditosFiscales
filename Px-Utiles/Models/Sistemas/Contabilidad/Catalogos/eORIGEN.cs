/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eORIGEN.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla ORIGEN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eORIGEN
    {
        public decimal ORIGEN { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public decimal SYS_CLAVE { get; set; } = 0;
        public string PERMITIR_DESAPLICAR { get; set; } = "";
        public string APLICACION_INMEDIATA { get; set; } = "";
        public string ELIMINA_BUZON { get; set; } = "";
    }
}
