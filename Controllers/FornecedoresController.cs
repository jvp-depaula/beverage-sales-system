using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class FornecedoresController : Controller
    {
        DAOFornecedores daoFornecedores = new DAOFornecedores();

        public ActionResult Index()
        {
            var daoFornecedores = new DAOFornecedores();
            List<Models.Fornecedores> list = daoFornecedores.GetFornecedores();

            foreach (var item in list)
            {
                item.nrCNPJ = Util.Util.FormatCPFCNPJ(item.nrCNPJ);
                item.nrTelefoneCelular = Util.Util.FormatTelefone(item.nrTelefoneCelular);
            }

            return View(list);
        }

        public ActionResult Create()
        {
            DAOCidades daoCidades = new();
            DAOCondicaoPgto daoCondicao = new();

            List<Models.Cidades> listCidades = daoCidades.GetCidades();
            List<Models.CondicaoPgto> listcondicaoPgto = daoCondicao.GetCondicoesPgto();

            var listaCidades = new Fornecedores 
            {
                ListaCidades = listCidades.Select(u => new SelectListItem
                {
                    Value = u.idCidade.ToString(),
                    Text = u.nmCidade.ToString()
                }),
                ListaCondicoesPgto = listcondicaoPgto.Select(u => new SelectListItem
                {
                    Value = u.idCondicaoPgto.ToString(),
                    Text = u.dsCondicaoPgto.ToString()
                })
            };
            return View(listaCidades);
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Fornecedores model)
        {
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Fornecedores model)
        {
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Update(model);
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
            daoFornecedores = new DAOFornecedores();
            daoFornecedores.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codFornecedor)
        {
            var daoFornecedores = new DAOFornecedores();
            var daoCidades = new DAOCidades();
            var daoCondicao = new DAOCondicaoPgto();

            var model = daoFornecedores.GetFornecedor(codFornecedor);
            List<Models.Cidades> listCidades = daoCidades.GetCidades();
            List<Models.CondicaoPgto> listcondicaoPgto = daoCondicao.GetCondicoesPgto();

            model.ListaCidades = listCidades.Select(u => new SelectListItem
            {
                Value = u.idCidade.ToString(),
                Text = u.nmCidade.ToString(),
            });

            model.ListaCondicoesPgto = listcondicaoPgto.Select(u => new SelectListItem
            {
                Value = u.idCondicaoPgto.ToString(),
                Text = u.dsCondicaoPgto.ToString()
            });

            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOFornecedores dAOFornecedores = new();
            List<Models.Fornecedores> list = daoFornecedores.GetFornecedores();

            return Json(list);
        }

        public JsonResult JsFornecedor(int idFornecedor)
        {
            DAOFornecedores dAOFornecedores = new();
            Fornecedores forn = daoFornecedores.GetFornecedor(idFornecedor);

            return Json(forn);
        }
    }
}
