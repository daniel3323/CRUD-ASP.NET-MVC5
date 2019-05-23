using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Endereco
    {
        [Key]
        public int ID { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        //public int Usuario_ID { get; set; }
        //[ForeignKey("Usuario_ID")]
        public virtual Usuario Usuario { get; set; }
        
    }
}