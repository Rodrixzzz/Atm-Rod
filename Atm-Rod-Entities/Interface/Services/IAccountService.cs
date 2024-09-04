using Atm_Rod_Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Interface.Services
{
    public interface IAccountService
    {
        Task<ResponseBalance> GetBalance(int cardNumber);
    }
}
