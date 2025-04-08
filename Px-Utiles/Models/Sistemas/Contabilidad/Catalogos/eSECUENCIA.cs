/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 eSECUENCIA.cs
/// Creación: 	 2024.07.15
/// Ult Mod: 	 2024.07.15
/// Descripción:
/// Clase para estructura de la tabla SECUENCIA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_Utiles.Models.Sistemas.Contabilidad.Catalogos
{
    public class eSECUENCIA
    {
        public decimal EMPRESA { get; set; } = 0;
        public decimal EJERCICIO { get; set; } = 0;
        public string TIPO_POLIZA { get; set; } = "";
        public decimal SECUENCIA { get; set; } = 0;


        public string DESCR { get; set; } = "";



    }
}
