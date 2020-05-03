using System;

namespace CreditSuisse
{
    public class VirtualCard
    {
        private readonly object _transactionLock = new object();
        private readonly string _pin;
        private decimal _balance;

        public VirtualCard(string pin, decimal startingBalance, IHashingAlgorithm hashingAlgorithm)
        {
            _pin = hashingAlgorithm.Hash(pin);
            _balance = startingBalance;
        }

        public decimal Balance
        {
            get
            {
                lock (_transactionLock) 
                    return _balance;
            }
        }

        public WithdrawResult Withdraw(string pin, decimal amount)
        {
            if (pin != _pin)
            {
                return WithdrawResult.InvalidPin;
            }
            lock (_transactionLock)
            {
                if (_balance < amount)
                {   
                    return WithdrawResult.LowBalance;
                }
                _balance -= amount;
            }
            return WithdrawResult.Ok;
        }

        public void TopUp(decimal amount)
        {
            lock (_transactionLock)
            {
                _balance += amount;
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
