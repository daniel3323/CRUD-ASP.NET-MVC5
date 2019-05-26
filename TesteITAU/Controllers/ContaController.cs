using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TesteITAU.Models;

namespace TesteITAU.Controllers
{
    public class ContaController : Controller
    {
        private int sessionID;
        private Random random;
        public readonly DbContexto db;

        public ContaController()
        {
            db = new DbContexto();
        }
        

        //Métodos
        [HttpPost]
        public ActionResult CriarConta(Conta conta)
        {
            sessionID = Convert.ToInt32(Session["ID"]);
            try
            {
                if (Session["ID"] != null)
                {
                    if (db.Conta.Count(c => c.Usuario_ID == sessionID) > 0)
                    {
                        ModelState.AddModelError("", "Você já possui uma conta.");
                        return View();
                    }

                    CriarNovaConta(conta);
                    return RedirectToAction("Depositar", "Lancamento");
                }

                ModelState.AddModelError("", "É necessário efetuar Login ou Cadastrar-se para abrir uma conta.");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "Falha ao criar conta.");
                return View();
            }                           
        }

        [HttpGet]
        public ActionResult CriarConta()
        {
            return View();
        }
        

        [HttpPost]
        public void Depositar(Lancamento lancamento, Conta conta)
        {
            DepositarValor(lancamento, conta);
        }


        [HttpGet]
        public ActionResult Sacar()
        {
            return View();
        }

        [HttpPost]
        public void Sacar(Lancamento lancamento, Conta conta)
        {
            SacarValor(lancamento, conta);
        }


        //Functions
        private void CriarNovaConta(Conta conta)
        {
            random = new Random();
            conta = new Conta();

            conta.NumeroConta = random.Next(1, 9999) + "-" + random.Next(1, 9);
            conta.Saldo = 0;
            conta.Usuario_ID = Convert.ToInt32(Session["ID"]);
            conta.Usuario = db.Usuario.Find(Session["ID"]);

            db.Conta.Add(conta);
            db.SaveChanges();
        }



        private void DepositarValor(Lancamento lancamento, Conta conta)
        {
            conta.Saldo += lancamento.Valor;
            conta.Usuario = db.Usuario.Find(Session);

            conta.Lancamentos.Add(lancamento);

            db.Entry(conta).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void SacarValor(Lancamento lancamento, Conta conta)
        {            
            if(conta.Saldo > 0)
            {
                conta.Saldo -= lancamento.Valor;
                conta.Usuario = db.Usuario.Find(Session);

                conta.Lancamentos.Add(lancamento);

                db.Entry(conta).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}