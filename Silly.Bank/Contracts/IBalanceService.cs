namespace Silly.Bank.Contracts
{
    public interface IBalanceService
    {
        bool HasEnoughBalance(string fromAccount, decimal ammount);
    }
}