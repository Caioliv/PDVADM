using PDVADM.Application.Features.Sales.Interfaces;
using PDVADM.Application.Features.Sales.Services;
using PDVADM.Infrastructure.Database;
using PDVADM.Infrastructure.Repositories.Dapper;
using PDVADM.Infrastructure.Repositories.InMemory;
using PDVADM.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infra
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

// Repositorios e Servicos
builder.Services.AddScoped<ISaleRepository, InMemorySaleRepository>();
builder.Services.AddScoped<IStockService, NoopStockService>();
builder.Services.AddScoped<IFastSaleService, FastSaleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
