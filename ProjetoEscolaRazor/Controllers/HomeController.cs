using ProjetoEscolaRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoEscolaRazor.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEscolaRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string Baseurl = "https://localhost:44393/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var listaEscolas = await GetEscolas();

            return View(listaEscolas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

    }
}
