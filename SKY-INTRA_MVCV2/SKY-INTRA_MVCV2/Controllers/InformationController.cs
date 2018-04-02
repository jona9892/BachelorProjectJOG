using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.Model;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers
{
    [Authorize(Roles = "Redaktør")]
    public class InformationController : Controller
    {
        private readonly IInformationGateway ig;
        public InformationController(IInformationGateway _informationgw)
        {
            ig = _informationgw;
        }
        
        // GET: Information
        public ActionResult Information()
        {

            IEnumerable<Information> informations = ig.ReadAll();
            InformationViewmodel ivm = new InformationViewmodel()
            {
                Informations = informations,
                InformationToEdit = informations.FirstOrDefault()
            };

            return View(ivm);
        }
        
        public ActionResult InformationDetails(int id)
        {

            IEnumerable<Information> informations = ig.ReadAll();
            Information information = ig.Read(id);
            InformationViewmodel ivm = new InformationViewmodel()
            {
                Informations = informations,
                InformationToEdit = information
            };

            return View(ivm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Information NewInformation)
        {
            if (ModelState.IsValid)
            {
                DateTime Now = DateTime.Now;
                NewInformation.ModifiedDate = Now;
                NewInformation.CreatedDate = Now;
                NewInformation.CreatedBy = User.Identity.Name;
                HttpResponseMessage response = ig.Add(NewInformation);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Information");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("Information");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Information InformationToEdit)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = ig.Update(InformationToEdit);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Information"); 
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("Information");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id, Title, Header, Body")]Information information)
        {
            HttpResponseMessage response = ig.Delete(information);

            return RedirectToAction("Information");
        }
    }
}