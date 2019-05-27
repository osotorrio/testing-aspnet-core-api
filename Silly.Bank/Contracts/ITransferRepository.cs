using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Silly.Bank.Contracts
{
    public interface ITransferRepository
    {
        void MakeTransfer(decimal ammount, string fromAccount, string toAccount);
    }
}
