using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.procesos
{



    public class ePOLIZA
    {
        public string POLIZA { get; set; } = "";
        public Int64 BANCO { get; set; } = 0;
        public Int64 CUENTA { get; set; } = 0;
        public decimal MOVTOS { get; set; } = 0;
        public DateTime FECHA { get; set; } = DateTime.Today;
        public string CONCEPTO { get; set; } = "";
        public decimal IMPORTE_GLOBAL { get; set; } = 0;
        public string STATUS { get; set; } = "";
        public string TIPO { get; set; } = "";
        public Int64 ORIGEN { get; set; } = 0;
        public decimal TRASPASO { get; set; } = 0;
        public decimal IMPORTE_ORIGINAL { get; set; } = 0;
        public string APP_LOGIN { get; set; } = "";
        public Int64 SYS_CLAVE { get; set; } = 0;
        public string ID_TRAMITE { get; set; } = "";
        public string ID_NOMINA { get; set; } = "";
        public string POL_PRESUP { get; set; } = "";
        public decimal IMPTE_ORIG_POLIZA { get; set; } = 0;
    }


}