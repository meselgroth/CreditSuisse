using CreditSuisse;
using FluentAssertions;
using NUnit.Framework;

namespace CreditSuisseTests
{
    public class VirtualCardTest
    {
        [Test]
        public void WithdrawShouldReturnOkAndBalanceUpdated()
        {
            const short pin = 0123;
            var sut = new VirtualCard(pin, 30);

            var result = sut.Withdraw(pin, 10);

            result.Should().Be(WithdrawResult.Ok);
            sut.Balance.Should().Be(20);
        }

        [Test]
        public void WithdrawWithBadPinShouldReturnBadPin()
        {
            var sut = new VirtualCard(0123, 30);

            var result = sut.Withdraw(9999, 10);

            result.Should().Be(WithdrawResult.InvalidPin);
        }

        [Test]
        public void WithdrawWithLowBalanceShouldReturnBadPin()
        {
            const short pin = 0123;
            var sut = new VirtualCard(pin, 10);

            var result = sut.Withdraw(pin, 20);

            result.Should().Be(WithdrawResult.LowBalance);
        }

        [Test]
        public void TopUpShouldAddToBalance()
        {
            var sut = new VirtualCard(0123, 10);

            sut.TopUp(20);

            sut.Balance.Should().Be(30);
        }
    }
}