namespace FastPoint.Domain.Entities
{
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime UltimaAtualizacao { get; set; } = DateTime.UtcNow;

        // Relacionamento: Chave estrangeira para Produto (relação 1-para-1)
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; } = null!;
    }
}