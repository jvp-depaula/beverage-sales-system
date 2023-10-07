using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class CidadesController : Controller
    {
        DAOCidades daoCidades = new();

        public ActionResult Index()
        {
            var daoCidades = new DAOCidades();
            List<Cidades> list = daoCidades.GetCidades();
            return View(list);
        }

        public ActionResult Create()
        {
            DAOEstados daoEstados = new ();
            List<Models.Estados> listEstados = daoEstados.GetEstados();
            var listaEstados = new Cidades
            {
                ListaEstados = listEstados.Select(u => new SelectListItem
                {
                    Value = u.idEstado.ToString(),
                    Text = u.nmEstado.ToString()
                })
            };

            return View(listaEstados);
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
            DAOEstados daoEstados = new ();
            DAOCidades daoCidades = new ();
            var model = daoCidades.GetCidade(codCidade, "");

            List<Models.Estados> listEstados = daoEstados.GetEstados();

            model.ListaEstados = listEstados.Where(u => u.idEstado == model.idEstado).Select(u => new SelectListItem
            {
                Value = u.idEstado.ToString(),
                Text = u.nmEstado.ToString(),
            });
            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOCidades daoCidades = new();
            List<Models.Cidades> list = daoCidades.GetCidades();

            return Json(list);
        }

        public JsonResult JsAddCidade(string nmCidade, int idEstado, string DDD)
        {
            DAOCidades dao = new();
            var obj = new Models.Cidades()
            {
                nmCidade = nmCidade,
                idEstado = idEstado,
                DDD = DDD
            };
            dao.Insert(obj);

            List<Models.Cidades> list = dao.GetCidades();
            return Json(new { success = true, novaListaCidades = list });
        }

        public JsonResult JsConsultaCidade(string nmCidade)
        {
            DAOCidades daoCidades = new();
            var cidade = daoCidades.GetCidade(null, nmCidade);
            return Json(cidade);
        }
    }
}
