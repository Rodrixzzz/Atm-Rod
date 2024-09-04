using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;

namespace Atm_Rod_Entities.Interface.Services
{
    public interface ICardService
    {
        Task<ResponseLogin> Login(RequestLogin requestLogin);
    }
}
