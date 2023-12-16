using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ticket
    {

        public int Id { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        [ForeignKey("Flight")]
        public int FlightIdFK { get; set; }
        public virtual Flight Flight { get; set; }

        public string Passport { get; set; }

        public double PricePaid { get; set; }

        public Boolean Cancelled { get; set; }

    }
}