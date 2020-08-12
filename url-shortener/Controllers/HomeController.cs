using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using url_shortener.Models;

namespace url_shortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlShortenerEntities db = new UrlShortenerEntities();

        public ActionResult Index()
        {
            try
            {
                ViewBag.sUrl = "";
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult ShortenUrl(string longUrl)
        {
            try
            {
                string shortUrl = "";
                Url result = null;
                int x = longUrl.LastIndexOf(@"/");
                x = x + 1;
                string token = Guid.NewGuid().ToString().Substring(1, 6);
                shortUrl = longUrl.Substring(0, x) + token;

                result = db.Urls.SingleOrDefault(c => c.ShortenedURL == shortUrl);               
                while (result != null)
                {
                    token = Guid.NewGuid().ToString().Substring(1, 6);
                    shortUrl = longUrl.Substring(0, x) + token;
                    result = db.Urls.SingleOrDefault(c => c.ShortenedURL == shortUrl);
                }                

                Url newUrl = new Url
                {
                    Url1 = longUrl,
                    Token = token,
                    ShortenedURL = shortUrl,
                    CreatedDate = DateTime.Now
                };

                db.Urls.Add(newUrl);
                db.SaveChanges();

                ViewBag.sUrl = shortUrl;

                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
            
        }        
    }
}