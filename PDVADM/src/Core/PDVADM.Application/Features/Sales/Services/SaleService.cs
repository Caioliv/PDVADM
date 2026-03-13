using PDVADM.Application.Features.Sales.DTOs;
using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Application.Features.Sales.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _repository;

    public SaleService(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task ProcessarVenda(VendaDto dto)
    {
        var saleId = await _repository.CreateAsync(
            dto.UserId,
            dto.Discount,
            dto.Notes);

        foreach (var item in dto.Items)
        {
            await _repository.AddItemAsync(
                saleId,
                item.ProductId,
                item.Quantity,
                item.UnitPrice);
        }
    }
}
