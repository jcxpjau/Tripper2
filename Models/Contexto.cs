using Microsoft.EntityFrameworkCore;
using Tripper.Models;

namespace Tripper.Models
{
    public class Contexto : DbContext
    {
        public Contexto( DbContextOptions<Contexto> options) : base (options) {}

        public DbSet<Estabelecimentos> Estabelecimentos { get; set; }

        public DbSet<Produtos> Produtos { get; set; }

        public DbSet<Fornecedores> Fornecedores { get; set; }

        public DbSet<Vendas> Vendas { get; set; }

        public DbSet<Compras> Compras { get; set; }
        public DbSet<Tripper.Models.Vendedores> Vendedores { get; set; } = default!;

    }
}
