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
using Microsoft.Extensions.Configuration;
using ProjetoEscolaRazor.Services;
using Microsoft.AspNetCore.Http;

namespace ProjetoEscolaRazor.Controllers
{
    public class EscolaController : Controller
    {
        private IConfiguration _config;
        readonly string BaseUrl;
        private EscolaService _escolaService;

        public EscolaController(IConfiguration config, EscolaService escolaService)
        {
            _config = config;
            BaseUrl = config.GetConnectionString("baseUrl");
            _escolaService = escolaService;
        }

        public async Task<IActionResult> Index()
        {
            var listaEscolas = await _escolaService.GetEscolas();

            return View(listaEscolas);
        }

        //[Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var escola = await _escolaService.GetEscolaById(id);
            return View(escola);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            var escola = new Escola(int.Parse(collection["EscolaId"]), collection["nome"], collection["endereco"]);
            var resultado = await _escolaService.EditEscola(escola);

            return RedirectToAction(nameof(Index));            
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var escola = await _escolaService.GetEscolaById(id);
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
                var create = await _escolaService.CreateEscola(escola);

                if (create.IsFailed)
                {
                    throw new Exception("FALHOU");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }


        //[HttpGet]
        //[Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var escola = await _escolaService.GetEscolaById(id);

            return View(escola);
        }

        
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delete = await _escolaService.DeleteEscola(id);

            if (delete.IsFailed)
            {
                throw new Exception("FALHOU");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
