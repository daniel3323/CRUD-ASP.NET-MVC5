using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false)]
        public string LoginUsuario { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }
}