using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tripper.Models
{
    [Table("Fornecedores")]
    public class Fornecedores
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
