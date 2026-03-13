using Microsoft.AspNetCore.Mvc;
using PDVADM.Application.DTOs.Sales;
using PDVADM.Application.Services.Sales;


[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly IFastSaleService _fastSaleService;

    public SalesController(IFastSaleService fastSaleService)
    {
        _fastSaleService = fastSaleService;
    }

    [HttpPost("fast")]
    public async Task<IActionResult> FastSale([FromBody] FastSaleRequest request)
    {
        await _fastSaleService.ExecuteAsync(request);

        return Ok(new
        {
            message = "Fast sale recebida com sucesso",
            timestamp = DateTime.UtcNow.AddHours(-3)
        });
    }
}
