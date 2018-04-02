using DTOModel;
using Infocenter_Screen.BLL.Abstraction;
using Infocenter_Screen.Models.ViewModels;
using ServiceGateway.APIGateway.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infocenter_Screen.Controllers
{
    public class TermoformController : Controller
    {
        private readonly IGuestAPIGateway guestGateway;
        private readonly IInfoscreenAPIGateway infoscreenGateway;
        private readonly IInfoscreenManager infoscreenMan;
        private readonly IImageManager imageMan;

        public TermoformController(IGuestAPIGateway _guestGateway, IInfoscreenManager _infoscreenMan, IImageManager _imageMan, IInfoscreenAPIGateway _infoscreenGateway)
        {
            guestGateway = _guestGateway;
            infoscreenMan = _infoscreenMan;
            imageMan = _imageMan;
            infoscreenGateway = _infoscreenGateway;
        }

        // GET: Kantine
        public ActionResult Display()
        {
            Infoscreen infoscreen = infoscreenGateway.Read(3);

            IEnumerable<Guest> guests = guestGateway.GetTodaysGuests();
            List<Information> informations = infoscreenMan.GetInformationFromInfoscreen(2);
            List<string> images = imageMan.GetAllImagesFromPath();
            var RSSFeedData = infoscreenMan.GetRSSFeed(infoscreenMan.GetRSSFeedPath(1));
            string ekstrudPath = infoscreen.EkstruderingFileImage.Path;
            string termoPath = infoscreen.TermoformFileImage.Path;

            TermoformViewModel kvm = new TermoformViewModel
            {
                Guests = guests,
                Informations = informations,
                Images = images,
                RSSFeed = RSSFeedData,
                TermoformProd = termoPath
            };


            return View(kvm);
        }

        public ActionResult InformationsPartial(List<Information> informations)
        {
            return PartialView("_InformationPartial", informations);
        }

        public ActionResult TodaysGuestsPartial(List<Guest> todaysguests)
        {
            return PartialView("_TodaysGuests", todaysguests);
        }

        public ActionResult RSSFeedPartial(String RSSFeed)
        {
            return PartialView("_RSSFeedPartial", RSSFeed);
        }
    }
}