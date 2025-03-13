using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tripper.Models
{
    [Table("Vendedores")]
    public class Vendedores
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("CPF")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = string.Empty;

    }
}
