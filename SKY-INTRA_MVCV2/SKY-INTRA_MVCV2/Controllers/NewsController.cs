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
    public class NewsController : Controller
    {
        private readonly IAPIGateway<Information> ig;

        public NewsController(IAPIGateway<Information> _informationgw)
        {
            ig = _informationgw;
        }

        IAPIGateway<News> ng = new NewsGateway();
        /*IAPIGateway<Information> ig = new InformationGateway()*/

        // GET: Information
        public ActionResult News()
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

            IEnumerable<News> allNews = ng.ReadAll();
            News News = ng.Read(id);
            NewsViewModel ivm = new NewsViewModel()
            {
                News = allNews,
                NewsToEdit = News
            };

            return View(ivm);
        }


        //public ActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News NewInformation)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = ng.Add(NewInformation);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("News");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("News");
        }

        public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
        {
            public RequireRequestValueAttribute(string valueName)
            {
                ValueName = valueName;
            }
            public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
            {
                return (controllerContext.HttpContext.Request[ValueName] != null);
            }

            public string ValueName { get; private set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News InformationToEdit)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = ng.Update(InformationToEdit);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("News");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("News");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id, Title, Header, Body")]News News)
        {
            HttpResponseMessage response = ng.Delete(News);

            return RedirectToAction("News");
        }
    }
}