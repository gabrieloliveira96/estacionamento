using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs
{
    public class VeiculoRequestDTO
    {
        public TipoVeiculoEnum TipoVeiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }

        public VeiculoRequestDTO(TipoVeiculoEnum tipoVeiculo, string marca, string modelo, string ano, string cor, string placa)
        {
            TipoVeiculo = tipoVeiculo;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            Placa = placa;
        }
    }
}
