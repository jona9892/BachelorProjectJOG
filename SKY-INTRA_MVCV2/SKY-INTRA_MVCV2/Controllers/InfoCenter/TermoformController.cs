using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.MessagingGateway.Abstraction;
using ServiceGateway.Model;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers.InfoCenter
{
    [Authorize(Roles = "Funktionærer")]
    public class TermoformController : Controller
    {
        private readonly IInfoscreenGateway infoscreengw;
        private readonly IInformationGateway informationgw;
        private readonly IAPIGateway<SmallFileImage> smallfilegw;
        private readonly IAPIGateway<FileImage> fileimagegw;
        private readonly IMessagingGateway infoscreenMessaging;

        public TermoformController(IInfoscreenGateway _infoscreengw, IInformationGateway _informationgw, IAPIGateway<SmallFileImage> _smallfilegw, IAPIGateway<FileImage> _fileimagegw, IMessagingGateway _infoscreenMessaging)
        {
            infoscreengw = _infoscreengw;
            informationgw = _informationgw;
            smallfilegw = _smallfilegw;
            fileimagegw = _fileimagegw;
            infoscreenMessaging = _infoscreenMessaging;
        }

        // GET: Termoform
        public ActionResult TermoformIndex()
        {
            IEnumerable<FileImage> fileImages = fileimagegw.ReadAll();
            IEnumerable<SmallFileImage> smallFileImages = smallfilegw.ReadAll();

            List<Information> informations = informationgw.ReadAll().ToList();
            List<Information> chosenInformations = infoscreengw.Read(2).InfoscreenInformations.Select(x => x.Information).ToList();
            List<Information> filteredInformations = informations.Where(p => !chosenInformations.Any(l => p.Id == l.Id)).ToList();

            Infoscreen infoscreen = infoscreengw.Read(2);

            List<SmallFileImage> termoFileImages = smallFileImages.Where(x => x.Production == "Termoform").ToList();
            TermoformViewModel kvm = new TermoformViewModel
            {
                Informations = filteredInformations,
                ChosenInformations = chosenInformations,
                Infoscreen = infoscreen
            };

            return View(kvm);
        }

        [HttpGet]
        public void UpdateInfoscreen()
        {
            infoscreenMessaging.update("Termoform");
        }
    }
}