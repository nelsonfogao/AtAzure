using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Pessoas;

namespace WebApp.ApiServices
{
    public class PessoaApi : IPessoaApi
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly IEstadoApi _estadoApi;
        private readonly IPaisApi _paisApi;

        public PessoaApi(IEstadoApi estadoApi, IPaisApi paisApi)
        {
            this._estadoApi = estadoApi;
            this._paisApi = paisApi;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri("https://localhost:44396/");
        }

        public async Task<List<PessoaViewModel>> GetAmigos(int id)
        {
            var response = await httpClient.GetAsync($"api/pessoas/{id}/amigos");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<PessoaViewModel>>(content);

            return viewModel;
        }

        public async Task<List<PessoaViewModel>> GetPessoas()
        {
            var response = await httpClient.GetAsync($"api/pessoas");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<PessoaViewModel>>(content);

            return viewModel;
        }

        public async Task<PessoaViewModel> GetPessoa(int id)
        {
            var response = await httpClient.GetAsync($"api/pessoas/" + id);

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<PessoaViewModel>(content);

            viewModel.PaisViewModel = await _paisApi.GetPais(viewModel.PaisId);

            viewModel.EstadoViewModel = await _estadoApi.GetEstado(viewModel.EstadoId);

            return viewModel;
        }

        public Task PostAmigos(int pessoaId, int[] ids)
        {
            var idsAsJson = JsonConvert.SerializeObject(new { ids });

            var content = new StringContent(idsAsJson, Encoding.UTF8, "application/json");

            httpClient.PostAsync($"api/pessoas/{pessoaId}/amigos", content);

            return Task.CompletedTask;
        }

        public async Task<CriarPessoaViewModel> PostPessoaAsync(CriarPessoaViewModel pessoaViewModel)
        {
            var pessoaViewModelJson = JsonConvert.SerializeObject(pessoaViewModel);

            var conteudo = new StringContent(pessoaViewModelJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/pessoas", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return pessoaViewModel;
            }

            return pessoaViewModel;
        }
    }
}
