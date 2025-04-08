using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Negocio.Comun
{
    public static class ConnString
    {
        public static string GetOracleConnection()
        {
            return "User Id=dgi;Password=dgi;Data Source=//172.80.22.6:1521/Lic";
        }
    }
}
