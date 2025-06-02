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
            string ip = "34.231.100.192";
            string url = $"http://{ip}:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:001/attributes/temperature?lastN=1";

            using (var httpClient = new HttpClient())
            {
                // Define os headers da FIWARE
                httpClient.DefaultRequestHeaders.Add("fiware-service", "smart");
                httpClient.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                try
                {

                    //var json = "{\r\n  \"contextResponses\": [\r\n    {\r\n      \"contextElement\": {\r\n        \"attributes\": [\r\n          {\r\n            \"name\": \"temperature\",\r\n            \"values\": [\r\n              {\r\n                \"_id\": \"68364f8271b50900070a64ef\",\r\n                \"recvTime\": \"2025-05-27T23:49:22.947Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.6\r\n              },\r\n              {\r\n                \"_id\": \"68364f8471b50900070a64f3\",\r\n                \"recvTime\": \"2025-05-27T23:49:23.971Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.6\r\n              },\r\n              {\r\n                \"_id\": \"68364f8571b50900070a64f7\",\r\n                \"recvTime\": \"2025-05-27T23:49:24.999Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.7\r\n              },\r\n              {\r\n                \"_id\": \"68364f8571b50900070a64fa\",\r\n                \"recvTime\": \"2025-05-27T23:49:25.893Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.7\r\n              },\r\n              {\r\n                \"_id\": \"68364f8771b50900070a64ff\",\r\n                \"recvTime\": \"2025-05-27T23:49:27.038Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.8\r\n              },\r\n              {\r\n                \"_id\": \"68364f8771b50900070a6502\",\r\n                \"recvTime\": \"2025-05-27T23:49:27.696Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 24.8\r\n              },\r\n              {\r\n                \"_id\": \"68364f8871b50900070a6507\",\r\n                \"recvTime\": \"2025-05-27T23:49:28.861Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8a71b50900070a650b\",\r\n                \"recvTime\": \"2025-05-27T23:49:30.315Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8a71b50900070a6510\",\r\n                \"recvTime\": \"2025-05-27T23:49:30.737Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8c71b50900070a6514\",\r\n                \"recvTime\": \"2025-05-27T23:49:32.073Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8d71b50900070a6517\",\r\n                \"recvTime\": \"2025-05-27T23:49:33.113Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8e71b50900070a651b\",\r\n                \"recvTime\": \"2025-05-27T23:49:33.983Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f8f71b50900070a651f\",\r\n                \"recvTime\": \"2025-05-27T23:49:35.018Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f9071b50900070a6523\",\r\n                \"recvTime\": \"2025-05-27T23:49:36.054Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25\r\n              },\r\n              {\r\n                \"_id\": \"68364f9171b50900070a6527\",\r\n                \"recvTime\": \"2025-05-27T23:49:37.082Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9271b50900070a652b\",\r\n                \"recvTime\": \"2025-05-27T23:49:38.066Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9371b50900070a652f\",\r\n                \"recvTime\": \"2025-05-27T23:49:39.134Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9471b50900070a6534\",\r\n                \"recvTime\": \"2025-05-27T23:49:40.140Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9571b50900070a6537\",\r\n                \"recvTime\": \"2025-05-27T23:49:41.175Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9671b50900070a653b\",\r\n                \"recvTime\": \"2025-05-27T23:49:42.219Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9771b50900070a653f\",\r\n                \"recvTime\": \"2025-05-27T23:49:43.229Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9871b50900070a6544\",\r\n                \"recvTime\": \"2025-05-27T23:49:44.272Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9971b50900070a6547\",\r\n                \"recvTime\": \"2025-05-27T23:49:45.276Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9a71b50900070a654b\",\r\n                \"recvTime\": \"2025-05-27T23:49:46.305Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9b71b50900070a6550\",\r\n                \"recvTime\": \"2025-05-27T23:49:47.319Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9c71b50900070a6554\",\r\n                \"recvTime\": \"2025-05-27T23:49:48.345Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9d71b50900070a6557\",\r\n                \"recvTime\": \"2025-05-27T23:49:49.368Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9e71b50900070a655b\",\r\n                \"recvTime\": \"2025-05-27T23:49:50.395Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364f9f71b50900070a655f\",\r\n                \"recvTime\": \"2025-05-27T23:49:51.423Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              },\r\n              {\r\n                \"_id\": \"68364fa071b50900070a6563\",\r\n                \"recvTime\": \"2025-05-27T23:49:52.467Z\",\r\n                \"attrName\": \"temperature\",\r\n                \"attrType\": \"Integer\",\r\n                \"attrValue\": 25.1\r\n              }\r\n            ]\r\n          }\r\n        ],\r\n        \"id\": \"urn:ngsi-ld:Lamp:001\",\r\n        \"isPattern\": false,\r\n        \"type\": \"Lamp\"\r\n      },\r\n      \"statusCode\": {\r\n        \"code\": \"200\",\r\n        \"reasonPhrase\": \"OK\"\r\n      }\r\n    }\r\n  ]\r\n}\r\n";

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

                var listaDadosEstufa = dados
                    .Where(d => d.AttrName == "temperature")
                    .Select(d => new DadosEstufaViewModel
                    {
                        Id = DAO.ProximoId(),
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
    }
}
