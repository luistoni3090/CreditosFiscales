///			Wero MX
/// Autor:		Marco Antonio Acuña Rosas
/// Nombre:		WSServicio.cs
/// Creación:		2024.07.01
/// Ult Mod:		2024.07.01
/// Descripción:
/// Clase para consumo de sericio

using Px_Licencias.Models.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Px_Licencias.Servicio
{
    public class WSServicio
    {

        /// <summary>
        /// Consumo de WS para base de datos
        /// </summary>
        /// <param name="oReq">Objeto de solicitud de consumo</param>
        /// <returns>Objeto de respuesta según lo solicitado</returns>
        public static async Task<eResponse> Servicio(eRequest oReq)
        {
            var oRes = new eResponse();

            string sEdnPoint = "https://localhost:7021/Servicio";

            var oClient = new HttpClient();
            var oRequest = new HttpRequestMessage(HttpMethod.Post, sEdnPoint);
            StringContent oContent;


            try
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
                    WriteIndented = true
                };

                oRequest = new HttpRequestMessage(HttpMethod.Post, sEdnPoint);
                oContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(oReq, options), null, "application/json");
                oRequest.Content = oContent;
                var response = await oClient.SendAsync(oRequest);
                response.EnsureSuccessStatusCode();

                var sRes = await response.Content.ReadAsStringAsync();

                Console.WriteLine(await response.Content.ReadAsStringAsync());

                oRes = JsonConvert.DeserializeObject<eResponse>(sRes);

            }
            catch (Exception ex)
            {
                oRes.Message = ex.Message;
            }


            return oRes;

        }
    }
}
