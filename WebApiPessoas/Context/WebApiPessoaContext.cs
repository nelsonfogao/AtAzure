using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPessoas.Models;

namespace WebApiPessoas.Context
{
    public class WebApiPessoaContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

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

        public class WebApiPaisesContextFactory : IDesignTimeDbContextFactory<WebApiPessoaContext>
        {
            public WebApiPessoaContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<WebApiPessoaContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AtAzure;Trusted_Connection=True;MultipleActiveResultSets=true");

                return new WebApiPessoaContext(optionsBuilder.Options);

            }
        }
    }
}
