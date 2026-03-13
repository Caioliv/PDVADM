using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVADM.Application.Features.Sales.DTOs;

public class FastSaleResponseDto
{
    public long SaleId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
