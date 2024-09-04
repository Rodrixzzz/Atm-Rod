using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using Atm_Rod_Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Interface.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<Transaction> GetLastTransaction(int accountID, TransacEnum transacType);
        Task<ResponseOperationsByPage> GetTransactionsPaginatedAsync(int accountID, int pageSize, int pageNumber);
    }
}
