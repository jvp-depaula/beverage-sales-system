using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class CategoriasController : Controller
    {
        DAOCategorias daoCategorias = new DAOCategorias();

        public ActionResult Index()
        {
            var daoCategorias = new DAOCategorias();
            List<Categorias> list = daoCategorias.GetCategorias();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Categorias model)
        {
            daoCategorias = new DAOCategorias();
            daoCategorias.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Categorias model)
        {
            daoCategorias = new DAOCategorias();
            daoCategorias.Update(model);
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
            daoCategorias = new DAOCategorias();
            daoCategorias.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codCategoria)
        {
            var DAOCategorias = new DAOCategorias();
            var model = DAOCategorias.GetCategoria(codCategoria);
            return View(model);
        }
    }
}
