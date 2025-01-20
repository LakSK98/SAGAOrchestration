using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.StateModels;

namespace OrchestratorService.Saga
{
    public class OrderStateDbContext : SagaDbContext
    {
        public OrderStateDbContext(DbContextOptions<OrderStateDbContext> options) : base(options) { }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderSagaClassMap(); }
        }
    }

    public class OrderSagaClassMap : SagaClassMap<OrderSagaState>
    {
        protected override void Configure(EntityTypeBuilder<OrderSagaState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState);
            entity.Property(x => x.OrderId);
            entity.Property(x => x.CustomerEmail);
            entity.Property(x => x.TotalPrice);
            entity.Property(x => x.FailureReason);
        }
    }
}
