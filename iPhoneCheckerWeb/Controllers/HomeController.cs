using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPhoneCheckerWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.IndexViewModel viewModel = new Models.IndexViewModel();
            return View(viewModel);
        }

        public JsonResult ListStores(iPhoneChecker.ModelCode modelCode)
            {
            iPhoneChecker.Lookup lookup = new iPhoneChecker.Lookup(
                     new Uri("https://reserve.cdn-apple.com/GB/en_GB/reserve/iPhone/stores.json"),
                    new Uri("https://reserve.cdn-apple.com/GB/en_GB/reserve/iPhone/availability.json")
                    );
            try
            {
                var stores = lookup.PhoneAvailable(modelCode);

                return new JsonResult()
                {
                    Data = new
                    {
                        success= true,
                        stores = stores
                    }
                };
            }
            catch (InvalidOperationException ex)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        success = false,
                        message = ex.Message
                    }
                };
            }
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
    }
}