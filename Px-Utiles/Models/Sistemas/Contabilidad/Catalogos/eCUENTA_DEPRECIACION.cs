/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eCUENTA_DEPRECIACION.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla CUENTA_DEPRECIACION

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eCUENTA_DEPRECIACION
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal EJERCICIO { get; set; } = 0;
        public decimal CVE_CUENTA { get; set; } = 0;
        public decimal ANIOS_VIDA { get; set; } = 0;
        public decimal PORC_DEPRECIA { get; set; } = 0;
        public decimal CVE_CUENTA_CONTRA { get; set; } = 0;
        public decimal CVE_CUENTA_DEPRE { get; set; } = 0;


        // Cuenta Activo
        public string CA_CUENTA { get; set; } = "";
        public decimal CA_CVE_CUENTA { get; set; } = 0;
        public string CA_DESCR { get; set; } = "";
        public string CA_NIVEL1 { get; set; } = "";
        public string CA_NIVEL2 { get; set; } = "";
        public string CA_NIVEL3 { get; set; } = "";
        public string CA_NIVEL4 { get; set; } = "";
        public string CA_NIVEL5 { get; set; } = "";
        public string CA_NIVEL6 { get; set; } = "";


        // Cuenta contra
        public string CC_CUENTA { get; set; } = "";
        public decimal CC_CVE_CUENTA { get; set; } = 0;
        public string CC_DESCR { get; set; } = "";
        public string CC_NIVEL1 { get; set; } = "";
        public string CC_NIVEL2 { get; set; } = "";
        public string CC_NIVEL3 { get; set; } = "";
        public string CC_NIVEL4 { get; set; } = "";
        public string CC_NIVEL5 { get; set; } = "";
        public string CC_NIVEL6 { get; set; } = "";


        // Cuenta depreciación
        public string CD_CUENTA { get; set; } = "";
        public decimal CD_CVE_CUENTA { get; set; } = 0;
        public string CD_DESCR { get; set; } = "";
        public string CD_NIVEL1 { get; set; } = "";
        public string CD_NIVEL2 { get; set; } = "";
        public string CD_NIVEL3 { get; set; } = "";
        public string CD_NIVEL4 { get; set; } = "";
        public string CD_NIVEL5 { get; set; } = "";
        public string CD_NIVEL6 { get; set; } = "";




    }
}
