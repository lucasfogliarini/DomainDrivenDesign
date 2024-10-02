using DomainDrivenDesign.Entities;
using MediatR;

namespace DomainDrivenDesign;
public class TransferCommand(int sourceAccountId, int destinationAccountId, decimal amount) : IRequest<Account>
{
    public int SourceAccountId { get; } = sourceAccountId;
    public int DestinationAccountId { get; } = destinationAccountId;
    public decimal Amount { get; } = amount;
}

