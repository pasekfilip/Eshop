using Eshop.Models.Checkout;
using Eshop.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models.TemporaryClasses
{
    public class BigModelForInformation
    {
        public List<ProductLabelImages> Data { get; set; }

        public Information InformationAboutCustomer { get; set; }
        public BillingInformation BillingInformation { get; set; }
    }
}