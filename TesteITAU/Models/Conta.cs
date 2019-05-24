using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Conta
    {
        [Key]
        public int ID { get; set; }
        public Double Saldo { get; set; }
        public string Numero { get; set; }

        public Usuario Usuario { get; set; }
    }
}