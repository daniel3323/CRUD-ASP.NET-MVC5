using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Lancamento
    {
        [Key]
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Valor Incorreto.", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Valor { get; set; }

        [ForeignKey("Conta")]
        public int Conta_ID { get; set; }
        public Conta Conta { get; set; }
    }
}