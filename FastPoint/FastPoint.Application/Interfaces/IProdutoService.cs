using FastPoint.Domain.Entities;

namespace FastPoint.Application.Interfaces
{
    // Usaremos os DTOs aqui depois, mas por enquanto vamos referenciar a entidade
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllProdutosAsync();
        Task<Produto?> GetProdutoByIdAsync(int id);
        Task AddProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
    }
}