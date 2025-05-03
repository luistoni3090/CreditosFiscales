using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eCREDITO_CONCEPTO
    {
        public string SIGLA_CPTO { get; set; } = "";
        public string DESCR { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";


    }

    public class eSECUENCIA_CREDITO
    {
        public string MUNICIPIO { get; set; } = "";
        public string DEPENDENCIA { get; set; } = "";
        public string ANIO { get; set; } = "";
        public string SECUENCIA { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";

    }


}
