///			Wero MX
/// Autor:		Marco Antonio Acuña Rosas
/// Nombre:		AxData.cs
/// Creación:		2024.07.01
/// Ult Mod:		2024.07.01
/// Descripción:
/// Clase para comunicación con bases de datos ORACLE

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

using Oracle.ManagedDataAccess.Client;
using Px_DBFactory.Models.Api;


namespace Px_DBFactory
{
    public static class AxData
    {
        /// <summary>
        /// Función para invocar las consultas a la base de datos (Select)
        /// </summary>
        /// <param name="oReq">Estructura que contiene los datos para generar la consulta</param>
        /// <returns>Objeto de resultado, DataSet, que contienen DataTalbe o DataTables según la petición</returns>
        public static async Task<eResponse> Consulta(eRequest oReq)
        {
            DataTable dt = new DataTable();
            eResponse oRes = new();
            try
            {
                using (var conn = new OracleConnection(oReq.Base))
                {
                    conn.Open();
                    using (var cmd = new OracleCommand(oReq.Query, conn))
                    {
                        if (oReq.Parametros != null)
                            foreach (var xPar in oReq.Parametros)
                                cmd.Parameters.Add(new OracleParameter { DbType = xPar.Tipo, ParameterName = $":{xPar.Nombre}", Value = xPar.Valor });

                        using (var adapter = new OracleDataAdapter(cmd))
                            adapter.Fill(oRes.Data);

                    }
                }
            }
            catch (Exception e)
            {
                oRes.Err = -1;
                oRes.Message = e.Message;
            }

            return oRes;
        }


        /// <summary>
        /// Función para ejecutar las sentencias a la base de datos (Insert, Update, Delete)
        /// </summary>
        /// <param name="oReq">Estructura que contiene los datos para generar la transacción</param>
        /// <returns>Objeto de resultado, ID resultante </returns>
        public static async Task<eResponse> Ejecuta(eRequest oReq)
        {
            eResponse oRes = new();

            try
            {
                using (var conn = new OracleConnection(oReq.Base))
                {
                    conn.Open();
                    using (var cmd = new OracleCommand(oReq.Query, conn))
                    {
                        if (oReq.Parametros != null)
                            foreach (var xPar in oReq.Parametros)
                                cmd.Parameters.Add(new OracleParameter { DbType = xPar.Tipo, ParameterName = xPar.Nombre, Value = xPar.Valor });

                        cmd.ExecuteNonQuery();

                        //cmd.CommandText = "SELECT SCOPE_IDENTITY()"; // Ajustar dependiendo de cómo Oracle maneje la devolución de PK autonumérica.
                        //oRes.ID = Convert.ToInt64(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                oRes.Message = ex.Message;
            }


            return oRes;
        }

        //public static bool ExecuteNonQuery(string sConn, string query, params OracleParameter[] parameters)
        //{
        //    using (var conn = new OracleConnection(sConn))
        //    {
        //        conn.Open();
        //        using (var cmd = new OracleCommand(query, conn))
        //        {
        //            if (parameters != null)
        //            {
        //                cmd.Parameters.AddRange(parameters);
        //            }

        //            return cmd.ExecuteNonQuery() > 0;
        //        }
        //    }
        //}

    }
}
