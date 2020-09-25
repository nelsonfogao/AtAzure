using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Pessoas;

namespace WebApp.ApiServices
{
    public interface IPessoaApi
    {
        Task PostPessoa(CriarPessoaViewModel form);
        Task PostAmigos(int pessoaId, int[] ids);
        Task<List<PessoaViewModel>> GetPessoas();
        Task<PessoaViewModel> GetPessoa(int id);
        Task<List<PessoaViewModel>> GetAmigos(int id);
    }
}
