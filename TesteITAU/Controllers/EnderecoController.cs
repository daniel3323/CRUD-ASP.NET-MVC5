using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class EnderecoController : Controller
    {
        Usuario enderecoUsuario;
        private string URL = "https://viacep.com.br/ws/";

        [HttpGet]
        public JsonResult GetEndereco(string cep)
        {
            enderecoUsuario = new Usuario();
            try
            {
                using(var client = new HttpClient())
                {
                    //Cabeçalho
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Request
                    var response = client.GetAsync(string.Format(URL + cep + "/json")).Result;
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    //Deserealizando conteúdo
                    dynamic jsonDeserialized = JsonConvert.DeserializeObject(responseString);


                    //Setando conteúdo no objeto "Endereço"
                    enderecoUsuario.Logradouro = jsonDeserialized.logradouro;
                    enderecoUsuario.Cidade = jsonDeserialized.localidade;
                    enderecoUsuario.Bairro = jsonDeserialized.bairro;
                    enderecoUsuario.Estado = jsonDeserialized.uf;

                    if(enderecoUsuario.Logradouro == null && enderecoUsuario.Cidade == null && enderecoUsuario.Bairro == null && enderecoUsuario.Estado == null)
                    {
                        return Json(new { erro = true, msg = "CEP não encontrado." }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(enderecoUsuario, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { erro = true, msg = "CEP não encontrado." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}