using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Px_Negocio.Comun;

using Oracle.ManagedDataAccess.Client;
using Px_DBFactory.Models.Api;
using System.Security.AccessControl;
using Px_DBFactory;
using System.Configuration;


namespace Px_Negocio.PadronIdentidadLicencias.licappl
{
    public class w_acceso_plus
    {
        string _ConnString = "";

        public w_acceso_plus(string sConn)
        {
            _ConnString = sConn;
        }

        public async Task<eResponse> cb_continuar(int li_sistema, string lc_applog, string lc_apppwd)
        {

            eResponse oRes = new();
            eRequest oReq = new eRequest();

            oReq.Base = _ConnString;
            oReq.Query = @"
            SELECT DGI_USR.RFC, DGI_USR.SYSUSER, DGI_USR_BD.SYSPWD, DGI_ROL_USR.ROL,
                   DGI_USR.OFI_REC, DGI_USR.MUNICIPIO, DGI_USR.DELEGACION, DGI_USR.STATUS,
                   P.ESTADO, NO_SISTEMA_GRAL, CAMBIA_LIC_DUP, FECHA_COBRO_CAJAS, CONCEPTO_CAJAS, SYS_CLAVE_CAJAS 
            FROM DGI.DGI_ROL_USR
            JOIN DGI.DGI_USR ON DGI_ROL_USR.APP_LOGIN = DGI_USR.APP_LOGIN
            JOIN DGI.DGI_USR_BD ON DGI_USR.SYSUSER = DGI_USR_BD.SYSUSER
            JOIN DGI.PARAMETROS P ON 1 = 1
            WHERE DGI_ROL_USR.SYS_CLAVE = :li_sistema
              AND DGI_USR_BD.SYS_CLAVE = :li_sistema
              AND DGI_USR.APP_LOGIN = :lc_applog
              AND DGI_USR.APP_PWD = :lc_apppwd
              AND DGI_USR.SYS_CLAVE = :li_sistema";

            oReq.Parametros = new List<eParametro> 
                {
                    new eParametro() { Tipo = DbType.String, Nombre = "li_sistema", Valor = li_sistema},
                    new eParametro() { Tipo = DbType.String, Nombre = "lc_applog", Valor = lc_applog},
                    new eParametro() { Tipo = DbType.String, Nombre = "lc_apppwd", Valor = lc_apppwd}
                };

            oRes = await AxData.Consulta(oReq);


            return oRes;

        }


    }
}
