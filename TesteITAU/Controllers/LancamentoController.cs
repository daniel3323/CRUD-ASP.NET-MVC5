using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class LancamentoController : Controller
    {
        private readonly DbContexto db;
        private ContaController contaController;

        public LancamentoController()
        {
            db = new DbContexto();
        }


        //Métodos  
        [HttpGet]
        public ActionResult Extrato()
        {
            var usuarioSessao = db.Usuario.Find(Session["ID"]);

            ViewBag.Lancamentos = db.Lancamento.Where(l => l.Conta.Usuario_ID == usuarioSessao.ID).ToList();
            ViewBag.Conta = db.Conta.Where(c => c.Usuario_ID == usuarioSessao.ID).FirstOrDefault();

            return View();
        }


        [HttpGet]
        public ActionResult Depositar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Depositar(Lancamento lancamento)
        {
            try
            {           
                if (ModelState.IsValid)
                {
                    var usuarioSessao = db.Usuario.Find(Session["ID"]);

                    if (db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault() != null)
                    {
                        DepositarValorLancamento(lancamento);
                        return RedirectToAction("Extrato", "Lancamento");
                    }

                    ModelState.AddModelError("", "Falha ao realizar Depósito, você não possui uma conta.");
                    return View(lancamento);
                }

                ModelState.AddModelError("", "Falha ao realizar Depósito, verifique o valor inserido.");
                return View(lancamento);
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao realizar Depósito, verifique o valor inserido.");
                return View(lancamento);
            }
        }


        [HttpGet]
        public ActionResult Sacar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sacar(Lancamento lancamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioSessao = db.Usuario.Find(Session["ID"]);

                    if (db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault() != null)
                    {
                        if (SacarValorLancamento(lancamento))
                        {
                            return RedirectToAction("Extrato", "Lancamento");
                        }

                        ModelState.AddModelError("", "Saldo insuficiente.");
                        return View(lancamento);
                    }

                    ModelState.AddModelError("", "Falha ao realizar Depósito, você não possui uma conta.");
                    return View(lancamento);
                }

                ModelState.AddModelError("", "Falha ao realizar Saque, verifique o valor inserido.");
                return View(lancamento.Valor);
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao realizar Saque, verifique o valor inserido.");
                return View();
            }

        }


        //Functions
        private void DepositarValorLancamento(Lancamento lancamento)
        {
            Usuario user = db.Usuario.Find(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "e";
            lancamento.Conta = user.Contas.Where(c => c.Usuario_ID == user.ID).FirstOrDefault();

            db.Lancamento.Add(lancamento);
            db.SaveChanges();


            contaController = new ContaController();
            contaController.Depositar(lancamento, lancamento.Conta);
        }


        private bool SacarValorLancamento(Lancamento lancamento)
        {
            Usuario user = db.Usuario.Find(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "s";
            lancamento.Conta = db.Conta.Where(c => c.Usuario_ID == user.ID).FirstOrDefault();

            if (lancamento.Conta.Saldo - lancamento.Valor >= 0)
            {
                db.Lancamento.Add(lancamento);
                db.SaveChanges();


                contaController = new ContaController();
                contaController.Sacar(lancamento, lancamento.Conta);

                return true;
            }

            return false;
        }
    }
}