using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tripper.Models
{
    [Table("Produtos")]
    public class Produtos
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Descricao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Column("QtdeEstoque")]
        [Display(Name = "Quantidade de Estoque")]
        public int QtdeEstoque { get; set; }

        [Column("Validade")]
        [Display(Name = "Data de Válidade")]
        public string Validade { get; set; } = string.Empty;

    }
}
