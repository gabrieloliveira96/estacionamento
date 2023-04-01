using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.RegistroVeiculoDTO
{
    public class FiltroRegistrosRequestDTO
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Ano { get; set; }
        public string? Cor { get; set; }
        public string? Placa { get; set; }
        public TipoVeiculoEnum? TipoVeiculo { get; set; }
        public string? NumeroVaga { get; set; }
        public string? DataEntrada { get; set; }
        public string? DataSaida { get; set; }

    }
}
