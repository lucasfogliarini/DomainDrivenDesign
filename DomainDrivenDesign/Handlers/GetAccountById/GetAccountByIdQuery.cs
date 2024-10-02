using DomainDrivenDesign.Entities;
using MediatR;

namespace DomainDrivenDesign;
public class GetAccountByIdQuery(int accountId) : IRequest<Account>
{
    public int AccountId { get; } = accountId;
}

