using Microsoft.AspNetCore.Mvc;
using PBL_EC5.Models;
using System;
using PBL_EC5.Models.DAO;
using System.Collections.Generic;

namespace PBL_EC5.Controllers
{
    public class EstufaController : Controller
    {
        EstufaDAO dao = new EstufaDAO();

        public IActionResult Index()
        {
            List<EstufaViewModel> lista = dao.ListarEstufas();

            return View(lista);
        }

        public IActionResult Consulta()
        {
            try
            {
                List<EstufaViewModel> estufa = dao.ListarEstufas();

                return View("Consulta", estufa);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Cadastrar()
        {
            EstufaViewModel estufa = new EstufaViewModel();

            return View("Cadastro", estufa);
        }

        public IActionResult Salvar(EstufaViewModel estufa)
        {
            dao.Inserir(estufa);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            dao.Excluir(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(EstufaViewModel estufa)
        {
            dao.Alterar(estufa);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
