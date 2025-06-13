using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class ContasPagarController : Controller
    {
        public ActionResult Index()
        {
            var daoContasPagar = new DAOContasPagar();
            List<ContasPagar> list = daoContasPagar.GetContasPagar();
            return View(list);
        }
    }
}
