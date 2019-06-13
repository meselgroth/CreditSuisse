using System;

namespace CreditSuisse
{
    public class VirtualCard
    {
        private readonly object _transactionLock = new object();
        private readonly string _pin;
        public VirtualCard(string pin, decimal startingBalance, IHashingAlgorithm hashingAlgorithm)
        {
            _pin = hashingAlgorithm.Hash(pin);
            Balance = startingBalance;
        }

        public decimal Balance { get; private set; }

        public WithdrawResult Withdraw(string pin, decimal amount)
        {
            if (pin != _pin)
            {
                return WithdrawResult.InvalidPin;
            }
            lock (_transactionLock)
            {
                if (Balance < amount)
                {   
                    return WithdrawResult.LowBalance;
                }
                Balance -= amount;
            }
            return WithdrawResult.Ok;
        }

        public void TopUp(decimal amount)
        {
            lock (_transactionLock)
            {
                Balance += amount;
            }
        }
    }

    public enum WithdrawResult
    {
        Ok,
        InvalidPin,
        LowBalance
    }
}
