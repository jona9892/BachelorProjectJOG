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
    public class FileImageController : Controller
    {
        private readonly IRepository<FileImage> fileImageRep;

        public FileImageController(IRepository<FileImage> fir)
        {
            fileImageRep = fir ?? throw new ArgumentNullException(nameof(fir));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<FileImage> Get()
        {
            var fileImages = fileImageRep.ReadAll();
            return fileImages;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //var fileImage = fileImageRep.Read(id);
            //if (fileImage == null)
            //{
            //    return NotFound();
            //}

            return Ok("Ok");
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]FileImage fileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSmallFileImage = fileImageRep.Add(fileImage);
            if (newSmallFileImage == null)
            {
                return NotFound();
            }
            return Ok(newSmallFileImage);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FileImage fileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newFileImage = fileImageRep.Update(fileImage);
            if (newFileImage == null)
            {
                return StatusCode(500);
            }
            return Ok(newFileImage);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var fileImage = fileImageRep.Read(Convert.ToInt32(id));
            fileImageRep.Delete(fileImage);
        }
    }
}
