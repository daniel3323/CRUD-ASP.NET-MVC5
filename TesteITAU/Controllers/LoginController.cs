using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioController usuarioController;
        public readonly DbContexto db;

        public LoginController()
        {
            db = new DbContexto();
        }


        //Métodos
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(Login login)
        {
            usuarioController = new UsuarioController();

            if(db.Usuario.Where(u => u.Login == login.LoginUsuario) != null)
            {
                if(db.Usuario.Where(u => u.Login == login.Senha) != null)
                {
                    return usuarioController.Logar(login);
                }                
            }
            return View(login);
        }
    }
}