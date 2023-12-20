using Domain.Models;
using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Presentation.Models.ViewModels
{
    public class CreateflightViewModel
    {
        public List<Country> Countries { get; set; }
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
        [ForeignKey("CountryFrom")]
        public string CountryFromFK { get; set; }

        public virtual Country CountryFrom { get; set; }
        
        [Required]
        [ForeignKey("CountryTo")]
        public string CountryToFK { get; set; }
        public virtual Country CountryTo { get; set; }


        public double WholesalePrice { get; set; }

        public double CommissionRate { get; set; }
    }
}
}
