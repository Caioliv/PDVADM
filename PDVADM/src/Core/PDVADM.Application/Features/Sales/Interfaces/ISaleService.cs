using PDVADM.Application.Features.Sales.DTOs;

namespace PDVADM.Application.Features.Sales.Interfaces;

public interface ISaleService
{
    Task ProcessarVenda(VendaDto dto);
}
