using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Entities.Response;
using Atm_Rod_DataAccess.DbUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private BankDbContext _context;
        public TransactionRepository(BankDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ResponseOperationsByPage> GetTransactionsPaginatedAsync(int accountID, int pageSize, int pageNumber)
        {
            var result = new ResponseOperationsByPage();
            var resultDb = _context.Transactions.Where(x => x.AccountID == accountID);
            result.PageNumber = pageNumber;
            result.TotalCount = resultDb.Count();
            var listToConvert = resultDb.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            listToConvert.ForEach(x =>{result.TransactionList.Add(new ResponseTransaction(x));});
            return result;
        }
        public async Task<Transaction> GetLastTransaction(int accountID, TransacEnum transacType)
        {
            var result = _context.Transactions.Where(x => x.AccountID == accountID && x.TransacType == (int)transacType).OrderBy(x=>x.Id).LastOrDefault();
            return result;
        }
    }
}
