﻿using System;
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
       
        public int Id { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string CountryFrom { get; set; }

        public string CountyTo { get; set; }

        public double WholesalePrice { get; set; }

        public double CommissionRate { get; set; }  

    }
}
