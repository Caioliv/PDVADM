using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVADM.Application.DTOs.Sales;

public class FastSaleRequestDto
{
    public Guid UserId { get; set; }
    public List<FastSaleItemDto> Items { get; set; } = [];
    public decimal Discount { get; set; }
    public string? Notes { get; set; }
}
