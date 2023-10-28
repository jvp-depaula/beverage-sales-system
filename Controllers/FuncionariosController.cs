using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class FuncionariosController : Controller
    {
        DAOFuncionarios daoFuncionarios = new DAOFuncionarios();

        public ActionResult Index()
        {
            var daoFuncionarios = new DAOFuncionarios();
            List<Models.Funcionarios> list = daoFuncionarios.GetFuncionarios();

            foreach (var item in list)
            {
                item.nrCPF = Util.Util.FormatCPFCNPJ(item.nrCPF);
                item.nrTelefoneCelular = Util.Util.FormatTelefone(item.nrTelefoneCelular);
            }

            return View(list);
        }

        public ActionResult Create()
        {
            DAOCidades daoCidades = new();

            List<Models.Cidades> listCidades = daoCidades.GetCidades();

            var listaCidades = new Funcionarios
            {
                ListaCidades = listCidades.Select(u => new SelectListItem
                {
                    Value = u.idCidade.ToString(),
                    Text = u.nmCidade.ToString()
                }),
            };
            return View(listaCidades);
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Funcionarios model)
        {
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Funcionarios model)
        {
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Update(model);
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
            daoFuncionarios = new DAOFuncionarios();
            daoFuncionarios.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codFuncionario)
        {
            var daoFuncionarios = new DAOFuncionarios();
            DAOCidades daoCidades = new();

            var model = daoFuncionarios.GetFuncionario(codFuncionario);

            List<Models.Cidades> listCidades = daoCidades.GetCidades();

            model.ListaCidades = listCidades.Select(u => new SelectListItem
            {
                Value = u.idCidade.ToString(),
                Text = u.nmCidade.ToString(),
            });            
            return View(model);
        }
    }
}
