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
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private BankDbContext _context;
        public CardRepository(BankDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Card> TryGetCard(int cardNumber)
        {
            var result = _context.Cards.Where(x=> x.Number == cardNumber).FirstOrDefault();
            
            return result;
        }
    }
}
