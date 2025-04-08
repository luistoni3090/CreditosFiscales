using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eFINANC_ESTATAL
    {

        public string ANIO { get; set; } = "";
        public string MES { get; set; } = "";
        public string IND_FINANC { get; set; } = "";
        public string IND_RECAR { get; set; } = "";
        public string FEC_PUBLICA_DOF { get; set; } = "";
        public string IND_RECAR_DIARIO { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eFINANC_FEDERAL
    {

        public string ANIO { get; set; } = "";
        public string MES { get; set; } = "";
        public string IND_FINANC { get; set; } = "";
        public string IND_RECAR { get; set; } = "";
        public string FEC_PUBLICA_DOF { get; set; } = "";
        public string IND_FED_MINIMO { get; set; } = "";
        public string IND_RECAR_MINIMO { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eValidar3
    {

        public decimal CONSEC { get; set; } = 0;
        public decimal TIPO_CREDITO { get; set; } = 0;
        public string DESCR { get; set; } = "";


    }

    public class eRECARGOS
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

    public class eTIPO3
    {
        public string TIPO { get; set; } = "";


    }


}
