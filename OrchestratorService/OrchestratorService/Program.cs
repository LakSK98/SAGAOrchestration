using MassTransit;
using OrchestratorService.Saga;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.StateModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddSagaStateMachine<OrderStateMachine, OrderSagaState>()
          .EntityFrameworkRepository(r =>
          {
              r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
              r.AddDbContext<DbContext, OrderStateDbContext>((provider, options) =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                      sqlOptions => sqlOptions.MigrationsAssembly("OrchestratorService"));
              });
          });

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
