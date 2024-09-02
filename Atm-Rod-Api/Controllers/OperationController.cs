using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Microsoft.AspNetCore.Mvc;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/[Operation]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ITransacService _transacService;
        public OperationController(ITransacService transacService)
        {
            _transacService = transacService;
        }
        /// <summary>
        /// Endpoint para consultar el listado de operaciones con el numero de tarjeta
        /// </summary>
        /// <param name="CardNumber">Numero de tarjeta</param>
        /// <param name="PageSize">Tamaño de Pagina</param>
        /// <param name="PageNumber">Numero de Pagina</param>
        /// <returns>Listado de operaciones.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOperationsByPage([FromQuery] RequestOperationsByPage request)
        {
            var result = await _transacService.QueryOperationsByPage(request);
            return Ok();
        }
        /// <summary>
        /// Endpoint para consultar el listado de operaciones con el numero de tarjeta
        /// </summary>
        /// <param name="CardNumber">Numero de tarjeta</param>
        /// <param name="Amount">monto de la operacion</param>
        /// <returns>Comprobante de la operacion.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] RequestOperation request)
        {
            var result = await _transacService.ProcessOperation(request);
            return Ok();
        }
    }
}
