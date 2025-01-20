using MassTransit;
using SharedLibrary.Contracts;

namespace PaymentService.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
    {
        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            var order = context.Message;

            // Simulate payment processing
            await Task.Delay(2000); // Simulate some processing delay

            // Payment success logic
            bool isPaymentSuccessful = true;
            string message = isPaymentSuccessful ? "Payment processed successfully." : "Payment failed.";

            // Publish PaymentProcessedEvent
            await context.Publish(new PaymentProcessedEvent(order.OrderId));

            // Log payment processing
            Console.WriteLine($"[Payment Service] Payment for OrderId: {order.OrderId} - {message}");
        }
    }
}
