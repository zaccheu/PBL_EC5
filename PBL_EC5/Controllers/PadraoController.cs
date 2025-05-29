using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Collections.Generic;

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

        public virtual IActionResult Salvar(T model, string Operacao, bool IsCadastro)
        {
            try
            {
                if (!IsCadastro)
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
                    if (IsCadastro)
                        return RedirectToAction("Login", "Usuario");

                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                    {
                        DAO.Insert(model);
                        if (IsCadastro)
                            return RedirectToAction("Index", "Home");
                    }
                    else
                        DAO.Update(model);

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
                    ViewBag.MeuPerfil = true;

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

        public IActionResult Deletar(int id)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

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
    }
}