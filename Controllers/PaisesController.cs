using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;
using System.Reflection;

namespace Sistema.Controllers
{
    public class PaisesController : Controller
    {
        DAOPaises daoPaises = new();

        public ActionResult Index()
        {
            DAOPaises daoPaises = new();
            List<Models.Paises> list = daoPaises.GetPaises();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Paises model)
        {
            daoPaises = new DAOPaises();
            daoPaises.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Paises model)
        {
            daoPaises = new DAOPaises();
            daoPaises.Update(model);
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
            daoPaises = new DAOPaises();
            daoPaises.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codPais)
        {
            var daoPaises = new DAOPaises();
            var model = daoPaises.GetPais(codPais);
            return View(model);
        }

        public List<Models.Paises> ListaPaises()
        {
            var daoPaises = new DAOPaises();
            var list = daoPaises.GetPaises();
            return list;
        }

        public JsonResult JsSearch(string str)
        {
            DAOPaises daoPaises = new();
            List<Models.Paises> list = daoPaises.GetPaises();

            return Json(list);
        }
    }
}
