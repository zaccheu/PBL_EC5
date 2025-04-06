using Microsoft.AspNetCore.Mvc;
using PBL_EC5.Models;
using System;
using System.Collections.Generic;
using PBL_EC5.DAO;

namespace PBL_EC5.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            return View("Login", usuario);
        }

        public IActionResult Consulta()
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                List<UsuarioViewModel> usuarios = usuarioDAO.Listar();

                return View("Consulta", usuarios);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Login(UsuarioViewModel usuario)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                var usuarioExiste = usuarioDAO.ConsultaUser(usuario) ;

                if (usuarioExiste == null)
                {
                    ViewBag.UsuarioLogado = "Usuario não encontrado";
                    return View(usuario);
                }

                //Guardar informações do usuário na session

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Cadastrar()
        {
            try
            {
                UsuarioViewModel usuario = new UsuarioViewModel();
                return View("Cadastro", usuario);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(UsuarioViewModel usuario)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();

                if (usuarioDAO.ConsultaUser(usuario) == null)
                    usuarioDAO.Inserir(usuario);
                else
                    usuarioDAO.Alterar(usuario);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                usuarioDAO.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }
        public IActionResult Editar(int id)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                UsuarioViewModel usuario = usuarioDAO.ConsultaPorId(id);
                if (usuario == null)
                    return RedirectToAction("index");
                else
                    return View("Cadastro", usuario);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
