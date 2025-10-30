// FastPoint/FastPoint.Application/Services/UsuarioService.cs
using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        // Injetando o Repositório Genérico específico para Usuario
        private readonly IGenericRepository<Usuario> _usuarioRepository;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            // (Aqui entrará a lógica de hash de senha antes de salvar)
            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}