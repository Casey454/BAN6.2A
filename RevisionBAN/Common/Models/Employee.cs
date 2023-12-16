using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey ("Department")]
        public string DepartmentFK { get; set; }
        public virtual Department Department { get; set; }

        public string PassportNo { get; set; }  

        public string Username { get; set; }    

        public string Password { get; set; }

    }
}
