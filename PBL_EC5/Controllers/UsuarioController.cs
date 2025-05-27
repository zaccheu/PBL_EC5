using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using System;
using System.IO;
using System.Linq;

namespace PBL_EC5.Controllers
{
    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        public UsuarioController()
        {
            DAO = new UsuarioDAO();
            GeraProximoId = true;
        }

        public IActionResult Login()
        {
            var model = new UsuarioViewModel();

            ViewBag.EsconderNavbar = true;
            ViewBag.Erro = TempData["Erro"];

            return View("Login", model);
        }

        public override IActionResult Cadastrar()
        {
            ViewBag.EsconderNavbar = true;
            ViewBag.CadInicial = true; 
            return base.Cadastrar();
        }

        public IActionResult Entrar(UsuarioViewModel model)
        {
            ModelState.Remove("Nome");

            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Preencha os campos corretamente!";
                return RedirectToAction("Login", model);
            }

            UsuarioDAO dao = new UsuarioDAO();
            UsuarioViewModel usuario = dao.ConsultaLogin(model);

            if (usuario != null)
            {
                ArmazenaDadosSession(usuario);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Erro"] = "Email ou senha incorreto!";
                return RedirectToAction("Login", model);
            }

        }

        public void ArmazenaDadosSession(UsuarioViewModel model)
        {
            HttpContext.Session.SetString("Logado", "true");
            HttpContext.Session.SetString("Id", model.Id.ToString());
            HttpContext.Session.SetString("Nome", model.Nome);
            HttpContext.Session.SetString("Email", model.Email);
            HttpContext.Session.SetString("Cpf", model.Cpf);
            HttpContext.Session.SetString("Administrador", model.Administrador.ToString());
            if (model.Foto != null)
                HttpContext.Session.Set("Foto", model.Foto);
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray(); // Converte para byte[]
                }
            else
                return null;
        }

        protected override void ValidaDados(UsuarioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Cpf))
                ModelState.AddModelError("Cpf", "Preencha o seu CPF.");

            if (model.FotoUpload != null)
            {
                var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extensao = Path.GetExtension(model.FotoUpload.FileName).ToLowerInvariant();
                var maxSize = 5 * 1024 * 1024; // 5 MB (ou o limite que desejar)

                if (string.IsNullOrEmpty(extensao) || !extensoesPermitidas.Contains(extensao) || model.FotoUpload.Length > maxSize)
                    ModelState.AddModelError("FotoUpload", "Arquivo de foto inválido ou muito grande (máx 5MB).");
            }

            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva. 
                if (operacao == "A" && model.FotoUpload == null && model.Foto != null)
                {
                    UsuarioViewModel usuario = DAO.Consulta(model.Id);
                    model.Foto = usuario.Foto;
                }
                else
                {
                    model.Foto = ConvertImageToByte(model.FotoUpload);
                }
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
