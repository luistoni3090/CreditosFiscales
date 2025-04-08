///			Wero MX
/// Autor:		Marco Antonio Acuña Rosas
/// Nombre:		eRequest.cs
/// Creación:		2024.07.01
/// Ult Mod:		2024.07.01
/// Descripción:
/// Clase para objeto de consulta

using System;
using System.Collections.Generic;
using System.Data;

namespace Px_Licencias.Models.Api
{

    /// <summary>
    /// Clase de cnsutla general
    /// </summary>
    public class eRequest
    {
        public int Tipo { get; set; } = 0;
        public string Base { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;

        public List<eParametro> Parametros { get; set; } = new List<eParametro>();
        public Int32 Tiempo { get; set; } = 0;

    }

    /// <summary>
    /// Parámetros para envío de solicitud
    /// </summary>
    public class eParametro
    {
        public string Nombre { get; set; } = string.Empty;
        public DbType Tipo { get; set; } =0;
        public object Valor { get; set; } = null;
    }

}
