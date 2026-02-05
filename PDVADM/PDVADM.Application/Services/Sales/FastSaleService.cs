using PDVADM.Application.DTOs.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVADM.Application.Services.Sales;

public class FastSaleService : IFastSaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IStockService _stockService;

    public FastSaleService(
        ISaleRepository saleRepository,
        IStockService stockService)
    {
        _saleRepository = saleRepository;
        _stockService = stockService;
    }

    public async Task<long> CreateAsync(FastSaleRequestDto request)
    {
        // 1. Criar venda
        var saleId = await _saleRepository.CreateAsync(
            request.UserId,
            request.Discount,
            request.Notes);

        // 2. Criar itens + baixar estoque
        foreach (var item in request.Items)
        {
            await _saleRepository.AddItemAsync(
                saleId,
                item.ProductId,
                item.Quantity,
                item.UnitPrice);

            await _stockService.DecreaseAsync(
                item.ProductId,
                item.Quantity,
                $"Venda #{saleId}");
        }

        return saleId;
    }
}
