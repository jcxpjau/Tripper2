using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tripper.Models
{
    [Table("Compras")]
    public class Compras
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [ForeignKey("ProdutosId")]
        [Display(Name = "Produto")]
        public int ProdutosId { get; set; }
        public Produtos? Produtos { get; set; }

        [ForeignKey("FornecedoresId")]
        [Display(Name = "Fornecedor")]
        public int FornecedoresId { get; set; }
        public Fornecedores? Fornecedores { get; set; }

        [Column("Data")]
        [Display(Name = "Data")]
        public string Data { get; set; } = string.Empty;
    }
}
