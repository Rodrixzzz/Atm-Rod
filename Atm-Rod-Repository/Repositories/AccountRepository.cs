using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Repository.DbUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Repository.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(BankDbContext context) : base(context)
        {
        }
    }
}
