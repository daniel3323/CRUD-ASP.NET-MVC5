using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteITAU.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        //[Required(ErrorMessage = "Nome é Obrigatório.", AllowEmptyStrings = false)]
        public string Nome { get; set; }
        //[Required(ErrorMessage = "Sobrenome é Obriatório.", AllowEmptyStrings = false)]
        public string Sobrenome { get; set; }
        //[Required(ErrorMessage = "E-mail é Obrigatório.", AllowEmptyStrings = false)]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Telefone é Obrigatório.", AllowEmptyStrings = false)]
        public string Telefone { get; set; }

        //[Required(ErrorMessage = "O Login é Obrigatório.", AllowEmptyStrings = false)]
        public string Login { get; set; }
        //[Required(ErrorMessage = "Senha é Obrigatório.", AllowEmptyStrings = false)]
        public string Senha { get; set; }

        //[Required(ErrorMessage = "CEP é Obrigatório.", AllowEmptyStrings = false)]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        //[Required(ErrorMessage = "Número é Obrigatório.", AllowEmptyStrings = false)]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }
    }
}