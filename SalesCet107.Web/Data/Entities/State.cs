using System.ComponentModel.DataAnnotations;

namespace SalesCet107.Web.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        //DataAnnotations
        [Display(Name = "Pais")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;

        public int GetCitiesNumber()
        {
            return Cities == null ? 0 : Cities.Count;
        }
    }
}
