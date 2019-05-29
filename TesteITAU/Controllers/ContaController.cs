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
        private Random random;
        private readonly DbContexto db;
        private Usuario usuarioSessao;

        public ContaController()
        {
            db = new DbContexto();
        }


        //Métodos
        [HttpGet]
        public ActionResult ExibirDadosConta(Conta conta)
        {
            try
            {
                return View(conta);
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao exibir dados bancários. Verifique se você está conectado corretamente no sistema.");
                return View();
            }
        }


        [HttpPost]
        public ActionResult CriarConta(Conta conta)
        {
            try
            {
                usuarioSessao = db.Usuario.Find(Session["ID"]);
                conta = db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault();

                if (usuarioSessao != null)
                {
                    if (conta != null)
                    {
                        ModelState.AddModelError("", "Você já possui uma conta.");                        
                        return View();
                    }

                    return RedirectToAction("ExibirDadosConta", CriarNovaConta(conta));
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


        [HttpGet]
        public ActionResult _SucessoExclusao()
        {
            return View();
        }


        [HttpPost]
        public void Depositar(Lancamento lancamento, Conta conta)
        {
            DepositarValor(lancamento, conta);
        }


        [HttpGet]
        public ActionResult Excluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Excluir(Conta conta)
        {
            try
            {
                usuarioSessao = db.Usuario.Find(Session["ID"]);                

                if (db.Conta.Where(c => c.NumeroConta == conta.NumeroConta && c.Usuario_ID == usuarioSessao.ID).ToList().Count > 0)
                {
                    conta = db.Usuario.Find(usuarioSessao.ID).Contas.FirstOrDefault();

                    if (conta.Saldo == 0)
                    {
                        ExcluirConta(conta);
                        return RedirectToAction("_SucessoExclusao", "Conta");
                    }

                    ModelState.AddModelError("", "Para excluir a conta é necessário zerar o saldo.");
                    return View(conta);
                }

                ModelState.AddModelError("", "Erro ao excluir conta, verifique as informações digitadas.");
                return View(conta);
            }
            catch
            {
                ModelState.AddModelError("", "Erro inesperado ao excluir conta. Tente mais tarde.");
                return View(conta);
            }
            
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
        private Conta CriarNovaConta(Conta conta)
        {
            random = new Random();
            conta = new Conta();

            conta.NumeroConta = random.Next(1, 9999) + "-" + random.Next(1, 9);
            conta.Saldo = 0;
            conta.Usuario_ID = Convert.ToInt32(Session["ID"]);
            conta.Usuario = db.Usuario.Find(Session["ID"]);

            db.Conta.Add(conta);
            db.SaveChanges();

            return conta;
        }


        private void ExcluirConta(Conta conta)
        {
            db.Entry(conta).State = EntityState.Deleted;
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
            if (conta.Saldo > 0)
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