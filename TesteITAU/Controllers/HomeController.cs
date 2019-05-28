using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContexto db;

        public HomeController()
        {
            db = new DbContexto();
        }


        //Métodos
        [HttpGet]
        public ActionResult Index()
        {            
            var usuarioSessao = Session["ID"];
            if(usuarioSessao != null)
            {
                ViewBag.Conta = db.Usuario.Find(usuarioSessao).Contas.FirstOrDefault();
                ViewBag.Usuario = db.Usuario.Find(usuarioSessao);
            }            

            return View();
        }
    }
}