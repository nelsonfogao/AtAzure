using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiPessoa.Models;

namespace WebApiPessoa.Context
{
    public class WebApiPessoaContext : DbContext
    {
        public WebApiPessoaContext (DbContextOptions<WebApiPessoaContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pessoa>()
                .HasMany(x => x.Amigos);
        }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
}
