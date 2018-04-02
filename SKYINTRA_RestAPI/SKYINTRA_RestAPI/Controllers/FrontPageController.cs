using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKYINTRA_RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class FrontPageController : Controller
    {
        private readonly IFrontPageRepository fpr;
        public FrontPageController(IFrontPageRepository _fpr)
        {
            fpr = _fpr ?? throw new ArgumentNullException(nameof(_fpr));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FrontPage frontPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newFrontPage = fpr.Update(frontPage);
            if (newFrontPage == null)
            {
                return StatusCode(500);
            }
            return Ok(newFrontPage);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("Frontpage")]
        public IActionResult GetFirst()
        {
            var frontpage = fpr.GetFirst();
            if (frontpage == null)
            {
                return NotFound();
            }

            return Ok(frontpage);
        }
    }
}
