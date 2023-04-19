using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjetoEscolaRazor.DTO;
using ProjetoEscolaRazor.Model;
using RestSharp;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ProjetoEscolaRazor.Services
{
    public class TurmaService
    {
        IConfiguration _config;
        readonly string BaseUrl;
        EscolaService _escolaService;        

        public TurmaService(IConfiguration config, EscolaService escolaService)
        {
            _config = config;
            BaseUrl = config.GetConnectionString("baseUrl");
            _escolaService = escolaService;            
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
    }
}
