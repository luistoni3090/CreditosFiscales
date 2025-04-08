using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eDESCUENTOS
    {
        public string CONSEC { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string VIGENCIA_INI { get; set; } = "";
        public string VIGENCIA_FIN { get; set; } = "";

        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eValidar2
    {

        public decimal CONSEC { get; set; } = 0;
        public decimal TIPO_CREDITO { get; set; } = 0;
        public string DESCR { get; set; } = "";


    }

    public class eINCISO2
    {

        public decimal CLAVE { get; set; } = 0;
        public decimal GRUPO { get; set; } = 0;
        public string CONCEPTO { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string INCISO { get; set; } = "";
        public string INCISO_2012 { get; set; } = "";
        public decimal ORDEN { get; set; } = 0;
        public string CAPITAL { get; set; } = "";
        public string IDENTIFICADOR { get; set; } = "";
        public string TIPO_IMPTO { get; set; } = "";
        public string GRUPO_EQUIVALENTE { get; set; } = "";
        public string INCISO_EQUIVALENTE { get; set; } = "";
        public string CCCP { get; set; } = "";
        public string CCLP { get; set; } = "";
        public string CING { get; set; } = "";
        public string PPTO_EJER { get; set; } = "";
        public string DEVENGADO { get; set; } = "";
        public string OBLIGACION { get; set; } = "";
        public string PASDIF_CP { get; set; } = "";
        public string PASDIF_LP { get; set; } = "";
        
        public string ALLCOLUMNS { get; set; } = "";
        public string ValidaCombos { get; set; } = "";


    }

    public class eTIPO2
    {
        public string TIPO { get; set; } = "";


    }


}
