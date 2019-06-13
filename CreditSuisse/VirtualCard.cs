namespace CreditSuisse
{
    public class VirtualCard
    {
        private readonly object _transactionLock = new object();
        private readonly short _pin;
        public VirtualCard(short pin, decimal startingBalance)
        {
            _pin = pin;
            Balance = startingBalance;
        }

        public decimal Balance { get; private set; }

        public WithdrawResult Withdraw(short pin, decimal amount)
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
    }

    public enum WithdrawResult
    {
        Ok,
        InvalidPin,
        LowBalance
    }
}
