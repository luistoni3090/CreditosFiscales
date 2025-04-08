/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eSISTEMA_CUENTA.cs
/// Creación: 	 2024.07.22
/// Ult Mod: 	 2024.07.22
/// Descripción:
/// Clase para estructura de la tabla SISTEMA_CUENTA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eSISTEMA_CUENTA
    {
        public decimal SISTEMA { get; set; } = 0;
        public decimal CVE_CUENTA { get; set; } = 0;
        public decimal EJERCICIO { get; set; } = 0;
        public decimal EMPRESA { get; set; } = 0;
        public string HABILITADO { get; set; } = "";
    }
}
