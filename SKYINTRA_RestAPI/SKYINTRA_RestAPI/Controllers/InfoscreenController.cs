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
    public class InfoscreenController : Controller
    {
        private readonly IInfoscreenRepository infoscreenRep;
        private readonly IManyToMany<Infoscreen, Information> infoscreenInformationRep;

        private readonly IManyToMany<Infoscreen, FileImage> infoscreenFileImageRep;



        public InfoscreenController(IInfoscreenRepository icr, IManyToMany<Infoscreen, Information> iir, IManyToMany<Infoscreen, FileImage> ifr)
        {
            infoscreenRep = icr ?? throw new ArgumentNullException(nameof(icr));
            infoscreenInformationRep = iir ?? throw new ArgumentNullException(nameof(iir));
            infoscreenFileImageRep = ifr ?? throw new ArgumentNullException(nameof(ifr));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Infoscreen> Get()
        {
            return infoscreenRep.ReadAll();
            
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var infoscreen = infoscreenRep.Read(id);
            if (infoscreen == null)
            {
                return NotFound();
            }

            return Ok(infoscreen);
        }

        [HttpGet("{infoscreen}")]
        public IActionResult Get(string infoscreen)
        {
            var info = infoscreenRep.Read(infoscreen);
            if (info == null)
            {
                return NotFound();
            }

            return Ok(infoscreen);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        [HttpPost("InformationRelation")]
        public IActionResult PostInformationRelation([FromBody]InfoscreenInformation infoscreenInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            infoscreenInformationRep.InsertRelationship(infoscreenInformation.InfoscreenId, infoscreenInformation.InformationId);

            return Ok();
        }

        [HttpPost("InformationDeleteRelation")]
        public IActionResult DeleteInformationRelation([FromBody]InfoscreenInformation infoscreenInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            infoscreenInformationRep.DeleteRelationship(infoscreenInformation.InfoscreenId, infoscreenInformation.InformationId);

            return Ok();
        }

        [HttpPost("PDFRelation")]
        public IActionResult PostPDFRelation([FromBody]InfoscreenFileImage infoscreenFileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            infoscreenFileImageRep.InsertRelationship(infoscreenFileImage.InfoscreenId, infoscreenFileImage.FileImageId);

            return Ok();
        }

        [HttpPost("PDFDeleteRelation")]
        public IActionResult DeletePDFRelation([FromBody]InfoscreenFileImage infoscreenFileImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            infoscreenFileImageRep.DeleteRelationship(infoscreenFileImage.InfoscreenId, infoscreenFileImage.FileImageId);

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Infoscreen infoscreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newInfoscreen = infoscreenRep.Update(infoscreen);
            if (newInfoscreen == null)
            {
                return StatusCode(500);
            }
            return Ok(newInfoscreen);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
