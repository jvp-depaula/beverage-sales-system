using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class FormaPgtoController : Controller
    {
        DAOFormaPgto daoFormaPgto = new DAOFormaPgto();

        public ActionResult Index()
        {
            var daoFormaPgto = new DAOFormaPgto();
            List<FormaPgto> list = daoFormaPgto.GetFormaPgtos();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormaPgto model)
        {
            daoFormaPgto = new DAOFormaPgto();
            daoFormaPgto.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(FormaPgto model)
        {
            daoFormaPgto = new DAOFormaPgto();
            daoFormaPgto.Update(model);
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
            daoFormaPgto = new DAOFormaPgto();
            daoFormaPgto.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? idFormaPgto)
        {
            var DAOFormaPgto = new DAOFormaPgto();
            var model = DAOFormaPgto.GetFormaPgto(idFormaPgto);
            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOFormaPgto daoFormaPgto = new();
            List<FormaPgto> list = daoFormaPgto.GetFormaPgtos();

            return Json(list);
        }

        public JsonResult JsAddCategoria(string dsFormaPgto)
        {
            DAOFormaPgto dao = new();
            var obj = new FormaPgto()
            {
                dsFormaPgto = dsFormaPgto,
            };
            dao.Insert(obj);

            List<FormaPgto> list = dao.GetFormaPgtos();
            return Json(new { success = true, novaListaFormaPgto = list });
        }
    }
}
