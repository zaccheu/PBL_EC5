using Microsoft.AspNetCore.Mvc.Rendering;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System.Collections.Generic;

namespace PBL_EC5.Controllers
{
    public class EstufaController : PadraoController<EstufaViewModel>
    {
        public EstufaController()
        {
            DAO = new EstufaDAO();
            GeraProximoId = true;
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
