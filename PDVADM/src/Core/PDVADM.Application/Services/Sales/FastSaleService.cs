using PDVADM.Application.DTOs.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVADM.Application.Services.Sales;

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
        // NOVA PROTEÇÃO: Impede erros de digitação financeira
        if (item.Quantity <= 0)
        {
            throw new Exception($"A quantidade do item {item.ProductId} deve ser maior que zero!");
        }

        // Registro obrigatório no banco
        await _saleRepository.AddItemAsync(
            saleId,
            item.ProductId,
            item.Quantity,
            item.UnitPrice);

        // NOVA PROTEÇÃO: O estoque agora é "best-effort" (tenta, mas não trava)
        try 
        {
            await _stockService.DecreaseAsync(
                item.ProductId,
                item.Quantity,
                $"Venda #{saleId}");
        }
        catch (Exception ex)
        {
            // O caixa não trava, o operador não vê erro, mas o ADM pode monitorar o log
            Console.WriteLine($"[AVISO DE ESTOQUE] Não foi possível atualizar o estoque para o produto {item.ProductId}: {ex.Message}");
        }
    }

    return saleId;
}