namespace FastPoint.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Relacionamento: Uma categoria tem vários produtos
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}