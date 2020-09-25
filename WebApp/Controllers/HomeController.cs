using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.ApiServices;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaisApi PaisApi;
        private readonly IEstadoApi EstadoApi;
        private readonly IPessoaApi PessoaApi;

        public HomeController(ILogger<HomeController> logger, IEstadoApi estadoApi, IPaisApi paisApi, IPessoaApi pessoaApi)
        {
            _logger = logger;
            this.EstadoApi = estadoApi;
            this.PaisApi = paisApi;
            this.PessoaApi = pessoaApi;
        }

        public async Task<IActionResult> Index()
        {
            var paginaInicial = new PaginaInicialViewModel();

            var paises = await PaisApi.GetPaises();
            paginaInicial.QuantidadeDePaises = paises.Count;

            var estados = await EstadoApi.GetEstados();
            paginaInicial.QuantidadeDeEstados = estados.Count;

            var pessoas = await PessoaApi.GetPessoas();
            paginaInicial.QuantidadeDePessoas = pessoas.Count;

            return View(paginaInicial);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
