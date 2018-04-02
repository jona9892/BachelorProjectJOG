using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.Model;
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
    [Authorize(Roles = "Funktionærer")]
    public class GuestController : Controller
    {
        private readonly IGuestGateway gg;
        public GuestController(IGuestGateway _guestgw)
        {
            gg = _guestgw; 
        }
        
        // GET: Gæster
        public ActionResult GuestIndex()
        {
            IEnumerable<Guest> allGuests = gg.ReadAll();
            var guestvm = new GuestViewModel
            {
                Guest = new Guest()
                {
                    Dato = DateTime.Today
                },
                Guests = allGuests,
                countries = {"Danmark",
                            "Belgien",
                            "England",
                            "Frankrig",
                            "Holland",
                            "Italien",
                            "Norge",
                            "Polen",
                            "Spanien",
                            "Sverige",
                            "Tjekkiet",
                            "Tyskland"}
            };



            return View(guestvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name, Company, Country, Dato")]Guest guest)
        {
            if (ModelState.IsValid)
            {
                guest.Country = guest.Country + ".png";
                HttpResponseMessage response = gg.Add(guest);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("GuestIndex");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("GuestIndex");
        }        

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = gg.Read(id.Value);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id, Name, Company, Country, Date")]Guest guest)
        {
            HttpResponseMessage response = gg.Delete(guest);
            
                return RedirectToAction("GuestIndex");
        }


    }
}