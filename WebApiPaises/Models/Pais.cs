using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiPaises.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public String Nome { get; set; }

        public String Foto { get; set; }

        public virtual List<Estado> Estados {get; set;}

        
    }
}
