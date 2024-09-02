using Atm_Rod_Entities.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/[Account]")]
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
        public async Task<IActionResult> GetBalance([FromQuery] int cardNumber)
        {
            var result = await _accountService.GetBalance(cardNumber);
            return Ok();
        }
    }
}
