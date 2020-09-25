using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Paises
{
    public class EstadoViewModel
    {
        public int Id { get; set; }

        public String Nome { get; set; }

        public String Foto { get; set; }

        public PaisViewModel Pais { get; set; }

        public int PaisId { get; set; }
    }
}
