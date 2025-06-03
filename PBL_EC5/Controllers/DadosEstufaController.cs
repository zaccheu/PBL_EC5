using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL_EC5.DAO;
using PBL_EC5.Models;
using PBL_EC5.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PBL_EC5.Controllers
{
    public class DadosEstufaController : PadraoController<DadosEstufaViewModel>
    {
        public DadosEstufaController()
        {
            DAO = new DadosEstufaDAO();
            GeraProximoId = true;
        }

        public IActionResult Visualizar(int id)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                DadosEstufaViewModel model = DAO.Consulta(id);
                if (model == null)
                    return View("Error", new ErrorViewModel("Dados não encontrados."));

                return View("Dashboard", model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        protected override void ValidaDados(DadosEstufaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
        }

        protected override void PreencheDadosParaView(string Operacao, DadosEstufaViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            //Preenche o combo de clientes
            EstufaDAO estufaDAO = new EstufaDAO();
            var estufas = estufaDAO.Listagem();

            List<SelectListItem> listaEstufas = new List<SelectListItem>();
            listaEstufas.Add(new SelectListItem("Selecione um cliente", "0"));
            foreach (EstufaViewModel cliente in estufas)
            {
                SelectListItem item = new SelectListItem(cliente.Numero_Serie, cliente.Id.ToString());
                listaEstufas.Add(item);
            }

            ViewBag.Estufas = listaEstufas;
        }

        [HttpPost]
        public async Task<IActionResult> BuscarTemperatura(int estufaId)
        {
            string ip = "75.101.135.55";
            string url = $"http://{ip}:8666/STH/v1/contextEntities/type/Temp/id/urn:ngsi-ld:Temp:001/attributes/temperature?lastN=30";

            using (var httpClient = new HttpClient())
            {
                // Define os headers da FIWARE
                httpClient.DefaultRequestHeaders.Add("fiware-service", "smart");
                httpClient.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                try
                {
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return Content(json, "application/json");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Erro ao acessar a API externa.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(500, $"Erro de requisição: {ex.Message}");
                }
            }
        }

        [HttpPost]
        public IActionResult SalvarDados([FromBody] DadosEstufaRequest request)
        {
            int idEstufa = request.IdEstufa;
            var dados = request.Dados;

            try
            {
                if (dados == null || !dados.Any())
                    return BadRequest("Nenhum dado recebido.");

                var proximoId = DAO.ProximoId();
                var listaDadosEstufa = dados
                    .Where(d => d.AttrName == "temperature")
                    .Select(d => new DadosEstufaViewModel
                    {
                        Id = proximoId++,
                        Id_Estufa = idEstufa,
                        Id_Temperatura = d.Id,
                        Temperatura = Convert.ToDouble(d.AttrValue),
                        Data = DateTime.Parse(d.RecvTime),
                        Tensao = null
                    }).ToList();

                foreach (var item in listaDadosEstufa)
                {
                    DadosEstufaDAO dao = new DadosEstufaDAO();
                    var existeIdTemperatura = dao.ConsultaIdTemperatura(item.Id_Temperatura);

                    if (existeIdTemperatura)
                        return Ok("Esta temperatura já foi salva outra vez.");

                    DAO.Insert(item);
                }

                return Ok("Dados salvos com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar os dados: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuscarHistorico([FromBody] FiltroHistoricoRequest filtro)
        {
            try
            {
                if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                    return RedirectToAction("Login", "Usuario");
                else
                    ViewBag.Logado = true;

                DadosEstufaDAO dao = new DadosEstufaDAO();
                var historico = await dao.BuscarHistorico(filtro);

                // Retorna a lista como JSON, já no formato esperado pelo JS
                return Json(historico);
            }
            catch (Exception erro)
            {
                return StatusCode(500, $"Erro ao buscar histórico: {erro.Message}");
            }
        }


    }
}
