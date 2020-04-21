using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        [Column("Label")]
        public string The_Label { get; set; }
    }
}