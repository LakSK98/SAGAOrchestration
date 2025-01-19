namespace SharedLibrary.Contracts
{
    public record OrderNotificationEvent(Guid OrderId, string CustomerEmail, string Message);
}
