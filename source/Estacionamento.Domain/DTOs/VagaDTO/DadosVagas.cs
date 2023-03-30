using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.DTOs.VagaDTO
{
    public class DadosVagas
    {
        public string? NumeroVaga { get; set; }
        public string? TipoVaga { get; set; }
        public int Ocupada { get; set; }
    }
}
