using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PBL_EC5.Controllers
{
    public abstract class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "form";

        public virtual IActionResult Index()
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                List<T> lista = DAO.Listagem();
                return View(NomeViewIndex, lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Cadastrar()
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                ViewBag.HabilitarSenha = true;
                ViewBag.Operacao = "I";
                T model = Activator.CreateInstance<T>();

                PreencheDadosParaView("I", model);
                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }

        public virtual IActionResult Salvar(T model, string Operacao, bool? IsCadastro, bool? IsPerfil)
        {
            try
            {
                if (IsCadastro == null || !IsCadastro.Value)
                {
                    if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                        return RedirectToAction("Login", "Usuario");
                    else
                        ViewBag.Logado = true;
                }

                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    if (IsCadastro != null && IsCadastro.Value)
                        return RedirectToAction("Login", "Usuario");

                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                    {
                        DAO.Insert(model);
                        if (IsCadastro != null && IsCadastro.Value)
                        {
                            ArmazenaDadosSessionUsuario(model);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        DAO.Update(model);

                        var sessionIdString = HttpContext.Session.GetString("Id");
                        if (int.TryParse(sessionIdString, out int sessionId) && model.Id == sessionId)
                        {
                            ArmazenaDadosSessionUsuario(model);
                        }

                        if (IsPerfil != null && IsPerfil.Value)
                            return RedirectToAction("Index", "Home");
                    }

                    return RedirecionaParaIndex(model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        protected virtual void ValidaDados(T model, string operacao)
        {
            ModelState.Clear();
            if (operacao == "I" && DAO.Consulta(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
        }

        public IActionResult Editar(int id, bool? isPerfil)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                var usuario = HelperControllers.RetornaDadosUsuario(HttpContext.Session);
                if (usuario.Id == id)
                    ViewBag.HabilitarSenha = true;

                ViewBag.IsPerfil = isPerfil ?? false;
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                if (model == null)
                    return RedirecionaParaIndex(model);
                else
                {
                    PreencheDadosParaView("A", model);
                    return View(NomeViewForm, model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        protected virtual IActionResult RedirecionaParaIndex(T model)
        {
            return RedirectToAction(NomeViewIndex);
        }

        public IActionResult Deletar(int id, bool? isCliente, bool? isUsuario)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                // Se for cliente, verifica se há estufas associadas
                if (isCliente != null && isCliente.Value)
                {
                    var estufaDAO = new EstufaDAO();
                    var estufas = estufaDAO.Listagem().Where(e => e.Id_Cliente == id).ToList();

                    if (estufas.Any())
                    {
                        TempData["Erro"] = "Não é possível deletar este cliente, pois existem estufas associadas a ele.";
                        return RedirectToAction("Index");
                    }
                }

                // Se for usuário, verifica se há clientes associadas. Se tiver, termina a relação de todos os clientes com o usuário
                if (isUsuario != null && isUsuario.Value)
                {
                    var clienteDAO = new ClienteDAO();
                    var clientes = clienteDAO.Listagem().Where(e => e.Id_Usuario == id).ToList();

                    if (clientes.Any())
                    {
                        // Desvincula todos os clientes do usuário antes de deletar
                        clienteDAO.DesvinculaClientesDoUsuario(id);
                    }
                }

                var model = DAO.Consulta(id);
                if (model != null)
                    DAO.Delete(id);
                return RedirecionaParaIndex(model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
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
        public void ArmazenaDadosSessionUsuario(T model)
        {
            if (model is UsuarioViewModel usuarioModel)
            {
                ArmazenaDadosSession(usuarioModel);
            }
        }

    }
}