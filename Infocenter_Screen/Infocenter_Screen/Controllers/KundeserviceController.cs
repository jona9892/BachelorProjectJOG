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
    public class KundeserviceController : Controller
    {
        private readonly IGuestAPIGateway guestGateway;
        private readonly IImageManager imageMan;
        private readonly IInfoscreenManager infoscreenMan;
        private string RSSFeedURL = "http://www.dr.dk/nyheder/service/feeds/allenyheder";

        public KundeserviceController(IImageManager _imageMan, IGuestAPIGateway _guestGateway, IInfoscreenManager _infoscreenMan)
        {
            imageMan = _imageMan;
            guestGateway = _guestGateway;
            infoscreenMan = _infoscreenMan;
        }

        // GET: Kundeservice
        public ActionResult Display()
        {
            List<string> images = imageMan.GetAllImagesFromPath();
            IEnumerable<Guest> guests = guestGateway.GetTodaysGuests();
            var RSSFeedData = infoscreenMan.GetRSSFeed(RSSFeedURL);
            KundeserviceViewModel kundeservicevm = new KundeserviceViewModel
            {
                Guests = guests,
                Images = images,
                RSSFeed = RSSFeedData
            };

            return View(kundeservicevm);
        }

        public ActionResult TableData(List<Guest> todaysguests)
        {
            return PartialView("_GuestsKundeservice", todaysguests);
        }

        public ActionResult RSSFeedPartial(String RSSFeed)
        {
            return PartialView("_RSSFeedPartial", RSSFeed);
        }
    }
}