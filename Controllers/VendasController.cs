using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;
using System.Globalization;

namespace Sistema.Controllers
{
    public class VendasController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var DAOVendas = new DAOVendas();
                List<Models.Vendas> list = DAOVendas.GetVendas(null);
                return View(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Create()
        {
            DAOFuncionarios daoFuncionarios = new();
            DAOProdutos daoProdutos = new();
            DAOCondicaoPgto daoCondicaoPgto = new();
            DAOClientes daoClientes = new();

            List<Funcionarios> listFunc = daoFuncionarios.GetFuncionarios();
            List<Produtos> listProd = daoProdutos.GetProdutos(null);
            List<CondicaoPgto> listCond = daoCondicaoPgto.GetCondicoesPgto();
            List<Clientes> listClientes = daoClientes.GetClientes();

            var lista = new Vendas
            {
                ListFuncionarios = listFunc.Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.nmFuncionario.ToString(),
                    Selected = false
                }),
                ListClientes = listClientes.Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.nmCliente.ToString(),
                    Selected = false
                }),
                ListProdutos = listProd.Select(u => new SelectListItem
                {
                    Value = u.idProduto.ToString(),
                    Text = u.dsProduto.ToString() + " - " + u.qtdEstoque.ToString(),
                    Selected = false                    
                }),
                ListCondicaoPgto = listCond.Select(u => new SelectListItem
                {
                    Value = u.idCondicaoPgto.ToString(),
                    Text = u.dsCondicaoPgto.ToString(),
                    Selected = false
                })                
            };

            return View(lista);
        }
        
        [HttpPost]
        public ActionResult Create(Vendas model)
        {
            try
            {
                DAOVendas daoVenda = new();
                daoVenda.Insert(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Details(int idVenda)
        {
            return this.GetView(idVenda);
        }
        public ActionResult Delete(int idVenda)
        {
            return this.GetView(idVenda);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int idVenda)
        {
            try
            {
                DAOVendas daoVendas = new ();
                daoVendas.CancelarVenda(idVenda);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private ActionResult GetView(int idVenda)
        {
            try
            {
                DAOVendas daoVendas = new();
                var model = daoVendas.GetVenda(idVenda);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JsonResult MontaParcelas(decimal vlTotal, int idCondicaoPgto)
        {
            var dataVenda = DateTime.Now.ToString("dd/MM/yyyy");
            var cultureInfo = new CultureInfo("pt-BR");
            var data = DateTime.Parse(dataVenda, cultureInfo);

            DAOCondicaoPgto daoCondicaoPgto = new();
            List<CondicaoPgto> parcelas = daoCondicaoPgto.GetParcelas(idCondicaoPgto);
            List<Parcelas> parcelasCompra = new();
            decimal aux = vlTotal;
            CondicaoPgto Last = parcelas.Last();

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
    }
}
