using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;
using Atm_Rod_Logic.Services;
using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Test.Services
{
    public class TransacServiceTest
    {
        private readonly Mock<IGenericRepository<Account>> _mockRepository = new Mock<IGenericRepository<Account>>();
        private readonly Mock<ICardRepository> _mockCardRepository = new Mock<ICardRepository>();
        private readonly Mock<ITransactionRepository> _mockTransactionRepository = new Mock<ITransactionRepository>();
        private TransacService _sut;
        private Fixture _dataGenerator = new();
        private RequestOperation requestOperation = new RequestOperation() { Amount = 100, CardNumber = 1};
        private RequestOperationsByPage requestOperationsByPage = new RequestOperationsByPage() { CardNumber = 1 };
        public TransacServiceTest()
        {
            _sut = new TransacService(_mockRepository.Object, _mockCardRepository.Object, _mockTransactionRepository.Object);
        }
        #region ProccesOperationMethod
        [Fact]
        public async Task ProcessOperation_Should_Return_Ok()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).With(x=> x.Balance, 400000).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x => x.TransacType, 1).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockTransactionRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(1);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Account>())).ReturnsAsync(1);
            //Act
            var result = await _sut.ProcessOperation(requestOperation);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_Amount_LessEqual_Zero()
        {
            //Arrange
            var request = requestOperation;
            request.Amount = 0;
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(request));
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_Card_Not_Found()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync((Card?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(requestOperation));
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_Account_Not_Found()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, 1).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Account?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(requestOperation));
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_Amount_Is_Greater_Than_Balance()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).With(x => x.Balance, 1).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x => x.TransacType, 1).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockTransactionRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(1);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(requestOperation));
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_SaveError()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).With(x => x.Balance, 1).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x => x.TransacType, 1).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockTransactionRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(0);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Account>())).ReturnsAsync(1);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(requestOperation));
        }
        [Fact]
        public async Task ProcessOperation_Should_Return_CustomException_When_UpdateError()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).With(x => x.Balance, 1).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x => x.TransacType, 1).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockTransactionRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(0);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Account>())).ReturnsAsync(0);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.ProcessOperation(requestOperation));
        }
        #endregion
        #region QueryOperationsByPage
        [Fact]
        public async Task QueryOperationsByPage_Should_Return_Ok()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).With(x => x.Balance, 400000).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x => x.TransacType, 1).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            _mockTransactionRepository.Setup(x=> x.GetTransactionsPaginatedAsync(It.IsAny<int>(),It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new ResponseOperationsByPage());
            //Act
            var result = await _sut.QueryOperationsByPage(requestOperationsByPage);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task QueryOperationsByPage_Should_Return_CustomException_When_Card_Not_Found()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync((Card?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.QueryOperationsByPage(requestOperationsByPage));
        }
        [Fact]
        public async Task QueryOperationsByPage_Should_Return_CustomException_When_Account_Not_Found()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, 1).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Account?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.QueryOperationsByPage(requestOperationsByPage));
        }
        #endregion

    }

}
