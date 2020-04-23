using Eshop.Models.Checkout;
using Eshop.Models.Tables;
using Eshop.Models.TemporaryClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Cart()
        {
            return View(Session["listOfProducts"]);
        }

        public ActionResult CheckoutCustomer()
        {
            var model = SetingUpBigModel(new Information());
            return View("CheckoutCustomer", "~/Views/Shared/EmptyLayout.cshtml", model);
        }

        public ActionResult CheckoutShipping()
        {
            return View();
        }

        public ActionResult CheckoutPayment()
        {
            return View();
        }

        [HttpPost]
        public void AddingProductsToCart(ProductLabelImages product)
        {
            CheckIfSessionExists();

            var ifProductExists = ((List<ProductLabelImages>)Session["listOfProducts"]).Find((x) => x.Name == product.Name);

            if (ifProductExists != null)
            {
                ((List<ProductLabelImages>)Session["listOfProducts"]).Remove(ifProductExists);

                product.Quantity += ifProductExists.Quantity;

                ((List<ProductLabelImages>)Session["listOfProducts"]).Add(product);
            }
            else
            {
                ((List<ProductLabelImages>)Session["listOfProducts"]).Add(product);
            }
        }

        [HttpPost]
        public ActionResult InformatinAboutCustomer(Information information)
        {
            if (ModelState.IsValid)
            {
                Session["InforamtionAboutCusotmer"] = information;
                RedirectToAction("Index", "Home");
            }

            var model = SetingUpBigModel(information);
            return View("CheckoutCustomer", "~/Views/Shared/EmptyLayout.cshtml", model);
        }

        [HttpPost]
        public void UpdateSession(int[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                ((List<ProductLabelImages>)Session["listOfProducts"])[i].Quantity = inputs[i];
            }
        }

        [HttpGet]
        public ActionResult DeleteProductFromCart(int id)
        {
            
            if (((List<ProductLabelImages>)Session["listOfProducts"]).Count == 1)
            {
                return ClearSession();
            }

            var product = ((List<ProductLabelImages>)Session["listOfProducts"]).Where(x => x.Id == id).FirstOrDefault();
            ((List<ProductLabelImages>)Session["listOfProducts"]).Remove(product);
            return View("Cart", Session["listOfProducts"]);
        }

        public ActionResult ClearSession()
        {
            Session["listOfProducts"] = null;
            return View("CartWithoutProducts");
        }

        private void CheckIfSessionExists()
        {
            if (Session["listOfProducts"] == null)
            {
                Session["listOfProducts"] = new List<ProductLabelImages>();
            }
        }

        private BigModelForInformation SetingUpBigModel(Information information)
        {
            var model = new BigModelForInformation();
            model.Data = (List<ProductLabelImages>)Session["listOfProducts"];
            model.InformationAboutCustomer = information;
            return model;
        }
    }
}
