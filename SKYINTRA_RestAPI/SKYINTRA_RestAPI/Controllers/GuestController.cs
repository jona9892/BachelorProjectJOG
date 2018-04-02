using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using SKYINTRA_RestAPI.BLL.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKYINTRA_RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class GuestController : Controller
    {
        private readonly IGuestRepository guestRep;
        private readonly IGuestManager guestMan;

        public GuestController(IGuestRepository gr, IGuestManager gm)
        {
            guestRep = gr ?? throw new ArgumentNullException(nameof(gr));
            guestMan = gm ?? throw new ArgumentNullException(nameof(gm));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Guest> Get()
        {
            var guests = guestRep.ReadAll();
            return guests;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var guest = guestRep.Read(id);
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newGuest = guestMan.AddGuest(guest);
            if (newGuest == null)
            {
                return NotFound();
            }
            return Ok(newGuest);
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, Guest guest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var newGuest = guestRep.Update(guest);
        //    if (newGuest == null)
        //    {
        //        return StatusCode(500);
        //    }
        //    return Ok(newGuest);
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var guest = guestRep.Read(Convert.ToInt32(id));
            guestRep.Delete(guest);
        }

        [Route("GuestsToday")]
        public IEnumerable<Guest> GetTodaysGuests()
        {
             var guests = guestRep.ReadTodaysGuests();
            return guests;
        }
    }
}
