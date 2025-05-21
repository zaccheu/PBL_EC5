using PBL_EC5.DAO;
using PBL_EC5.Models;

namespace PBL_EC5.Controllers
{
    public class UsuarioController : PadraoController<UsuarioViewModel>
    {

        public UsuarioController()
        {
            DAO = new UsuarioDAO();
            GeraProximoId = true;
        }

        protected override void ValidaDados(UsuarioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Cpf))
                ModelState.AddModelError("Cpf", "Preencha o seu CPF.");
        }
    }
}
