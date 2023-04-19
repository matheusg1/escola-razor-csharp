using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoEscolaRazor.Model;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ProjetoEscolaRazor.DTO;
using FluentResults;

namespace ProjetoEscolaRazor.Controllers
{
    public class TurmaController : Controller
    {        
        private IConfiguration _config;
        readonly string BaseUrl;

        public TurmaController(IConfiguration config)
        {            
            _config = config;
            BaseUrl = config.GetConnectionString("baseUrl");
        }
        // GET: TurmaController
        public async Task<ActionResult> Index()
        {
            var listaTurmas = await GetTurmas();

            return View(listaTurmas);
        }

        // GET: TurmaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var turma = await GetTurmaById(id);
            return View(turma);
        }

        // GET: TurmaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TurmaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("EscolaId, Codigo")] CreateTurmaRequest turma)
        {
            if (ModelState.IsValid)
            {
                var create = await CreateTurma(turma);

                if (create.IsFailed)
                {
                    throw new Exception("FALHOU");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        public async Task<Result> CreateTurma(CreateTurmaRequest turma)
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Turma/create", Method.Post).AddJsonBody(turma);

            var response = await client.ExecuteAsync(request);

            var result = (int)response.StatusCode;

            if (result >= 200 && result < 100)
            {
                return Result.Fail("");
            }
            return Result.Ok();
        }

        // GET: TurmaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var turma = await GetTurmaById(id);
            return View(turma);
        }

        // POST: TurmaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TurmaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TurmaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<List<Turma>> GetTurmas()
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Turma/findAll", Method.Get);

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

            var lista = JsonConvert.DeserializeObject<List<Turma>>(resultadoRequest);
            return lista;
        }

        public async Task<Turma> GetTurmaById(int id)
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Turma/findById", Method.Get).AddQueryParameter("id", id);

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

            var lista = JsonConvert.DeserializeObject<Turma>(resultadoRequest);
            return lista;
        }
    }
}
