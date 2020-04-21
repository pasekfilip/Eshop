using Eshop.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class TestingController : Controller
    {
        private ProductRepository _repository = new ProductRepository();
        // GET: Testing
        public ActionResult Index()
        {
            return View();
        }



    }
}