using PDVADM.Application.Features.Sales.DTOs;

namespace PDVADM.Application.Features.Sales.Interfaces;

public interface IFastSaleService
{
    Task<long> CreateAsync(FastSaleRequestDto request);
}
