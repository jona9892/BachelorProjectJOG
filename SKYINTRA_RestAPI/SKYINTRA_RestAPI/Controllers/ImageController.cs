using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKYINTRA_RestAPI.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKYINTRA_RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private Manager manager = new Manager();
        private string ImagesPath = @"C:\Users\jog\Desktop\SKY_INTRA\Billeder";

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return manager.GetFiles(ImagesPath, ".jpeg", ".png", ".jpg");
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
