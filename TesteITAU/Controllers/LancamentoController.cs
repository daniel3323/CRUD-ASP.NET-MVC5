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
        public readonly DbContexto db;
        private ContaController contaController;
        private static int sessionID;

        public LancamentoController()
        {
            db = new DbContexto();
        }


        //Métodos  
        [HttpGet]
        public ActionResult Extrato()
        {
            sessionID = Convert.ToInt32(Session["ID"]);

            ViewBag.Lancamentos = db.Lancamento.Where(l => l.Conta.Usuario_ID == sessionID).ToList();
            ViewBag.Conta = db.Conta.Where(c => c.Usuario_ID == sessionID).FirstOrDefault();
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
                if(ModelState.IsValid)
                {
                    DepositarValorLancamento(lancamento);
                    return RedirectToAction("Extrato", "Lancamento");
                }

                ModelState.AddModelError("", "Falha ao realizar Depósito, verifique o valor depositado.");
                return View(lancamento);
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao realizar Depósito, verifique o valor depositado.");
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
                    if(SacarValorLancamento(lancamento))
                    {
                        return RedirectToAction("Extrato", "Lancamento");
                    }

                    ModelState.AddModelError("", "Saldo insuficiente.");
                    return View(lancamento);
                }

                ModelState.AddModelError("", "Falha ao realizar Saque, verifique o valor sacado.");
                return View(lancamento.Valor);
            }
            catch 
            {
                ModelState.AddModelError("", "Falha ao realizar Saque, verifique o valor sacado.");
                return View();
            }
            
        }


        //Functions
        private void DepositarValorLancamento(Lancamento lancamento)
        {      
            sessionID = Convert.ToInt32(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "e";
            lancamento.Conta = db.Conta.Where(c => c.Usuario_ID == sessionID).FirstOrDefault();                      

            db.Lancamento.Add(lancamento);
            db.SaveChanges();


            contaController = new ContaController();
            contaController.Depositar(lancamento, lancamento.Conta);
        }


        private bool SacarValorLancamento(Lancamento lancamento)
        {
            sessionID = Convert.ToInt32(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "s";
            lancamento.Conta = db.Conta.Where(c => c.Usuario_ID == sessionID).FirstOrDefault();

            if(lancamento.Conta.Saldo - lancamento.Valor >= 0)
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