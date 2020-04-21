using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        [Column("Image")]
        public string TheImage { get; set; }

    }
}