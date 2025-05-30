using Microsoft.AspNetCore.Mvc;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using System;
using System.Linq;

namespace PBL_EC5.Controllers
{
    public class ClienteController : PadraoController<ClienteViewModel>
    {
        public ClienteController()
        {
            DAO = new ClienteDAO();
            GeraProximoId = true;
        }

        [HttpPost]
        public IActionResult Pesquisar(string Razao_Social, string CNPJ, string CEP, string Ativo)
        {
            var lista = DAO.Listagem();

            if (!string.IsNullOrEmpty(Razao_Social))
                lista = lista.Where(c => c.Razao_Social != null && c.Razao_Social.Contains(Razao_Social, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(CNPJ))
                lista = lista.Where(c => c.CNPJ != null && c.CNPJ.Contains(CNPJ)).ToList();

            if (!string.IsNullOrEmpty(CEP))
                lista = lista.Where(c => c.CEP != null && c.CEP.Contains(CEP)).ToList();

            if (!string.IsNullOrEmpty(Ativo))
                lista = lista.Where(c => c.Ativo.ToString() == Ativo).ToList();

            return PartialView("_TabelaClientes", lista);
        }

        protected override void ValidaDados(ClienteViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            // Validação do CNPJ
            if (string.IsNullOrEmpty(model.CNPJ))
                ModelState.AddModelError("CNPJ", "O CNPJ é obrigatório.");
            else if (model.CNPJ.Length != 18)
                ModelState.AddModelError("CNPJ", "O CNPJ inválido.");

            // Validação da Razão Social
            if (string.IsNullOrEmpty(model.Razao_Social))
                ModelState.AddModelError("Razao_Social", "A Razão Social é obrigatória.");
            else if (model.Razao_Social.Length > 255)
                ModelState.AddModelError("Razao_Social", "A Razão Social deve ter no máximo 255 caracteres.");

            // Validação do CEP
            if (!string.IsNullOrEmpty(model.CEP) && model.CEP.Length != 9)
                ModelState.AddModelError("CEP", "Digite um CEP válido.");

            // Validação da Rua
            if (!string.IsNullOrEmpty(model.Rua) && model.Rua.Length > 255)
                ModelState.AddModelError("Rua", "A Rua deve ter no máximo 255 caracteres.");
        }
    }
}
