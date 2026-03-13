using Microsoft.AspNetCore.Mvc;
using PDVADM.Application.Features.Sales.DTOs;
using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Api.Controllers;

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
    public async Task<IActionResult> FastSale([FromBody] FastSaleRequestDto request)
    {
        var saleId = await _fastSaleService.CreateAsync(request);
        return Ok(new
        {
            sale_id = saleId,
            timestamp = DateTime.UtcNow.AddHours(-3)
        });
    }
}
