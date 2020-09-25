using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Paises;

namespace WebApp.ApiServices
{
    public interface IEstadoApi
    {
        Task<CriarEstadoViewModel> PostEstadoAsync(CriarEstadoViewModel estadoViewModel);
        Task<List<EstadoViewModel>> GetEstados();
        Task<EstadoViewModel> GetEstado(int id);
    }
}
