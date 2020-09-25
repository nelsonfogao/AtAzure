using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Models;

namespace WebApiPaises.Mapping
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Foto);

            builder.Property(x => x.PaisId).IsRequired();

            builder.HasOne<Pais>(x => x.Pais);

        }
    }
}
