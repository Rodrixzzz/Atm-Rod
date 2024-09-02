using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;

namespace Atm_Rod_Entities.Interface.Services
{
    public interface ITransacService
    {
        Task<ResponseOperation> ProcessOperation(RequestOperation request);
        Task<ResponseOperationsByPage> QueryOperationsByPage(RequestOperationsByPage request);
    }
}
