using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Flight
    {
       
        public Guid Id { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }
       
        [Required]
        [ForeignKey("CountryFrom")]
        public string CountryFromFK{ get; set; }

        public virtual Country CountryFrom{ get; set; }

        [Required]
        [ForeignKey("CountryTo")]
        public string CountryToFK { get; set; }
        public virtual Country CountryTo { get; set; }

        public double WholesalePrice { get; set; }

        public double CommissionRate { get; set; }  

    }
}

