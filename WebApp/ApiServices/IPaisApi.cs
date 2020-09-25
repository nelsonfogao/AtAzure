using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Paises;

namespace WebApp.ApiServices
{
    public interface IPaisApi
    {
        Task<List<PaisViewModel>> GetPaises();
        Task<PaisViewModel> GetPais(int id);

        Task<CriarPaisViewModel> PostAsync(CriarPaisViewModel paisViewModel);
    }
}
