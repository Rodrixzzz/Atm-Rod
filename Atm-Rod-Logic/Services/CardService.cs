using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Logic.Services
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IJwtService _jwtService;
        public CardService(ICardRepository cardRepository, IJwtService jwtService)
        {
            _cardRepository = cardRepository;
            _jwtService = jwtService;
        }
        public async Task<ResponseLogin> Login(RequestLogin requestLogin)
        {
            var resultCard = await _cardRepository.TryGetCard(requestLogin.CardNumber);
            if (resultCard != null)
            {
                if (resultCard.Pin == requestLogin.Pin && !resultCard.IsBlocked.Value)
                {
                    resultCard.LoginCounter = 0;
                    resultCard.IsBlocked = false;
                }
                else
                {
                    if (resultCard.Pin != requestLogin.Pin)
                    {
                        resultCard.LoginCounter++;
                        resultCard.IsBlocked = resultCard.LoginCounter > 4;
                    }
                    if (resultCard.IsBlocked.Value)
                    {
                        throw new CustomException("Card Blocked", HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        throw new CustomException("Invalid Pin", HttpStatusCode.BadRequest);
                    }
                }
                var resultUpdate = await _cardRepository.UpdateAsync(resultCard);
                if (resultUpdate == 0)
                {
                    throw new CustomException("Update Error", HttpStatusCode.InternalServerError);
                }

            }
            else
            {
                throw new CustomException("Card: " + requestLogin.CardNumber + "not found", HttpStatusCode.BadRequest);
            }
            return _jwtService.GenerateToken(resultCard.Number);
        }
    }
}
