using DomainDrivenDesign.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Transfer")]
        public async Task<Account> Transfer(TransferCommand transferCommand)
        {
            var sourceAccount = await _mediator.Send(transferCommand);
            return sourceAccount;
        }

        [HttpGet("{id}")]
        public async Task<Account> Get(int id)
        {
            var sourceAccount = await _mediator.Send(new GetAccountByIdQuery(id));
            return sourceAccount;
        }
    }
}
