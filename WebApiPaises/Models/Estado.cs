using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiPaises.Models
{
    public class Estado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public String Nome { get; set; }

        public String Foto { get; set; }
        [Required(ErrorMessage = "Campo País é obrigatório")]
        public int PaisId { get; set; }

        [JsonIgnore]
        public virtual Pais Pais { get; set; }

    }
}
