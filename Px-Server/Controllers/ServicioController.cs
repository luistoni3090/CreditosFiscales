///			Wero MX
/// Autor:		Marco Antonio Acuña Rosas
/// Nombre:		ServicioController.cs
/// Creación:		2024.07.01
/// Ult Mod:		2024.07.01
/// Descripción:
/// EndPoint para solicitud depeticiones a la base de datos


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Linq;

using Px_DBFactory;
using Px_DBFactory.Models.Api;


namespace Px_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioController : Controller
    {

        private readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor, se inyecta la configuración
        /// </summary>
        /// <param name="configuration">Objeto de configuración</param>
        public ServicioController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// EndPoint genérico de consumo de servicios a la base de datos
        /// </summary>
        /// <param name="model">Objeto de solicitud de petición hacia la base de datos</param>
        /// <returns>Objeto que contiene el resultado de la transacción</returns>
        [HttpPost]
        public async Task<ActionResult> Ejecuta(eRequest model)
        {
            var sConn = (string.IsNullOrEmpty(model.Base)) ? Configuration.GetConnectionString("licencias") : Configuration.GetConnectionString(model.Base);

            // Esta madre es temporal por que selializa los valores con ValueKind = tipo dato / valor, si funciona se queda asi
            //Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(model));
            model = Newtonsoft.Json.JsonConvert.DeserializeObject<eRequest>(System.Text.Json.JsonSerializer.Serialize(model));

            var oRes = new eResponse();
            model.Base = sConn;


            switch (model.Tipo)
            {
                case 0:
                    oRes = await AxData.Consulta(model);
                    break;
                case 1:
                    oRes = await AxData.Ejecuta(model);
                    break;
            }

            return Ok(JsonConvert.SerializeObject(oRes));
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
    }
