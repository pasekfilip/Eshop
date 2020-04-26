using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eshop.Models.Checkout
{
    public class BillingInformation
    {
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        public string Address { get; set; }

        public string Apartment { get; set; }

        [Required(ErrorMessage = "Enter your city")]
        public string City { get; set; }

        public string Country { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "Enter a valid postal code")]
        [Required(ErrorMessage = "Enter a valid postal code")]
        public string PostalCode { get; set; }
    }
}