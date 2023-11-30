using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.DAO;
using Sistema.Models;
using System.Security.Cryptography.X509Certificates;

namespace Sistema.Controllers
{
    public class ProdutosController : Controller
    {
        DAOProdutos daoProdutos = new DAOProdutos();

        public ActionResult Index()
        {
            var daoProdutos = new DAOProdutos();
            List<Models.Produtos> list = daoProdutos.GetProdutos();
            return View(list);
        }

        public ActionResult Create()
        {
            DAOFornecedores daoFornecedores = new();
            DAOCategorias daoCategorias = new();
            DAOUnidades daoUnidades = new();
            DAOMarcas daoMarcas = new();

            List<Models.Fornecedores> listFornecedores = daoFornecedores.GetFornecedores();
            List<Models.Categorias> listCategorias = daoCategorias.GetCategorias();
            List<Models.Unidades> listUnidades = daoUnidades.GetUnidades();
            List<Models.Marcas> listMarcas = daoMarcas.GetMarcas();

            var listas = new Produtos
            {
                ListaFornecedores = listFornecedores.Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.nmFornecedor.ToString()
                }),

                ListaCategorias = listCategorias.Select(u => new SelectListItem
                {
                    Value = u.idCategoria.ToString(),
                    Text = u.nmCategoria.ToString()
                }),

                ListaUnidades = listUnidades.Select(u => new SelectListItem
                {
                    Value = u.idUnidade.ToString(),
                    Text = u.dsUnidade.ToString()
                }),

                ListaMarcas = listMarcas.Select(u => new SelectListItem
                {
                    Value = u.idMarca.ToString(),
                    Text = u.nmMarca.ToString()
                })
            };            
            return View(listas);
        }

        [HttpPost]
        public ActionResult Create(Sistema.Models.Produtos model)
        {
            daoProdutos = new DAOProdutos();
            daoProdutos.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(Sistema.Models.Produtos model)
        {
            daoProdutos = new DAOProdutos();
            daoProdutos.Update(model);
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
            daoProdutos = new DAOProdutos();
            daoProdutos.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return this.GetView(id);
        }

        private ActionResult GetView(int? codProduto)
        {
            var daoMarcas = new DAOMarcas();
            var daoFornecedores = new DAOFornecedores();
            var daoCategorias = new DAOCategorias();
            var daoUnidades = new DAOUnidades();
            
            List<Models.Marcas> listMarcas = daoMarcas.GetMarcas();
            List<Fornecedores> listFornecedores = daoFornecedores.GetFornecedores();
            List<Categorias> listCategorias = daoCategorias.GetCategorias();
            List<Unidades> listUnidades = daoUnidades.GetUnidades();

            var daoProdutos = new DAOProdutos();
            var model = daoProdutos.GetProduto(codProduto);

            model.ListaCategorias = listCategorias.Where(u => u.idCategoria == model.idCategoria).Select(u => new SelectListItem
            {
                Value = u.idCategoria.ToString(),
                Text = u.nmCategoria.ToString(),
            });

            model.ListaFornecedores = listFornecedores.Where(u => u.id == model.idFornecedor).Select(u => new SelectListItem
            {
                Value = u.id.ToString(),
                Text = u.nmFornecedor.ToString(),
            });

            model.ListaMarcas = listMarcas.Where(u => u.idMarca == model.idMarca).Select(u => new SelectListItem
            {
                Value = u.idMarca.ToString(),
                Text = u.nmMarca.ToString(),
            });

            model.ListaUnidades = listUnidades.Where(u => u.idUnidade == model.idUnidade).Select(u => new SelectListItem
            {
                Value = u.idUnidade.ToString(),
                Text = u.dsUnidade.ToString(),
            });

            return View(model);
        }

        public JsonResult JsSearch()
        {
            DAOProdutos daoProdutos = new();
            List<Models.Produtos> list = daoProdutos.GetProdutos();
            return Json(list);
        }
        
        public JsonResult jsGetProduto(int idProduto)
        {
            DAOProdutos daoProdutos = new();
            Produtos prod = daoProdutos.GetProduto(idProduto);
            return Json(prod);
        }               
    }
}
