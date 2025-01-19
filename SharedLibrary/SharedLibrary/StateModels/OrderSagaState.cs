using MassTransit;

namespace SharedLibrary.StateModels
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; } 
        public string CurrentState { get; set; }
        public Guid OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public string FailureReason { get; set; }
    }
}
