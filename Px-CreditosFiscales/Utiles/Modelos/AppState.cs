﻿
using Px_Utiles.Models.Sistemas.CreditosFiscales.Catalogos;
using Px_Utiles.Models.Sistemas.CreditosFiscales.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_CreditosFiscales.Utiles.Modelos
{

    /// <summary>
    /// Clase para obtener parámetros generales de la aplicación
    /// </summary>
    public class AppState
    {

        // De la app
        public string Base { get; set; } = string.Empty;
        public string EndPoint { get; set; } = string.Empty;

        // Del sistema
        public object Usuario { get; set; }     // modificar para el usuario de la sesión
        public int Empresa { get; set; } = 0;
        public int Ejercicio { get; set; } = 0;
        public int Periodo { get; set; } = 0;

        public string QuerysSeparador { get; set; } = "°";

        public eEMPRESA _Empresa { get; set; } = new eEMPRESA();
        public eEMPLEADO _Empleado { get; set; } = new eEMPLEADO();

        public List<eCUENTA> _Cuentas { get; set; }
        public string Owner { get; internal set; }
    }
}
