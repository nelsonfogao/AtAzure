using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Paises;

namespace WebApp.Models.Pessoas
{
    public class CriarPessoaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public String Nome { get; set; }
        [Required(ErrorMessage = "Campo Sobrenome é obrigatório")]
        public String Sobrenome { get; set; }
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Campo Telefone é obrigatório")]
        public String Telefone { get; set; }
        [Required(ErrorMessage = "Campo Data de nascimento é obrigatório")]
        public DateTime DataDeNascimento { get; set; }
        public IFormFile ImgFoto { get; set; }
        public String Foto { get; set; }
        public List<PaisViewModel> Paises { get; set; }
        public List<EstadoViewModel> Estados { get; set; }
        [Required(ErrorMessage = "Campo Estado é obrigatório")]
        public int EstadoId { get; set; }
        [Required(ErrorMessage = "Campo Pais é obrigatório")]
        public int PaisId { get; set; }


    }
}
