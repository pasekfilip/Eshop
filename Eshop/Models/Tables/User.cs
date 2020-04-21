using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Eshop.Models.Tables
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\w+([\.]?\w+)+@\w+([\.]\w+)+$", ErrorMessage = "Its not an Email")]
        public string Email { get; set; }

        [StringLength(40, MinimumLength = 8)]
        public string Password { get; set; }
    }
}