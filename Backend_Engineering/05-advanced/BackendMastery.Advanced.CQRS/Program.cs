using BackendMastery.Advanced.CQRS.Infrastructure;
using BackendMastery.Advanced.CQRS.Services.Commands;
using BackendMastery.Advanced.CQRS.Services.Queries;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<InMemoryOrderStore>();

builder.Services.AddScoped<OrderCommandService>();
builder.Services.AddScoped<OrderQueryService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
