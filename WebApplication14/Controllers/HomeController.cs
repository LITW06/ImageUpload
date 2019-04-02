using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ImageUpload.Data;

namespace WebApplication14.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            var images = mgr.Get();
            return View(images);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, string description)
        {
            string ext = Path.GetExtension(image.FileName);
            string fileName = $"{Guid.NewGuid()}{ext}";
            string fullPath = $"{Server.MapPath("/UploadedImages")}\\{fileName}";
            image.SaveAs(fullPath);
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            mgr.SaveImage(new Image
            {
                FileName = fileName,
                Description = description
            });
            return Redirect("/");
        }
    }
}