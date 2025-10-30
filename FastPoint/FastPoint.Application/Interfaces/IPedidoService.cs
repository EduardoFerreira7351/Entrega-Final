using FastPoint.Domain.Entities;

namespace FastPoint.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido?> GetPedidoByIdAsync(int id);       
        Task AddPedidoAsync(Pedido pedido);
        Task UpdatePedidoAsync(Pedido pedido);
        Task DeletePedidoAsync(int id);
    }
}