using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tripper.Models
{
    [Table( "Estabelecimentos" ) ]
    public class Estabelecimentos
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("RazaoSocial")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("CNPJ")]
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; } = string.Empty;

    }
}
