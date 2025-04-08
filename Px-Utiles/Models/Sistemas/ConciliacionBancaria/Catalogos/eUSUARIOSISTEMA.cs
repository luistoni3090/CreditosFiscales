/// 	 Wero MX
/// Autor: 	 Luis Molina
/// Nombre: 	 eUSUARIOSISTEMA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla usuarios del sistema

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos
{
    public class eUSUARIOSISTEMA
    {

        public string APP_LOGIN { get; set; } = "";
        public string AP_PATER { get; set; } = "";
        public string AP_MATER { get; set; } = "";
        public string NOMBRE { get; set; } = "";
        public string SYSUSER { get; set; } = "";
        public string APP_PWD { get; set; } = "";
        public string MUNICIPIO { get; set; } = "";
        public string MPO { get; set; } = "";
        public string RFC { get; set; } = "";
        public string SISTEMA { get; set; } = "";
        public decimal REC_RTAS { get; set; } = 0;
        public string STATUS { get; set; } = "";
        public string Nuevo_Editar{ get; set; } = "";

        public string ALLCOLUMNS { get; set; } = "";


    }
}
