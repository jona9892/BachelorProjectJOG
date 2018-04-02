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
    public class InformationController : Controller
    {
        private readonly IInformationRepository informationRep;

        public InformationController(IInformationRepository ir)
        {
            informationRep = ir ?? throw new ArgumentNullException(nameof(ir));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Information> Get()
        {
            var informations = informationRep.ReadAll();
            return informations;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var information = informationRep.Read(id);
            if (information == null)
            {
                return NotFound();
            }

            return Ok(information);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Information information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newGuest = informationRep.Add(information);
            if (newGuest == null)
            {
                return NotFound();
            }
            return Ok(newGuest);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Information information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newInformation = informationRep.Update(information);
            if (newInformation == null)
            {
                return StatusCode(500);
            }
            return Ok(newInformation);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var information = informationRep.Read(Convert.ToInt32(id));
            informationRep.Delete(information);
        }

        [Route("LatestInformations")]
        public IEnumerable<Information> GetLatestInformations()
        {
            var informations = informationRep.GetLatestInforamtions();
            return informations;
        }
    }
}
