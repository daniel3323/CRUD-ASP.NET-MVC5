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
                    if(db.Usuario.Any(u => u.Login == usuario.Login))
                    {
                        if(db.Usuario.Any(u => u.Email == usuario.Email))
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
    }
}