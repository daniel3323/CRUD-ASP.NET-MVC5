using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteITAU.Controllers
{
    public class LancamentoController : Controller
    {
        public readonly DbContexto db;

        public LancamentoController()
        {
            db = new DbContexto();
        }

        [HttpGet]
        public ActionResult Extrato()
        {
            ViewBag.Lancamentos = db.Lancamento.Where(c => c.Conta.NumeroConta == "123").ToList();
            return View();
        }
    }
}