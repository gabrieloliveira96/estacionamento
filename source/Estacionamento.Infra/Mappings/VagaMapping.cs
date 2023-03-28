using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Enum;

namespace Estacionamento.Infra.Mappings
{
    public class VagaMapping : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(m => m.NumeroVaga)
                .HasColumnType("varchar(10)")
                .HasColumnName("NumeroVaga");


            builder.Property(c => c.TipoVaga)
              .HasColumnType("varchar(10)")
              .HasConversion(x => x.ToString(), // to converter
                             x => (TipoVagaEnum)Enum.Parse(typeof(TipoVagaEnum), x));

            builder.HasData(
                new Vaga(1, "01",   TipoVagaEnum.Media),
                new Vaga(2, "02",   TipoVagaEnum.Media),
                new Vaga(3, "03",   TipoVagaEnum.Media),
                new Vaga(4, "04",   TipoVagaEnum.Media),
                new Vaga(5, "05",   TipoVagaEnum.Pequena),
                new Vaga(6, "06",   TipoVagaEnum.Pequena),
                new Vaga(7, "07",   TipoVagaEnum.Pequena),
                new Vaga(8, "08",   TipoVagaEnum.Pequena),
                new Vaga(9, "09",   TipoVagaEnum.Grande),
                new Vaga(10, "10",  TipoVagaEnum.Grande),
                new Vaga(11, "11",  TipoVagaEnum.Grande),
                new Vaga(12, "12",  TipoVagaEnum.Grande)
            );

            builder.ToTable("T_VAGA");
        }
    }
}