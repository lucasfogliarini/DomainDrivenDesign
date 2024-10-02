namespace DomainDrivenDesign
{
    using System.Threading;
    using System.Threading.Tasks;
    using DomainDrivenDesign.Entities;
    using MediatR;

    public class GetAccountByIdQueryHandler(IDatabase database) : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IDatabase _database = database;

        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = _database.Query<Account>().FirstOrDefault(e=>e.Id == request.AccountId);
            if (account == null)
            {
                throw new InvalidOperationException("Conta não encontrada.");
            }

            return account;
        }
    }

}
