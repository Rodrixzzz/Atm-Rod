using Atm_Rod_Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Interface.Repositories
{
    public interface ICardRepository: IGenericRepository<Card>
    {
        public Task<Card> TryGetCard(int cardNumber);
    }
}
