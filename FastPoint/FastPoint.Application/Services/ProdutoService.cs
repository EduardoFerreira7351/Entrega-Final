// FastPoint/FastPoint.Application/Services/ProdutoService.cs
using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        // Agora injeta o repositório específico
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosParaCardapioAsync()
        {
            // Chama o método específico do repositório
            return await _produtoRepository.GetProdutosAtivosAsync();
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
            await _produtoRepository.SaveChangesAsync();
        }

        public async void UpdateProduto(Produto produto)
        {
             _produtoRepository.Update(produto);
             await _produtoRepository.SaveChangesAsync();
        }
    }
}