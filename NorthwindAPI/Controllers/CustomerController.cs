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
    public class CustomerController : ControllerBase
    {
         //get all = api/Customer/getAll
        [HttpGet("getAll")]
        public List<Customer> GetAllCustomers()
        {
            List<Customer> result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Customers.ToList();
            }
            return result;
        }
        //get by Name = api/Customer/getByCompanyName?name=Acme
        [HttpGet("getByCompanyName")]
        public Customer GetByCompanyName(string name)
        {
            Customer result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Customers.ToList().Find(a => a.CompanyName == name);
            }
            return result;
        }
    }
}
