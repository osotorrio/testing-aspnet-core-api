using Silly.Bank.Contracts;

namespace Silly.Bank.Services
{
    public class BalanceService : IBalanceService
    {
        private IHttpClient _httpClient;

        public BalanceService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool HasEnoughBalance(string fromAccount, decimal ammount)
        {
            var balance = _httpClient.Get<decimal>($"api/balances/{fromAccount}");
            return ammount <= balance;
        }
    }
}
