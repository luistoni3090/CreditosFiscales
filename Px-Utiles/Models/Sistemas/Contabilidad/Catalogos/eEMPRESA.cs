/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eEMPRESA.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Clase para estructura de la tabla EMPRESA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eEMPRESA
    {
        public Int32 EMPRESA { get; set; } = 0;
        public string NOMBRE { get; set; } = "";
        public string DIRECCION { get; set; } = "";
        public string MUNICIPIO { get; set; } = "";
        public string ESTADO { get; set; } = "";
        public string RFC { get; set; } = "";
        public string TELEFONO { get; set; } = "";
        public string REP_LEGAL { get; set; } = "";
        public string CURP { get; set; } = "";
        public decimal CP { get; set; } = 0;
        public string ENCABE1 { get; set; } = "";
        public string ENCABE2 { get; set; } = "";
        public string ENCABE3 { get; set; } = "";
        public string PATH { get; set; } = "";
        public decimal CUENTA_BANCOS { get; set; } = 0;
        public decimal CUENTA_CUADRA_B { get; set; } = 0;
        public decimal CUENTA_CUADRA_R { get; set; } = 0;
        public decimal CUENTA_CUADRA_A { get; set; } = 0;
        public decimal CUENTA_CUADRA_C { get; set; } = 0;
        public string BAL_ORDEN_ACRE { get; set; } = "";
        public string FORMATO_POLIZA { get; set; } = "";
        public string NO_POLIZA_PERIODO { get; set; } = "";
        public string NO_PREPOLIZA_POLIZA { get; set; } = "";
        public string APLICA_PREPOLIZA { get; set; } = "";
        public string PREPOLIZA_DISP { get; set; } = "";
        public string MANTO_CUENTA { get; set; } = "";
        public string POLIZA_AUTOMATICA { get; set; } = "";
        public string CAMBIO_EJERCICIO { get; set; } = "";
        public decimal ANIO_ARMONIZACION { get; set; } = 0;
        public string APERTURA_SALDOS { get; set; } = "";
        public string RUTA_CONSOLIDACION { get; set; } = "";
    }

}
