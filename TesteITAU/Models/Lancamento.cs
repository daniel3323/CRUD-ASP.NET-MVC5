using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Lancamento
    {
        [Key]
        public int ID { get; set; }
        public DateTime DataLancamento { get; set; }
        public string TipoLancamento { get; set; }
        public double ValorLancamento { get; set; }

        public Conta Conta { get; set; }
    }
}