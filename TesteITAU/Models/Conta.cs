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
        public double Saldo { get; set; }
        public string NumeroConta { get; set; }        

        public Usuario Usuario { get; set; }
        public ICollection<Lancamento> Lancamentos { get; set; }
    }
}