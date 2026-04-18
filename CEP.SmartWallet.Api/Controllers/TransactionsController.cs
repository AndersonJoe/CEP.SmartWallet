using MediatR;
using Microsoft.AspNetCore.Mvc;
using CEP.SmartWallet.Application.Transactions.Commands.CreateTransaction;
using CEP.SmartWallet.Application.Transactions.Queries.GetTransactions;

namespace CEP.SmartWallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetTransactionsQuery()); 
            return Ok(result);
        }
    }
}