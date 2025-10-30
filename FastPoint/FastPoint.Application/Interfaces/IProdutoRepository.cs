// FastPoint/FastPoint.Application/Interfaces/IProdutoRepository.cs
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Interfaces
{
    // Herda do Genérico, mas pode ter métodos customizados
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        // Método específico para produtos, seguindo a regra de negócio
        Task<IEnumerable<Produto>> GetProdutosAtivosAsync(); 
    }
}