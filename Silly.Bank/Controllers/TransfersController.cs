using Microsoft.AspNetCore.Mvc;
using Silly.Bank.Contracts;
using Silly.Bank.Domain;

namespace Silly.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    { 
        private readonly ITransferService _transferService;

        public TransfersController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TransferResult> Post([FromBody] TransferRequest transfer)
        {
            var result = _transferService.MakeTransfer(transfer);
            return new TransferResult { Result = result };
        }
    }
}
