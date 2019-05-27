using Silly.Bank.Contracts;
using Silly.Bank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Silly.Bank.Services
{
    public class TransferService : ITransferService
    {
        private readonly IAccountService _accountService;
        private readonly IBalanceService _balanceService;
        private readonly ITransferRepository _transferRepository;

        public TransferService(IAccountService accountService, IBalanceService balanceService, ITransferRepository transferRepository)
        {
            _accountService = accountService;
            _balanceService = balanceService;
            _transferRepository = transferRepository;
        }

        public bool MakeTransfer(TransferRequest request)
        {
            if (!_accountService.DoesUserOwnAccount(request.UserId, request.FromAccount))
            {
                return false;
            }

            if (!_balanceService.HasEnoughBalance(request.FromAccount, request.Ammount))
            {
                return false;
            }

            _transferRepository.MakeTransfer(request.Ammount, request.FromAccount, request.ToAccount);
            return true;
        }
    }
}
