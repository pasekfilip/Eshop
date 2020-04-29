using Eshop.Models.Checkout;
using Eshop.Models.Repo;
using Eshop.Models.Tables;
using Eshop.Models.TemporaryClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly OrderRepository _repository = new OrderRepository();

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

            Session["BillingAddress"] = billingAdress;

            SaveInformationToDB();

            ViewBag.TotalSum = CountTotalSum();
            var model = SetingUpBigModel(billingAdress);

            ClearSession();

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
        public JavaScriptResult AddingProductsToCart(ProductLabelImages product)
        {
            CheckIfSessionExists();

            var ifProductExists = ((List<ProductLabelImages>)Session["listOfProducts"]).Find((x) => x.Name == product.Name);

            if (ifProductExists != null)
            {
                ((List<ProductLabelImages>)Session["listOfProducts"]).Remove(ifProductExists);

                product.Quantity += ifProductExists.Quantity;
            }

            ((List<ProductLabelImages>)Session["listOfProducts"]).Add(product);
            return JavaScript("window.location.pathname = 'Payment/Cart'");
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

        [HttpPost]
        public ActionResult ValidateBillingAddress(BillingInformation information)
        {
            if (ModelState.IsValid)
            {
                Session["BillingAddress"] = information;

                SaveInformationToDB();

                ViewBag.TotalSum = CountTotalSum();
                var model = SetingUpBigModel(information);

                ClearSession();

                return View("CheckoutSuccess", model);
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

        private void SaveInformationToDB()
        {
            Information shippingData = (Information)Session["InformationAboutCustomer"];
            BillingInformation billingData = (BillingInformation)Session["BillingAddress"];
            List<ProductLabelImages> products = (List<ProductLabelImages>)Session["listOfProducts"];

            Order order = new Order();
            ProductForOrder productsForOrder = new ProductForOrder();
            int idOrder = 0;

            order.Shipping_Email_Or_Number = shippingData.EmailOrPhoneNumber;
            order.Shipping_First_Name = shippingData.FirstName;
            order.Shipping_Last_Name = shippingData.LastName;
            order.Shipping_Address = shippingData.Address;
            order.Shipping_Apartment = shippingData.Apartment;
            order.Shipping_City = shippingData.City;
            order.Shipping_Country = shippingData.Country;
            order.Shipping_Postal_Code = shippingData.PostalCode;

            order.Billing_First_Name = billingData.FirstName;
            order.Billing_Last_Name = billingData.LastName;
            order.Billing_Address = billingData.Address;
            order.Billing_Apartment = billingData.Apartment;
            order.Billing_City = billingData.City;
            order.Billing_Country = billingData.Country;
            order.Billing_Postal_Code = billingData.PostalCode;

            idOrder = this._repository.AddOrder(order);

            this._repository.AddProductsForOrder(products, idOrder);
        }

        private void ClearSessions()
        {
            Session["InformationAboutCustomer"] = null;
            Session["BillingAddress"] = null;
            Session["listOfProducts"] = null;
        }

    }
}
