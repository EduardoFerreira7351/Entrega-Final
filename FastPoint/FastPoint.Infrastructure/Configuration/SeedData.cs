using FastPoint.Domain.Entities;
using FastPoint.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FastPoint.Infrastructure.Configurations
{
    public static class SeedData
    {
        // NOTA: Em um projeto real, o hash da senha não seria feito assim.
        // Usaríamos o Identity ou um serviço de hash. Isso é apenas para teste.
        private const string AdminSenhaHash = "ADMIN_SENHA_HASH_SECRETA"; // Simulação de hash

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context.AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<Context.AppDbContext>>()))
            {
                // Verifica se já existem usuários
                if (context.Usuarios.Any())
                {
                    return; // Banco de dados já populado
                }

                // Criar Admin
                context.Usuarios.Add(new Usuario
                {
                    Nome = "Admin FastPoint",
                    Email = "admin@fastpoint.com",
                    SenhaHash = AdminSenhaHash, // Lembre-se, isso é apenas um placeholder
                    Perfil = PerfilUsuario.Administrador,
                    DataCadastro = DateTime.UtcNow
                });

                // Criar Categorias
                var catHamburgueres = new Categoria { Nome = "Hambúrgueres" };
                var catBebidas = new Categoria { Nome = "Bebidas" };
                var catSobremesas = new Categoria { Nome = "Sobremesas" };

                context.Categorias.AddRange(catHamburgueres, catBebidas, catSobremesas);
                
                // Salva para obter os IDs das categorias
                context.SaveChanges();

                // Criar Produtos e Estoque
                var prod1 = new Produto 
                { 
                    Nome = "Classic Burger", 
                    Descricao = "Pão, carne e queijo", 
                    Preco = 25.50m, 
                    Ativo = true, 
                    CategoriaId = catHamburgueres.Id 
                };
                var prod2 = new Produto 
                { 
                    Nome = "Refrigerante 350ml", 
                    Descricao = "Lata", 
                    Preco = 6.00m, 
                    Ativo = true, 
                    CategoriaId = catBebidas.Id 
                };

                context.Produtos.AddRange(prod1, prod2);
                context.SaveChanges(); // Salva para obter os IDs dos produtos

                // Adiciona estoque
                context.Estoques.AddRange(
                    new Estoque { ProdutoId = prod1.Id, Quantidade = 100 },
                    new Estoque { ProdutoId = prod2.Id, Quantidade = 200 }
                );

                // Salva todos os dados de seed
                context.SaveChanges();
            }
        }
    }
}