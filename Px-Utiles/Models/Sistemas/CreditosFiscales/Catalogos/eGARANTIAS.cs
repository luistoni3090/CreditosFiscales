using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eTIPO_GARANTIA
    {
        public string TIPO_GARANTIA { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string TIPO { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eDET_TIPO_GARANTIA
    {
        public string TIPO_DETALLE { get; set; } = "";
        public string TIPO_GARANTIA { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";

    }


}
