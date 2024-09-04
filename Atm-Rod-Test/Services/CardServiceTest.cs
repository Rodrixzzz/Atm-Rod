using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Request;
using Atm_Rod_Entities.Response;
using Atm_Rod_Logic.Services;
using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Test.Services
{
    public class CardServiceTest
    {
        private readonly Mock<ICardRepository> _mockCardRepository = new Mock<ICardRepository>();
        private readonly Mock<IJwtService> _mockJwtService = new Mock<IJwtService>();
        private CardService _sut;
        private Fixture _dataGenerator = new();
        private RequestLogin request = new RequestLogin() { CardNumber = 1, Pin = 1 };
        public CardServiceTest()
        {
            _sut = new CardService(_mockCardRepository.Object, _mockJwtService.Object);
        }
        [Fact]
        public async Task Should_Return_Ok()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.Number, 1).With(x => x.Pin, 1).With(x => x.LoginCounter, 0).With(x=> x.IsBlocked,false).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockCardRepository.Setup(x => x.UpdateAsync(It.IsAny<Card>())).ReturnsAsync(1);
            _mockJwtService.Setup(x => x.GenerateToken(It.IsAny<int>())).Returns(new ResponseLogin("Token",10));
            //Act
            var result = await _sut.Login(request);
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
            await Assert.ThrowsAsync<CustomException>(() => _sut.Login(request));
        }
        [Fact]
        public async Task Should_Return_CustomException_When_PIN_Is_Invalid()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.Number, 1).With(x => x.Pin, 20).With(x => x.LoginCounter, 0).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockCardRepository.Setup(x => x.UpdateAsync(It.IsAny<Card>())).ReturnsAsync(1);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.Login(request));
        }
        [Fact]
        public async Task Should_Return_CustomException_When_Card_Is_Blocked()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.Number, 1).With(x => x.Pin, 20).With(x => x.LoginCounter, 4).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockCardRepository.Setup(x => x.UpdateAsync(It.IsAny<Card>())).ReturnsAsync(1);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.Login(request));
        }
        [Fact]
        public async Task Should_Return_CustomException_When_UpdateError()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().With(x => x.Number, 1).With(x => x.Pin, 20).With(x => x.LoginCounter, 4).Without(x => x.Account).Create();
            _mockCardRepository.Setup(x => x.TryGetCard(It.IsAny<int>())).ReturnsAsync(card);
            _mockCardRepository.Setup(x => x.UpdateAsync(It.IsAny<Card>())).ReturnsAsync(0);
            //Act
            //Assert
            await Assert.ThrowsAsync<CustomException>(() => _sut.Login(request));
        }
    }
}
