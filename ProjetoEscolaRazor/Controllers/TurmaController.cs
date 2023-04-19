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
using ProjetoEscolaRazor.Services;

namespace ProjetoEscolaRazor.Controllers
{
    public class TurmaController : Controller
    {                
        EscolaService _escolaService;
        TurmaService _turmaService;        

        public TurmaController(TurmaService turmaService)
        {                        
            _turmaService = turmaService;
        }
        // GET: TurmaController
        public async Task<ActionResult> Index()
        {
            var listaTurmas = await _turmaService.GetTurmas();

            return View(listaTurmas);
        }

        // GET: TurmaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var turma = await _turmaService.GetTurmaById(id);
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
                var create = await _turmaService.CreateTurma(turma);

                if (create.IsFailed)
                {
                    throw new Exception("FALHOU");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: TurmaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var turma = await _turmaService.GetTurmaById(id);
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
    }
}
