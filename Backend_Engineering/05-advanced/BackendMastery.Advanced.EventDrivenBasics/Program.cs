using BackendMastery.Advanced.EventDrivenBasics.Infrastructure.Messaging;
using BackendMastery.Advanced.EventDrivenBasics.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<RabbitMqConnection>();
builder.Services.AddSingleton<EventPublisher>();
builder.Services.AddSingleton<OrderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Start consumer safely in background
var consumer = new EventConsumer();
_ = consumer.StartAsync();

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
