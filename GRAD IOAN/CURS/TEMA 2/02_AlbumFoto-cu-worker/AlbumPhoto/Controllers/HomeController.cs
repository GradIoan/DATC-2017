using AlbumPhoto.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlbumPhoto.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var service = new AlbumFotoService();
            return View(service.GetPoze());
        }

        public ActionResult ViewComments(string pictureName)
        {
            try
            {
                var service = new AlbumFotoService();
                return View(service.GetComments(pictureName));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddComments()
        {
            try
            {
                var service = new AlbumFotoService();
                string userName = Request["txtUserName"].ToString();
                string comment = Request["txtComment"].ToString();
                string pictureName = Request["picName"].ToString();
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(comment))
                {
                    service.AddComment(userName, comment, pictureName);
                }
                return View("Index", service.GetPoze());
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult IncarcaPoza(HttpPostedFileBase file)
        {
            var service = new AlbumFotoService();
            if (file!=null && file.ContentLength > 0)
            {
                service.IncarcaPoza("guest", file.FileName, file.InputStream);
            }

            return View("Index", service.GetPoze());
        }

        public ActionResult GetLink(string pictureName)
        {
            try
            {
                var service = new AlbumFotoService();
                string link = service.GetLink(pictureName);
                ViewData["LINK"] = link;
                return View();
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
