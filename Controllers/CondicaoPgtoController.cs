using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;
using System;
using System.Reflection;

namespace Sistema.Controllers
{
    public class CondicaoPgtoController : Controller
    {
        DAOCondicaoPgto daoCondicaoPgto = new DAOCondicaoPgto();

        public ActionResult Index()
        {
            var daoCondicaoPgto = new DAOCondicaoPgto();
            List<CondicaoPgto> list = daoCondicaoPgto.GetCondicoesPgto();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.CondicaoPgto model)
        {
            daoCondicaoPgto = new DAOCondicaoPgto();
            daoCondicaoPgto.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.CondicaoPgto model)
        {
            daoCondicaoPgto = new DAOCondicaoPgto();
            daoCondicaoPgto.Update(model);
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
            daoCondicaoPgto = new DAOCondicaoPgto();
            daoCondicaoPgto.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codCategoria)
        {
            var DAOCondicaoPgto = new DAOCondicaoPgto();
            var model = DAOCondicaoPgto.GetCondicaoPgto(codCategoria);
            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOCondicaoPgto daoCondicaoPgto = new();
            List<Models.CondicaoPgto> list = daoCondicaoPgto.GetCondicoesPgto();

            return Json(list);
        }       
        
        public JsonResult JsAddCondicao(string dscondicaoPgto, decimal vlMulta, decimal vlDesconto, decimal vlJuros)
        {
            DAOCondicaoPgto dao = new();
            var obj = new Models.CondicaoPgto()
            {
                dsCondicaoPgto = dscondicaoPgto,
                vlMulta = vlMulta,
                vlDesconto= vlDesconto,
                vlJuros = vlJuros
            };
            dao.Insert(obj);

            List<Models.CondicaoPgto> list = dao.GetCondicoesPgto();


            return Json(new { success = true, novaListaCondicoes = list });
        }
    }
}
