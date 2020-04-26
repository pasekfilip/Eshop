using Eshop.Models.Checkout;
using Eshop.Models.Tables;
using Eshop.Models.TemporaryClasses;
using System;
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
            Information information = (Information)Session["InformationAboutCustomer"] ?? new Information();
            var model = SetingUpBigModel(information);
            return View("CheckoutCustomer", "~/Views/Shared/EmptyLayout.cshtml", model);
        }

        public ActionResult CheckoutSuccess()
        {
            var information = (Information)Session["InformationAboutCustomer"];
            var billingAdress = new BillingInformation();
            billingAdress.FirstName = information.FirstName;
            billingAdress.LastName = information.LastName;
            billingAdress.Address = information.Address;
            billingAdress.City = information.City;
            billingAdress.Country = information.Country;
            billingAdress.PostalCode = information.PostalCode;
            Session["billingAdress"] = billingAdress;
            ViewBag.TotalSum = CountTotalSum();
            var model = SetingUpBigModel(billingAdress);
            return View(model);
        }

        public ActionResult CheckoutShipping()
        {
            var model = SetingUpBigModel((Information)Session["InformationAboutCustomer"]);
            return View(model);
        }

        public ActionResult CheckoutPayment()
        {
            var model = new BillingInformation();
            Session["HashCode"] = model.GetHashCode();
            return View(model);
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
            }

            ((List<ProductLabelImages>)Session["listOfProducts"]).Add(product);
        }

        [HttpPost]
        public ActionResult InformatinAboutCustomer(Information information)
        {
            var model = SetingUpBigModel(information);
            if (ModelState.IsValid)
            {
                Session["InformationAboutCustomer"] = information;
                return View("CheckoutShipping", model);
            }

            return View("CheckoutCustomer", "~/Views/Shared/EmptyLayout.cshtml", model);
        }

        public ActionResult ValidateBillingAdress(BillingInformation information)
        {
            if (ModelState.IsValid)
            {
                Session["BillingAddress"] = information;
                ViewBag.TotalSum = CountTotalSum();
                var model = SetingUpBigModel(information);
                return View("CheckoutSuccsess", model);
            }

            return View("CheckoutPayment", information);
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

        private BigModelForInformation SetingUpBigModel(BillingInformation information)
        {
            var model = new BigModelForInformation();
            model.Data = (List<ProductLabelImages>)Session["listOfProducts"];
            model.InformationAboutCustomer = (Information)Session["InformationAboutCustomer"];
            model.BillingInformation = information;
            return model;
        }

        private string CountTotalSum()
        {
            decimal total = 0M;
            foreach (var item in (List<ProductLabelImages>)Session["listOfProducts"])
            {
                total += item.Price * item.Quantity;
            }
            total += 0.24M;
            return total.ToString("0.00");
        }
    }
}
