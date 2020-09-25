using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Paises;

namespace WebApp.ApiServices
{
    public class EstadoApi : IEstadoApi
    {
        private readonly HttpClient httpClient;

        private readonly IPaisApi paisApi;

        public EstadoApi(IPaisApi paisApi)
        {
            this.paisApi = paisApi;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri("https://localhost:44337/");
        }
        public async Task<CriarEstadoViewModel> PostEstadoAsync(CriarEstadoViewModel estadoViewModel)
        {
            var estadoViewModelJson = JsonConvert.SerializeObject(estadoViewModel);

            var conteudo = new StringContent(estadoViewModelJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/estados", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return estadoViewModel;
            }

            return estadoViewModel;
        }

        public async Task<List<EstadoViewModel>> GetEstados()
        {
            var response = await httpClient.GetAsync($"api/estados");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<EstadoViewModel>>(content);

            return viewModel;
        }

        public async Task<EstadoViewModel> GetEstado(int id)
        {
            var response = await httpClient.GetAsync($"api/estados/" + id);

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<EstadoViewModel>(content);

            viewModel.Pais = await paisApi.GetPais(viewModel.PaisId);

            return viewModel;
        }
    }
}
