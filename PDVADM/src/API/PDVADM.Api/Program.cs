using PDVADM.Infrastructure.Database;
using PDVADM.Application.Services.Sales;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Registro de Serviços
builder.Services.AddSingleton<DbConnectionFactory>();

// Agora o compilador vai encontrar as classes porque você adicionou os 'using' acima
builder.Services.AddScoped<PDVADM.Application.Services.Sales.ISaleRepository, PDVADM.Api.Services.Sales.InMemorySaleRepository>();
builder.Services.AddScoped<PDVADM.Application.Services.Sales.IStockService, PDVADM.Api.Services.Sales.NoopStockService>();
builder.Services.AddScoped<PDVADM.Application.Services.Sales.IFastSaleService, PDVADM.Application.Services.Sales.FastSaleService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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