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
            ViewBag.Saldo = db.Contas.Where(c => c.Usuario_ID == sessionID).ToList();
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

                return View(lancamento.Valor);
            }
            catch (Exception ex)
            {
                return Json(new { erro = true, msg = ex.Message });
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
                    SacarValorLancamento(lancamento);
                    return RedirectToAction("Extrato", "Lancamento");
                }

                return View(lancamento.Valor);
            }
            catch (Exception ex)
            {
                return Json(new { erro = true, msg = ex.Message });
            }
            
        }


        //Functions
        private void DepositarValorLancamento(Lancamento lancamento)
        {      
            sessionID = Convert.ToInt32(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "e";
            lancamento.Conta = db.Contas.Where(c => c.Usuario_ID == sessionID).FirstOrDefault();                      

            db.Lancamento.Add(lancamento);
            db.SaveChanges();


            contaController = new ContaController();
            contaController.Depositar(lancamento, lancamento.Conta);
        }


        private void SacarValorLancamento(Lancamento lancamento)
        {
            sessionID = Convert.ToInt32(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "s";
            lancamento.Conta = db.Contas.Where(c => c.Usuario_ID == sessionID).FirstOrDefault();

            db.Lancamento.Add(lancamento);
            db.SaveChanges();


            contaController = new ContaController();
            contaController.Sacar(lancamento, lancamento.Conta);
        }
    }
}