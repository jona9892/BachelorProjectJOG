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
    public class EkstruderingController : Controller
    {
        private readonly IAPIGateway<String> imagegw;
        private readonly IInfoscreenGateway infoscreengw;
        private readonly IInformationGateway informationgw;
        private readonly IAPIGateway<SmallFileImage> smallfilegw;
        private readonly IAPIGateway<FileImage> fileimagegw;
        private readonly IMessagingGateway infoscreenMessaging;

        public EkstruderingController(IAPIGateway<String> _imagegateway, IInfoscreenGateway _infoscreengateway,
            IInformationGateway _informationgateway, IAPIGateway<SmallFileImage> _smallfilegateway, IAPIGateway<FileImage> _fileimagegateway, IMessagingGateway _infoscreenMessaging)
        {
            imagegw = _imagegateway;
            infoscreengw = _infoscreengateway;
            informationgw = _informationgateway;
            smallfilegw = _smallfilegateway;
            fileimagegw = _fileimagegateway;
            infoscreenMessaging = _infoscreenMessaging;
        }

        // GET: Ekstrudering
        public ActionResult EkstruderingIndex()
        {
            IEnumerable<FileImage> fileImages = fileimagegw.ReadAll();
            IEnumerable<SmallFileImage> smallFileImages = smallfilegw.ReadAll();

            List<Information> informations = informationgw.ReadAll().ToList();
            List<Information> chosenInformations = infoscreengw.Read(2).InfoscreenInformations.Select(x => x.Information).ToList();
            List<Information> filteredInformations = informations.Where(p => !chosenInformations.Any(l => p.Id == l.Id)).ToList();

            Infoscreen infoscreen = infoscreengw.Read(2);

            List<SmallFileImage> ekstrudFileImages = smallFileImages.Where(x => x.Production == "Ekstrudering").ToList();

            EkstruderingViewModel kvm = new EkstruderingViewModel
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
            infoscreenMessaging.update("Ekstrudering");
        }
    }
}