using System;

namespace Silly.Bank.Domain
{
    public class TransferRequest
    {
        public Guid UserId { get; set; }

        public decimal Ammount { get; set; }

        public string FromAccount { get; set; }

        public string ToAccount { get; set; }
    }
}