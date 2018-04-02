using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DTOModel;
using EasyNetQ;
using Infocenter_Screen.BLL;
using Infocenter_Screen.BLL.Abstraction;
using Infocenter_Screen.Models.ViewModels;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceGateway.APIGateway.Abstraction;

namespace Infocenter_Screen.Controllers
{
    public class KantineController : Controller
    {
        private readonly IGuestAPIGateway guestGateway;
        private readonly IInfoscreenAPIGateway infoscreenGateway;
        private readonly IInfoscreenManager infoscreenMan;
        private readonly IImageManager imageMan;

        public KantineController(IGuestAPIGateway _guestGateway, IInfoscreenManager _infoscreenMan, IImageManager _imageMan, IInfoscreenAPIGateway _infoscreenGateway)
        {
            guestGateway = _guestGateway;
            infoscreenMan = _infoscreenMan;
            imageMan = _imageMan;
            infoscreenGateway = _infoscreenGateway; 
        }

        // GET: Kantine
        public ActionResult Display()
        {
            Infoscreen infoscreen = infoscreenGateway.Read(2);

            IEnumerable<Guest> guests = guestGateway.GetTodaysGuests();

            List<InfoscreenInformation> infoscreenInformations = infoscreen.InfoscreenInformations.ToList();
            List<Information> informations = new List<Information>();
            if (infoscreenInformations.Count() != 0)
            {
                informations = infoscreenInformations.Select(x => x.Information).ToList();
            }
            
            List<string> images = imageMan.GetAllImagesFromPath();

            List<InfoscreenFileImage> infoscreenFileImages = infoscreen.InfoscreenFileImages.ToList();
            List<FileImage> fileImages = infoscreenGateway.Read(2).InfoscreenFileImages.Select(x => x.FileImage).ToList();
            var RSSFeedData = infoscreenMan.GetRSSFeed(infoscreenMan.GetRSSFeedPath(1));
            string ekstrudPath = infoscreen.EkstruderingFileImage.Path;
            string termoPath = infoscreen.TermoformFileImage.Path;


            KantineViewModel kvm = new KantineViewModel
            {
                Guests = guests,
                Informations = informations,
                Images = images,
                RSSFeed = RSSFeedData,
                EkstruderingProd = ekstrudPath,
                TermoformProd = termoPath,
                Reports = fileImages
            };


            return View(kvm);
        }


        public ActionResult TodaysGuestsPartial(List<Guest> todaysguests)
        {
            return PartialView("_TodaysGuests", todaysguests);
        }

        public ActionResult InformationsPartial(List<Information> informations)
        {
            return PartialView("_InformationPartial", informations);
        }

        public ActionResult RSSFeedPartial(String RSSFeed)
        {
            return PartialView("_RSSFeedPartial", RSSFeed);
        }

        public ActionResult GetImg(string path)
        {
            if (path != null)
            {
                string pathsplit = path.Split('.')[0];
                //var path = $@"C:\temp\images\{id}.png";
                var bytes = System.IO.File.ReadAllBytes(pathsplit + ".png");
                return File(bytes, "image/png");
            }
            return null;

        }
    }
}