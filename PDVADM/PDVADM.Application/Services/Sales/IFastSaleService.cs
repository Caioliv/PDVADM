using PDVADM.Application.DTOs.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVADM.Application.Services.Sales;

public interface IFastSaleService
{
    Task<long> CreateAsync(FastSaleRequestDto request);
}
