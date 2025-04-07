using Microsoft.AspNetCore.Mvc;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System.Collections.Generic;

namespace PBL_EC5.Controllers
{
    public class MotorController : Controller
    {
        private MotorDAO dao = new MotorDAO();

        public IActionResult Index()
        {
            List<MotorViewModel> lista = dao.Listar();

            return View(lista);
        }

        public IActionResult Editar(int id)
        {
            MotorViewModel motor = dao.ObterPorId(id);

            if (motor == null)
                return NotFound();

            return View("Cadastro", motor);
        }

        public IActionResult Cadastrar()
        {
            MotorViewModel model = new MotorViewModel();

            return View("Cadastro", model);
        }

        public IActionResult Salvar(MotorViewModel motor)
        {
            dao.Inserir(motor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarMotor(MotorViewModel motor)
        {
            if (motor != null)
                return NotFound();

            dao.Alterar(motor);

            return View(motor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(MotorViewModel motor)
        {
            if (!ModelState.IsValid)
                return View(motor);

            dao.Alterar(motor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            MotorViewModel motor = dao.ObterPorId(id);
            try
            {
                dao.Excluir(motor.Id);

                if (motor == null)
                    return NotFound();
            }
            catch
            {
                return View("Error", new ErrorViewModel("Erro ao excluir o motor."));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
