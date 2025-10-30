using FastPoint.Domain.Enums;

namespace FastPoint.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; } = StatusPedido.Recebido;

        // Regra de negócio: "Cliente não pode fazer novo pedido se houver pendência financeira"
        // Vamos simplificar: um pedido "Não Pago" é uma pendência.
        public bool Pago { get; set; } = false;

        // Relacionamento: Chave estrangeira para Usuario (Cliente)
        public int ClienteId { get; set; }
        public virtual Usuario Cliente { get; set; } = null!;

        // Relacionamento: Um pedido tem vários itens
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}