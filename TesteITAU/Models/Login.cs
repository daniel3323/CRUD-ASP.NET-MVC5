using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Login
    {
        [Required(ErrorMessage = "O Login é Obrigatório.", AllowEmptyStrings = false)]
        public string LoginUsuario { get; set; }
        [Required(ErrorMessage = "Senha é Obrigatório.", AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }
}