// FastPoint/FastPoint.Application/Interfaces/IUsuarioService.cs
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task AddUsuarioAsync(Usuario usuario);
        
        // Métodos de autenticação serão adicionados aqui depois
    }
}