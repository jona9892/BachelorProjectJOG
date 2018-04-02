using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKYINTRA_RestAPI.BLL.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKYINTRA_RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository employeeRep;
        private readonly IEmployeeManager employeeMan;

        public EmployeeController(IEmployeeRepository er, IEmployeeManager em)
        {
            employeeRep = er ?? throw new ArgumentNullException(nameof(er));
            employeeMan = em ?? throw new ArgumentNullException(nameof(em));
        }

        // GET: api/<controller>
        [Route("AnniversaryWeek")]
        public IEnumerable<Anniversary> GetAnniveraries()
        {
            List<Anniversary> result = new List<Anniversary>();
            result.AddRange(employeeMan.GetBirthdaysWeek());
            result.AddRange(employeeMan.GetEmploymentsWeek());

            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var employee = employeeRep.GetEmployee(name);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);

        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
