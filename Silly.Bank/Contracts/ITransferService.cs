using Silly.Bank.Domain;

namespace Silly.Bank.Contracts
{
    public interface ITransferService
    {
        bool MakeTransfer(TransferRequest request);
    }
}