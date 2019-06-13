using CreditSuisse;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CreditSuisseTests
{
    public class VirtualCardTest
    {
        private const string TestPin = "0123";
        private Mock<IHashingAlgorithm> _hashingAlgorithmMock;

        [SetUp]
        public void Setup()
        {
            _hashingAlgorithmMock = new Mock<IHashingAlgorithm>();
            _hashingAlgorithmMock.Setup(m => m.Hash(It.IsAny<string>())).Returns(TestPin);
        }

        [Test]
        public void WithdrawShouldReturnOkAndBalanceUpdated()
        {
            var sut = new VirtualCard(TestPin, 30, _hashingAlgorithmMock.Object);

            var result = sut.Withdraw(TestPin, 10);

            result.Should().Be(WithdrawResult.Ok);
            sut.Balance.Should().Be(20);
        }

        [Test]
        public void WithdrawWithBadPinShouldReturnBadPin()
        {
            var sut = new VirtualCard(TestPin, 30, _hashingAlgorithmMock.Object);

            var result = sut.Withdraw("9999", 10);

            result.Should().Be(WithdrawResult.InvalidPin);
        }

        [Test]
        public void WithdrawWithLowBalanceShouldReturnBadPin()
        {
            var sut = new VirtualCard(TestPin, 10, _hashingAlgorithmMock.Object);

            var result = sut.Withdraw(TestPin, 20);

            result.Should().Be(WithdrawResult.LowBalance);
        }

        [Test]
        public void TopUpShouldAddToBalance()
        {
            var sut = new VirtualCard(TestPin, 10, _hashingAlgorithmMock.Object);

            sut.TopUp(20);

            sut.Balance.Should().Be(30);
        }
    }
}