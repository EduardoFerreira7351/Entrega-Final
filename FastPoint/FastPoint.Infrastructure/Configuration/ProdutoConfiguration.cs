using FastPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastPoint.Infrastructure.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Descricao).HasMaxLength(500);
            
            // Configura o tipo decimal para dinheiro
            builder.Property(p => p.Preco).HasColumnType("decimal(18,2)");

            // Relacionamento 1-para-N com Categoria
            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);

            // Relacionamento 1-para-1 com Estoque
            builder.HasOne(p => p.Estoque)
                .WithOne(e => e.Produto)
                .HasForeignKey<Estoque>(e => e.ProdutoId);
        }
    }
}