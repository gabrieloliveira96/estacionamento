using Estacionamento.Domain.Entities.Vagas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estacionamento.Domain.Entities.Veiculos;
using Estacionamento.Domain.Enum;

namespace Estacionamento.Infra.Mappings
{
    public class RegistroVeiculoMapping : IEntityTypeConfiguration<RegistroVeiculo>
    {
        public void Configure(EntityTypeBuilder<RegistroVeiculo> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(r => r.Marca)
                .HasColumnType("varchar(30)");

            builder.Property(r => r.Modelo)
                .HasColumnType("varchar(30)");

            builder.Property(r => r.Ano)
                .HasColumnType("varchar(4)");

            builder.Property(r => r.Cor)
                .HasColumnType("varchar(15)");

            builder.Property(r => r.Placa)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.TipoVeiculo)
              .HasColumnType("varchar(10)")
              .HasConversion(x => x.ToString(), // to converter
                             x => (TipoVeiculoEnum)Enum.Parse(typeof(TipoVeiculoEnum), x))
              .HasColumnName("TipoVeiculo");


            builder.Property(c => c.VagaId)
                .HasColumnType("int");

            builder.Property(c => c.DataEntrada)
               .HasColumnType("datetime2");


            builder.Property(c => c.DataSaida)
               .HasColumnType("datetime2");

            builder.ToTable("T_REGISTRO_VEICULO");
        }
    }
}