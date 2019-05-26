using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly DbContexto db;

        public UsuarioController()
        {
            db = new DbContexto();
        }


        //Métodos
        [HttpGet]
        public ActionResult ListarUsuarios()
        {
            ViewBag.Usuarios = db.Usuario.ToList();
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
            if(ModelState.IsValid)
            {
                LogarValidado(login);
                return RedirectToAction("ListarUsuarios", "Usuario");
            }
            else
            {
                ModelState.AddModelError("", "Login ou Senha incorretos.");
                return View();
            }
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(db.Usuario.Where(u => u.Login == usuario.Login) == null)
                    {
                        if(db.Usuario.Where(u => u.Email == usuario.Email) == null)
                        {
                            CadastrarNovoUsuario(usuario);
                            return RedirectToAction("Logar", "Usuario");
                        }
                        else
                        {
                            return Json(new { erro = true, msg = "Email já existente." });
                        }                        
                    }
                    else
                    {
                        return Json(new { erro = true, msg = "Login já existente." });
                    }
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                return Json(new { erro = true, msg = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult AlterarUsuario()
        {
            return View("AlterarUsuario", db.Usuario.Find(Session["ID"]));
        }

        [HttpPost]
        public ActionResult AlterarUsuario(Usuario usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    AlterarUsuarioCadastrado(usuario);

                    return View("Index", "Home");
                }

                return View(usuario);                
            }
            catch (Exception ex)
            {
                return Json(new { erro = true, msg = ex.Message });
            }
        }
        

        //Functions
        private void CadastrarNovoUsuario(Usuario usuario)
        {
            db.Usuario.Add(usuario);
            db.SaveChanges();
        }

        private void AlterarUsuarioCadastrado(Usuario usuario)
        {
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void LogarValidado(Login login)
        {
            using (DbContexto db = new DbContexto())
            {
                var validaAcesso = db.Usuario.Where(u => u.Login.Equals(login.LoginUsuario)).FirstOrDefault();

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