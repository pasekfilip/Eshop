using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eshop.Models.Checkout
{
    public class Information
    {
        [RegularExpression(@"^(\w+([\.]?\w+)+@\w+([\.]\w+)+)$|^([\d]{9})$", ErrorMessage = "Enter a valid email or a phone number")]
        [Required(ErrorMessage = "Enter your email or phone number")]
        public string EmailOrPhoneNumber { get; set; }

        public bool SendNews { get; set; }

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

        public bool SaveInformation { get; set; }
    }
}