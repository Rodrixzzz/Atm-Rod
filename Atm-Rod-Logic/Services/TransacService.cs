using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;
using System.Net;

namespace Atm_Rod_Logic.Services
{
    public class TransacService:ITransacService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionRepository;
        public TransacService(IGenericRepository<Account> repository, ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _cardRepository = cardRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<ResponseOperation> ProcessOperation(RequestOperation request)
        {
            var response = new ResponseOperation() { Amount = request.Amount, Date = DateTime.Now, OperationType = TransacEnum.Extraction.ToString() };
            if (request.Amount <= 0)
            {
                throw new CustomException("Invalid Amount", HttpStatusCode.BadRequest);
            }
            var resultCard = await _cardRepository.TryGetCard(request.CardNumber);
            if (resultCard == null)
            {
                throw new CustomException("Card: " + request.CardNumber + " not found", HttpStatusCode.BadRequest);
            }
            var resultAccount = await _repository.GetByIdAsync(resultCard.AccountID);
            if (resultAccount == null)
            {
                throw new CustomException("Account not found", HttpStatusCode.BadRequest);
            }
            if (resultAccount.Balance < request.Amount)
            {
                throw new CustomException("The operetation's amount is greater than Account's Balance", HttpStatusCode.BadRequest);
            }
            response.BeforeBalance = resultAccount.Balance;
            response.AfterBalance = resultAccount.Balance - request.Amount;
            var transacToAdd = new Transaction() { AccountID = resultAccount.Id, TransacType = (int)TransacEnum.Extraction, CreatedBy = resultAccount.Name + " " + resultAccount.LastName, Amount = request.Amount, CreatedAt = DateTime.Now };
            var responseTransac = await _transactionRepository.AddAsync(transacToAdd);
            if (responseTransac != 0)
            {
                throw new CustomException("System Error", HttpStatusCode.InternalServerError);
            }
            return response;
        }
        public async Task<ResponseOperationsByPage> QueryOperationsByPage(RequestOperationsByPage request)
        {
            if (request.PageNumber <= 0)
            {
                throw new CustomException("Invalid Param: PageNumber", HttpStatusCode.BadRequest);
            }
            if (request.PageSize <= 0)
            {
                throw new CustomException("Invalid Param: PageSize", HttpStatusCode.BadRequest);
            }
            var resultCard = await _cardRepository.TryGetCard(request.CardNumber);
            if (resultCard == null)
            {
                throw new CustomException("Card: " + request.CardNumber + " not found", HttpStatusCode.BadRequest);
            }
            var resultAccount = await _repository.GetByIdAsync(resultCard.AccountID);
            if (resultAccount == null)
            {
                throw new CustomException("Account not found", HttpStatusCode.BadRequest);
            }
            return await _transactionRepository.GetTransactionsPaginatedAsync(request.PageSize, request.PageNumber, resultAccount.Id);
            
        }
    }
}
