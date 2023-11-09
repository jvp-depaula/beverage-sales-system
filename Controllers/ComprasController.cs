using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;
using Sistema.Util;

namespace Sistema.Controllers
{
    public class ComprasController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var daoCompras = new DAOCompras();
                List<Compras> list = daoCompras.GetCompras();
                return View(list);
            }
            catch (Exception ex)
            {
                this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                return View();
            }
        }
        public ActionResult Create()
        {
            DAOFornecedores daoFornecedores = new();
            DAOProdutos daoProdutos = new();
            DAOCondicaoPgto daoCondicaoPgto = new();

            List<Fornecedores> list = daoFornecedores.GetFornecedores();
            List<Produtos> listProd = daoProdutos.GetProdutos();
            List<CondicaoPgto> listCond = daoCondicaoPgto.GetCondicoesPgto();

            var lista = new Compras
            {
                ListFornecedores = list.Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.nmFornecedor.ToString(),
                    Selected = false
                }),
                ListProdutos = listProd.Select(u => new SelectListItem
                {
                    Value = u.idProduto.ToString(),
                    Text = u.dsProduto.ToString() + " - " + Convert.ToInt32(u.qtdEstoque).ToString(),
                    Selected = false                    
                }),
                ListCondicaoPagamento = listCond.Select(u => new SelectListItem
                {
                    Value = u.idCondicaoPgto.ToString(),
                    Text = u.dsCondicaoPgto.ToString(),
                    Selected = false
                })                
            };

            return View(lista);
        }
        
        [HttpPost]
        public ActionResult Create(Compras model)
        {
            try
            {
                DAOCompras daoCompra = new();
                daoCompra.Insert(model);
                this.AddFlashMessage(AlertMessage.INSERT_SUCESS);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                return View(model);
            }
        }
        public ActionResult Details(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            return this.GetView(idFornecedor, nrModelo, nrSerie, nrNota);
        }
        public ActionResult Cancelar(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            return this.GetView(idFornecedor, nrModelo, nrSerie, nrNota);
        }

        [HttpPost]
        public ActionResult Cancelar(int idFornecedor, string nrModelo, string nrSerie, int nrNota, Models.Compras model)
        {
            try
            {
                DAOCompras daoCompra = new ();
                daoCompra.CancelarCompra(idFornecedor, nrModelo, nrSerie, nrNota);
                this.AddFlashMessage("Registro cancelado com sucesso!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                return this.GetView(idFornecedor, nrModelo, nrSerie, nrNota);
            }
        }
        public JsonResult JsVerificaNF(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            DAOCompras daoCompra = new();
            var validNF = daoCompra.validNota(idFornecedor, nrModelo, nrSerie, nrNota);
            var type = string.Empty;
            var msg = string.Empty;
            if (validNF)
            {
                type = "success";
                msg = "Nota Fiscal válida!";
            }
            else
            {
                type = "danger";
                msg = "Já existe uma Nota Fiscal registrada com este número e fornecedor, verifique!";
            }
            var result = new
            {
                type = type,
                message = msg,
            };
            return Json(result);
        }
        private ActionResult GetView(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            try
            {
                var daoCompra = new DAOCompras();
                var model = daoCompra.GetCompra(null, idFornecedor, nrModelo, nrSerie, nrNota);
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                return View();
            }
        }
    }
}
