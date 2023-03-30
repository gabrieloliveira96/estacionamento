using Estacionamento.Domain.Custom;
using Estacionamento.Domain.Data;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.VeiculoDTO
{
    public class VeiculosVagaDTO
    {
        public string? Marca { get;  set; }
        public string? Modelo { get;  set; }
        public string? Ano { get;  set; }
        public string? Cor { get;  set; }
        public string? Placa { get;  set; }
        public string? TipoVeiculo { get;  set; }
        public string? NumeroVaga { get; set; }
        public DateTime? DataEntrada { get;  set; }
    }
}
