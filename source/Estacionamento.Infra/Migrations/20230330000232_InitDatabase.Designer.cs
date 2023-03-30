﻿// <auto-generated />
using System;
using Estacionamento.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Estacionamento.Infra.Migrations
{
    [DbContext(typeof(EstacionamentoContext))]
    [Migration("20230330000232_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Estacionamento.Domain.Entities.Vagas.Vaga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumeroVaga")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("NumeroVaga");

                    b.Property<int>("Ocupada")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoVaga")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("T_VAGA", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NumeroVaga = "01",
                            Ocupada = 0,
                            TipoVaga = "Media"
                        },
                        new
                        {
                            Id = 2,
                            NumeroVaga = "02",
                            Ocupada = 0,
                            TipoVaga = "Media"
                        },
                        new
                        {
                            Id = 3,
                            NumeroVaga = "03",
                            Ocupada = 0,
                            TipoVaga = "Media"
                        },
                        new
                        {
                            Id = 4,
                            NumeroVaga = "04",
                            Ocupada = 0,
                            TipoVaga = "Media"
                        },
                        new
                        {
                            Id = 5,
                            NumeroVaga = "05",
                            Ocupada = 0,
                            TipoVaga = "Pequena"
                        },
                        new
                        {
                            Id = 6,
                            NumeroVaga = "06",
                            Ocupada = 0,
                            TipoVaga = "Pequena"
                        },
                        new
                        {
                            Id = 7,
                            NumeroVaga = "07",
                            Ocupada = 0,
                            TipoVaga = "Pequena"
                        },
                        new
                        {
                            Id = 8,
                            NumeroVaga = "08",
                            Ocupada = 0,
                            TipoVaga = "Pequena"
                        },
                        new
                        {
                            Id = 9,
                            NumeroVaga = "09",
                            Ocupada = 0,
                            TipoVaga = "Grande"
                        },
                        new
                        {
                            Id = 10,
                            NumeroVaga = "10",
                            Ocupada = 0,
                            TipoVaga = "Grande"
                        },
                        new
                        {
                            Id = 11,
                            NumeroVaga = "11",
                            Ocupada = 0,
                            TipoVaga = "Grande"
                        },
                        new
                        {
                            Id = 12,
                            NumeroVaga = "12",
                            Ocupada = 0,
                            TipoVaga = "Grande"
                        });
                });

            modelBuilder.Entity("Estacionamento.Domain.Entities.Veiculos.RegistroVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ano")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime?>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TipoVeiculo")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("TipoVeiculo");

                    b.Property<int>("VagaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VagaId");

                    b.ToTable("T_REGISTRO_VEICULO", (string)null);
                });

            modelBuilder.Entity("Estacionamento.Domain.Entities.Veiculos.RegistroVeiculo", b =>
                {
                    b.HasOne("Estacionamento.Domain.Entities.Vagas.Vaga", "Vaga")
                        .WithMany()
                        .HasForeignKey("VagaId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Vaga");
                });
#pragma warning restore 612, 618
        }
    }
}