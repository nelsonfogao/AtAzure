using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPessoa.Models;

namespace WebApiPessoa.Mapper
{
    public class PessoaMap : Profile
    {
        public PessoaMap()
        {
            CreateMap<Pessoa, PessoaResponse>();
        }
    }
}
