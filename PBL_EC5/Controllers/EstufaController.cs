using PBL_EC5.Models;
using PBL_EC5.Models.DAO;

namespace PBL_EC5.Controllers
{
    public class EstufaController : PadraoController<EstufaViewModel>
    {
        public EstufaController()
        {
            DAO = new EstufaDAO();
            GeraProximoId = true;
        }

        //protected override void ValidaDados(EstufaViewModel model, string operacao)
        //{
        //    base.ValidaDados(model, operacao);
        //    if (string.IsNullOrEmpty(model.Descricao))
        //        ModelState.AddModelError("Descricao", "Preencha o nome da estufa.");
        //}
    }
}
