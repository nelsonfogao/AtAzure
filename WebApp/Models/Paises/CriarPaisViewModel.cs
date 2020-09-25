using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Paises
{
    public class CriarPaisViewModel
    {
        public IFormFile ImgFoto { get; set; }

        [Required(ErrorMessage = "Campo Foto é obrigatório")]

        public String Foto { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public String Nome { get; set; }

        public List<EstadoViewModel> Estados { get; set; }
    }
}
