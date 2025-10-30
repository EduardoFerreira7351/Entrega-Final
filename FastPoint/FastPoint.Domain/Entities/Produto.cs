namespace FastPoint.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        
        // Regra de negócio: Produtos inativos não devem aparecer no cardápio
        public bool Ativo { get; set; } = true; 

        // Relacionamento: Chave estrangeira para Categoria
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; } = null!;

        // Relacionamento: 1-para-1 com Estoque
        public virtual Estoque Estoque { get; set; } = null!;

        // Relacionamento: Um produto pode estar em vários ItensDePedido
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}