using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public string Shipping_Email_Or_Number { get; set; }
        public string Shipping_First_Name { get; set; }
        public string Shipping_Last_Name { get; set; }
        public string Shipping_Address { get; set; }
        public string Shipping_Apartment { get; set; }
        public string Shipping_City { get; set; }
        public string Shipping_Country { get; set; }
        public string Shipping_Postal_Code { get; set; }
        public string Billing_First_Name { get; set; }
        public string Billing_Last_Name { get; set; }
        public string Billing_Address { get; set; }
        public string Billing_Apartment { get; set; }
        public string Billing_City { get; set; }
        public string Billing_Country { get; set; }
        public string Billing_Postal_Code { get; set; }

    }
}