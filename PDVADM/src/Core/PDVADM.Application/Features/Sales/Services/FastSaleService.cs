using PDVADM.Application.Features.Sales.DTOs;
using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Application.Features.Sales.Services;

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
            // proteção de quantidade
            if (item.Quantity <= 0)
            {
                throw new Exception($"A quantidade do item {item.ProductId} deve ser maior que zero!");
            }

            // salvar item
            await _saleRepository.AddItemAsync(
                saleId,
                item.ProductId,
                item.Quantity,
                item.UnitPrice);

            // atualizar estoque
            try
            {
                await _stockService.DecreaseAsync(
                    item.ProductId,
                    item.Quantity,
                    $"Venda #{saleId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AVISO DE ESTOQUE] Não foi possível atualizar o estoque para o produto {item.ProductId}: {ex.Message}");
            }
        }

        return saleId;
    }
}
