using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class ContasReceberController : Controller
    {
        public ActionResult Index()
        {
            var daoContasReceber = new DAOContasReceber();
            List<ContasReceber> list = daoContasReceber.GetContasReceber();
            return View(list);
        }
    }
}
