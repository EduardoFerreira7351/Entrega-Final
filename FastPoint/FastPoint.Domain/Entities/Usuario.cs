using FastPoint.Domain.Enums;

namespace FastPoint.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty; // Nunca armazene senhas em texto puro
        public PerfilUsuario Perfil { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Relacionamento: Um usuário (cliente) pode ter vários pedidos
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}