using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/Amount")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
             _accountService = accountService;
        }
        /// <summary>
        /// Endpoint para consultar el saldo con el numero de tarjeta
        /// </summary>
        /// <param name="cardNumber">Numero de tarjeta</param>
        /// <returns>Datos de la cuenta.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomResponse<ResponseBalance>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetBalance([FromQuery] int cardNumber)
        {
            var result = await _accountService.GetBalance(cardNumber);
            return Ok(result);
        }
    }
}
