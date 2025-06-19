/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eCUENTA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla CUENTA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos
{
    public class eCUENTA
    {
        public int EMPRESA { get; set; } = 0;
        public Int64 CVE_CUENTA { get; set; } = 0;
        public string DESCR { get; set; } = "";
        public Int64 GRUPO { get; set; } = 0;
        public decimal NIVEL { get; set; } = 0;
        public decimal MONEDA { get; set; } = 0;
        public string MOVTO_MANUAL { get; set; } = "";
        public Int64 CUENTA_PADRE { get; set; } = 0;
        public string ACTIVA { get; set; } = "";
        public string TRANSITORIA { get; set; } = "";
        public string AFECT_PAT { get; set; } = "";
        public string TERMINAL { get; set; } = "";
        public string NATURALEZA { get; set; } = "";
        public decimal CENTRO_COSTOS { get; set; } = 0;
        public DateTime FECHA { get; set; } = DateTime.Today;
        public string NIVEL1 { get; set; } = "";
        public string NIVEL2 { get; set; } = "";
        public string NIVEL3 { get; set; } = "";
        public string NIVEL4 { get; set; } = "";
        public string NIVEL5 { get; set; } = "";
        public string NIVEL6 { get; set; } = "";
        public int EJERCICIO { get; set; } = 0;
        
        public decimal CARGOS { get; set; } = 0;
        public decimal ABONOS { get; set; } = 0;

        public string CUENTACONTABLE { get; set; } = "";
        public string ALLCOLUMNS { get; set; } = "";



        public long ID { get; set; }
        public long? ParentID { get; set; }
    }
}
