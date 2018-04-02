using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.MessagingGateway.Abstraction;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers.InfoCenter
{
    [Authorize(Roles = "Funktionærer")]
    public class KundeServiceController : Controller
    {
        IAPIGateway<String> ig = new ImageGateway();
        private readonly IMessagingGateway infoscreenMessaging;
        public KundeServiceController(IMessagingGateway _infoscreenMessaging)
        {
            infoscreenMessaging = _infoscreenMessaging;
        }
        // GET: KundeService
        public ActionResult Kundeservice()
        {
            IEnumerable<string> images = ig.ReadAll();
            ImageViewModel imv = new ImageViewModel
            {
                Images = images
            };
            return View(imv);
        }

        [HttpGet]
        public void UpdateInfoscreen()
        {
            infoscreenMessaging.update("Kundeservice");
        }
    }
}