using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Repositories;
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
    public class AccountServiceTest
    {
        private readonly Mock<IGenericRepository<Account>> _mockRepository = new Mock<IGenericRepository<Account>>();
        private readonly Mock<ICardRepository> _mockCardRepository = new Mock<ICardRepository>();
        private readonly Mock<ITransactionRepository> _mockTransactionRepository = new Mock<ITransactionRepository>();
        private AccountService _sut;
        private Fixture _dataGenerator = new();
        public AccountServiceTest()
        {
            _sut = new AccountService(_mockRepository.Object, _mockCardRepository.Object, _mockTransactionRepository.Object);
        }
        [Fact]
        public async Task Should_Return_Ok()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x=> x.Transactions).Without(x=>x.Cards).Create();
            var card = _dataGenerator.Build<Card>().With(x=> x.AccountID,account.Id).With(x=> x.Account, account).Create();
            var transac = _dataGenerator.Build<Transaction>().With(x => x.AccountID, account.Id).With(x=>x.TransacType,1).With(x=>x.Account,account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockTransactionRepository.Setup(x => x.GetLastTransaction(It.IsAny<int>(), It.IsAny<TransacEnum>())).ReturnsAsync(transac);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            //Act
            var result = await _sut.GetBalance(card.Number);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_Return_CustomException_When_Card_Not_Found()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync((Card?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.GetBalance(1));
        }
        [Fact]
        public async Task Should_Return_CustomException_When_Account_Not_Found()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(new Card());
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Account?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.GetBalance(1));
        }
        [Fact]
        public async Task Should_Return_CustomException_When_Transac_Not_Found()
        {
            //Arrange
            var account = _dataGenerator.Build<Account>().Without(x => x.Transactions).Without(x => x.Cards).Create();
            var card = _dataGenerator.Build<Card>().With(x => x.AccountID, account.Id).With(x => x.Account, account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
            _mockTransactionRepository.Setup(x => x.GetLastTransaction(It.IsAny<int>(), It.IsAny<TransacEnum>())).ReturnsAsync((Transaction?)null);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.GetBalance(1));
        }
    }
}
