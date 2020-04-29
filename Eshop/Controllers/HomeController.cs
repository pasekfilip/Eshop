using Eshop.Models.Repo;
using Eshop.Models.Tables;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class HomeController : Controller
    {
        private ProductRepository _repository = new ProductRepository();
        public ActionResult Index()
        {
            dynamic list = new ExpandoObject();
            list.TopSell = _repository.GetEightTopSoldProducts();
            list.TopRated = _repository.GetBestRated();
            list.Featured = _repository.GetThreeProducts();
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OnProductClick(int id)
        {
            dynamic list = new ExpandoObject();
            ProductLabelImages product = _repository.GetProductById(id);
            list.ProductById = product;
            list.RelatedProducts = _repository.GetRelatedProducts(product);
            return View("ProductDetail", list);
        }

        public PartialViewResult TopSell()
        {
            List<ProductLabelImages> list = _repository.GetEightTopSoldProducts();
            return PartialView("Categories", list);
        }

        public PartialViewResult BestSellers()
        {
            List<ProductLabelImages> list = _repository.GetEightBestSellers();
            return PartialView("Categories", list);
        }

        public PartialViewResult NewArrivals()
        {
            List<ProductLabelImages> list = _repository.GetEightNewArrivals();
            return PartialView("Categories", list);
        }
    }

}