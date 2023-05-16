using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class CidadesController : Controller
    {
        DAOCidades daoCidades = new DAOCidades();

        public ActionResult Index()
        {
            var daoCidades = new DAOCidades();
            List<Cidades> list = daoCidades.GetCidades();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cidades model)
        {
            daoCidades = new DAOCidades();
            daoCidades.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Cidades model)
        {
            daoCidades = new DAOCidades();
            daoCidades.Update(model);
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
            daoCidades = new DAOCidades();
            daoCidades.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codCidade)
        {
            var daoCidades = new DAOCidades();
            var model = daoCidades.GetCidade(codCidade);
            return View(model);
        }
    }
}
