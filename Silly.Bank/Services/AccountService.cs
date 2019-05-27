using Silly.Bank.Contracts;
using Silly.Bank.Entities;
using System;
using System.Linq;

namespace Silly.Bank.Services
{
    public class AccountService : IAccountService
    {
        private IHttpClient _httpClient;

        public AccountService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool DoesUserOwnAccount(Guid userId, string fromAccount)
        {
            var userAccounts = _httpClient.GetList<Account>($"api/accounts/{userId}");
            return userAccounts.Any(acc => acc.AccountNumber.Equals(fromAccount));
        }
    }
}
