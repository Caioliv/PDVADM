namespace PDVADM.Application.Features.Sales.DTOs;

public class VendaDto
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<VendaItemDto> Items { get; set; } = new();
}
