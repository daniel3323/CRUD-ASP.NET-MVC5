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
        private Usuario usuarioSessao;

        private ContaController contaController;

        public LancamentoController()
        {
            db = new DbContexto();
        }


        //Métodos  
        [HttpGet]
        public ActionResult Extrato()
        {
            usuarioSessao = db.Usuario.Find(Session["ID"]);
            if(usuarioSessao != null)
            {
                ViewBag.Lancamentos = db.Lancamento.Where(l => l.Conta.Usuario_ID == usuarioSessao.ID).ToList().OrderByDescending(l => l.Data);
                ViewBag.Conta = db.Conta.Where(c => c.Usuario_ID == usuarioSessao.ID).FirstOrDefault();
            }            

            return View();
        }


        [HttpGet]
        public ActionResult Depositar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _SucessoTransacao()
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
                    usuarioSessao = db.Usuario.Find(Session["ID"]);

                    if(usuarioSessao != null)
                    {
                        if (db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault() != null)
                        {
                            DepositarValorLancamento(lancamento);
                            return RedirectToAction("_SucessoTransacao", "Lancamento");
                        }

                        ModelState.AddModelError("", "Falha ao realizar Depósito, você não possui uma conta.");
                        return View(lancamento);
                    }

                    ModelState.AddModelError("", "Falha ao realizar Depósito, você não está logado no sistema.");
                    return View(lancamento);
                }

                ModelState.AddModelError("", "Falha ao realizar Depósito, verifique o valor inserido.");
                return View(lancamento);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Falha ao realizar Depósito, tente novamente mais tarde.");
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
                    usuarioSessao = db.Usuario.Find(Session["ID"]);

                    if(usuarioSessao != null)
                    {
                        if (db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault() != null)
                        {
                            if (SacarValorLancamento(lancamento))
                            {
                                return RedirectToAction("_SucessoTransacao", "Lancamento");
                            }

                            ModelState.AddModelError("", "Saldo insuficiente.");
                            return View(lancamento);
                        }

                        ModelState.AddModelError("", "Falha ao realizar Saque, você não possui uma conta.");
                        return View(lancamento);
                    }

                    ModelState.AddModelError("", "Falha ao realizar Saque, você não está logado no sistema.");
                    return View(lancamento);
                }

                ModelState.AddModelError("", "Falha ao realizar Saque, verifique o valor inserido.");
                return View(lancamento.Valor);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Falha ao realizar Saque, tente novamente mais tarde.");
                return View();
            }

        }


        //Functions
        private void DepositarValorLancamento(Lancamento lancamento)
        {
            usuarioSessao = db.Usuario.Find(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "e";
            lancamento.Conta = usuarioSessao.Contas.Where(c => c.Usuario_ID == usuarioSessao.ID).FirstOrDefault();

            db.Lancamento.Add(lancamento);
            db.SaveChanges();


            contaController = new ContaController();
            contaController.Depositar(lancamento, lancamento.Conta);
        }


        private bool SacarValorLancamento(Lancamento lancamento)
        {
            usuarioSessao = db.Usuario.Find(Session["ID"]);

            lancamento.Data = DateTime.Now;
            lancamento.Tipo = "s";
            lancamento.Conta = db.Conta.Where(c => c.Usuario_ID == usuarioSessao.ID).FirstOrDefault();

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