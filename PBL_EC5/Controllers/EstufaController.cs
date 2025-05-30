using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System.Collections.Generic;
using System.Linq;

namespace PBL_EC5.Controllers
{
    public class EstufaController : PadraoController<EstufaViewModel>
    {
        public EstufaController()
        {
            DAO = new EstufaDAO();
            GeraProximoId = true;
        }

        [HttpPost]
        public IActionResult Pesquisar(string Numero_Serie, string Marca, string Id_Cliente, string Id_Estado)
        {
            var lista = DAO.Listagem(); 

            if (!string.IsNullOrEmpty(Numero_Serie))
                lista = lista.Where(e => e.Numero_Serie != null && e.Numero_Serie.Contains(Numero_Serie)).ToList();

            if (!string.IsNullOrEmpty(Marca))
                lista = lista.Where(e => e.Marca != null && e.Marca.Contains(Marca)).ToList();

            if (!string.IsNullOrEmpty(Id_Cliente) && int.TryParse(Id_Cliente, out int clienteId))
                lista = lista.Where(e => e.Id_Cliente == clienteId).ToList();

            if (!string.IsNullOrEmpty(Id_Estado) && int.TryParse(Id_Estado, out int estadoId))
                lista = lista.Where(e => e.Id_Estado == estadoId).ToList();

            return PartialView("_TabelaEstufas", lista);
        }

        protected override void ValidaDados(EstufaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Numero_Serie))
                ModelState.AddModelError("Descricao", "Preencha o numero de serie da estufa.");
        }

        protected override void PreencheDadosParaView(string Operacao, EstufaViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            ClienteDAO clienteDAO = new ClienteDAO();
            var clientes = clienteDAO.Listagem();
            List<SelectListItem> listaCliente = new List<SelectListItem>();

            listaCliente.Add(new SelectListItem("Selecione uma cliente...", "0"));
            foreach (ClienteViewModel cliente in clientes)
            {
                SelectListItem item = new SelectListItem(cliente.Razao_Social, cliente.Id.ToString());
                listaCliente.Add(item);
            }
            ViewBag.Clientes = listaCliente;
        }
    }
}
