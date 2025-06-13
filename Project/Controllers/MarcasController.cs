using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;

namespace Sistema.Controllers
{
    public class MarcasController : Controller
    {
        DAOMarcas daoMarcas = new();

        public ActionResult Index()
        {
            DAOMarcas daoMarcas = new();
            List<Models.Marcas> list = daoMarcas.GetMarcas();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Marcas model)
        {
            daoMarcas = new DAOMarcas();
            daoMarcas.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Marcas model)
        {
            daoMarcas = new DAOMarcas();
            daoMarcas.Update(model);
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
            daoMarcas = new DAOMarcas();
            daoMarcas.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codMarca)
        {
            var daoMarca = new DAOMarcas();
            var model = daoMarca.GetMarca(codMarca);
            return View(model);
        }

        public List<Models.Marcas> ListaMarcas()
        {
            DAOMarcas daoMarcas = new();
            var list = daoMarcas.GetMarcas();
            return list;
        }

        public JsonResult JsSearch(string str)
        {
            DAOMarcas daoMarcas = new();
            List<Models.Marcas> list = daoMarcas.GetMarcas();

            return Json(list);
        }

        public JsonResult JsAddMarca(string nmMarca)
        {
            DAOMarcas dao = new();
            var obj = new Models.Marcas()
            {
                nmMarca = nmMarca,
            };
            dao.Insert(obj);

            List<Models.Marcas> list = daoMarcas.GetMarcas();
            return Json(new { success = true, novaListaMarcas = list });
        }
    }
}
