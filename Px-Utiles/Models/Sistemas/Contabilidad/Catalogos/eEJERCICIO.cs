/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eEJERCICIO.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para estructura de la tabla EJERCICIO
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eEJERCICIO
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal EJERCICIO { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public DateTime FECHA_INI { get; set; } = DateTime.Today;
        public DateTime FECHA_TER { get; set; } = DateTime.Today;
        public string STATUS { get; set; } = "";
        public string CPTO_POL_CIERRE { get; set; } = "";
        public string TIPO_PERIODO { get; set; } = "";
        public string ACTIVO { get; set; } = "";
        public decimal RESULT_EJERCICIO { get; set; } = 0;
        public string POLIZA_CIERRE { get; set; } = "";
        public string HISTORICO { get; set; } = "";
        public string CD { get; set; } = "";
        public string RELLENO { get; set; } = "";
        public string SEPARA_CUENTA { get; set; } = "";
        public decimal MAX_LONG_NIVEL { get; set; } = 0;
        public decimal NO_POLIZA_CIERRE { get; set; } = 0;
    }
}
