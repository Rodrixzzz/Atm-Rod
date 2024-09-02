using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICardService _cardService;
        public LoginController(ICardService cardService)
        {
            _cardService = cardService;
        }
        /// <summary>
        /// Endpoint para hacer Login en el sistema con el numero de tarjeta y Pin
        /// </summary>
        /// <param name="RequestLogin">Numero de tarjeta y Pin.</param>
        /// <returns>Jwt asociado a la tarjeta.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] RequestLogin request)
        {
            var result = await _cardService.Login(request);
            return Ok(result);
        }
    }
}
