using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Paises
{
    public class PaisViewModel
    {
        public int Id { get; set; }

        public String Foto { get; set; }

        public String Nome { get; set; }
        public List<EstadoViewModel> Estados { get; set; }
    }
}
