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
        [HttpGet]
        public ActionResult Depositar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Depositar(double valor)
        {
            DepositarValor(valor);
            return View();
        }


        [HttpGet]
        public ActionResult Extrato()
        {
            ViewBag.Conta = db.Contas.Where(c => c.Usuario.ID == Convert.ToInt32(Session["ID"]));
            return View();
        }

        //Functions
        private void DepositarValor(double valor)
        {
            conta = new Conta();

            conta.DataLancamento = DateTime.Now;
            conta.Saldo += valor;
            conta.Usuario = db.Usuario.Find(Session["ID"]);
            conta.TipoLancamento = 'e';
            conta.ValorLancamento = valor;

            db.Entry(conta).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SacarValor(double valor, Conta conta)
        {
            conta = new Conta();
            if(conta.Saldo > 0)
            {
                conta.DataLancamento = DateTime.Now;
                conta.Saldo -= valor;
                conta.Usuario = db.Usuario.Find(Session["ID"]);
                conta.TipoLancamento = 's';
                conta.ValorLancamento = valor;               
            }
        }
    }
}