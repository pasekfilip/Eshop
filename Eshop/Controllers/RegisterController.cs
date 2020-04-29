using Eshop.Models;
using Eshop.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class RegisterController : Controller
    {
        private MyContext _context = MyContext.GetInstance();

        [HttpGet]
        // GET: Register
        public ActionResult Index()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (this.ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Home", "Index");
            }

            return View(user);
        }
    }
}