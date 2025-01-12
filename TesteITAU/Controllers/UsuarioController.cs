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
        private readonly DbContexto db;

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
        public ActionResult _SucessoCriacao()
        {
            return View();
        }


        [HttpGet]
        public ActionResult _SucessoAlteracao()
        {
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
                    if(db.Usuario.Where(u => u.Email == usuario.Email).ToList().Count == 0 )
                    {
                        if(db.Usuario.Where(u => u.Login == usuario.Login).ToList().Count == 0 )
                        {
                            if(usuario.Senha.Length >= 8)
                            {
                                CadastrarNovoUsuario(usuario);
                                return RedirectToAction("_SucessoCriacao", "Usuario");
                            }
                            else
                            {
                                ModelState.AddModelError("Senha", "A senha deve conter, no mínimo, 8 caracteres.");
                                return View(usuario);
                            }                                
                        }
                        else
                        {
                            ModelState.AddModelError("Login", "Login já Existente.");
                            return View(usuario);
                        }                        
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email já existente.");
                        return View(usuario);
                    }
                }

                return View(usuario);
            }
            catch 
            {
                ModelState.AddModelError("", "Falha ao cadastrar usuário. Tente novamente mais tarde.");
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
                if (ModelState.IsValid)
                {
                    if(usuario.Senha.Length >= 8)
                    {
                        AlterarUsuarioCadastrado(usuario);
                        return RedirectToAction("_SucessoAlteracao", "Usuario");
                    }

                    ModelState.AddModelError("Senha", "A senha deve conter, no mínimo, 8 caracteres.");
                    return View(usuario);
                }

                ModelState.AddModelError("", "Verifique as informações digitadas e os campos obrigatórios.");
                return View(usuario);
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao atualizar cadastro.");
                return View(usuario);
            }

            //try
            //{
            //    if(ModelState.IsValid)
            //    {
            //        if (usuario.Email == db.Usuario.Find(usuario.ID).Email)
            //        {
            //            if (usuario.Login == db.Usuario.Find(usuario.ID).Login)
            //            {
            //                if (usuario.Senha.Length >= 8)
            //                {   
            //                    AlterarUsuarioCadastrado(usuario);
            //                    return View("Index", "Home");
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("Senha", "A senha deve conter, no mínimo, 8 caracteres.");
            //                    return View(usuario);
            //                }  
            //            }
            //            else
            //            {
            //                if (db.Usuario.Where(u => u.Login == usuario.Login).ToList().Count > 0)
            //                {
            //                    ModelState.AddModelError("Login", "Login já existente.");
            //                    return View(usuario);
            //                }
            //                else
            //                {
            //                    if (usuario.Senha.Length >= 8)
            //                    {
            //                        AlterarUsuarioCadastrado(usuario);
            //                        return View("Index", "Home");
            //                    }
            //                    else
            //                    {
            //                        ModelState.AddModelError("Senha", "A senha deve conter, no mínimo, 8 caracteres.");
            //                        return View(usuario);
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if(db.Usuario.Where(u => u.Email == usuario.Email).ToList().Count > 0)
            //            {
            //                ModelState.AddModelError("Email", "Email já existente.");
            //                return View(usuario);
            //            }
            //            else
            //            {
            //                if (usuario.Senha.Length >= 8)
            //                {
            //                    AlterarUsuarioCadastrado(usuario);
            //                    return View("Index", "Home");
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("Senha", "A senha deve conter, no mínimo, 8 caracteres.");
            //                    return View(usuario);
            //                }
            //            }
            //        }                        
            //    }
            //    return View(usuario);                
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", "Falha ao alterar usuário. Tente novamente mais tarde.");
            //    return View(usuario);
            //}
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