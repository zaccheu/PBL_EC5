using PBL_EC5.DAO;
using PBL_EC5.Models;

namespace PBL_EC5.Controllers
{
    public class ClienteController : PadraoController<ClienteViewModel>
    {
        public ClienteController()
        {
            DAO = new ClienteDAO();
            GeraProximoId = true;
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
