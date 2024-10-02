namespace DomainDrivenDesign
{
    using System.Threading;
    using System.Threading.Tasks;
    using DomainDrivenDesign.Entities;
    using MediatR;

    public class TransferCommandHandler(IMediator mediator, IDatabase database) : IRequestHandler<TransferCommand, Account>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IDatabase _database = database;

        public async Task<Account> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _mediator.Send(new GetAccountByIdQuery(request.SourceAccountId), cancellationToken);

            var destinationAccount = await _mediator.Send(new GetAccountByIdQuery(request.DestinationAccountId), cancellationToken);

            sourceAccount.Debit(request.Amount);
            destinationAccount.Credit(request.Amount);

            _database.Update(sourceAccount);
            _database.Update(destinationAccount);

            await _database.CommitAsync();

            return sourceAccount;
        }
    }

}
