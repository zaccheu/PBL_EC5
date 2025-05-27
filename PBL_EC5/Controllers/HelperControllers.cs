using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBL_EC5.Models;
using System;
using System.Reflection;

namespace PBL_EC5.Controllers
{
    public class HelperControllers : Controller
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

        public static Boolean VerificaAdm(ISession session)
        {
            string adm = session.GetString("Administrador");
            if (adm == "1")
                return true;
            else
                return false;
        }

        public static UsuarioViewModel RetornaDadosUsuario(ISession session)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();

            usuario.Id = int.TryParse(session.GetString("Id"), out int id) ? id : 0;
            usuario.Nome = session.GetString("Nome");
            usuario.Email = session.GetString("Email");
            usuario.Cpf = session.GetString("Cpf");
            if (session.TryGetValue("Foto", out byte[] fotoBytes))
                usuario.Foto = fotoBytes;

            return usuario;
        }
    }
}
