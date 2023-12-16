using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeFileRepository : IEmployeesRepository
    {
        private string _path;
        public EmployeeFileRepository(string path)
        {
            _path = path;

            if (File.Exists(_path) == false)
            {
              using(var myFile= File.CreateText(path))
                {
                    myFile.Close();
                }
            }   
            
        }
        public void AddEmployee(Employee e)
        {
            List<Employee> list = GetEmployees().ToList();
            e.Id = list.Count;
            list.Add(e);
            string[] employees = new string[list.Count];
            int counter = 0;
            foreach (var item in list)
            {
                employees[counter] = item.Id.ToString() + "," + item.Name + "," + item.DepartmentFK + "," + item.PassportNo + "," + item.Username + "," + item.Password + ",";
                counter++;
            }
            File.WriteAllLines(_path, employees);
        }
    
        public IQueryable<Employee> GetEmployees()
        { 
       
         string [] contents= File.ReadAllLines(_path);
            List<Employee> employees= new List<Employee>();
            string contentofOneEmployee = "";
            foreach(string line in contents)
            {
                string[] employeeDetails = line.Split(',');
                employees.Add(
                    new Employee()
                    {
                        Id=Convert.ToInt32(employeeDetails[0]),
                        Name=employeeDetails[1],
                        DepartmentFK=employeeDetails[2],
                        PassportNo=employeeDetails[3],
                        Username=employeeDetails[4],
                        Password=employeeDetails[5]
                    });
            }

            return employees.AsQueryable();
         
        }
    }
}
