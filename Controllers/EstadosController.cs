using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class EstadosController : Controller
    {

        DAOEstados daoEstados = new();

        public ActionResult Index()
        {
            var daoEstados = new DAOEstados();
            List<Models.Estados> list = daoEstados.GetEstados();
            return View(list);
        }

        public ActionResult Create()
        {
            var daoPaises = new DAOPaises();
            List<Models.Paises> listPaises = daoPaises.GetPaises();
            var listaPaises = new Estados
            {
                ListaPaises = listPaises.Select(u => new SelectListItem
                {
                    Value = u.idPais.ToString(),
                    Text = u.nmPais.ToString()
                })
            };

            return View(listaPaises);
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Estados model)
        {
            daoEstados = new DAOEstados();
            daoEstados.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Estados model)
        {
            daoEstados = new DAOEstados();
            daoEstados.Update(model);
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
            daoEstados = new DAOEstados();
            daoEstados.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codEstado)
        {
            var daoPaises = new DAOPaises();
            List<Models.Paises> listPaises = daoPaises.GetPaises();
            var daoEstados = new DAOEstados();
            var model = daoEstados.GetEstado(codEstado);
            model.ListaPaises = listPaises.Where(u => u.idPais == model.idPais).Select(u => new SelectListItem
            {
                Value = u.idPais.ToString(),
                Text = u.nmPais.ToString(),
            });
            return View(model);
        }

        public JsonResult JsSearch(string str)
        {
            DAOEstados daoEstados = new();
            List<Models.Estados> list = daoEstados.GetEstados();

            return Json(list);
        }

        public JsonResult JsAddEstado(string nmEstado, string flUF, int idPais)
        {
            DAOEstados dao = new();
            var obj = new Models.Estados()
            {
                nmEstado = nmEstado,
                flUF = flUF,
                idPais = idPais
            };
            dao.Insert(obj);

            List<Models.Estados> list = dao.GetEstados();
            return Json(new { success = true, novaListaEstados = list });
        }
    }
}
