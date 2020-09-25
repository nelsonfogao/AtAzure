using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPessoas.Controllers;
using WebApiPessoas.Models;

namespace WebApiPessoas.Mapper
{
    public class PessoaMap : Profile
    {
        public PessoaMap()
        {
            CreateMap<Pessoa, PessoaResponse>();
        }
    }
}
