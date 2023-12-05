using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;
using System.Globalization;
using static Sistema.Models.CondicaoPgto;

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
                throw new Exception(ex.Message);
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Details(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            return this.GetView(idFornecedor, nrModelo, nrSerie, nrNota);
        }
        public ActionResult Delete(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            return this.GetView(idFornecedor, nrModelo, nrSerie, nrNota);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            try
            {
                DAOCompras daoCompra = new ();
                daoCompra.CancelarCompra(idFornecedor, nrModelo, nrSerie, nrNota);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
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
                // this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                // return View();
                throw new Exception(ex.Message);
            }
        }

        public JsonResult MontaParcelas(string dtEmissao, decimal vlTotal, int idCondicaoPgto)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            var data = DateTime.Parse(dtEmissao, cultureInfo);

            DAOCondicaoPgto daoCondicaoPgto = new();
            List<CondicaoPgto.CondicaoPgtoVM> parcelas = daoCondicaoPgto.GetParcelas(idCondicaoPgto);
            List<Parcelas> parcelasCompra = new ();
            decimal aux = vlTotal;
            CondicaoPgtoVM Last = parcelas.Last();

            foreach (var item in parcelas)
            {
                Parcelas parcelaCompra = new()
                {
                    idFormaPgto = item.idFormaPgto,
                    dsFormaPgto = item.dsFormaPgto,
                    nrParcela = item.nrParcela,
                    vlParcela = Math.Round((vlTotal * item.txPercentual / 100), 2),
                    dtVencimento = Convert.ToDateTime(data.AddDays((double)item.qtDias).ToString("dd/MM/yyyy")),                    
                };

                aux -= parcelaCompra.vlParcela;

                if (item.Equals(Last)) 
                {
                    parcelaCompra.vlParcela += aux;        
                }

                parcelasCompra.Add(parcelaCompra);
            }

            return Json(parcelasCompra);
        }

        public JsonResult VerificaNota(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            DAOCompras daoCompras = new DAOCompras();
            var validNF = daoCompras.validNota(idFornecedor, nrModelo, nrSerie, nrNota);
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
                msg = "Já existe uma Nota Fiscal registrada com esse número e fornecedor, verifique!";
            }
            var result = new
            {
                type = type,
                msg = msg
            };
            return Json(result);
        }
    }
}
