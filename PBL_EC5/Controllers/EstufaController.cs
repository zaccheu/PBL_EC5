using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
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

        public override IActionResult Index()
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                var isAdm = HelperControllers.VerificaAdm(HttpContext.Session);
                List<EstufaViewModel> lista = DAO.Listagem();

                if (!isAdm)
                {
                    // Pega o id do usuário logado
                    var usuario = HelperControllers.RetornaDadosUsuario(HttpContext.Session);

                    // Busca os clientes do usuário
                    var clienteDAO = new ClienteDAO();
                    var clientesDoUsuario = clienteDAO.Listagem()
                        .Where(c => c.Id_Usuario == usuario.Id)
                        .Select(c => c.Id)
                        .ToList();

                    // Filtra as estufas apenas dos clientes do usuário
                    lista = lista.Where(e => clientesDoUsuario.Contains(e.Id_Cliente ?? 0)).ToList();
                }

                return View(NomeViewIndex, lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Visualizar(int id)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                EstufaViewModel model = DAO.Consulta(id);
                if (model == null)
                    return View("Error", new ErrorViewModel("Estufa não encontrada."));

                return View("Dashboard", model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        [HttpPost]
        public IActionResult Pesquisar(string Numero_Serie, string Marca, string Id_Cliente, string Ativo)
        {
            var lista = DAO.Listagem(); 

            if (!string.IsNullOrEmpty(Numero_Serie))
                lista = lista.Where(e => e.Numero_Serie != null && e.Numero_Serie.Contains(Numero_Serie, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(Marca))
                lista = lista.Where(e => e.Marca != null && e.Marca.Contains(Marca, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(Id_Cliente) && int.TryParse(Id_Cliente, out int clienteId))
                lista = lista.Where(e => e.Id_Cliente == clienteId).ToList();

            if (!string.IsNullOrEmpty(Ativo))
                lista = lista.Where(c => c.Ativo.ToString() == Ativo).ToList();

            return PartialView("_TabelaEstufas", lista);
        }

        protected override void ValidaDados(EstufaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (string.IsNullOrEmpty(model.Numero_Serie))
                ModelState.AddModelError("Descricao", "Preencha o numero de serie da estufa.");

            if (model.Id_Cliente == null || model.Id_Cliente <= 0)
                ModelState.AddModelError("Id_Cliente", "Preencha o cliente dono da estufa.");
        }

        protected override void PreencheDadosParaView(string Operacao, EstufaViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            //Preenche o combo de clientes
            ClienteDAO clienteDAO = new ClienteDAO();
            var clientes = clienteDAO.Listagem();

            List<SelectListItem> listaCliente = new List<SelectListItem>();
            listaCliente.Add(new SelectListItem("Selecione um cliente", "0"));
            foreach (ClienteViewModel cliente in clientes)
            {
                SelectListItem item = new SelectListItem(cliente.Razao_Social, cliente.Id.ToString());
                listaCliente.Add(item);
            }

            ViewBag.Clientes = listaCliente;
        }
    }
}
