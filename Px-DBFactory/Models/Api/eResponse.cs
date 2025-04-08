///			Wero MX
/// Autor:		Marco Antonio Acuña Rosas
/// Nombre:		eResponse.cs
/// Creación:		2024.07.01
/// Ult Mod:		2024.07.01
/// Descripción:
/// Clase para objeto de respuesta de solicitud

using System.Data;

namespace Px_DBFactory.Models.Api
{
    /// <summary>
    /// Clase para respuesta de servicio
    /// </summary>
    public class eResponse
    {
        public Int64 ID { get; set; } = 0;
        public Int16 Err { get; set; } = 0;
        public string Message { get; set; } = "Ejecución satisfactoria";
        public DataSet Data { get; set; } = new();
        public object DataObj { get; set; } = new();
    }
}
