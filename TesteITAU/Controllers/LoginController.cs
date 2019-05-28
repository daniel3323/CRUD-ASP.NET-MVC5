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
        private readonly DbContexto db;

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

            if (ModelState.IsValid)
            {
                if (db.Usuario.Where(u => u.Login == login.LoginUsuario && u.Senha == login.Senha).ToList().Count > 0)
                {
                    LogarValidado(login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                }
            }
            else
            {
                ModelState.AddModelError("","Login ou Senha incorretos.");
                return View(login);
            }

            return View(login);
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }


        //Functions
        private void LogarValidado(Login login)
        {
            using (DbContexto db = new DbContexto())
            {
                var validaAcesso = db.Usuario.Where(u => u.Login.Equals(login.LoginUsuario) && u.Senha.Equals(login.Senha)).FirstOrDefault();

                if (validaAcesso.Senha == login.Senha && validaAcesso.Login == login.LoginUsuario)
                {
                    Session["Nome"] = validaAcesso.Nome;
                    Session["Sobrenome"] = validaAcesso.Sobrenome;
                    Session["ID"] = validaAcesso.ID;
                    Session["UsuarioLogado"] = true;
                }
            }
        }
    }
}