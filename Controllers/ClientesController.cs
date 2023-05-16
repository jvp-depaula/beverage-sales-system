using Microsoft.AspNetCore.Mvc;
using Sistema.DAO;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class ClientesController : Controller
    {
        DAOClientes daoClientes = new DAOClientes();

        public ActionResult Index()
        {
            var daoClientes = new DAOClientes();
            List<Models.Clientes> list = daoClientes.GetClientes();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
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
            var model = daoClientes.GetCliente(codCliente);
            return View(model);
        }
    }
}
