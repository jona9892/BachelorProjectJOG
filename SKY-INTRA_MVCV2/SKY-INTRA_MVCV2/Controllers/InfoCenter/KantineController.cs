using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.MessagingGateway;
using ServiceGateway.MessagingGateway.Abstraction;
using ServiceGateway.Model;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers.InfoCenter
{
    [Authorize(Roles = "Funktionærer")]
    public class KantineController : Controller
    {
        private readonly IAPIGateway<String> imagegw;
        private readonly IInfoscreenGateway infoscreengw;
        private readonly IInformationGateway informationgw;
        private readonly IAPIGateway<FileImage> fileimagegw;
        private readonly IMessagingGateway infoscreenMessaging; 

        public KantineController(IAPIGateway<String> _imagegw, IInfoscreenGateway _infoscreengw, IInformationGateway _informationgw,
            IAPIGateway<FileImage> _fileimagegw, IMessagingGateway _infoscreenMessaging)
        {
            imagegw = _imagegw;
            infoscreengw = _infoscreengw;
            informationgw = _informationgw;            
            fileimagegw = _fileimagegw;
            infoscreenMessaging = _infoscreenMessaging;
        }

        // GET: Kantine
        public ActionResult KantineIndex()
        {
            IEnumerable<FileImage> fileImages = fileimagegw.ReadAll();
            List<FileImage> chosenFileImages = infoscreengw.Read(2).InfoscreenFileImages.Select(x => x.FileImage).ToList();
            List<FileImage> filteredFileImages = fileImages.Where(p => !chosenFileImages.Any(l => p.Id == l.Id)).ToList();

            List<Information> informations = informationgw.ReadAll().ToList();
            List<Information> chosenInformations = new List<Information>();
            if (infoscreengw.Read(2).InfoscreenInformations.Count != 0)
            {
                chosenInformations = infoscreengw.Read(2).InfoscreenInformations.Select(x => x.Information).ToList();
            }
            List<Information> filteredInformations = informations.Where(p => !chosenInformations.Any(l => p.Id == l.Id)).ToList();



            Infoscreen infoscreen = infoscreengw.Read(2);
            KantineViewModel kvm = new KantineViewModel
            {
                Informations = filteredInformations,
                ChosenInformations = chosenInformations,
                Infoscreen = infoscreen,
                EkstrudFileImagePath = infoscreengw.Read(2).EkstruderingFileImage,
                TermoFileImagePath = infoscreengw.Read(2).TermoformFileImage,
                FileImages = fileimagegw.ReadAll()
            };

            return View(kvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSmallFileImage(KantineViewModel kantinevm)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = infoscreengw.Update(kantinevm.Infoscreen);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("KantineIndex");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return View(kantinevm);

        }

        public ActionResult InsertRelation(int id)
        {
            InfoscreenInformation newInfoscreenInformaiton = new InfoscreenInformation();
            newInfoscreenInformaiton.InformationId = id;
            newInfoscreenInformaiton.InfoscreenId = 2;
            infoscreengw.InsertInformationRelation(newInfoscreenInformaiton);

            return Json("Informations besked tilføjet!");
        }

        public ActionResult DeleteRelation(int id)
        {
            InfoscreenInformation newInfoscreenInformaiton = new InfoscreenInformation();
            newInfoscreenInformaiton.InformationId = id;
            newInfoscreenInformaiton.InfoscreenId = 2;
            infoscreengw.DeleteInformationRelation(newInfoscreenInformaiton);

            return Json("Informations besked slettet!");
        }

        [HttpGet]
        public void UpdateInfoscreen()
        {
            infoscreenMessaging.update("Kantine");
        }
    }
}