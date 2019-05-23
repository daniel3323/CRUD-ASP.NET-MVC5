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
        private Endereco endereco;
        private string URL = "https://viacep.com.br/ws/";
        public readonly DbContexto db;
        
        public EnderecoController()
        {
            db = new DbContexto();
        }


        //Metodos
        [HttpGet]
        public ActionResult Index()
        {
            //Instancia uma viewbag ao chamar o Index da pagina
            //ViewBag.NomeViewBag
            ViewBag.Usuarios = db.Usuario.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarEndereco(Endereco endereco)
        {
            try
            {
                CadastrarEnderecoUsuario(endereco);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Json(new { erro = true, msg = ex.Message });
            }
        } 


        [HttpGet]
        public JsonResult GetEndereco(string cep)
        {
            endereco = new Endereco();
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
                    endereco.Logradouro = jsonDeserialized.logradouro;
                    endereco.Cidade = jsonDeserialized.localidade;
                    endereco.Bairro = jsonDeserialized.bairro;
                    endereco.Estado = jsonDeserialized.uf;

                    return Json(endereco, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { erro = true, msg = "CEP não encontrado." }, JsonRequestBehavior.AllowGet);
            }
        }

        //Functions
        private void CadastrarEnderecoUsuario(Endereco endereco)
        {
            db.Endereco.Add(endereco);
            db.SaveChanges();
        }

        private void AlterarEndereco(Endereco endereco)
        {
            db.Entry(endereco).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void DeletarEnderecoUsuario(Endereco endereco)
        {
            db.Entry(endereco).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}