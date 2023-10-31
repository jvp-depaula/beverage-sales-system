using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class NotaFiscalEntradaController : Controller
    {
        public IActionResult Index()
        {
            var daoNFEntrada = new DAONotaFiscalEntrada();
            List<NotaFiscalEntrada> list = daoNFEntrada.GetNotasFiscaisEntrada();
            return View(list);
        }

        public IActionResult Create()
        {
            DAOFornecedores daoFornecedores = new();
            DAOProdutos daoProdutos = new();
            // DAOCondicaoPgto daoCondicaoPgto = new();

            List<Fornecedores> list = daoFornecedores.GetFornecedores();
            List<Produtos> listProd = daoProdutos.GetProdutos();
            // List<CondicaoPgto> listCond = daoCondicaoPgto.GetCondicoesPgto();

            var lista = new NotaFiscalEntrada
            {
                ListaFornecedores = list.Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.nmFornecedor.ToString(),
                    Selected = false
                }),
                ListaProdutos = listProd.Select(u => new SelectListItem
                {
                    Value = u.idProduto.ToString(),
                    Text = u.dsProduto.ToString() + " - " + u.vlSaldo.ToString(),
                    Selected = false                    
                }),
                /*
                ListaCondicaoPgto = listCond.Select(u => new SelectListItem
                {
                    Value = u.idCondicaoPgto.ToString(),
                    Text = u.dsCondicaoPgto.ToString(),
                    Selected = false
                })
                */
            };

            return View(lista);
        }
    }
}
