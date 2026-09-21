using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;

namespace SalesCet107.Web.Data.Entities
{
    public class City
    {
        public int Id { get; set; }

        //DataAnnotations
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Name { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }
    }
}
