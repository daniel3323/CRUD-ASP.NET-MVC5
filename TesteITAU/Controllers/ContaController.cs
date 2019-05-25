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
        private Conta conta;
        private Usuario usuario;
        private Lancamento lancamento;

        public readonly DbContexto db;
        private static int sessionID;

        public ContaController()
        {
            db = new DbContexto();
        }
        
        
        //Métodos
        [HttpGet]
        public ActionResult Depositar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Depositar(double valor)
        {
            DepositarValor(valor, db.Contas.Where(c => c.Usuario.ID.Equals(Session["ID"])).FirstOrDefault());
            return View();
        }


        [HttpGet]
        public ActionResult Extrato()
        {
            int sessionID = Convert.ToInt32(Session["ID"]);

            ViewBag.Conta = db.Contas.Where(c => c.Usuario.ID == sessionID).ToList();
            return View();
        }


        [HttpGet]
        public ActionResult Sacar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sacar(double valor)
        {
            SacarValor(valor, db.Contas.Where(c => c.Usuario.ID.Equals(Session["ID"])).FirstOrDefault());
            return View();
        }

        //Functions
        private void DepositarValor(double valor, Conta conta)
        {
            conta.Saldo += valor;
            conta.Usuario = db.Usuario.Find(Session["ID"]);

            lancamento = new Lancamento();

            lancamento.DataLancamento = DateTime.Now;
            lancamento.TipoLancamento = "e";
            lancamento.ValorLancamento = valor;

            conta.Lancamentos.Add(lancamento);

            db.Entry(conta).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SacarValor(double valor, Conta conta)
        {            
            if(conta.Saldo > 0)
            {

                conta.Saldo -= valor;
                conta.Usuario = db.Usuario.Find(Session["ID"]);

                lancamento = new Lancamento();

                lancamento.DataLancamento = DateTime.Now;
                lancamento.TipoLancamento = "e";
                lancamento.ValorLancamento = valor;

                conta.Lancamentos.Add(lancamento);

                db.Entry(conta).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}