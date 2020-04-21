using Eshop.Models;
using Eshop.Models.Repo;
using Eshop.Models.Tables;
using System.Linq;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class LoginController : Controller
    {
        private UserRepository _repository = new UserRepository();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            User result = this._repository.CheckIfUserExists(user);
            if (result != null)
            {
                this.Session["Token"] = true;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "UserName or Password is incorrect");
            return View(user);
        }

        public ActionResult Logout()
        {
            this.Session["Token"] = null;
            return RedirectToAction("Index", "Login");
        }

    }
}