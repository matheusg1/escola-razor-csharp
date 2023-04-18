using ProjetoEscolaRazor.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentResults;
using ProjetoEscolaRazor.DTO;

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

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var escola = await GetEscolaById(id);
            return View(escola);
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var escola = await GetEscolaById(id);
            return View(escola);
        }
        
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> Create([Bind("EscolaId, Nome, Endereco")] CreateEscolaRequest escola)
        {
            if (ModelState.IsValid)
            {
                var create = await CreateEscola(escola);

                if (create.IsFailed)
                {
                    throw new Exception("FALHOU");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }


        /*public async Task<List<Escola>> CreateEscola()
        {
            RestClient client = new RestClient(Baseurl);

            RestRequest request = new RestRequest("Escola/create", Method.Post);
                //.AddJsonBody();

            var response = await client.ExecuteAsync(request);
        }*/

        public async Task<Result> CreateEscola(CreateEscolaRequest escola)
        {
            RestClient client = new RestClient(Baseurl);

            RestRequest request = new RestRequest("Escola/create", Method.Post).AddJsonBody(escola);

            var response = await client.ExecuteAsync(request);

            var result = (int) response.StatusCode;

            if(result != 200)
            {
                return Result.Fail("");
            }
            return Result.Ok();
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
