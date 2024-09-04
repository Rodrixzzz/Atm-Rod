using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public BalanceController(IAccountService accountService)
        {
             _accountService = accountService;
        }
        /// <summary>
        /// Endpoint para consultar el saldo con el numero de tarjeta
        /// </summary>
        /// <param name="cardNumber">Numero de tarjeta</param>
        /// <returns>Datos de la cuenta.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(CustomResponse<ResponseBalance>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetBalance([FromQuery, BindRequired] int cardNumber)
        {
            var result = await _accountService.GetBalance(cardNumber);
            return Ok(result);
        }
    }
}
