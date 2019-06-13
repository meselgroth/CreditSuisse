namespace CreditSuisse
{
    public interface IHashingAlgorithm
    {
        string Hash(string pin);
    }
}