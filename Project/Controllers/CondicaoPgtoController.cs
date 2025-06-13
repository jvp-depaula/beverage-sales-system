﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var daoFormaPgto = new DAOFormaPgto();
            List<FormaPgto> list = daoFormaPgto.GetFormaPgtos();

            var condicao = new CondicaoPgto();

            condicao.ListaFormaPgto = list.Select(u => new SelectListItem
            {
                Value = u.idFormaPgto.ToString(),
                Text = u.dsFormaPgto.ToString()
            });

            return View(condicao);
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.CondicaoPgto model)
        {
            decimal percentualParcelas = 0;

            foreach (var item in model.ListParcelas)
            {
                percentualParcelas += item.txPercentual;
            }

            if (percentualParcelas == (decimal)100)
            {
                daoCondicaoPgto = new DAOCondicaoPgto();
                daoCondicaoPgto.Insert(model);
            } 
            else
            {
                throw new Exception("Percentual das parcelas não fecha 100%");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.CondicaoPgto model)
        {
            decimal percentualParcelas = 0;

            foreach (var item in model.ListParcelas)
            {
                percentualParcelas += item.txPercentual;
            }

            if (percentualParcelas == (decimal)100)
            {
                daoCondicaoPgto = new DAOCondicaoPgto();
                daoCondicaoPgto.Update(model);
            }
            else
            {
                throw new Exception("Percentual das parcelas não fecha 100%");
            }
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

        private ActionResult GetView(int? idCondicaoPgto)
        {
            var DAOCondicaoPgto = new DAOCondicaoPgto();
            var model = DAOCondicaoPgto.GetCondicaoPgto(idCondicaoPgto);

            var daoFormaPgto = new DAOFormaPgto();
            List<FormaPgto> list = daoFormaPgto.GetFormaPgtos();

            var condicao = new CondicaoPgto();

            model.ListaFormaPgto = list.Select(u => new SelectListItem
            {
                Value = u.idFormaPgto.ToString(),
                Text = u.dsFormaPgto.ToString()
            });

            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOCondicaoPgto daoCondicaoPgto = new();
            List<Models.CondicaoPgto> list = daoCondicaoPgto.GetCondicoesPgto();

            return Json(list);
        }       

        public JsonResult JsGetCondicao(int idCondicaoPgto)
        {
            DAOCondicaoPgto daoCondicaoPgto = new();
            CondicaoPgto cond = daoCondicaoPgto.GetCondicaoPgto(idCondicaoPgto);

            return Json(cond);
        }
        
        public JsonResult JsAddCondicao(string dscondicaoPgto, decimal txMulta, decimal txDesconto, decimal txJuros)
        {
            DAOCondicaoPgto dao = new();
            var obj = new Models.CondicaoPgto()
            {
                dsCondicaoPgto = dscondicaoPgto,
                txMulta = txMulta,
                txDesconto= txDesconto,
                txJuros = txJuros
            };
            dao.Insert(obj);

            List<Models.CondicaoPgto> list = dao.GetCondicoesPgto();


            return Json(new { success = true, novaListaCondicoes = list });
        }        
    }
}
