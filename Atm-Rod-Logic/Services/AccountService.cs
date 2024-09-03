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
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionRepository;
        public AccountService(IGenericRepository<Account> repository, ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<ResponseBalance> GetBalance(int cardNumber)
        {
            var resultCard = await _cardRepository.TryGetCard(cardNumber);
            if(resultCard == null)
            {
                throw new CustomException("Card: " + cardNumber + " not found", HttpStatusCode.BadRequest);
            }
            var resultAccount = await _repository.GetByIdAsync(resultCard.AccountID);
            if (resultAccount == null)
            {
                throw new CustomException("Account not found", HttpStatusCode.BadRequest);
            }
            var resultTransac = await _transactionRepository.GetLastTransaction(resultCard.AccountID, Atm_Rod_Entities.Enum.TransacEnum.Extraction);
            if (resultAccount == null)
            {
                throw new CustomException("Not transaction for this account", HttpStatusCode.BadRequest);
            }
            return new ResponseBalance() { Balance = resultAccount.Balance.Value, LastTransaction = resultTransac.CreatedAt.Value, Name = resultAccount.Name, LastName = resultAccount.LastName };
        }
        
    }
}
