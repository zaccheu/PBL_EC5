using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
            return View("Login", model);
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
    }
}
