using System.ComponentModel.DataAnnotations;

namespace SalesCet107.Web.Data.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="País")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no maximo {1}!")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
    }
}
