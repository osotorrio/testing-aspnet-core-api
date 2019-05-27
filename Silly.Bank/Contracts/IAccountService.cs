using System;

namespace Silly.Bank.Contracts
{
    public interface IAccountService
    {
        bool DoesUserOwnAccount(Guid userId, string fromAccount);
    }
}