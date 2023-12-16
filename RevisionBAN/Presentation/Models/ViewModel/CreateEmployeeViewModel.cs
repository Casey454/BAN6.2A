using Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ViewModel
{
    public class CreateEmployeeViewModel
    {


        public string Name { get; set; }

        public string DepartmentFK { get; set; }
        

        public string PassportNo { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
