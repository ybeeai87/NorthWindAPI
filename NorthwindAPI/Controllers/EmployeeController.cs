using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {       //get all = api/Employee/getAll
        [HttpGet("getAll")]
        public List<Employee> GetAllEmployees()
        {
            List<Employee> result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Employees.ToList();
            }
            return result;
        }
        //get by Name = api/Employee/getByLastName?lastName=Smith
        [HttpGet("getByLastName")]
        public Employee GetByLastName(string lastName)
        {
            Employee result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Employees.ToList().Find(a => a.LastName == lastName);
            }
            return result;
        }
        //add product = api/Employee/add?(all the parameters with & in betweeen)
        [HttpPost("add")]
        public Employee AddEmployee(int employeeId, string lastName, string firstName, string title, string titleOfCourtesy, string address, string city, string postalCode, int reportsTo)
        {
            Employee newEmployee = new Employee();
            newEmployee.LastName = lastName;
            newEmployee.FirstName = firstName;
            newEmployee.FirstName = firstName;
            newEmployee.Title = title;
            newEmployee.TitleOfCourtesy = titleOfCourtesy;
            newEmployee.Address = address;
            newEmployee.City = city;
            newEmployee.PostalCode = postalCode;
            newEmployee.ReportsTo = reportsTo;

            using (NorthwindContext context = new NorthwindContext())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }
            return newEmployee;
        }
    }
}
