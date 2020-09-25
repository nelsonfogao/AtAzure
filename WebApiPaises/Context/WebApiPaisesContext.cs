using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApiPaises.Mapping;
using WebApiPaises.Models;

namespace WebApiPaises.Context
{
    public class WebApiPaisesContext : DbContext
    {
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estados { get; set; }

        public WebApiPaisesContext (DbContextOptions<WebApiPaisesContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());

            base.OnModelCreating(modelBuilder);
        }
        public class WebApiPaisesContextFactory : IDesignTimeDbContextFactory<WebApiPaisesContext>
        {
            public WebApiPaisesContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<WebApiPaisesContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AtAzure;Trusted_Connection=True;MultipleActiveResultSets=true");

                return new WebApiPaisesContext(optionsBuilder.Options);

            }
        }
    }
}
