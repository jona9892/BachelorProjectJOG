using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.Model;
using SKY_INTRA_MVCV2.Manager;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeGateway employeeGateway;
        private readonly IGuestGateway guestGateway;
        private readonly IInformationGateway informationGateway;
        private readonly FrontPageGateway frontpageGateway = new FrontPageGateway();
        private readonly IAPIGateway<FileImage> fileimagegw;
        ImageManager ImageManager = new ImageManager();


        public HomeController(IGuestGateway _guestGateway, IInformationGateway _informationGateway, IEmployeeGateway _employeeGateway, IAPIGateway<FileImage> _fileimagegw)
        {
            guestGateway = _guestGateway;
            informationGateway = _informationGateway;
            employeeGateway = _employeeGateway;
            fileimagegw = _fileimagegw;
        }

        public ActionResult Index()
        {
            IEnumerable<FileImage> fileImages = fileimagegw.ReadAll();
            var todaysGuests = guestGateway.GetTodaysGuests();
            var latestInformations = informationGateway.GetLatestInformations();
            var frontpage = frontpageGateway.GetFrontpage();
            var anniversaries = employeeGateway.GetAnniversaries();
            ImageManager.SavePDFAsImageTrim(@"C:\Users\jog\Desktop\SKY_INTRA\Produktionstal", @"C:\Users\jog\Desktop\SKY_INTRA\Produktionstal\");

            var ekstrudFilePath = new FileImage(); 
            var termoFilePath = new FileImage();

            if (frontpage != null)
            {
                ekstrudFilePath = frontpageGateway.GetFrontpage().EkstruderingFileImage;
            }

            if (frontpage != null)
            {
                termoFilePath = frontpageGateway.GetFrontpage().TermoformFileImage;
            }

            foreach (var ani in anniversaries)
            {
                var month = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(ani.date.Month);

                ani.MonthName = month;
            }

            HomeViewModel hvm = new HomeViewModel
            {
                Guests = todaysGuests,
                Informations = latestInformations,
                anniversaries = anniversaries,
                EkstrudFileImage = ekstrudFilePath,
                TermoFileImage = termoFilePath, 
                FileImages = fileImages
            };

            return View(hvm);
        }

        public ActionResult GetImg(string path)
        {
            if(path != null)
            {
                string pathsplit = path.Split('.')[0];
                var bytes = System.IO.File.ReadAllBytes(pathsplit + ".png");
                return File(bytes, "image/png");
            }
            return null;
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Nyheder()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Funktionær")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEkstrudProduction(HomeViewModel homevm)
        {
            if (ModelState.IsValid)
            {
                FrontPage fp = new FrontPage
                {
                    Id = 1, 
                    EkstruderingFileImage = homevm.EkstrudFileImage,
                    TermoformFileImage = homevm.TermoFileImage

                };
                HttpResponseMessage response = frontpageGateway.Update(fp);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return View(homevm);

        }


        public ActionResult InformationDetailsModal(int id)
        {
            Information information = informationGateway.Read(id);

            // to do  : You can pass some data (view model) to the partial view as needed
            return PartialView(information);
        }
    }
}