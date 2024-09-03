using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_DataAccess.DbUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_DataAccess.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private BankDbContext _context;
        public AccountRepository(BankDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
