using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.RegistroVeiculoDTO
{
    public class RegistrosGeraisDTO
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Ano { get; set; }
        public string? Cor { get; set; }
        public string? Placa { get; set; }
        public string? TipoVeiculo { get; set; }
        public string? NumeroVaga { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }

    }
}
