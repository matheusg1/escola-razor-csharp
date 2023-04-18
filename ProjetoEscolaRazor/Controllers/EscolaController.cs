using ApiProjetoEscola.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjetoEscolaRazor.Controllers
{
    public class EscolaController : Controller
    {
        private readonly ILogger<EscolaController> _logger;
        string Baseurl = "https://localhost:44393/";

        public EscolaController(ILogger<EscolaController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var listaEscolas = await GetEscolas();

            return View(listaEscolas);
        }
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var escola = await GetEscolaById(id);
            return View(escola);
        }

        public async Task<List<Escola>> GetEscolas()
        {
            RestClient client = new RestClient(Baseurl);

            RestRequest request = new RestRequest("Escola/findAll", Method.Get);

            var response = await client.ExecuteAsync(request);

            if ((int)response.StatusCode == 404)
            {
                throw new Exception("BLABLA1");
            }
            else if ((int)response.StatusCode == 403)
            {
                throw new Exception("22");
            }
            var resultadoRequest = response.Content;

            var lista = JsonConvert.DeserializeObject<List<Escola>>(resultadoRequest);
            return lista;
        }

        public async Task<Escola> GetEscolaById(int id)
        {
            RestClient client = new RestClient(Baseurl);

            RestRequest request = new RestRequest("Escola/findById", Method.Get).AddQueryParameter("id", id);

            var response = await client.ExecuteAsync(request);

            if ((int)response.StatusCode == 404)
            {
                throw new Exception("BLABLA1");
            }
            else if ((int)response.StatusCode == 403)
            {
                throw new Exception("22");
            }
            var resultadoRequest = response.Content;

            var lista = JsonConvert.DeserializeObject<Escola>(resultadoRequest);
            return lista;
        }

    }
}
