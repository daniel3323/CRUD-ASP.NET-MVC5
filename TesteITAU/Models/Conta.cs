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
        public float saldo { get; set; }

        public Usuario usuario { get; set; }
    }
}