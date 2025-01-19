namespace SharedLibrary.Contracts
{
    public record OrderPlacedEvent(Guid OrderId, string CustomerEmail, decimal TotalPrice);

    public record PaymentProcessedEvent(Guid OrderId);

    public record PaymentFailedEvent(Guid OrderId, string Reason);

    public record OrderCompletedEvent(Guid OrderId);

    public record OrderFailedEvent(Guid OrderId, string Reason);
}
