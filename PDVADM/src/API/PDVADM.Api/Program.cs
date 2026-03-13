using PDVADM.Infrastructure.Database;
using PDVADM.Application.Services.Sales;
    
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped < PDVADM.Application.Services.Sales.FastSaleService,
        PDVADM.Application.Services.Sales.IFastSaleService>

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
