using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Pessoas
{
    public class CriarPessoaViewModel
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Email { get; set; }
        public String Telefone { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public String Foto { get; set; }

        public int EstadoId { get; set; }

        public int PaisId { get; set; }


    }
}
