/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eEMPLEADO.cs
/// Creación: 	 2024.09.17
/// Ult Mod: 	 2024.09.17
/// Descripción:
/// Clase para estructura de la tabla EMPLEADO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Bancos.Seguridad
{
    public class eEMPLEADO
    {
        public Int64 EMPRESA { get; set; } = 0;
        public string LOGIN { get; set; } = "";
        public string APP_LOGIN { get; set; } = "";
        public string EMP_PATER { get; set; } = "";
        public string EMP_MATER { get; set; } = "";
        public string EMP_NOMBRE { get; set; } = "";
        public string EMP_DOMI { get; set; } = "";
        public decimal EMP_TELE { get; set; } = 0;
        public string EMP_PASSWD { get; set; } = "";
        public string EMP_MPACT { get; set; } = "";
        public string EMP_USER { get; set; } = "";
        public string NIP { get; set; } = "";
        public string APLICA_MOMENTO { get; set; } = "";
        public string RFC { get; set; } = "";
        public string SYSUSER { get; set; } = "";
        public string ROL { get; set; } = "";
        public string SYSPWD { get; set; } = "";
        public string APP_PWD { get; set; } = "";


        public string Nombre => $"{EMP_NOMBRE} {EMP_PATER} {EMP_MATER}";




    }
}
