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
        public ActionResult Sacar(Lancamento lancamento, Conta conta)
        {
            SacarValor(lancamento, conta);
            return View();
        }


        //Functions
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