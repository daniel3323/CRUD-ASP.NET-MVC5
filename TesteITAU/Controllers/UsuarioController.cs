﻿using System;
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
                    if(db.Usuario.Where(u => u.Login == usuario.Login) != null)
                    {
                        if(db.Usuario.Where(u => u.Email == usuario.Email) != null)
                        {
                            CadastrarNovoUsuario(usuario);
                            return RedirectToAction("Logar", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "E-mail já Existente.");
                            return View(usuario);
                        }                        
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Login já existente.");
                        return View(usuario);
                    }
                }

                return View(usuario);
            }
            catch 
            {
                ModelState.AddModelError("", "Falha ao cadastrar usuário.");
                return View();
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
                    if (db.Usuario.Where(u => u.Login == usuario.Login) != null)
                    {
                        if (db.Usuario.Where(u => u.Email == usuario.Email) != null)
                        {
                            AlterarUsuarioCadastrado(usuario);
                            return View("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Login já existente.");
                            return View(usuario);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Login já existente.");
                        return View(usuario);
                    }                        
                }
                return View(usuario);                
            }
            catch 
            {
                ModelState.AddModelError("", "Falha ao alterar usuário.");
                return View(usuario);
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