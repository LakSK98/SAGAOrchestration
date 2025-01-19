using Microsoft.EntityFrameworkCore;
using SharedLibrary.StateModels;

namespace OrchestratorService.Saga
{
    public class OrderStateDbContext : DbContext
    {
        public OrderStateDbContext(DbContextOptions<OrderStateDbContext> options) : base(options) { }

        public DbSet<OrderSagaState> OrderSagaStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the saga state model
            modelBuilder.Entity<OrderSagaState>(entity =>
            {
                entity.HasKey(x => x.CorrelationId);
                entity.Property(x => x.CurrentState);
                entity.Property(x => x.OrderId);
                entity.Property(x => x.CustomerEmail);
                entity.Property(x => x.TotalPrice);
                entity.Property(x => x.FailureReason);
            });
        }
    }
}
