using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Paises;

namespace WebApp.ApiServices
{
    public class PaisApi : IPaisApi
    {
        private readonly HttpClient httpClient;

        public PaisApi()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri("https://localhost:44337/");
        }
        public async Task<CriarPaisViewModel> PostAsync(CriarPaisViewModel paisViewModel)
        {
            var paisViewModelJson = JsonConvert.SerializeObject(paisViewModel);

            var conteudo = new StringContent(paisViewModelJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/paises", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return paisViewModel;
            }

            return paisViewModel;
        }
        public async Task<PaisViewModel> GetPais(int id)
        {
            var response = await httpClient.GetAsync($"api/paises/" + id);

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<PaisViewModel>(content);

            return viewModel;
        }

        public async Task<List<PaisViewModel>> GetPaises()
        {
            var response = await httpClient.GetAsync($"api/paises");

            var content = await response.Content.ReadAsStringAsync();

            var viewModel = JsonConvert.DeserializeObject<List<PaisViewModel>>(content);

            return viewModel;
        }
    }
}
