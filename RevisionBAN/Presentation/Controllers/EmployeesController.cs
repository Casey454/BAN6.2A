using Common.Interfaces;
using Common.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModel;

namespace Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeesRepository _repo;
        public EmployeesController(IEmployeesRepository repo)//this allows me to pass now an instance of employeeFileRepo
        {
            _repo = repo;   
        }

        public IActionResult SearchEmployee(string keyword)
        {
           IQueryable<Employee> list= _repo.GetEmployees().Where(x => x.Name.Contains(keyword));
            var resultList = from employee in list
                             select new EmployeeViewModel()
                             {
                                 Id = employee.Id,
                                 Name = employee.Name,
                                 DepartmentName = employee.Department.Name
                             };
            return View();
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterEmployee(CreateEmployeeViewModel model)
        {
            var foundEmployee= _repo.GetEmployees().SingleOrDefault(x=>x.PassportNo==model.PassportNo);
            if (foundEmployee == null)
            {
                _repo.AddEmployee(new Employee()
                {
                    Name = model.Name,
                    PassportNo = model.PassportNo,
                    Password = model.Password,
                    Username = model.Username,
                    DepartmentFK = model.DepartmentFK,

                });

            }
            else
            {
                throw new Exception("Employee already esists in db");
            }

            return View(model);
        }
    }
}
