using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eCPTO
    {
        public string CLAVE { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eMULTA
    {
        public string INFERIOR { get; set; } = "";
        public string SUPERIOR { get; set; } = "";
        public string CLAVE { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string IMPORTE { get; set; } = "";
        public string EQUIVALENTE { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";

    }


}
