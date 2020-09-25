using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Pessoas
{
    public class ListarAmigosViewModel
    {
        public List<PessoaViewModel> Pessoas { get; set; }
        public PessoaViewModel Pessoa { get; set; }
        public List<PessoaViewModel> Amigos { get; set; }
    }
}
