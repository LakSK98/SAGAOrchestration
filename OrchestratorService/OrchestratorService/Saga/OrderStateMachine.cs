using MassTransit;
using SharedLibrary.Contracts;
using SharedLibrary.StateModels;

namespace OrchestratorService.Saga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderSagaState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState); // track the state of the saga

            // Define Events
            Event(() => OrderPlacedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentSuccessEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentFailureEvent, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderPlacedEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderId = context.Message.OrderId;
                        context.Saga.CustomerEmail = context.Message.CustomerEmail;
                        context.Saga.TotalPrice = context.Message.TotalPrice;
                    })
                    .TransitionTo(Processing)
                    .Publish(context => new PaymentRequestedEvent(context.Saga.OrderId, context.Saga.TotalPrice))
            );

            During(Processing,
                When(PaymentSuccessEvent)
                    .TransitionTo(Completed)
                    .Publish(context => new OrderCompletedEvent(context.Saga.OrderId)),
                When(PaymentFailureEvent)
                    .Then(context => context.Saga.FailureReason = context.Message.Reason)
                    .TransitionTo(Failed)
                    .Publish(context => new OrderFailedEvent(context.Message.OrderId, context.Message.Reason))
            );

            SetCompletedWhenFinalized();
        }

        // States
        public State Processing { get; private set; }
        public State Completed { get; private set; }
        public State Failed { get; private set; }

        // Events
        public Event<OrderPlacedEvent> OrderPlacedEvent { get; private set; }
        public Event<PaymentProcessedEvent> PaymentSuccessEvent { get; private set; }
        public Event<PaymentFailedEvent> PaymentFailureEvent { get; private set; }
    }
}
