using Dapper;
using Estacionamento.Domain.DTOs.RegistroVeiculoDTO;
using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.VagaDTO
{
    public class FiltroVagasResponseDTO
    {
        [Slapper.AutoMapper.Id]
        public string? NumeroVaga { get; set; }
        public TipoVagaEnum? TipoVaga { get; set; }
        public bool? Ocupada { get; set; }
        public List<FiltroRegistrosResponseDTO>? Veiculos { get; set; }



    }
}
