using Common.Interfaces;
using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeesDbRepository:IEmployeesRepository 
    {
        private EmployeeDbContext _db;
        public EmployeesDbRepository(EmployeeDbContext db)
        {
            _db = db;
        }
        public EmployeesDbRepository() { }

        public void AddEmployee(Employee e)
        {
            _db.Employees.Add(e);
            _db.SaveChanges();
        }

        public IQueryable<Employee> GetEmployees()
        {
            return _db.Employees;

        }
    }
}
