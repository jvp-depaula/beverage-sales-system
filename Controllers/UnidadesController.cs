using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;

namespace Sistema.Controllers
{
    public class UnidadesController : Controller
    {
        DAOUnidades daoUnidades = new();

        public ActionResult Index()
        {
            DAOUnidades daoUnidades = new();
            List<Models.Unidades> list = daoUnidades.GetUnidades();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Unidades model)
        {
            daoUnidades = new DAOUnidades();
            daoUnidades.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Unidades model)
        {
            daoUnidades = new DAOUnidades();
            daoUnidades.Update(model);
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
            daoUnidades = new DAOUnidades();
            daoUnidades.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codUnidade)
        {
            var daoUnidades = new DAOUnidades();
            var model = daoUnidades.GetUnidade(codUnidade);
            return View(model);
        }

        public List<Models.Unidades> ListaUnidades()
        {
            DAOUnidades daoUnidades = new();
            var list = daoUnidades.GetUnidades();
            return list;
        }

        public JsonResult JsSearch(string str)
        {
            DAOUnidades daoUnidades = new();
            List<Models.Unidades> list = daoUnidades.GetUnidades();

            return Json(list);
        }

        public JsonResult JsAddUnidade(string dsUnidade, string sigla)
        {
            DAOUnidades dao = new();
            var obj = new Models.Unidades()
            {
                dsUnidade = dsUnidade,
                sigla = sigla
            };
            dao.Insert(obj);

            List<Models.Unidades> list = dao.GetUnidades();
            return Json(new { success = true, novaListaUnidades = list });
        }
    }
}
