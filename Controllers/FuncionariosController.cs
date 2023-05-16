using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class FuncionariosController : Controller
    {
        DAOFuncionarios daoFuncionarios = new DAOFuncionarios();

        public ActionResult Index()
        {
            var daoFuncionarios = new DAOFuncionarios();
            List<Models.Funcionarios> list = daoFuncionarios.GetFuncionarios();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Funcionarios model)
        {
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Funcionarios model)
        {
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codFuncionario)
        {
            var daoFuncionarios = new DAOFuncionarios();
            var model = daoFuncionarios.GetFuncionario(codFuncionario);
            return View(model);
        }
    }
}
