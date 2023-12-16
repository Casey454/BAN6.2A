using Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ViewModel
{
    public class EmployeeViewModel
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

      
        public string DepartmentName{ get; set; }
       
    }
}
