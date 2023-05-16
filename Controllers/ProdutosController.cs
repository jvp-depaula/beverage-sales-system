using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class ProdutosController : Controller
    {
        DAOProdutos daoProdutos = new DAOProdutos();

        public ActionResult Index()
        {
            var daoProdutos = new DAOProdutos();
            List<Models.Produtos> list = daoProdutos.GetProdutos();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Produtos model)
        {
            daoProdutos = new DAOProdutos();
            daoProdutos.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Produtos model)
        {
            daoProdutos = new DAOProdutos();
            daoProdutos.Update(model);
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
            daoProdutos = new DAOProdutos();
            daoProdutos.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codProduto)
        {
            var daoProdutos = new DAOProdutos();
            var model = daoProdutos.GetProduto(codProduto);
            return View(model);
        }
    }
}
