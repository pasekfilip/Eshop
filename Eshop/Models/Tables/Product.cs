using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Vendor { get; set; }
        public string Sku { get; set; }
        public bool Availability { get; set; }
        public int Score { get; set; }
        public int Label_Id { get; set; }

    }
}