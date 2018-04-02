using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers
{
    public class InfoCenterController : Controller
    {
        // GET: InfoCenter
        public ActionResult Index()
        {
            return View();
        }

        // GET: InfoCenter
        public ActionResult Kundeservice()
        {
            return View();
        }

        public ActionResult Kantine()
        {
            return View();
        }

        public ActionResult Ekstrudering()
        {
            return View();
        }

        public ActionResult Termoform()
        {
            return View();
        }
    }
}