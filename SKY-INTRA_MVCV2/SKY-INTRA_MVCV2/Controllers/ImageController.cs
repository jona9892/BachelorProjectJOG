using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers
{
    public class ImageController : Controller
    {
        private readonly IAPIGateway<String> ig;

        public ImageController(IAPIGateway<String> _imagegw)
        {
            ig = _imagegw;
        }

        //IAPIGateway<String> ig = new ImageGateway();
        // GET: Image
        public ActionResult Gallery()
        {
            IEnumerable<string> images = ig.ReadAll();
            ImageViewModel imv = new ImageViewModel
            {
                Images = images
            };

            return View(imv);
        }


    }
}