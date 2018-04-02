using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.Model;
using SKY_INTRA_MVCV2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2.Controllers.InfoCenter
{
    [Authorize(Roles = "Redaktør")]
    public class PDFController : Controller
    {
        private readonly IAPIGateway<FileImage> fileimagegw;

        public PDFController(IAPIGateway<FileImage> _fileimagegw)
        {
            fileimagegw = _fileimagegw;
        }

        // GET: PDF
        public ActionResult PDFIndex()
        {

            IEnumerable<FileImage> fileImages = fileimagegw.ReadAll();

            PDFViewModel pdfvm = new PDFViewModel
            {
                FileImages = fileImages

            };

            return View(pdfvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFileImage(FileImage fileImage)
        {
            if (ModelState.IsValid)
            {
                fileImage.Title = Path.GetFileName(fileImage.Path);
                HttpResponseMessage response = fileimagegw.Add(fileImage);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("PDFIndex");
                else
                    return new HttpStatusCodeResult(response.StatusCode);
            }
            return RedirectToAction("PDFIndex");
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFileImage([Bind(Include = "Id, Title, Production, Path")]FileImage fileImage)
        {
            HttpResponseMessage response = fileimagegw.Delete(fileImage);

            return RedirectToAction("PDFIndex");
        }
    }
}