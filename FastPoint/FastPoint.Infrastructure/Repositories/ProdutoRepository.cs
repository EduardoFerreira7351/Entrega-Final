// FastPoint/FastPoint.Infrastructure/Repositories/ProdutoRepository.cs
using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;
using FastPoint.Infrastructure.Context; // Usando seu AppDbContext
using Microsoft.EntityFrameworkCore;

namespace FastPoint.Infrastructure.Repositories
{
    // Importante: Herda de GenericRepository e implementa IProdutoRepository
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        // O construtor base (GenericRepository) precisa do AppDbContext
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        // Implementação do método customizado
        public async Task<IEnumerable<Produto>> GetProdutosAtivosAsync()
        {
            // Regra de Negócio: "Produtos inativos não devem aparecer no cardápio"
            return await _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.Category) // Exemplo de Eager Loading
                .AsNoTracking()
                .ToListAsync();
        }
    }
}