using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tripper.Models
{
    [Table("Vendas")]
    public class Vendas
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [ForeignKey("ProdutosId")]
        [Display(Name = "Produto")]
        public int ProdutosId { get; set; }
        public Produtos? Produtos { get; set; }

        [ForeignKey("EstabelecimentosId")]
        [Display(Name = "Estabelecimentos")]
        public int EstabelecimentosId { get; set; }
        public Estabelecimentos? Estabelecimentos { get; set; }

        [ForeignKey("VendedoresId")]
        [Display(Name = "Produto")]
        public int VendedoresId { get; set; }
        public Vendedores? Vendedores { get; set; }

        [Column("Data")]
        [Display(Name = "Data")]
        public string Data { get; set; } = string.Empty;


    }
}
