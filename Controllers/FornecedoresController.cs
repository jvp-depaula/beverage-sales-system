using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class FornecedoresController : Controller
    {
        DAOFornecedores daoFornecedores = new DAOFornecedores();

        public ActionResult Index()
        {
            var daoFornecedores = new DAOFornecedores();
            List<Models.Fornecedores> list = daoFornecedores.GetFornecedores();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Fornecedores model)
        {
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Fornecedores model)
        {
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Update(model);
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
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codFornecedor)
        {
            var daoFornecedores = new DAOFornecedores();
            var model = daoFornecedores.GetFornecedor(codFornecedor);
            return View(model);
        }
    }
}
