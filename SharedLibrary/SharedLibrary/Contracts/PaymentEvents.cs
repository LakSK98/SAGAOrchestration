namespace SharedLibrary.Contracts
{
    public record PaymentRequestedEvent(Guid OrderId, decimal Amount);

    public record PaymentSuccessEvent(Guid OrderId);

    public record PaymentFailureEvent(Guid OrderId, string Error);
}
