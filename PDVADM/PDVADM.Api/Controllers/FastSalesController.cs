using Microsoft.AspNetCore.Mvc;
using PDVADM.Application.DTOs.Sales;
using PDVADM.Application.Services.Sales;


[ApiController]
[Route("api/fast-sale")]
public class FastSaleController : ControllerBase
{
    private readonly IFastSaleService _fastSaleService;

    public FastSaleController(IFastSaleService fastSaleService)
    {
        _fastSaleService = fastSaleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(FastSaleRequestDto request)
    {
        var saleId = await _fastSaleService.CreateAsync(request);
        return Ok(new { saleId });
    }
}
