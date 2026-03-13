using PDVADM.Infrastructure.Database;
using PDVADM.Application.Services.Sales;
using PDVADM.Api.Services.Sales;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
builder.Services.AddSingleton<IStockService, NoopStockService>();
builder.Services.AddScoped<IFastSaleService, FastSaleService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia Controllers
app.MapControllers();

app.Run();
