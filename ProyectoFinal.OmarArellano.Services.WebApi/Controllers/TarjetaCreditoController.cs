using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.OmarArellano.Application.DTO;
using ProyectoFinal.OmarArellano.Application.Interface;
using ProyectoFinal.OmarArellano.Transversal.Common;
using System.Threading.Tasks;

namespace ProyectoFinal.OmarArellano.Services.WebApi.Controllers
{
    /// <summary>
    /// API TarjetaCreditoController
    /// </summary>

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TarjetaCreditoController : Controller
    {
        private readonly ITarjetaCreditoApplication _tarjetaCreditoApplication;



        /// <summary>
        /// TarjetaCreditoController
        /// </summary>
        /// <param name="tarjetaCreditoApplication"></param>
        public TarjetaCreditoController(ITarjetaCreditoApplication tarjetaCreditoApplication)
        {
            _tarjetaCreditoApplication = tarjetaCreditoApplication;
        }

        /// <summary>
        /// Guardar numero de tarjeta de credito
        /// </summary>
        /// <param name="tarjetaCredito"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GuardarNumeroTarjeta(TarjetaCreditoDto tarjetaCredito)
        {
            var response = new Response<ComprobacionDto>();
            try
            {
                if (tarjetaCredito == null)
                    return BadRequest();

                response = await _tarjetaCreditoApplication.ProcesarTarjetaCredito(tarjetaCredito);

                return Ok(response);

            }
            catch
            {
               return BadRequest();
            }
        }
    }
}
