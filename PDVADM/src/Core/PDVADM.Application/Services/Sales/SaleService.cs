public class SaleService : ISaleService
{
    private readonly ISaleRepository _repository;
    private readonly DbConnectionFactory _connectionFactory;

    public async Task ProcessarVenda(VendaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(); // Inicia a trava

        try
        {
            // 1. Cria a Venda
            var saleId = await _repository.CreateSaleAsync(dto.UserId, dto.Discount, dto.Notes, connection, transaction);

            // 2. Adiciona os Itens
            foreach (var item in dto.Items)
            {
                await _repository.AddItemAsync(saleId, item.ProductId, item.Quantity, item.UnitPrice, connection, transaction);
            }

            transaction.Commit(); // Se tudo deu certo, salva no banco
        }
        catch (Exception)
        {
            transaction.Rollback(); // Se deu qualquer erro, cancela TUDO
            throw; // Relança o erro para você tratar na UI
        }
    }
}