namespace DomainDrivenDesign.Entities;
public class Account : Entity
{
    public int Id { get; set; }
    public decimal Balance { get; set; }

    public void Debit(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
        }
        else
        {
            throw new InvalidOperationException("Saldo insuficiente.");
        }
    }

    public void Credit(decimal amount)
    {
        Balance += amount;
    }
}

