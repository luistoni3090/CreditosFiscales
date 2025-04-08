using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.ConciliacionBancaria.Catalogos
{
    public class eCUENTASBANCARIAS
    {

        public decimal BANCO { get; set; } = 0;
        public decimal CUENTA { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public string DIRECC { get; set; } = "";
        public string TEL { get; set; } = "";
        public string EJECUTIVO { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eValida
    {

        public decimal BANCO { get; set; } = 0;
        public decimal CUENTA { get; set; } = 0;


    }

    public class eCUENTASBANCARIAS2
    {

        public decimal BANCO { get; set; } = 0;
        public decimal CUENTA { get; set; } = 0;
        public string MPO { get; set; } = "";
        public string DEL { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string MONEDA { get; set; } = "";
        public string CTACONTABLE { get; set; } = "";
        public string SUBCTA { get; set; } = "";
        public string SUBSUBCTA { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";

        public string ValidaCombos { get; set; } = "";


    }

    public class eTIPO
    {
        public string TIPO { get; set; } = "";
        

    }


}
