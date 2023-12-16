using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
     public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }   

        public string Author { get; set; }

        [ForeignKey("Category")]
        public string CategoryFK { get; set; }
        public virtual CategoryType Category { get; set; }



    }
}
