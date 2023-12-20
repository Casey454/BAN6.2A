using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Presentation.Models.ViewModels
{
    public class FlightViewModel
    {

        public List<Country> Countries{ get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Rows { get; set; }
        [Required]
        public int Columns { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public string CountryFrom { get; set; }
        [Required]
        public string CountryTo { get; set; }

        public double WholesalePrice { get; set; }

        public double CommissionRate { get; set; }
    }
}
