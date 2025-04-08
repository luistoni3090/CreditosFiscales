/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 AppState.cs
/// Creación: 	 2024.07.17
/// Ult Mod: 	 2024.07.17
/// Descripción:
/// Variales generales de la app

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Px_CajasServicioPublico.Utiles.Modelos
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

    }
}
