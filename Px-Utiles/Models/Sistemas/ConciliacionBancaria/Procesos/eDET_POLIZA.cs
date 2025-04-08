using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.procesos
{


    public class eDET_POLIZA

    {
        public decimal MOVAUX { get; set; } = 0;
        public string POLIZA { get; set; } = "";
        public Int64 BANCO { get; set; } = 0;
        public Int64 CUENTA { get; set; } = 0;
        public string NIVEL1 { get; set; } = "";
        public string NIVEL2 { get; set; } = "";
        public string NIVEL3 { get; set; } = "";
        public string NIVEL4 { get; set; } = "";
        public string NIVEL5 { get; set; } = "";
        public string NIVEL6 { get; set; } = "";
        public DateTime FECHA { get; set; } = DateTime.Today;
        public string TIPO { get; set; } = "";
        public decimal REFERENCIA { get; set; } = 0;
        public string CONCEPTO { get; set; } = "";
        public decimal IMPORTE { get; set; } = 0;
        public decimal IMPTE_DLLS { get; set; } = 0;
        public string CC_NIVEL1 { get; set; } = "";
        public string CC_NIVEL2 { get; set; } = "";
        public string CC_NIVEL3 { get; set; } = "";
        public string CC_NIVEL4 { get; set; } = "";
        public string CC_NIVEL5 { get; set; } = "";
        public string CC_NIVEL6 { get; set; } = "";
        public string GLOBAL { get; set; } = "";
        public decimal FOLIO { get; set; } = 0;
        public Int64 TRAMITE { get; set; } = 0;



    }

}