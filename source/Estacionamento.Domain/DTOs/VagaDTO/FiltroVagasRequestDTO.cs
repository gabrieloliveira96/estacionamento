using Estacionamento.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.VagaDTO
{
    public class FiltroVagasRequestDTO
    {
        public string? NumeroVaga { get; set; }
        public TipoVagaEnum? TipoVaga { get; set; }
        public bool? Ocupada { get; set; }
        public string? PeriodoInicial { get; set; }
        public string? PeriodoFinal { get; set; }



    }
}
