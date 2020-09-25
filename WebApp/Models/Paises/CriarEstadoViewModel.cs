using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Paises
{
    public class CriarEstadoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome é obrigatório")]

        public String Nome { get; set; }
        public IFormFile ImgFoto { get; set; }
        public String Foto { get; set; }

        public List<PaisViewModel> Paises { get; set; }

        [Required(ErrorMessage = "Campo Pais é obrigatório")]
        public int PaisId { get; set; }
    }
}
