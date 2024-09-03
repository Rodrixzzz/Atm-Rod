using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Atm_Rod_Api.Controllers
{
    [Route("api/Login")]
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
        [ProducesResponseType(typeof(CustomResponse<ResponseLogin>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody] RequestLogin request)
        {
            var result = await _cardService.Login(request);
            return Ok(result);
        }
    }
}
