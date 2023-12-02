using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;
using System.Net.Http;
using System.Security.Policy;

namespace Sistema.Controllers
{
    public class ClientesController : Controller
    {
        DAOClientes daoClientes = new DAOClientes();

        public ActionResult Index()
        {
            var daoClientes = new DAOClientes();
            List<Models.Clientes> list = daoClientes.GetClientes();

            foreach (var item in list)
            {
                item.flTipo = item.flTipo == "F" ? "Física" : "Jurídica";
                item.nrCPFCNPJ = Util.Util.FormatCPFCNPJ(item.nrCPFCNPJ);
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

            var listaCidades = new Clientes
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
        public ActionResult Create(Sistema.Models.Clientes model)
        {
            daoClientes = new DAOClientes();
            daoClientes.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Clientes model)
        {
            daoClientes = new DAOClientes();
            daoClientes.Update(model);
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
            daoClientes = new DAOClientes();
            daoClientes.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codCliente)
        {
            var daoClientes = new DAOClientes();
            DAOCidades daoCidades = new();
            DAOCondicaoPgto daoCondicao = new();

            var model = daoClientes.GetCliente(codCliente);

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
            DAOClientes daoClientes = new();
            List<Models.Clientes> list = daoClientes.GetClientes();

            return Json(list);
        }

        public JsonResult JsCliente(int idCliente)
        {
            DAOClientes daoCliente = new();
            Clientes cliente = daoCliente.GetCliente(idCliente);

            return Json(cliente);
        }
    }
}
