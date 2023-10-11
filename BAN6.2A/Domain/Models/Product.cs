﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]//reminder :set id to be auto-generated 
        
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
      
        [Required]
        [Range(0,int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryFK { get; set; }  //foreign key property:holds the value
        public Category Category { get; set; } //navigational property



    }
}