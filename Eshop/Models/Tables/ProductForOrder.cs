using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Products_For_Orders")]
    public class ProductForOrder
    {
        public int Id { get; set; }
        public int Id_Order { get; set; }
        public int Id_Product { get; set; }
    }
}