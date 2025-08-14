namespace Contracts.Events
{
    public interface OrderCreated
    {
        int OrderId { get; }
        int CustomerId { get; }
        decimal Total { get; }
        DateTime CreatedAt { get; }
    }
}
