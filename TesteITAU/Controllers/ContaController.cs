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
        public Conta conta;
        public readonly DbContexto db;

        public ContaController()
        {
            db = new DbContexto();
        }
        
        
        //Métodos
        public ActionResult Index()
        {
            return View();
        }


        //Functions
        private void DepositarValor(double valor)
        {
            conta = new Conta();
            conta.Saldo += valor;

            db.Entry(conta).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SacarValor(double valor, Conta conta)
        {
            conta = new Conta();
            if(conta.Saldo > 0)
            {
                conta.Saldo -= valor;
            }
        }
    }
}