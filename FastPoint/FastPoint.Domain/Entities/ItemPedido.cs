namespace FastPoint.Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; } // Guarda o preço no momento da compra

        // Relacionamento: Chave estrangeira para Pedido
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; } = null!;

        // Relacionamento: Chave estrangeira para Produto
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; } = null!;
    }
}