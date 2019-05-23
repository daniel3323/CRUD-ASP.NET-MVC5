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


        //Metodos
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Usuarios = db.Usuario.Where(usuario => usuario.Nome != null).ToList();
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
                CadastrarNovoUsuario(usuario);
                return RedirectToAction("Index", "Home");
                //return Json(new { erro = false, msg = "Cadastrado" });
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

        private void DeletarUsuario(Usuario usuario)
        {
            db.Entry(usuario).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}