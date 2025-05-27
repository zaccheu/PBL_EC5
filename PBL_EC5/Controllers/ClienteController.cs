using Microsoft.AspNetCore.Mvc;
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
    }
}
