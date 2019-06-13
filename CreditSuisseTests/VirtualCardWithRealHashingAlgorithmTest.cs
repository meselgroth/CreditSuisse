using CreditSuisse;
using FluentAssertions;
using NUnit.Framework;

namespace CreditSuisseTests
{
    public class VirtualCardWithRealHashingAlgorithmTest
    {
        [Test]
        public void VirtualCardShouldStorePinAsHash()
        {
            const string testPin = "0123";
            var hashedPin = new Sha256HashingAlgorithm().Hash(testPin);
            var sut = new VirtualCard(testPin, 20, new Sha256HashingAlgorithm());

            var result = sut.Withdraw(hashedPin, 20);

            result.Should().Be(WithdrawResult.Ok);
        }
    }
}
