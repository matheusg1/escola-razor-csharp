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
    public class EscolaService
    {
        IConfiguration _config;
        readonly string BaseUrl;

        public EscolaService(IConfiguration config)
        {
            _config = config;
            BaseUrl = config.GetConnectionString("baseUrl");
        }

        public async Task<List<Escola>> GetEscolas()
        {
            RestClient client = new RestClient(BaseUrl);

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
            RestClient client = new RestClient(BaseUrl);

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

        public async Task<Result> CreateEscola(CreateEscolaRequest escola)
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Escola/create", Method.Post).AddJsonBody(escola);

            var response = await client.ExecuteAsync(request);

            var result = (int)response.StatusCode;

            if (result >= 200 && result < 100)
            {
                return Result.Fail("");
            }
            return Result.Ok();
        }


        public async Task<Result> EditEscola(Escola escola)
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Escola/update", Method.Put)
                .AddJsonBody(new { escola.EscolaId, escola.Nome, escola.Endereco });

            var response = await client.ExecuteAsync(request);

            var result = (int)response.StatusCode;

            if (result >= 200 && result < 100)
            {
                return Result.Fail("");
            }
            return Result.Ok();
        }

        public async Task<Result> DeleteEscola(int id)
        {
            RestClient client = new RestClient(BaseUrl);

            RestRequest request = new RestRequest("Escola/delete", Method.Delete).AddQueryParameter("id", id);

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
